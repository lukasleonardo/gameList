using gameList.Data;
using gameList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gameList.Controllers
{
    public class GameListController : Controller
    {
        private readonly GameDbContext gameDbContext;

        public GameListController(GameDbContext gameDbContext)
        {
            this.gameDbContext = gameDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var games = await gameDbContext.Games.OrderBy(g => g.name).ToListAsync();
            return View(games);
        }

        [HttpGet]
        public IActionResult Add()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddGame addGameRequest)
        {
            var game = new Game()
            {
                id = Guid.NewGuid(),
                name = addGameRequest.name,
                description = addGameRequest.description,
                imageUrl = addGameRequest.imageUrl,
                isDone = addGameRequest.isDone,
                createdAt = DateTime.UtcNow,
            };
            await gameDbContext.Games.AddAsync(game);
            await gameDbContext.SaveChangesAsync();

            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id) {
            var game = await gameDbContext.Games.FirstOrDefaultAsync(x => x.id == id);
            if(game != null) {
                var updateModel = new EditGame()
                {
                    id = game.id,
                    name = game.name,
                    description = game.description, 
                    imageUrl = game.imageUrl,
                    isDone = game.isDone,
                    createdAt = game.createdAt,

                };
                return await Task.Run(() => View("View", updateModel));
            
            }
            return RedirectToAction("Index");
        }
         
        [HttpPost]
        public async Task<IActionResult> View( EditGame editGame)
        {
            var game = await gameDbContext.Games.FindAsync(editGame.id);
            if(game != null)
            {
                game.name = editGame.name;
                game.description = editGame.description;
                game.imageUrl = editGame.imageUrl;
                game.isDone = editGame.isDone;

                await gameDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditGame editGame)
        {
            var game = await gameDbContext.Games.FindAsync(editGame.id);
            if (game != null)
            {
                gameDbContext.Games.Remove(game);
                await gameDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
