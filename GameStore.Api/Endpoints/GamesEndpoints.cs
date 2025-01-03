using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Repositories;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGamesEndpoint = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
    {

        var group = routes.MapGroup("/games")
            .WithParameterValidation();

        group.MapGet("/", async (IGamesRepository repository) =>
            (await repository.GetAllAsync()).Select(game => game.AsDto()));
        group.MapGet("/{id}", async (IGamesRepository repository, int id) =>
        {
            Game? game =await repository.GetAsync(id);

            return game != null ? Results.Ok(game.AsDto) : Results.NotFound();

        }).WithName(GetGamesEndpoint);


        group.MapPost("/", async(IGamesRepository repository, CreateGameDto gameDto) =>
        {

            Game game = new()
            {
                Name = gameDto.Name,
                Genre = gameDto.Genre,
                Price = gameDto.Price,
                ReleaseDate = gameDto.ReleaseDate,
                ImageUri = gameDto.ImageUri,
            };
            await repository.CreateAsync(game);
            return Results.CreatedAtRoute(GetGamesEndpoint, new { Id = game.Id }, game);

        });

        group.MapPut("/{id}", async (IGamesRepository repository, int id, UpdateGameDto updateGameDto) =>
        {

            Game? existingGame = await repository.GetAsync(id);
            if (existingGame is null)
            {
                return Results.NotFound();
            }
            existingGame.Name = updateGameDto.Name;
            existingGame.Genre = updateGameDto.Genre;
            existingGame.Price = updateGameDto.Price;
            existingGame.ReleaseDate = updateGameDto.ReleaseDate;
            existingGame.ImageUri = updateGameDto.ImageUri;

            await repository.UpdateAsync(existingGame);

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (IGamesRepository repository, int id) =>
        {

            Game? game = await repository.GetAsync(id);
            if (game is not null)
            {
                await repository.DeleteAsync(id);
            }

            return Results.NoContent();
        });

        return group;


    }
}