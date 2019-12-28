using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Frontend.Blazor.Data;

namespace Actio.Frontend.Blazor.Services
{
    public interface IActivityService
    {
        Task<List<Activity>> GetAsync();
        Task<Activity> GetAsync(Guid id);
    }
}