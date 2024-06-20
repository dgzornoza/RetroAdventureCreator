using RetroAdventureCreator.Core.Extensions;
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
    /// Split game component data into element bytes
    /// </summary>
    /// <param name="gameComponentPointers">Game component pointers</param>
    /// <param name="data">Game component data</param>
    /// <returns>Game component data splited into elements bytes</returns>
    internal static IEnumerable<byte[]> SplitDataBytes(IEnumerable<GameComponentPointerModel> gameComponentPointers, byte[] data)
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

    /// <summary>
    /// Split game component data into two bytes addresses (16 bits)
    /// </summary>
    /// <param name="gameComponentPointers">Game component pointers</param>
    /// <param name="data">Game component data</param>
    /// <returns>Game component data splited into adresses (16 bits)</returns>
    internal static IEnumerable<short[]> SplitDataAddresses(IEnumerable<GameComponentPointerModel> gameComponentPointers, byte[] data) =>
        SplitDataBytes(gameComponentPointers, data).Select(item => item.Chunk(2).Select(bytes => bytes.ToShort()).ToArray());
}
