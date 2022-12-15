using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDBContext nZWalksDBContext;
        public WalkRepository(NZWalksDBContext obj)
        {
            this.nZWalksDBContext = obj;

        }

        public async Task<Walk> AddWalkAsync(Walk walk)
        {
            walk.Id=Guid.NewGuid();
            await nZWalksDBContext.AddAsync(walk);
            await nZWalksDBContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteWalkAsync(Guid id)
        {
            var walkdetails = await nZWalksDBContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if(walkdetails==null)
            {
                return null;
            }
            nZWalksDBContext.Walks.Remove(walkdetails);
            await nZWalksDBContext.SaveChangesAsync();
            return walkdetails;
        }

        public async Task<IEnumerable<Walk>> GetAllWalk()
        {
            return await nZWalksDBContext.Walks
                //to fetch region and walkdifficulty row
                .Include(x=>x.Region)
                .Include(x=>x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetWalks(Guid id)
        {
            return await nZWalksDBContext.Walks
                .Include(x=>x.Region)
                .Include(x=>x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> UpdateWalkAsync(Guid id, Walk walk)
        {
            var Existing_walk = await nZWalksDBContext.Walks.FindAsync(id);
            if(Existing_walk != null)
            {
                Existing_walk.Name=walk.Name;
                Existing_walk.Length=walk.Length;
                Existing_walk.RegionId=walk.RegionId;
                Existing_walk.WalkDifficultyId=walk.WalkDifficultyId;
                await nZWalksDBContext.SaveChangesAsync();
                return Existing_walk;
            }
            return null;
            
        }
    }
}
