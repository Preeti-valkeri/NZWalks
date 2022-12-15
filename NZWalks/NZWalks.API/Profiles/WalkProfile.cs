using AutoMapper;

namespace NZWalks.API.Profiles
{
    public class WalkProfile: Profile
    {
        public WalkProfile()
        {
            CreateMap<Models.Domain.Walk, DTO.Walk>().ReverseMap();
            CreateMap<Models.Domain.WalkDifficulty, DTO.WalkDifficulty>().ReverseMap();
        }
        
        
    }
}
