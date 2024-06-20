using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Infrastructure.Builders;

namespace RetroAdventureCreator.Test.Facts.SerializerTests;

public class CommandsSerializerTest : SerializerBaseTest
{
    [Fact]
    public void CommandsSerializer_Serialize_AsExpected()
    {
        // Arrange
        CreateGame<GameInPawsTutorialBuilder>();
        var serializerFactory = new SerializerFactory(game);

        // Act
        var actual = serializerFactory.Serialize<CommandsSerializer>();

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Data);

        // TODO: falta implementar validador de splited data
    }

    [Fact]
    public void CommandsSerializer_MaxCommands_throwsExcepion()
    {
        // Arrange
        CreateGame<GameMaxLengthLimitsBuilder>();
        var messageError = string.Format(Core.Properties.Resources.MaxLengthCommandsAllowedError, Constants.MaxLengthCommandsAllowed);

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new CommandsSerializer(game.Commands)).Message == messageError);
    }

    [Fact]
    public void CommandsSerializer_GenerateGameComponentPointers_CodeNull_throwsExcepion()
    {
        // Arrange        
        CreateGame<GameCodeNullBuilder>();

        var messageError = Core.Properties.Resources.CodeIsRequiredError;

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new CommandsSerializer(game.Commands).GenerateGameComponentPointers()).Message == messageError);
    }

    [Fact]
    public void CommandsSerializer_GenerateGameComponentPointers_DuplicateCode_throwsExcepion()
    {
        // Arrange        
        CreateGame<GameDuplicateCodeBuilder>();

        var messageError = string.Format(Core.Properties.Resources.DuplicateCodeError, "CommandCodeDuplicated");

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new CommandsSerializer(game.Commands).GenerateGameComponentPointers()).Message == messageError);
    }

}
