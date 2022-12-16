using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public class WalkdifficultyRepository : IWalkdifficultyRepository
    {
        private readonly NZWalksDBContext nZWalksDBContext;
        public WalkdifficultyRepository(NZWalksDBContext obj)
        {
         this.nZWalksDBContext = obj;
        }

        public async Task<WalkDifficulty> AddWalkdifficultyAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id=Guid.NewGuid();
           await nZWalksDBContext.AddAsync(walkDifficulty);
           await nZWalksDBContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteWalkDifficultyAsyc(Guid id)
        {
            var walkdifficulty = await nZWalksDBContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            if (walkdifficulty == null)
            {
                return null;
            }
            nZWalksDBContext.WalkDifficulty.Remove(walkdifficulty);
            await nZWalksDBContext.SaveChangesAsync();
            return walkdifficulty;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetWalkDifficultiesAsync()
        {
            return await nZWalksDBContext.WalkDifficulty.ToListAsync();
           
        }

        public async Task<WalkDifficulty> GetWalkDifficultyBy_IDAsyc(Guid id)
        {
          return await  nZWalksDBContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WalkDifficulty> UpdateWalkDifficultyAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var Existing_walkdefficultydetails = await nZWalksDBContext.WalkDifficulty.FindAsync(id);
            if(Existing_walkdefficultydetails == null)
            {
                return null;
            }
            Existing_walkdefficultydetails.Code=walkDifficulty.Code;
            await nZWalksDBContext.SaveChangesAsync();
            return Existing_walkdefficultydetails;
        }
    }
}
