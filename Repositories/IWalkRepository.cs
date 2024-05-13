using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NZWalks.API.Models.Domains;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllAsync();
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk> CreateAsync(Walk walk);
        Task<Walk?> UpdateAsync(Guid id,Walk walkUpdate);
        Task<Walk?> DeleteAsync(Guid id);
    }
}