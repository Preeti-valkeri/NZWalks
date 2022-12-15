using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NZWalks.API.DTO;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;
        public WalksController(IWalkRepository obj,IMapper objmapper)
        {
            this.walkRepository = obj;
            this.mapper = objmapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
           var walks= await walkRepository.GetAllWalk();
            var walkDTO = mapper.Map<List<DTO.Walk>>(walks);
            return Ok(walkDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalk_IDAsync")]
        public async Task<IActionResult> GetWalk_IDAsync(Guid id)
        {
            var walks = await walkRepository.GetWalks(id);
            if (walks == null)
            {
                return NotFound();
            }
            var walkDTO = mapper.Map<DTO.Walk>(walks);
            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkdetailsAsync([FromBody] DTO.AddWalkRequest addWalkRequest)
        {
            var walkdomain = new Models.Domain.Walk
            {
                Length = addWalkRequest.Length,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            };
            walkdomain = await walkRepository.AddWalkAsync(walkdomain);
            var walkDTOdetails = new DTO.Walk
            {
                Id=walkdomain.Id,
                Length=walkdomain.Length,
                Name=walkdomain.Name,
                RegionId=walkdomain.RegionId,
                WalkDifficultyId=walkdomain.WalkDifficultyId
            };
            return CreatedAtAction(nameof(GetWalk_IDAsync), new { id = walkDTOdetails.Id }, walkDTOdetails);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            var walkdetails= await walkRepository.DeleteWalkAsync(id);
            if(walkdetails==null)
            {
                return NotFound();
            }
            var walkDTO = new DTO.Walk()
            {
                Id = walkdetails.Id,
                Length = walkdetails.Length,
                Name = walkdetails.Name,
                RegionId=walkdetails.RegionId,
                WalkDifficultyId=walkdetails.WalkDifficultyId
            };
            return Ok(walkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id,[FromBody] DTO.UpdateWalkRequest updateWalkRequest)
        {
            var walkdomain = new Models.Domain.Walk
            {
                Name = updateWalkRequest.Name,
                Length = updateWalkRequest.Length,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId=updateWalkRequest.WalkDifficultyId
            };

            var Existing_Walk = await walkRepository.UpdateWalkAsync(id, walkdomain);
            if(Existing_Walk ==null)
            {
                return NotFound();
            }

            var walkDTO = new DTO.Walk
            {
                Id=walkdomain.Id,
                Name = walkdomain.Name,
                Length = walkdomain.Length,
                RegionId = walkdomain.RegionId,
                WalkDifficultyId = walkdomain.WalkDifficultyId
            };
            return Ok(walkDTO);
        }
    }
}
