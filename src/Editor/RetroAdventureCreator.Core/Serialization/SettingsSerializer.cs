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
/// Charset = 3 bits (8)
/// Color = 4 bits (15)
/// BackgroundColor = 4 bits (15)
/// BorderColor = 4 bits (15)
/// 
/// </remarks>
internal class SettingsSerializer : ISerializer
{
    public SerializerResultModel Serialize(GameModel game)
    {
        throw new NotImplementedException();
    }
}
