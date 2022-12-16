using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public interface IWalkdifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetWalkDifficultiesAsync();
        Task<WalkDifficulty> GetWalkDifficultyBy_IDAsyc(Guid id);

        Task<WalkDifficulty> AddWalkdifficultyAsync(WalkDifficulty walkDifficulty);
        Task<WalkDifficulty> UpdateWalkDifficultyAsync(Guid id,WalkDifficulty walkDifficulty);
        Task<WalkDifficulty> DeleteWalkDifficultyAsyc(Guid id);   
    }
}
