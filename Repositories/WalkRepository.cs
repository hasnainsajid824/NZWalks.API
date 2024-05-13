using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domains;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public WalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var walk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);  
            if (walk == null){
                return null;
            }
            dbContext.Walks.Remove(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await dbContext.Walks.Include(x=> x.difficulty).Include(x => x.region).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walkUpdate)
        {
            var walk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);  
            if (walk == null){
                return null;
            }
            walk.Name = walkUpdate.Name;
            walk.LengthInKm = walkUpdate.LengthInKm;
            walk.Description = walkUpdate.Description;
            walk.WalkImageUrl = walkUpdate.WalkImageUrl;
            walk.RegionId = walkUpdate.RegionId;
            walk.DifficultyId = walkUpdate.DifficultyId;
            
            await dbContext.SaveChangesAsync();
            return walk;
        }
    }
}