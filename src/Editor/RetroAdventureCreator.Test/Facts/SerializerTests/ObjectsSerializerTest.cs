using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Test.Infrastructure.Builders;

namespace RetroAdventureCreator.Test.Facts.SerializerTests;

public class ObjectsSerializerTest
{
    [Fact]
    public void ObjectsSerializer_Serialize_AsExpected()
    {
        // Arrange
        var headersLength = 7; // 7 bytes
        var builder = new GameInPawsTutorialBuilder();
        var game = builder.BuildGame();
        var indexes = builder.BuildGameComponentsIndexes();

        var vocabularySerializer = new VocabularySerializer().Serialize(indexes, game.Assets.Vocabulary);
        var messageSerializer = new MessagesSerializer().Serialize(indexes, game.Assets.Messages);

        var objects = game.Assets.Objects;
        var headerLength = headersLength * objects.Count();
        var expectedCount = objects.Count();
        var expectedDataLength = objects.Sum(item => item.ChildObjects?.Count() + item.RequiredComplements?.Count() + item.Complements?.Count());

        // Act
        var objectsSerializerArguments = new ObjectsSerializerArgumentsModel(objects, vocabularySerializer, messageSerializer);
        var actual = new ObjectsSerializer().Serialize(indexes, objectsSerializerArguments);

        // Assert
        Assert.NotNull(actual);

        Assert.NotNull(actual.GameComponentKeysModel);
        Assert.True(actual.GameComponentKeysModel.Count() == expectedCount);
        Assert.True(actual.GameComponentKeysModel.Select(item => item.Code).Distinct().Count() == expectedCount);
        Assert.True(actual.Header.Length == headerLength);
        Assert.True(actual.Data.Length == expectedDataLength);
    }
}
