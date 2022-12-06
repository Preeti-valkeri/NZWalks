using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext nZWalksDBContext;
        public RegionRepository(NZWalksDBContext obj)
        {
            this.nZWalksDBContext = obj;
        }
        public async Task<IEnumerable<Region>> GetAll()
        {
            return await nZWalksDBContext.Regions.ToListAsync();
        }
    }
}
