using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Helpers;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

/// <summary>
/// Flags model serializer
/// </summary>
/// <remarks>
/// Format Flags serializer:
/// ----------------------------------------------
/// 
/// Data: --
/// FlagsSize = 8 bits (256)
/// Flags = 1 bit per flag
/// 
/// </remarks>
internal class FlagsSerializer : ISerializer<IEnumerable<FlagModel>>
{
    private record struct Header(int FlagsLength, short DataAddress);

    private record struct Data(IEnumerable<FlagModel> Flags, int TotalBytes);

    public IEnumerable<GameComponentPointerModel> GenerateGameComponentKeys(IEnumerable<FlagModel> flags)
    {
        throw new NotImplementedException();
    }

    public SerializerResultModel Serialize(GameComponentsPointers gameComponentsIndexes, IEnumerable<FlagModel> flags)
    {
        EnsureHelpers.EnsureMaxLength(flags, Constants.MaxLengthFlagsAllowed,
            string.Format(Properties.Resources.MaxLengthFlagsAllowedError, Constants.MaxLengthFlagsAllowed));

        var sortedFlags = flags.SortByKey();

        var totalBytes = (int)Math.Ceiling(sortedFlags.Count() / 8M);
        var header = new Header(totalBytes, 0);
        var headerBytes = CreateHeaderBytes(header);

        var dataBytes = CreateDataBytes(new Data(sortedFlags, totalBytes));

        return new SerializerResultModel(headerBytes.ToArray(), dataBytes.ToArray());
    }

    private static byte[] CreateHeaderBytes(Header header) => new byte[]
    {
            (byte)header.FlagsLength,
            header.DataAddress.GetByte(2),
            header.DataAddress.GetByte(1),
    };

    private static byte[] CreateDataBytes(Data data)
    {
        var bits = new BitArray(data.Flags.Select(item => item.Value).ToArray());
        var result = new byte[data.TotalBytes];
        bits.CopyTo(result, 0);

        return result;
    }
}
