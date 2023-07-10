﻿using System;
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

        var vocabularySerializer = new VocabularySerializer().Serialize(indexes, game.Assets.Vocabulary);

        var inputCommands = game.Assets.InputCommands;
        var headerLength = headersLength * inputCommands.Count();
        var expectedCount = inputCommands.Count();
        var expectedDataLength = inputCommands.Sum(item => item.Nouns?.Count());

        // Act
        var inputCommandsSerializerArguments = new InputCommandsSerializerArgumentsModel(inputCommands, vocabularySerializer);
        var actual = new InputCommandsSerializer().Serialize(indexes, inputCommandsSerializerArguments);

        // Assert
        Assert.NotNull(actual);

        Assert.NotNull(actual.GameComponentKeysModel);
        Assert.True(actual.GameComponentKeysModel.Count() == expectedCount);
        Assert.True(actual.GameComponentKeysModel.Select(item => item.Code).Distinct().Count() == expectedCount);
        Assert.True(actual.Header.Length == headerLength);
        Assert.True(actual.Data.Length == expectedDataLength);
    }

    [Fact]
    public void InputCommandsSerializer_Serialize_WithNullInputCommands_ThrowsInvalidOperationException()
    {
        // Arrange
        var builder = new GameInPawsTutorialBuilder();
        var game = builder.BuildGame();
        var indexes = builder.BuildGameComponentsIndexes();
        var vocabularySerializer = new VocabularySerializer().Serialize(indexes, game.Assets.Vocabulary);

        // Act
        var inputCommandsSerializerArguments = new InputCommandsSerializerArgumentsModel(null, vocabularySerializer);

        // Assert
        Assert.Throws<InvalidOperationException>(() => new InputCommandsSerializer().Serialize(indexes, inputCommandsSerializerArguments));
    }
}
