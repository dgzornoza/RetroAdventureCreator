using RetroAdventureCreator.Core.Models;

namespace RetroAdventureCreator.Core.Serialization;

internal interface ISerializer<out T> : ISerializer where T : class
{
    T GameComponent { get; }
}

internal interface ISerializer
{
    IEnumerable<GameComponentPointerModel> GenerateGameComponentPointers();

    SerializerResultModel Serialize(GameComponentsPointersModel gameComponentsIndexes);
}
