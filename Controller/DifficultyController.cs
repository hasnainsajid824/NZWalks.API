using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.Models.Domains;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DifficultyController : ControllerBase
    {
        private readonly NZWalksDbContext DbContext;
        private readonly IDifficultyRepository difficultyRepository;
        private readonly IMapper mapper;

        public DifficultyController(NZWalksDbContext dbContext, IDifficultyRepository difficultyRepository,
                                    IMapper mapper)
        {
            this.DbContext = dbContext;
            this.difficultyRepository = difficultyRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var difficulty = await difficultyRepository.GetAllAsync();

            return Ok(mapper.Map<List<DifficultyDto>>(difficulty));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var difficulty = await difficultyRepository.GetByIdAsync(id);
            if (difficulty == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<DifficultyDto>(difficulty));
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddDifficultyDto dto)
        {
            var difficulty = mapper.Map<Difficulty>(dto);

            difficulty = await difficultyRepository.CreateAsync(difficulty);

            var difficultyDto = mapper.Map<DifficultyDto>(difficulty);

            return CreatedAtAction(nameof(GetById), new { id = difficultyDto.Id }, difficultyDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] UpdateDifficultyDto updateDto)
        {

            var difficulty =  mapper.Map<Difficulty>(updateDto);
            difficulty = await difficultyRepository.UpdateAsync(Id, difficulty);
            if (difficulty == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<DifficultyDto>(difficulty));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            var difficulty = await difficultyRepository.DeleteAsync(Id);
            if (difficulty == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<DifficultyDto>(difficulty));
        }

    }
}