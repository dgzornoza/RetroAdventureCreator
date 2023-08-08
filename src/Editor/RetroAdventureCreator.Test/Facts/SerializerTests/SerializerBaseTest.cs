using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Infrastructure.Builders;

namespace RetroAdventureCreator.Test.Facts.SerializerTests;

public abstract class SerializerBaseTest
{
    protected readonly string EndTokenString = Encoding.ASCII.GetString(new byte[] { Constants.EndToken });

    protected GameBuilder builder = default!;
    protected GameModel game = default!;

    protected void CreateGame<T>() where T : GameBuilder, new()
    {
        builder = new T();
        game = builder.BuildGame();
    }
}
