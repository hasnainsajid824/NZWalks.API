using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domains;
using NZWalks.API.Models.Domains.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class RegionsController : ControllerBase
{
    private readonly NZWalksDbContext DbContext;
    private readonly IRegionRepository RegionRepository;


    public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository)
    {
        this.DbContext = dbContext;
        RegionRepository = regionRepository;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var regions = await RegionRepository.GetAllAsync();

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
        var region = await RegionRepository.GetByIdAsync(id);
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

        regions = await RegionRepository.CreateAsync(regions);

        var regionDto = new RegionDto
        {
            Id = regions.Id,
            Code = regions.Code,
            Name = regions.Name,
            ImageRegionUrl = regions.ImageRegionUrl,
        };

        return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
    }

    [HttpPut]   
    [Route("{id:Guid}")]
     public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] UpdateRegionDto updateDto)
    {
        var region = new Region{
            Code = updateDto.Code,
            Name = updateDto.Name,
            ImageRegionUrl = updateDto.ImageRegionUrl,
        };
        region = await RegionRepository.UpdateAsync(Id, region);
        if (region == null)
        {
            return NotFound();
        }
        var regionDto = new RegionDto{
            Id = region.Id,
            Code = region.Code, 
            Name = region.Name,
            ImageRegionUrl = region.ImageRegionUrl,
        };

        return Ok(regionDto);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute]Guid Id)
    {
        var region =  await RegionRepository.DeleteAsync(Id);
        if (region == null)
        {
            return NotFound();
        }

        var regionDto = new RegionDto{
            Id = region.Id,
            Code = region.Code, 
            Name = region.Name,
            ImageRegionUrl = region.ImageRegionUrl,
        };
        return Ok(regionDto);
    }

}