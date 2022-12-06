using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public  async Task<IActionResult> GetAll()
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
}
}
