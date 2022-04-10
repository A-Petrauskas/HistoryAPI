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

            return allLevelsContract;
        }

        public async Task<LevelContract> GetLevelAsync(string id)
        {
            var levelEntity = await _levelsRepository.GetAsync(id);

            var levelContract = _mapper.Map<LevelContract>(levelEntity);

            return levelContract;
        }

        public async Task<LevelContract> GetLevelByNameAsync(string name)
        {
            var levelEntity = await _levelsRepository.GetByNameAsync(name);

            var levelContract = _mapper.Map<LevelContract>(levelEntity);

            return levelContract;
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

            await _levelsRepository.CreateLevelAsync(newLevelEntity);

            await _eventsService.CreateEventsAsync(newLevelEntity.events);

            var newLevelContract = _mapper.Map<LevelContract>(newLevelEntity);


            return newLevelContract;
        }

        public async Task<LevelContract> UpdateLevelAsync(LevelContract level)
        {
            var levelEntity = _mapper.Map<LevelEntity>(level);

            await _levelsRepository.UpdateLevelAsync(levelEntity);

            return level;
        }

        public async Task<DeleteResult> RemoveLevelAsync(string id)
        {
            var deleteResult = await _levelsRepository.RemoveLevelAsync(id);

            return deleteResult;
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

            for (int i = 0; i <imageName.Count; i++)
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
    }
}
