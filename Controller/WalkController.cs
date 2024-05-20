
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domains;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WalkController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository repository;

        public WalkController(IMapper mapper, IWalkRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Post([FromBody] AddWalkDto dto)
        {
            var walk = mapper.Map<Walk>(dto);
            walk = await repository.CreateAsync(walk);
            var walkDto = mapper.Map<WalkDto>(walk);
            return Ok(walkDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery
        ,[FromQuery] string? sortBy, [FromQuery] bool isAscending, [FromQuery] int pageNum= 1,[FromQuery] int pageSize = 1000)
        {
            var walk = await repository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending, pageNum, pageSize);
            return Ok(mapper.Map<List<WalkDto>>(walk));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walk = await repository.GetByIdAsync(id);
            return Ok(mapper.Map<WalkDto>(walk));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkDto dto)
        {
            var walk = mapper.Map<Walk>(dto);
            walk = await repository.UpdateAsync(id, walk);
            if (walk == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<UpdateWalkDto>(walk));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var walk = await repository.DeleteAsync(id);
            if (walk == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walk));
        }

    }
}