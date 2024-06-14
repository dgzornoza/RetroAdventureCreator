using RetroAdventureCreator.Core.Models;
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

    /// <summary>
    /// Split game component data into elements
    /// </summary>
    /// <param name="gameComponentPointers">Game component pointers</param>
    /// <param name="data">Game component data</param>
    /// <returns>Game component data splited into elements</returns>
    internal IEnumerable<byte[]> SplitData(IEnumerable<GameComponentPointerModel> gameComponentPointers, byte[] data)
    {
        var pointers = gameComponentPointers.ToArray();
        List<byte[]> result = new List<byte[]>(pointers.Length);
        for (var i = 0; i < pointers.Length; i++)
        {
            var currentPointer = pointers[i].RelativePointer;
            var nextPointer = i == pointers.Length - 1 ? currentPointer : pointers[i + 1].RelativePointer;

            result.Add(data.Skip(currentPointer).Take(nextPointer - currentPointer).ToArray());
        }

        return result;
    }
}
