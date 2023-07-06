using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Test.Infrastructure.Builders;

namespace RetroAdventureCreator.Test.Facts.SerializerTests;

public class ObjectsSerializerTest
{
    [Fact]
    public void ObjectsSerializer_Serialize_AsExpected()
    {
        // Arrange
        var headersSize = 7; // 7 bytes
        var game = new GameInPawsTutorialBuilder().BuildGame();
        var vocabularySerializer = new VocabularySerializer().Serialize(game.Assets.Vocabulary);
        var messageSerializer = new MessagesSerializer().Serialize(game.Assets.Messages);

        var objects = game.Assets.Objects;
        var headerSize = headersSize * objects.Count();
        var expectedCount = objects.Count();
        // 1 object = 8 bits
        var expectedDataSize = objects.Sum(item => item.ChildObjects?.Count() + item.RequiredComplements?.Count() + item.Complements?.Count()) * 8;

        // Act
        var objectsSerializerArguments = new ObjectsSerializerArguments(objects, vocabularySerializer.Nouns, messageSerializer);
        var actual = new ObjectsSerializer().Serialize(objectsSerializerArguments);

        // Assert
        Assert.NotNull(actual);

        Assert.NotNull(actual.GameComponentKeysModel);
        Assert.True(actual.GameComponentKeysModel.Count() == expectedCount);
        Assert.True(actual.GameComponentKeysModel.Select(item => item.Code).Distinct().Count() == expectedCount);
        Assert.True(actual.Data.Length == expectedDataSize);
    }
}
