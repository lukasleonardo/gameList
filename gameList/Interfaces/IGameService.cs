using gameList.Models;

namespace gameList.Interfaces
{
    public interface IGameService
    {
        Task<PagedList<Game>> GetPagedGames(int page, int pageSize);
        Task<PagedList<Game>> SearchGames(string searchTerm, int page, int pageSize);
    }
}
