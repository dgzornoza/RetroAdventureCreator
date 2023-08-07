using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Helpers;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

/// <summary>
/// Player model serializer
/// </summary>
/// <remarks>
/// Format Player serializer:
/// ----------------------------------------------
/// 
/// Data:
/// Health: 3 bits (7)
/// Objects: object id bytes (end with 0x00)
///  
/// </remarks>
internal class PlayerSerializer : ISerializer<PlayerModel>
{
    private record struct Header(byte Health, byte Objects, short DataAddress);

    private record struct Data(IEnumerable<byte>? ObjectsIndexes);

    public IEnumerable<GameComponentPointerModel> GenerateGameComponentKeys(PlayerModel player)
    {
        throw new NotImplementedException();
    }

    public SerializerResultModel Serialize(GameComponentsPointers gameComponentsIndexes, PlayerModel player)
    {
        EnsureObjectProperties(player);

        var header = new Header
        {
            Health = (byte)player.Health,
            Objects = (byte)(player.Objects?.Count() ?? 0),
            DataAddress = 0,
        };
        var headerBytes = CreateHeaderBytes(header);

        var playerObjectIndexes = player.Objects?.Select(item => (byte)gameComponentsIndexes.Objects.Find(item.Code).RelativePointer);

        var data = new Data(playerObjectIndexes);
        var dataBytes = CreateDataBytes(data);

        return new SerializerResultModel(headerBytes, dataBytes);
    }

    private static void EnsureObjectProperties(PlayerModel player)
    {
        EnsureHelpers.EnsureMaxLength(player.Health, Constants.MaxLengthPlayerHealthAllowed,
        string.Format(Properties.Resources.MaxLengthPlayerHealthAllowedError, Constants.MaxLengthPlayerHealthAllowed));

        if (player.Objects != null)
        {
            EnsureHelpers.EnsureMaxLength(player.Objects, Constants.MaxLengthPlayerObjectsAllowed,
                string.Format(Properties.Resources.MaxLengthPlayerObjectsAllowedError, Constants.MaxLengthPlayerObjectsAllowed));
        }
    }

    private static byte[] CreateHeaderBytes(Header header) => new byte[]
    {
        (byte)(header.Health << 3 | header.Objects),
        header.DataAddress.GetByte(2),
        header.DataAddress.GetByte(1),
    };

    private static byte[] CreateDataBytes(Data data)
    {
        var result = new List<byte>();
        if (data.ObjectsIndexes != null)
        {
            result.AddRange(data.ObjectsIndexes);
        }

        return result.ToArray();
    }

}
