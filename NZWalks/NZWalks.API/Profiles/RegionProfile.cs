using AutoMapper;


namespace NZWalks.API.Profiles
{
    public class RegionProfile: Profile
    {
        public RegionProfile()
        {
            CreateMap<Models.Domain.Region,DTO.Region>().ReverseMap();
        }  
    }
}
