using RetroAdventureCreator.Core.Models;

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
