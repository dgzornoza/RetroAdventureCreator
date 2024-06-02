using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Infrastructure.Builders;

namespace RetroAdventureCreator.Test.Facts.SerializerTests;

public abstract class SerializerBaseTest
{
    protected GameBuilder builder = default!;
    protected GameModel game = default!;

    protected void CreateGame<T>() where T : GameBuilder, new()
    {
        builder = new T();
        game = builder.BuildGame();
    }
}
