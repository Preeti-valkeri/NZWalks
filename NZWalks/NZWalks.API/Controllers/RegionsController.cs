using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repository;
using System.Runtime.InteropServices;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        public RegionsController(IRegionRepository obj,IMapper objm)
        {
            this.regionRepository = obj;
            this.mapper=objm;
        }

        [HttpGet]
        //Asynchrounous we can achieve by using task/await/async keyword
        public  async Task<IActionResult> GetAllRegions()
        {
            //static data
            //            var regions = new List<Region>()
            //            { 
            //            new Region
            //            {
            //                Id = Guid.NewGuid(),
            //                Code = "jjjk",
            //                Name = "sdgf",
            //                Area = 1.00,
            //                Lat = 121.0,
            //                Long = 5265.98,
            //                Population = 65
            //            },
            //            new Region
            //            {
            //Id = Guid.NewGuid(),
            //                Code = "dfd",
            //                Name = "ffg",
            //                Area = 1.00,
            //                Lat = 121.0,
            //                Long = 5265.98,
            //                Population = 65
            //            }
            //            };

            //Dynamic data call which reads from DB
            var regions = await regionRepository.GetAll();
            //return Ok(regions); 
           
            //DTO Returns
            //var RegionDTOs = new List<DTO.Region>();
            //regions.ToList().ForEach(Region =>
            //{
            //    var regionDTO = new DTO.Region()
            //    {
            //    Id = Region.Id,
            //    Code = Region.Code,
            //    Name = Region.Name,
            //    };
            //    RegionDTOs.Add(regionDTO);
            //});

            //Automapper mapping of dto object to db object
            var RegionDTOs= mapper.Map<List<DTO.Region>>(regions);

           return Ok(RegionDTOs); 
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var regions = await regionRepository.GetRegion(id);
            if(regions == null)
            {
                return NotFound();
            }
            var RegionDTOs = mapper.Map<DTO.Region>(regions);
            return Ok(RegionDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(DTO.AddRegionRequest addRegionRequest)
        {
            //Request(DTO) to domain model
            var region = new Models.Domain.Region()
            {
                Code= addRegionRequest.Code,
                Name=addRegionRequest.Name,
                Area=addRegionRequest.Area,
                Lat=addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population
            };
            //Pass details to request
            region=await regionRepository.AddRegionAsync(region);

            //convert back to DTO 
            var regionDTO = new DTO.Region()
            {
                Id=region.Id,
                Area=region.Area,
                Code=region.Code,
                Lat=region.Lat,
                Long=region.Long,
                Name=region.Name,
                Population = region.Population
            };
            return CreatedAtAction(nameof(GetRegionAsync), new {id=regionDTO.Id },regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //get region from database
            var region = await regionRepository.DeleteRegionAsync(id);
           //if null notfound
            if(region==null)
            {
                return NotFound();
            }
            //convert response back to DTO
            var regionDTO = new DTO.Region()
            {
                Id = region.Id,
                Area = region.Area,
                Code = region.Code,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionsAsync([FromRoute] Guid id,[FromBody] DTO.UpdateRegionRequest UpdateRegion)
        {
            //convert DTO to domain model
            var region = new Models.Domain.Region()
            {
                Code=UpdateRegion.Code,
                Area = UpdateRegion.Area,
                Lat = UpdateRegion.Lat,
                Long=UpdateRegion.Long,
                Name=UpdateRegion.Name,
                Population=UpdateRegion.Population
            };
            //update Region using repository
            region = await regionRepository.UpdateRegionAsync(id, region);
            //if null then return not found
            if(region == null)
            {
                return NotFound();
            }
            //convert domain back to DTO
            var regionDTO = new DTO.Region 
            { 
                Id = region.Id, 
                Code = region.Code,
                Area=region.Area,
                Lat=region.Lat,
                Long=region.Long,
                Name=region.Name,
                Population=region.Population
            };
            return Ok(regionDTO);
        }

}
}
