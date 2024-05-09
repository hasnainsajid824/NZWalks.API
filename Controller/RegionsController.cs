using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domains;
using NZWalks.API.Models.Domains.DTO;

namespace NZWalks.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class RegionsController : ControllerBase
{
    private readonly NZWalksDbContext DbContext;
    public RegionsController(NZWalksDbContext dbContext)
    {
        this.DbContext = dbContext;

    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var regions = await DbContext.Regions.ToListAsync();

        var regionDto = new List<RegionDto>();
        foreach (var region in regions)
        {
            regionDto.Add(new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                ImageRegionUrl = region.ImageRegionUrl,
            });
        }
        return Ok(regionDto);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async  Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var region = await DbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        if (region == null)
        {
            return NotFound();
        }
        var regionDto = new RegionDto
        {
            Id = region.Id,
            Code = region.Code,
            Name = region.Name,
            ImageRegionUrl = region.ImageRegionUrl,
        };
        return Ok(regionDto);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddRegionDto dto)
    {
        var regions = new Region
        {
            Code = dto.Code,
            Name = dto.Name,
            ImageRegionUrl = dto.ImageRegionUrl,
        };

        await DbContext.Regions.AddAsync(regions);
        await DbContext.SaveChangesAsync();

        var regionDto = new RegionDto
        {
            Id = regions.Id,
            Code = regions.Code,
            Name = regions.Name,
            ImageRegionUrl = regions.ImageRegionUrl,
        };

        return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
    }

    [HttpPut("{id:Guid}")]   
     public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] UpdateRegionDto updateDto)
    {
        var region = await DbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
        if (region == null)
        {
            return NotFound();
        }
        region.Code = updateDto.Code;
        region.Name = updateDto.Name;
        region.ImageRegionUrl = updateDto.ImageRegionUrl;
        await DbContext.SaveChangesAsync();
        var regionDto = new RegionDto{
            Id = region.Id,
            Code = region.Code, 
            Name = region.Name,
            ImageRegionUrl = region.ImageRegionUrl,
        };

        return Ok(region);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute]Guid Id)
    {
        var region =  await DbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
        if (region == null)
        {
            return NotFound();
        }

        DbContext.Regions.Remove(region);
        await DbContext.SaveChangesAsync();

        var regionDto = new RegionDto{
            Id = region.Id,
            Code = region.Code, 
            Name = region.Name,
            ImageRegionUrl = region.ImageRegionUrl,
        };
        return Ok(regionDto);
    }

}