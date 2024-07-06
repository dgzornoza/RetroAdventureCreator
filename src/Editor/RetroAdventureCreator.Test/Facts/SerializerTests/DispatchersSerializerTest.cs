using System.Text;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Infrastructure.Builders;

namespace RetroAdventureCreator.Test.Facts.SerializerTests;

public class DispatchersSerializerTest : SerializerBaseTest
{
    [Fact]
    public void AfterInputCommandDispatchersSerializerTest_Serialize_AsExpected()
    {
        // Arrange
        CreateGame<GameInPawsTutorialBuilder>();
        var serializerFactory = new SerializerFactory(game);

        var afterInputCommandDispatcher = game.Dispatchers.Where(item => item.Trigger == Trigger.AfterInputCommand).SortByKey();

        // Act
        var actual = serializerFactory.Serialize<AfterInputCommandDispatchersSerializer>();
        var splitedData = SplitDataAddresses(serializerFactory.GameComponentsPointersModel.AfterInputCommandDispatchers, actual.Data);

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Data);

        ValidateSplittedInputCommandDispatchers(serializerFactory.GameComponentsPointersModel, afterInputCommandDispatcher, splitedData);
    }

    [Fact]
    public void BeforeInputCommandDispatchersSerializerTest_Serialize_AsExpected()
    {
        // Arrange
        CreateGame<GameInPawsTutorialBuilder>();
        var serializerFactory = new SerializerFactory(game);

        var beforeInputCommandDispatcher = game.Dispatchers.Where(item => item.Trigger == Trigger.BeforeInputCommand).SortByKey();

        // Act
        var actual = serializerFactory.Serialize<BeforeInputCommandDispatchersSerializer>();
        var splitedData = SplitDataAddresses(serializerFactory.GameComponentsPointersModel.BeforeInputCommandDispatchers, actual.Data);

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Data);

        ValidateSplittedInputCommandDispatchers(serializerFactory.GameComponentsPointersModel, beforeInputCommandDispatcher, splitedData);
    }

    [Fact]
    public void AfterInputCommandDispatchersSerializer_MaxInputCommands_throwsExcepion()
    {
        // Arrange
        CreateGame<GameMaxLengthLimitsBuilder>();
        var messageError = string.Format(Core.Properties.Resources.MaxLengthAfterInputCommandDispatchersAllowedError, Constants.MaxLengthAfterInputCommandDispatchersAllowed);

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new AfterInputCommandDispatchersSerializer(game.Dispatchers)).Message == messageError);
    }

    [Fact]
    public void BeforeInputCommandDispatchersSerializer_MaxInputCommands_throwsExcepion()
    {
        // Arrange
        CreateGame<GameMaxLengthLimitsBuilder>();
        var messageError = string.Format(Core.Properties.Resources.MaxLengthBeforeInputCommandDispatchersAllowedError, Constants.MaxLengthBeforeInputCommandDispatchersAllowed);

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new BeforeInputCommandDispatchersSerializer(game.Dispatchers)).Message == messageError);
    }

    [Fact]
    public void AfterInputCommandDispatchersSerializer_GenerateGameComponentPointers_CodeNull_throwsExcepion()
    {
        // Arrange        
        CreateGame<GameCodeNullBuilder>();

        var messageError = Core.Properties.Resources.CodeIsRequiredError;

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new AfterInputCommandDispatchersSerializer(game.Dispatchers).GenerateGameComponentPointers()).Message == messageError);
    }

    [Fact]
    public void BeforeInputCommandDispatchersSerializer_GenerateGameComponentPointers_CodeNull_throwsExcepion()
    {
        // Arrange        
        CreateGame<GameCodeNullBuilder>();

        var messageError = Core.Properties.Resources.CodeIsRequiredError;

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new BeforeInputCommandDispatchersSerializer(game.Dispatchers).GenerateGameComponentPointers()).Message == messageError);
    }

    [Fact]
    public void BeforeInputCommandDispatchersSerializer_GenerateGameComponentPointers_DuplicateCode_throwsExcepion()
    {
        // Arrange        
        CreateGame<GameDuplicateCodeBuilder>();

        var messageError = string.Format(Core.Properties.Resources.DuplicateCodeError, "AfterDispatcherCodeDuplicated");

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new AfterInputCommandDispatchersSerializer(game.Dispatchers).GenerateGameComponentPointers()).Message == messageError);
    }

    [Fact]
    public void AfterInputCommandDispatchersSerializer_GenerateGameComponentPointers_DuplicateCode_throwsExcepion()
    {
        // Arrange        
        CreateGame<GameDuplicateCodeBuilder>();

        var messageError = string.Format(Core.Properties.Resources.DuplicateCodeError, "BeforeDispatcherCodeDuplicated");

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new BeforeInputCommandDispatchersSerializer(game.Dispatchers).GenerateGameComponentPointers()).Message == messageError);
    }

    internal static void ValidateSplittedInputCommandDispatchers(
        GameComponentsPointersModel gameComponentsPointersModel,
        IEnumerable<DispatcherModel> inputCommands,
        IEnumerable<short[]> splitedData)
    {
        for (int i = 0; i < inputCommands.Count(); i++)
        {
            var element = inputCommands.ElementAt(i);

            // input commands
            if (element.InputCommands != null)
            {
                var inputCommandCodes = element.InputCommands.Select(item => item.Code).ToArray();
                for (int j = 0; j < inputCommandCodes.Length; j++)
                {
                    var address = gameComponentsPointersModel.InputCommands
                        .First(item => item.Code == inputCommandCodes[j]).RelativePointer;
                    Assert.Equal(address, splitedData.ElementAt(i)[j]);
                }
            }

            // commands
            var commandCodes = element.Commands.Select(item => item.Code).ToArray();
            var offset = element.InputCommands?.Count() ?? 0;
            for (int j = offset; j < offset + commandCodes.Length; j++)
            {
                var address = gameComponentsPointersModel.Commands
                    .First(item => item.Code == commandCodes[j - offset]).RelativePointer;
                Assert.Equal(address, splitedData.ElementAt(i)[j]);
            }
        }
    }
}
