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
    protected readonly GameComponentsIndexes gameComponentsIndexes;

    protected Serializer(GameComponentsIndexes gameComponentsIndexes)
    {
        this.gameComponentsIndexes = gameComponentsIndexes ?? throw new InvalidOperationException(nameof(gameComponentsIndexes));
    }

    public abstract SerializerResultModel Serialize(T @object);
}
