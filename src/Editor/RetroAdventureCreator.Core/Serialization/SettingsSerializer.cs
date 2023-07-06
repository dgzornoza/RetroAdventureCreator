using System.Text;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

/// <summary>
/// Settings model serializer
/// </summary>
/// <remarks>
/// Format Settings serializer:
/// ----------------------------------------------
/// 
/// Header: (2 bytes)
/// Charset = 4 bits
/// Color = 4 bits
/// BackgroundColor = 4 bits
/// BorderColor = 4 bits
/// 
/// </remarks>
internal class SettingsSerializer : ISerializer<SettingsModel, SerializerResultModel>
{
    public SerializerResultModel Serialize(SettingsModel @object)
    {
        var settings = @object ?? throw new ArgumentNullException(nameof(@object));

        var headerBytes = CreateHeaderBytes(settings);

        return new SerializerResultModel(headerBytes, Array.Empty<byte>());
    }

    private static byte[] CreateHeaderBytes(SettingsModel settings) => new byte[]
    {
        (byte)(settings.Charset << 4 | (byte)settings.Color),
        (byte)((byte)settings.BackgroundColor << 4 | (byte)settings.BorderColor),
    };
}
