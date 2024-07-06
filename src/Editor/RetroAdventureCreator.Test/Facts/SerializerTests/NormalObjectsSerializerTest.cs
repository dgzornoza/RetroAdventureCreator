using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Helpers;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Infrastructure.Builders;

namespace RetroAdventureCreator.Test.Facts.SerializerTests;

public class NormalObjectsSerializerTest : SerializerBaseTest
{
    [Fact]
    public void NormalObjectsSerializerTest_Serialize_AsExpected()
    {
        // Arrange
        CreateGame<GameInPawsTutorialBuilder>();
        var serializerFactory = new SerializerFactory(game);

        // Act
        var actual = serializerFactory.Serialize<NormalObjectsSerializer>();
        var splitedData = SplitDataBytes(serializerFactory.GameComponentsPointersModel.Objects, actual.Data);

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Data);

        ValidateSplittedObjects(serializerFactory.GameComponentsPointersModel, game.Objects, splitedData);
    }

    internal static void ValidateSplittedObjects(
        GameComponentsPointersModel gameComponentsPointersModel,
        IEnumerable<ObjectModel> objects,
        IEnumerable<byte[]> splitedData)
    {
        for (int i = 0; i < objects.Count(); i++)
        {
            var element = objects.ElementAt(i);
            var offset = 0;

            // Name (1 byte)
            var index = gameComponentsPointersModel.VocabularyNouns.IndexOf(element.Name.Code).ToBaseZero();
            Assert.Equal(index, splitedData.ElementAt(i)[offset]);
            offset += 1;

            // Description (2 bytes)
            var address = gameComponentsPointersModel.Messages.First(item => item.Code == element.Description.Code).RelativePointer;
            Assert.Equal(address, splitedData.ElementAt(i).Skip(offset).Take(2).ToArray().ToShort());
            offset += 2;

            // Properties (1 byte)
            Assert.Equal(((byte)element.Properties), splitedData.ElementAt(i).Skip(offset).ToArray()[1]);
            offset += 1;

            // OwnerCode (2 bytes)
            address = FindOwnerPointer(element, gameComponentsPointersModel);
            Assert.Equal(address, splitedData.ElementAt(i).Skip(offset).Take(2).ToArray().ToShort());
            offset += 2;
        }
    }

    private static short FindOwnerPointer(ObjectModel @object, GameComponentsPointersModel gameComponentsPointers)
    {
        var ownerComponents = new List<GameComponentPointerModel>();
        ownerComponents.AddRange(gameComponentsPointers.Actors);
        ownerComponents.AddRange(gameComponentsPointers.Scenes);
        ownerComponents.AddRange(gameComponentsPointers.Objects);

        return ownerComponents.Find(@object.OwnerCode).RelativePointer;
    }
}
