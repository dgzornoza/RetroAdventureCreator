using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Interfaces;

namespace RetroAdventureCreator.Core.Serialization;

internal abstract class Serializer<T> : ISerializer<T> where T : class
{
    protected Serializer(T gameComponent)
    {
        GameComponent = gameComponent;
    }

    public virtual T GameComponent { get; init; }

    public abstract IEnumerable<GameComponentPointerModel> GenerateGameComponentPointers();

    public abstract SerializerResultModel Serialize(GameComponentsPointersModel gameComponentsIndexes);
}


internal abstract class SerializerList<T> : Serializer<IEnumerable<T>> where T : IUniqueKey
{
    protected SerializerList(IEnumerable<T> gameComponent) : base(gameComponent)
    {
        GameComponent = gameComponent.SortByKey();
    }

    public override IEnumerable<T> GameComponent { get; init; }
}
