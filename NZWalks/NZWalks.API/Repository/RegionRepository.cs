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

        public async Task<Region> AddRegionAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await nZWalksDBContext.Regions.AddAsync(region);
            await nZWalksDBContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteRegionAsync(Guid id)
        {
            var region= await nZWalksDBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
            {
                return null;
            }
             nZWalksDBContext.Regions.Remove(region);
            await nZWalksDBContext.SaveChangesAsync();
            return region;
            
        }

        public async Task<IEnumerable<Region>> GetAll()
        {
            return await nZWalksDBContext.Regions.ToListAsync();
        }

        public  async Task<Region> GetRegion(Guid id)
        {
            return await  nZWalksDBContext.Regions.FirstOrDefaultAsync(x=>x.Id == id);
            
        }

        public async Task<Region> UpdateRegionAsync(Guid id, Region region)
        {
            var existingRegion = await nZWalksDBContext.Regions.FirstOrDefaultAsync(x=>x.Id==id);
            if(existingRegion==null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area=region.Area;
            existingRegion.Lat=region.Lat;
            existingRegion.Long=region.Long;
            existingRegion.Population=region.Population;
           await nZWalksDBContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
