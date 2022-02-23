﻿using Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ILevelsService
    {
        Task<List<Level>> GetLevelsAsync();

        Task<Level> GetLevelAsync(string id);
    }
}