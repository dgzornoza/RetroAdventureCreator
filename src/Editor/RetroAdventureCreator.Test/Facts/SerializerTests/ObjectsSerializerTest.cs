﻿using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Infrastructure.Builders;

namespace RetroAdventureCreator.Test.Facts.SerializerTests;

public class ObjectsSerializerTest : SerializerBaseTest
{
    [Fact]
    public void ObjectsSerializer_Serialize_AsExpected()
    {
        // Arrange
        CreateGame<GameInPawsTutorialBuilder>();
        var serializerFactory = new SerializerFactory(game);

        var expectedDataBytes = GetObjectData(game.Objects, serializerFactory);

        // Act
        var actual = serializerFactory.Serialize<ObjectsSerializer>();

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Data);
        Assert.True(actual.Data.Length == expectedDataBytes.Length);
        Assert.Equal(expectedDataBytes, actual.Data);
    }

    private byte[] GetObjectData(IEnumerable<ObjectModel> objects, SerializerFactory serializerFactory) =>
        objects.SortByKey().SelectMany(@object =>
        {
            var result = new List<byte>
            {
                // description
                serializerFactory.GameComponentsPointersModel.VocabularyNouns.IndexOf(@object.Name.Code),
                serializerFactory.GameComponentsPointersModel.Messages.IndexOf(@object.Description.Code),
                (byte)(@object.Weight << 3 | @object.Health),
                (byte)@object.Properties,
            };

            // ChildObjects        
            if (@object.Properties.HasFlag(ObjectProperties.IsContainer))
            {
                if (@object.ChildObjects != null && @object.ChildObjects.Any())
                {
                    result.AddRange(@object.ChildObjects.Select(item => serializerFactory.GameComponentsPointersModel.Objects.IndexOf(item.Code)));
                }
                var objectsCount = @object.ChildObjects?.Count() ?? 0;
                if (objectsCount < Constants.MaxLengthChildObjectsAllowed)
                {
                    result.AddRange(Enumerable.Range(0, Constants.MaxLengthChildObjectsAllowed - objectsCount).Select(item => (byte)0x00));
                }
            }
            return result;

        }).ToArray();
}
