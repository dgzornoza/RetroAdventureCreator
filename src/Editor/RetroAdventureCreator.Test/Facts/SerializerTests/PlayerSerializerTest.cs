using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Test.Infrastructure.Builders;

namespace RetroAdventureCreator.Test.Facts.SerializerTests;

public class PlayerSerializerTest
{
    [Fact]
    public void PlayerSerializerTest_Serialize_AsExpected()
    {
        // Arrange
        var headersLength = 3; // 3 bytes
        var builder = new GameInPawsTutorialBuilder();
        var game = builder.BuildGame();
        var indexes = builder.BuildGameComponentsIndexes();

        var player = game.Player;
        var headerLength = headersLength;
        var expectedDataLength = player.Objects?.Count();

        // Act
        var actual = new PlayerSerializer(indexes).Serialize(player);

        // Assert
        Assert.NotNull(actual);
        Assert.True(actual.Header.Length == headerLength);
        Assert.True(actual.Data.Length == expectedDataLength);
    }
}
