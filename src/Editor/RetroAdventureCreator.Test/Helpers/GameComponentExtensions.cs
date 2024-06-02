using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Models;

namespace RetroAdventureCreator.Test.Helpers;

internal static class GameComponentExtensions
{
    public static IEnumerable<byte[]> SplitData(this IEnumerable<GameComponentPointerModel> gameComponentPointers, byte[] data)
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
