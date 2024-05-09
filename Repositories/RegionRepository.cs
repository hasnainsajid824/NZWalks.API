using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domains;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public RegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var ExistingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (ExistingRegion == null)
            {
                return null;
            }
            dbContext.Regions.Remove(ExistingRegion);
            await dbContext.SaveChangesAsync();

            return ExistingRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var ExistingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (ExistingRegion == null)
            {
                return null;
            }
            ExistingRegion.Code = region.Code;
            ExistingRegion.Name = region.Name;
            ExistingRegion.ImageRegionUrl = region.ImageRegionUrl;

            await dbContext.SaveChangesAsync();

            return ExistingRegion;
        }
    }
}