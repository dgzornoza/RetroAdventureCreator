using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Helpers;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Infrastructure.Builders;

namespace RetroAdventureCreator.Test.Facts.SerializerTests;

public class ObjectsSerializerTest : SerializerBaseTest
{
    [Fact]
    public void ObjectsSerializerTest_Serialize_AsExpected()
    {
        // Arrange
        CreateGame<GameInPawsTutorialBuilder>();
        var serializerFactory = new SerializerFactory(game);

        // Act
        var actual = serializerFactory.Serialize<ObjectsSerializer>();
        var splitedData = SplitDataBytes(serializerFactory.GameComponentsPointersModel.Objects, actual.Data);

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Data);

        ValidateSplittedObjects(serializerFactory.GameComponentsPointersModel, game.Objects, splitedData);
    }

    [Fact]
    public void ObjectsSerializerTest_MaxInputCommands_throwsExcepion()
    {
        // Arrange
        CreateGame<GameMaxLengthLimitsBuilder>();
        var messageError = string.Format(Core.Properties.Resources.MaxLengthObjectsAllowedError, Constants.MaxLengthObjectsAllowed);

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new ObjectsSerializer(game.Objects)).Message == messageError);
    }

    [Fact]
    public void ObjectsSerializerTest_GenerateGameComponentPointers_CodeNull_throwsExcepion()
    {
        // Arrange        
        CreateGame<GameCodeNullBuilder>();

        var messageError = Core.Properties.Resources.CodeIsRequiredError;

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new ObjectsSerializer(game.Objects).GenerateGameComponentPointers()).Message == messageError);
    }

    [Fact]
    public void ObjectsSerializerTest_GenerateGameComponentPointers_DuplicateCode_throwsExcepion()
    {
        // Arrange        
        CreateGame<GameDuplicateCodeBuilder>();

        var messageError = string.Format(Core.Properties.Resources.DuplicateCodeError, "ObjectCodeDuplicated");

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new ObjectsSerializer(game.Objects).GenerateGameComponentPointers()).Message == messageError);
    }

    [Fact]
    public void ObjectsSerializerTest_GenerateGameComponentPointers_DuplicateOwnerCode_throwsExcepion()
    {
        // Arrange        
        CreateGame<GameDuplicateCodeBuilder>();

        var messageError = string.Format(Core.Properties.Resources.DuplicateCodeError, "ObjectCodeDuplicated");

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new ObjectsSerializer(game.Objects).GenerateGameComponentPointers()).Message == messageError);
    }

    [Fact]
    public void ObjectsSerializerTest_GenerateGameComponentPointers_EmptyName_throwsExcepion()
    {
        // Arrange        
        var code = "code1";
        var messageError = string.Format(Core.Properties.Resources.NameIsRequiredError, code);
        var objects = Enumerable.Range(0, 1).Select(item => new ObjectModel() 
        {
            Code = code, 
            Description = new MessageModel(),
            OwnerCode = "owner",
        });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new ObjectsSerializer(objects).GenerateGameComponentPointers()).Message == messageError);
    }

    [Fact]
    public void ObjectsSerializerTest_GenerateGameComponentPointers_EmptyDescription_throwsExcepion()
    {
        // Arrange        
        var code = "code1";
        var messageError = string.Format(Core.Properties.Resources.DescriptionIsRequiredError, code);
        var objects = Enumerable.Range(0, 1).Select(item => new ObjectModel()
        {
            Code = code,
            Name = new VocabularyModel(),
            OwnerCode = "owner",
        });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new ObjectsSerializer(objects).GenerateGameComponentPointers()).Message == messageError);
    }

    [Fact]
    public void ObjectsSerializerTest_GenerateGameComponentPointers_EmptyOwnerCode_throwsExcepion()
    {
        // Arrange        
        var code = "code1";
        var messageError = string.Format(Core.Properties.Resources.OwnerIsRequiredError, code);
        var objects = Enumerable.Range(0, 1).Select(item => new ObjectModel()
        {
            Code = code,
            Name = new VocabularyModel(),
            Description = new MessageModel(),
        });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new ObjectsSerializer(objects).GenerateGameComponentPointers()).Message == messageError);
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
