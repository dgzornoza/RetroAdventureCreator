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

        var objects = game.Assets.Objects;
        var headerLength = headersLength * objects.Count();
        var expectedDataLength = objects.Sum(item => item.ChildObjects?.Count() + item.RequiredComplements?.Count() + item.Complements?.Count());

        // Act
        var actual = new ObjectsSerializer(indexes).Serialize(objects);

        // Assert
        Assert.NotNull(actual);
        Assert.True(actual.Header.Length == headerLength);
        Assert.True(actual.Data.Length == expectedDataLength);
    }
}
