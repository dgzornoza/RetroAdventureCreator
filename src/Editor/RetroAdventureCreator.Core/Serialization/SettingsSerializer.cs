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
/// Data: (2 bytes)
/// Charset = 4 bits
/// Color = 4 bits
/// BackgroundColor = 4 bits
/// BorderColor = 4 bits
/// MaxChildObjectsLength = 4 bits
/// 
/// </remarks>
internal class SettingsSerializer : Serializer<SettingsModel>
{
    public SettingsSerializer(SettingsModel gameComponent) : base(gameComponent)
    {
    }

    public override IEnumerable<GameComponentPointerModel> GenerateGameComponentPointers() => Enumerable.Empty<GameComponentPointerModel>();

    public override SerializerResultModel Serialize(GameComponentsPointersModel gameComponentsPointers)
    {
        var dataBytes = CreateDataBytes();

        return new SerializerResultModel(dataBytes);
    }

    private byte[] CreateDataBytes() => new byte[]
    {
        (byte)(GameComponent.Charset << 4 | (byte)GameComponent.Color),
        (byte)((byte)GameComponent.BackgroundColor << 4 | (byte)GameComponent.BorderColor),
    };
}
