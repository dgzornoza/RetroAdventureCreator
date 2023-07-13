using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Test.Infrastructure.Builders;

namespace RetroAdventureCreator.Test.Facts.SerializerTests;

public class InputCommandsSerializerTest
{
    [Fact]
    public void InputCommandsSerializer_Serialize_AsExpected()
    {
        // Arrange
        var headersLength = 3; // 3 bytes
        var builder = new GameInPawsTutorialBuilder();
        var game = builder.BuildGame();
        var indexes = builder.BuildGameComponentsIndexes();

        var inputCommands = game.Assets.InputCommands;
        var headerLength = headersLength * inputCommands.Count();
        var expectedDataLength = inputCommands.Sum(item => item.Nouns?.Count());

        // Act
        var actual = new InputCommandsSerializer(indexes).Serialize(inputCommands);

        // Assert
        Assert.NotNull(actual);
        Assert.True(actual.Header.Length == headerLength);
        Assert.True(actual.Data.Length == expectedDataLength);
    }
}
