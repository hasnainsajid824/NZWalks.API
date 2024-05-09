using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domains;
using NZWalks.API.Models.Domains.DTO;

namespace NZWalks.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DifficultyController : ControllerBase
    {
        private readonly NZWalksDbContext DbContext;
    public DifficultyController(NZWalksDbContext dbContext)
    {
        this.DbContext = dbContext;

    }
    [HttpGet]
    public IActionResult GetAll()
    {
        var difficulty = DbContext.Difficulties.ToList();

        var difficultyDto = new List<DifficultyDto>();
        foreach (var difficult in difficulty)
        {
            difficultyDto.Add(new DifficultyDto
            {
                Id = difficult.Id,
                Name = difficult.Name,
            });
        }
        return Ok(difficultyDto);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        var difficulties = DbContext.Difficulties.FirstOrDefault(x => x.Id == id);
        if (difficulties == null)
        {
            return NotFound();
        }
        var difficultyDto = new DifficultyDto
        {
            Id = difficulties.Id,
            Name = difficulties.Name,
        };
        return Ok(difficultyDto);
    }


    [HttpPost]
    public IActionResult Create([FromBody] AddDifficultyDto dto)
    {
        var difficulty = new Difficulty
        {
            Name = dto.Name,
        };

        DbContext.Difficulties.Add(difficulty);
        DbContext.SaveChanges();

        var difficultyDto = new DifficultyDto
        {
            Id = difficulty.Id,
            Name = difficulty.Name,
        };

        return CreatedAtAction(nameof(GetById), new { id = difficultyDto.Id }, difficultyDto);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    public IActionResult Update([FromRoute] Guid Id, [FromBody] UpdateDifficultyDto updateDto)
    {
        var difficulty = DbContext.Difficulties.FirstOrDefault(x => x.Id == Id);
        if (difficulty == null)
        {
            return NotFound();
        }
        difficulty.Name = updateDto.Name;
        DbContext.SaveChanges();
        var difficultyDto = new DifficultyDto
        {
            Id = difficulty.Id,
            Name = difficulty.Name,
        };

        return Ok(difficultyDto);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public IActionResult Delete([FromRoute]Guid Id)
    {
        var difficulty = DbContext.Difficulties.FirstOrDefault(x => x.Id == Id);
        if (difficulty == null)
        {
            return NotFound();
        }

        DbContext.Difficulties.Remove(difficulty);
        DbContext.SaveChanges();

        var difficultyDto = new DifficultyDto
        {
            Id = difficulty.Id,
            Name = difficulty.Name,
        };
        return Ok(difficultyDto);
    }

    }
}