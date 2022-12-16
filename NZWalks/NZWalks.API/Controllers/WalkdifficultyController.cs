using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WalkdifficultyController : Controller
    {
        private readonly IWalkdifficultyRepository walkdifficulty;
        private readonly IMapper mapper;
        public WalkdifficultyController(IWalkdifficultyRepository obj, IMapper mobj)
        {
            this.walkdifficulty = obj;
            this.mapper = mobj;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll_WalkdifficultyAsyc()
        {
            var walkdifficulty_details = await walkdifficulty.GetWalkDifficultiesAsync();
            if (walkdifficulty_details == null)
            {
                return NotFound();
            }
            var walkdifficultyDTO = mapper.Map<List<DTO.WalkDifficulty>>(walkdifficulty_details);

            return Ok(walkdifficultyDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkdifficultyby_IDAsync")]
        public async Task<IActionResult> GetWalkdifficultyby_IDAsync(Guid id)
        {
           var walkdifficulty_details=await walkdifficulty.GetWalkDifficultyBy_IDAsyc(id);
            if(walkdifficulty_details==null)
            {
                return NotFound();
            }
            var walkdifficultyDTO = mapper.Map<DTO.WalkDifficulty>(walkdifficulty_details);
            return Ok(walkdifficultyDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddWalkdifficulty_Async(DTO.AddwalkDifficulty addwalkDifficulty)
        {
            var walkdifficulty_details = new Models.Domain.WalkDifficulty()
            {
                Code = addwalkDifficulty.Code
            };
            walkdifficulty_details = await walkdifficulty.AddWalkdifficultyAsync(walkdifficulty_details);
            //var walkdifficulty_detailsDTO = new DTO.WalkDifficulty()
            //{
            //    Id=walkdifficulty_details.Id,
            //    Code=walkdifficulty_details.Code
            //};
            var walkdifficulty_detailsDTO = mapper.Map<DTO.WalkDifficulty>(walkdifficulty_details);
            return CreatedAtAction(nameof(GetWalkdifficultyby_IDAsync), new { id = walkdifficulty_detailsDTO.Id }, walkdifficulty_detailsDTO);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficulty_Async([FromRoute] Guid id, [FromBody] DTO.UpdateWalkdifficulty updatewalkdifficulty)
        {
            var existing_walkdifficulty = new Models.Domain.WalkDifficulty()
            {
                Code = updatewalkdifficulty.Code
            };
            existing_walkdifficulty = await walkdifficulty.UpdateWalkDifficultyAsync(id, existing_walkdifficulty);
            if (existing_walkdifficulty == null)
            {
                return NotFound();
            }
            //var walkdifficulty_detailsDTO = new DTO.WalkDifficulty()
            //{
            //    Id=existing_walkdifficulty.Id,
            //    Code = existing_walkdifficulty.Code
            //};
            var walkdifficulty_detailsDTO = mapper.Map<DTO.WalkDifficulty>(existing_walkdifficulty);
            return Ok(walkdifficulty_detailsDTO);
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkdifficulty_Async(Guid id)
        {
            var existing_walkdifficulty = await walkdifficulty.DeleteWalkDifficultyAsyc(id);
            if (existing_walkdifficulty == null)
            {
                return NotFound();
            }
            //var walkdifficulty_detailsDTO = new DTO.WalkDifficulty()
            //{
            //    Id = existing_walkdifficulty.Id,
            //    Code = existing_walkdifficulty.Code
            //};
            var walkdifficulty_detailsDTO = mapper.Map<DTO.WalkDifficulty>(existing_walkdifficulty);
            return Ok(walkdifficulty_detailsDTO);
        }

    }
}
