using AutoMapper;
using MongoDB.Driver;
using Repositories;
using Repositories.Entities;
using Services.Contracts;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class LevelsService : ILevelsService
    {
        private readonly ILevelsRepository _levelsRepository;
        private readonly IMapper _mapper;
        private readonly IEventsService _eventsService;

        public LevelsService(ILevelsRepository levelsRepository, IMapper mapper, IEventsService eventsService)
        {
            _levelsRepository = levelsRepository;
            _eventsService = eventsService;
            _mapper = mapper;
        }

        public async Task<List<LevelContract>> GetLevelsAsync()
        {
            var allLevelsEntity = await _levelsRepository.GetAllAsync();

            var allLevelsContract = _mapper.Map<List<LevelContract>>(allLevelsEntity);

            var allLevelsWithBC = ChangeMinusDatesLevelsToBC(allLevelsContract);

            return allLevelsWithBC;
        }

        public async Task<LevelContract> GetLevelAsync(string id)
        {
            var levelEntity = await _levelsRepository.GetAsync(id);

            if (levelEntity == null)
            {
                return null;
            }

            var levelContract = _mapper.Map<LevelContract>(levelEntity);

            var newLevelWithBC = ChangeMinusDatesToBC(levelContract);

            return newLevelWithBC;
        }

        public async Task<LevelContract> GetLevelNoBCAsync(string id)
        {
            var levelEntity = await _levelsRepository.GetAsync(id);

            if (levelEntity == null)
            {
                return null;
            }

            var levelContract = _mapper.Map<LevelContract>(levelEntity);

            return levelContract;
        }

        public async Task<LevelContract> GetLevelByNameAsync(string name)
        {
            var levelEntity = await _levelsRepository.GetByNameAsync(name);

            if (levelEntity == null)
            {
                return null;
            }

            var levelContract = _mapper.Map<LevelContract>(levelEntity);

            var newLevelWithBC = ChangeMinusDatesToBC(levelContract);

            return newLevelWithBC;
        }

        public async Task<LevelContract> CreateLevelAsync(CreationContract newLevel, string path)
        {
            var listOfImagesSrcs = await SaveImages(newLevel, path);

            var creationContract = AddImageSrcs(newLevel, listOfImagesSrcs);

            var newLevelEntity = _mapper.Map<LevelEntity>(creationContract);

            newLevelEntity.eventCount = newLevelEntity.events.Count;

            if (await DuplicateExists(newLevelEntity))
            {
                return null;
            }

            var levelWithoutBC = ChangeBCToIntString(newLevelEntity);

            await _levelsRepository.CreateLevelAsync(levelWithoutBC);

            await _eventsService.CreateEventsAsync(levelWithoutBC.events);

            var newLevelContract = _mapper.Map<LevelContract>(levelWithoutBC);

            var newLevelWithBC = ChangeMinusDatesToBC(newLevelContract);

            return newLevelWithBC;
        }

        public async Task<LevelContract> UpdateLevelAsync(LevelContract level)
        {
            var levelEntity = _mapper.Map<LevelEntity>(level);

            var updateResult = await _levelsRepository.UpdateLevelAsync(levelEntity);

            if (updateResult == null)
            {
                return null;
            }

            var newLevelWithBC = ChangeMinusDatesToBC(level);

            return newLevelWithBC;
        }

        public async Task<DeleteResult> RemoveLevelAsync(string id)
        {
            var deleteResult = await _levelsRepository.RemoveLevelAsync(id);

            return deleteResult;
        }


        private LevelContract ChangeMinusDatesToBC(LevelContract level)
        {
            if (level.fullDates)
            {
                return level;
            }

            foreach (EventContract levelEvent in level.events)
            {
                if (levelEvent.date.Contains('-'))
                {
                    levelEvent.date = levelEvent.date.Remove(0, 1) + " BC";
                }
            }

            return level;
        }
        private List<LevelContract> ChangeMinusDatesLevelsToBC(List<LevelContract> levels)
        {
            var levelsWithBC = new List<LevelContract>(levels);

            foreach (LevelContract level in levelsWithBC)
            {
                if (!level.fullDates)
                {
                    foreach (EventContract levelEvent in level.events)
                    {
                        if (levelEvent.date.Contains('-'))
                        {
                            levelEvent.date = levelEvent.date.Remove(0, 1) + " BC";
                        }
                    }
                }
            }

            return levelsWithBC;
        }


        private async Task<List<string>> SaveImages(CreationContract levelCreationContract, string path)
        {
            var levelPicture = levelCreationContract.image;
            string imagesPath = Directory.GetParent(path).Parent.FullName + @"\history-react-app\public\Images";
            List<string> imageNames = new List<string>();

            if (levelPicture != null)
            {
                if (levelPicture.Length > 0)
                {
                    var extension = levelPicture.FileName.Substring(levelPicture.FileName.LastIndexOf('.'));
                    var pictureName = Guid.NewGuid().ToString() + extension;
                    string filePath = Path.Combine(imagesPath, pictureName);
                    imageNames.Add(pictureName);

                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await levelPicture.CopyToAsync(fileStream);
                    }
                }
            }

            foreach (CreationEventContract eventContract in levelCreationContract.events)
            {
                if (eventContract.image != null)
                {
                    if (eventContract.image.Length > 0)
                    {
                        var extension = eventContract.image.FileName.Substring(eventContract.image.FileName.LastIndexOf('.'));
                        var pictureName = Guid.NewGuid().ToString() + extension;
                        string filePath = Path.Combine(imagesPath, pictureName);
                        imageNames.Add(pictureName);

                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await eventContract.image.CopyToAsync(fileStream);
                        }
                    }
                    else
                    {
                        imageNames.Add(null);
                    }
                }
                else
                {
                    imageNames.Add(null);
                }
            }


            return imageNames;
        }

        private CreationContract AddImageSrcs(CreationContract levelCreationContract, List<string> imageName)
        {
            levelCreationContract.imageSrc = "/images/" + imageName[0];
            imageName.RemoveAt(0);

            for (int i = 0; i < imageName.Count; i++)
            {
                if (imageName[i] != null)
                {
                    levelCreationContract.events[i].imageSrc = "/images/" + imageName[i];
                }
            }

            return levelCreationContract;
        }

        private async Task<bool> DuplicateExists(LevelEntity levelToCreate)
        {
            var allLevels = await _levelsRepository.GetAllAsync();

            var containsItem = allLevels.Any(level => level.description == levelToCreate.description
                && level.name == levelToCreate.name && level.timeConstraint == levelToCreate.timeConstraint
                && level.mistakes == levelToCreate.mistakes && level.eventCount == levelToCreate.eventCount);

            return containsItem;
        }

        private LevelEntity ChangeBCToIntString (LevelEntity level)
        {
            if (level.fullDates)
            {
                return level;
            }

            foreach(EventEntity levelEvent in level.events)
            {
                if (levelEvent.date.Contains("BC"))
                {
                    levelEvent.date = "-" + levelEvent.date.Substring(0, levelEvent.date.Length - 3);
                }
            }

            return level;
        }
    }
}
