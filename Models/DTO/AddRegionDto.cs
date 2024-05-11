using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZWalks.API.Models.DTO
{
    public class AddRegionDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? ImageRegionUrl { get; set; }
        
    }
}