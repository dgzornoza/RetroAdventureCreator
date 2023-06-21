using RetroAdventureCreator.Core;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Infrastructure.Builders;

namespace RetroAdventureCreator.Test.Facts.SerializerTests;

public class SettingsSerializerTest
{
    [Fact]
    public void SettingsSerializer_Serialize_AsExpected()
    {
        // Arrange
        var game = new GameInPawsTutorialBuilder().BuildGame();
        // header = 2 bytes
        var expectedHeaderBytes = new byte[]
        {
            (byte)(game.Settings.Charset << 4 | (byte)game.Settings.Color),
            (byte)((byte)game.Settings.BackgroundColor << 4 | (byte)game.Settings.BorderColor),
        };

        // Act
        var actual = new SettingsSerializer().Serialize(game.Settings);

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Data);
        Assert.True(actual.Data.Length == expectedHeaderBytes.Length);
        Assert.Equal(actual.Data, expectedHeaderBytes);
    }
}
