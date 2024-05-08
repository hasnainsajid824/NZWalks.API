using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    public IActionResult GetAll()
    {
        var regions = DbContext.Regions.ToList();

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
    public IActionResult GetById([FromRoute] Guid id)
    {
        var region = DbContext.Regions.FirstOrDefault(x => x.Id == id);
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
    public IActionResult Create([FromBody] AddRegionDto dto)
    {
        var regions = new Region
        {
            Code = dto.Code,
            Name = dto.Name,
            ImageRegionUrl = dto.ImageRegionUrl,
        };

        DbContext.Regions.Add(regions);
        DbContext.SaveChanges();

        var regionDto = new RegionDto
        {
            Id = regions.Id,
            Code = regions.Code,
            Name = regions.Name,
            ImageRegionUrl = regions.ImageRegionUrl,
        };

        return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public IActionResult Update([FromRoute] Guid Id, [FromBody] UpdateRegionDto updateDto)
    {
        var region = DbContext.Regions.FirstOrDefault(x => x.Id == Id);
        if (region == null)
        {
            return NotFound();
        }
        region.Code = updateDto.Code;
        region.Name = updateDto.Name;
        region.ImageRegionUrl = updateDto.ImageRegionUrl;
        DbContext.SaveChanges();
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
    public IActionResult Delete([FromRoute]Guid Id)
    {
        var region = DbContext.Regions.FirstOrDefault(x => x.Id == Id);
        if (region == null)
        {
            return NotFound();
        }

        DbContext.Regions.Remove(region);
        DbContext.SaveChanges();

        var regionDto = new RegionDto{
            Id = region.Id,
            Code = region.Code, 
            Name = region.Name,
            ImageRegionUrl = region.ImageRegionUrl,
        };
        return Ok(regionDto);
    }

}