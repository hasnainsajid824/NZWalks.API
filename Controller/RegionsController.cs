using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domains;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class RegionsController : ControllerBase
{
    private readonly NZWalksDbContext DbContext;
    private readonly IRegionRepository RegionRepository;

    private readonly IMapper Mapper;

    public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository,
    IMapper mapper)
    {
        this.DbContext = dbContext;
        this.RegionRepository = regionRepository;
        this.Mapper = mapper;
    }
    [HttpGet]
    [Authorize(Roles = "Reader")]
    public async Task<IActionResult> GetAll()
    {
        var regions = await RegionRepository.GetAllAsync();
        return Ok(Mapper.Map<List<RegionDto>>(regions));
    }

    [HttpGet]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Reader")]

    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var region = await RegionRepository.GetByIdAsync(id);
        if (region == null)
        {
            return NotFound();
        }
        return Ok(Mapper.Map<RegionDto>(region));
    }


    [HttpPost]
    [ValidateModel]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Create([FromBody] AddRegionDto dto)
    {
        var regions = Mapper.Map<Region>(dto);

        regions = await RegionRepository.CreateAsync(regions);

        var regionDto = Mapper.Map<RegionDto>(regions);

        return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);

    }

    [HttpPut]
    [Route("{id:Guid}")]
    [ValidateModel]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] UpdateRegionDto updateDto)
    {
        var region = Mapper.Map<Region>(updateDto);
        region = await RegionRepository.UpdateAsync(Id, region);
        if (region == null)
        {
            return NotFound();
        }
        var regionDto = Mapper.Map<RegionDto>(region);
        return Ok(regionDto);

    }

    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Writer")]

    public async Task<IActionResult> Delete([FromRoute] Guid Id)
    {
        var region = await RegionRepository.DeleteAsync(Id);
        if (region == null)
        {
            return NotFound();
        }
        return Ok(Mapper.Map<RegionDto>(region));
    }

}