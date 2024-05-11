using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NZWalks.API.Models.Domains;


namespace NZWalks.API.Repositories
{
    public interface IDifficultyRepository
    {
        Task<List<Difficulty>> GetAllAsync();
        Task<Difficulty?> GetByIdAsync(Guid id);
        Task<Difficulty> CreateAsync(Difficulty difficulty);
        Task<Difficulty?> UpdateAsync(Guid id,Difficulty difficulty);
        Task<Difficulty?> DeleteAsync(Guid id);
    }
}