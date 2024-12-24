using GameStore.Api.Entities;

namespace GameStore.Api.Repositories;
public class InMemGamesRepository : IGamesRepository
{
    private readonly List<Game> games = new()

{
    new Game()
    {
        Id = 1,
        Name = "Street Fighter 11",
        Genre = "Fighting",
        Price = 19.99M,
        ReleaseDate = new DateTime(1991,2,1),
        ImageUri = "http://placehold.co/100"
    },
    new Game()
    {
        Id = 2,
        Name = "Final Fantasy ",
        Genre = "Role Playings",
        Price = 59.99M,
        ReleaseDate = new DateTime(2010,2,1),
        ImageUri = "http://placehold.co/100"
    },
    new Game()
    {
        Id = 3,
        Name = "Taken 3",
        Genre = "Fighting",
        Price = 19.99M,
        ReleaseDate = new DateTime(1991,2,1),
        ImageUri = "http://placehold.co/100"
    }
};


    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await Task.FromResult(games);
    }

    public async Task<Game?> GetAsync(int id)
    {
        return await Task.FromResult (games.Find(game => game.Id == id));
    }

    public async Task CreateAsync(Game game)
    {
        game.Id = games.Max(games => games.Id) + 1;
        games.Add(game);

        await Task.CompletedTask;
    }
    public async Task UpdateAsync(Game updateGame)
    {
        var index = games.FindIndex(games => games.Id == updateGame.Id);
        games[index] = updateGame;

        await Task.CompletedTask;

    }
    public async Task DeleteAsync(int id)
    {
        var index = games.FindIndex(games => games.Id == id);
        games.RemoveAt(index);

        await Task.CompletedTask;
    }
}