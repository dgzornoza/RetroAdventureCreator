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
        var builder = new GameInPawsTutorialBuilder();
        var game = builder.BuildGame();
        var indexes = builder.BuildGameComponentsIndexes();
        // header = 2 bytes
        var expectedHeaderBytes = new byte[]
        {
            (byte)(game.Settings.Charset << 4 | (byte)game.Settings.Color),
            (byte)((byte)game.Settings.BackgroundColor << 4 | (byte)game.Settings.BorderColor),
        };

        // Act
        var actual = new SettingsSerializer().Serialize(indexes, game.Settings);

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Header);
        Assert.NotNull(actual.Data);
        Assert.True(actual.Header.Length == expectedHeaderBytes.Length);
        Assert.Equal(actual.Header, expectedHeaderBytes);
        Assert.Empty(actual.Data);
    }
}
