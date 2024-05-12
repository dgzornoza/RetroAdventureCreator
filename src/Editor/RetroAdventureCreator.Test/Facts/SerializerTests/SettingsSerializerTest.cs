using RetroAdventureCreator.Core;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Infrastructure.Builders;

namespace RetroAdventureCreator.Test.Facts.SerializerTests;

public class SettingsSerializerTest : SerializerBaseTest
{
    [Fact]
    public void SettingsSerializer_Serialize_AsExpected()
    {
        // Arrange
        CreateGame<GameInPawsTutorialBuilder>();
        var serializerFactory = new SerializerFactory(game);

        var expectedDataBytes = new byte[]
        {
            (byte)(game.Settings.Charset << 4 | (byte)game.Settings.Color),
            (byte)((byte)game.Settings.BackgroundColor << 4 | (byte)game.Settings.BorderColor),
        };

        // Act
        var actual = serializerFactory.Serialize<SettingsSerializer>();

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Data);
        Assert.True(actual.Data.Length == expectedDataBytes.Length);
        Assert.Equal(expectedDataBytes, actual.Data);
    }
}
