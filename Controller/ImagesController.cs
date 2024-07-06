using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domains;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequestDto request)
        {
            ValidateUploadFile(request);
            if (ModelState.IsValid)
            {
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription
                };

                await imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);
        }

        private void ValidateUploadFile (ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[]{".jpg",".png",".jpeg"};

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file","Unspported File Extension");
            }
            if(request.File.Length > 10485760)
            {
                ModelState.AddModelError("file","File size more than 10 Mb, please upload an Image with a smaller size.");                
            }
        }
    }
}