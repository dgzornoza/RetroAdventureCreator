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
/// Header:
/// Charset = 4 bits (16)
/// Color = 4 bits (16)
/// BackgroundColor = 4 bits (16)
/// BorderColor = 4 bits (16)
/// 
/// </remarks>
internal class SettingsSerializer : ISerializer<SettingsModel, SerializerResultModel>
{
    public SerializerResultModel Serialize(SettingsModel @object)
    {
        var settings = @object ?? throw new ArgumentNullException(nameof(@object));

        var headerBytes = CreateHeaderBytes(settings);

        return new SerializerResultModel(headerBytes);
    }

    private static byte[] CreateHeaderBytes(SettingsModel settings) => new byte[]
    {
        (byte)(settings.Charset << 4 | (byte)settings.Color),
        (byte)((byte)settings.BackgroundColor << 4 | (byte)settings.BorderColor),
    };
}
