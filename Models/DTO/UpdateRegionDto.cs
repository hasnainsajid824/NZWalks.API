using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Code Length Cannot be Less than 3")]
        [MaxLength(5, ErrorMessage = "Code Length Cannot be Greater than 5")]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string? ImageRegionUrl { get; set; }
    }
}