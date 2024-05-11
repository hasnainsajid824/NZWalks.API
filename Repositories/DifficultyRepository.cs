using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domains;

namespace NZWalks.API.Repositories;

public class DifficultyRepository : IDifficultyRepository
{
    private readonly NZWalksDbContext dbContext;
    public DifficultyRepository(NZWalksDbContext nZWalksDbContext)
    {
        dbContext = nZWalksDbContext;
    }


    public async Task<Difficulty> CreateAsync(Difficulty difficulty)
    {
        await dbContext.Difficulties.AddAsync(difficulty);
        await dbContext.SaveChangesAsync();
        return difficulty;
    }

    public async Task<Difficulty?> DeleteAsync(Guid id)
    {
        var difficulty = await dbContext.Difficulties.FirstOrDefaultAsync(x => x.Id == id);
        if (difficulty == null)     
        {
            return null;
        }
        dbContext.Difficulties.Remove(difficulty);
        await dbContext.SaveChangesAsync(); 
        return difficulty;
    }

    public Task<List<Difficulty>> GetAllAsync()
    {
        return dbContext.Difficulties.ToListAsync();
    }

    public Task<Difficulty?> GetByIdAsync(Guid id)
    {
        return dbContext.Difficulties.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Difficulty?> UpdateAsync(Guid id, Difficulty difficulty)
    {
        var difficult = await dbContext.Difficulties.FirstOrDefaultAsync(x => x.Id == id);
        if (difficulty == null)     
        {
            return null;
        }
        difficult.Name = difficulty.Name;
        
        await dbContext.SaveChangesAsync(); 
        return difficult;
    }
}