﻿using Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IEventsRepository
    {
        Task<List<Event>> GetAsync();
    }
}
