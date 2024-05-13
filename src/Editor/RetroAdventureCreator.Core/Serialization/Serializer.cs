using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

internal abstract class Serializer<T> : ISerializer<T> where T : class
{
    protected Serializer(T gameComponent)
    {
        GameComponent = gameComponent;
    }

    public T GameComponent { get; init; }

    public abstract IEnumerable<GameComponentPointerModel> GenerateGameComponentPointers();

    public abstract SerializerResultModel Serialize(GameComponentsPointersModel gameComponentsIndexes);
}
