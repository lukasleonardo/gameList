using gameList.Data;
using gameList.Interfaces;
using gameList.Models;
using Microsoft.EntityFrameworkCore;

namespace gameList.Services
{
    public class GameService : IGameService
    {
        private readonly GameDbContext _gameDbContext;

        public GameService(GameDbContext gameDbContext)
        {
            _gameDbContext = gameDbContext;
        }

        public async Task<PagedList<Game>> GetPagedGames(int page, int pageSize)
        {
            var totalItems = await _gameDbContext.Games.CountAsync();
            var games = await _gameDbContext.Games.OrderBy(g => g.name)
                                               .Skip((page - 1) * pageSize)
                                               .Take(pageSize)
                                               .ToListAsync();

            var model =  new PagedList<Game>
            {
                Items = games,
                TotalItems = totalItems,
                PageNumber = page,
                PageSize = pageSize
            };
            return model;
        }

        public async Task<PagedList<Game>> SearchGames(string searchTerm, int page, int pageSize)
        {
            searchTerm = searchTerm.ToLower(); // Converter para minúsculas para a pesquisa ser case-insensitive

            var totalItems = await _gameDbContext.Games.CountAsync(g => g.name.ToLower().Contains(searchTerm));
            var games = await _gameDbContext.Games.Where(g => g.name.ToLower().Contains(searchTerm))
                                                  .OrderBy(g => g.name)
                                                  .Skip((page - 1) * pageSize)
                                                  .Take(pageSize)
                                                  .ToListAsync();

            return new PagedList<Game>
            {
                Items = games,
                TotalItems = totalItems,
                PageNumber = page,
                PageSize = pageSize
            };
        }
    }
}
