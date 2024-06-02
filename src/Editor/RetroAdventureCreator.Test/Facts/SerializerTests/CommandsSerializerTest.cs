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

        var expectedDataBytes = GetCommandData(game.Commands, serializerFactory);

        // Act
        var actual = serializerFactory.Serialize<CommandsSerializer>();

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Data);
        Assert.True(actual.Data.Length == expectedDataBytes.Length);
        Assert.Equal(expectedDataBytes, actual.Data);
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

    private byte[] GetCommandData(IEnumerable<CommandModel> commands, SerializerFactory serializerFactory) =>
        commands.SortByKey().SelectMany(command =>
        {
            var result = new List<byte>
            {
                // Verb
                //serializerFactory.GameComponentsPointersModel.VocabularyVerbs.IndexOf(command.Verb.Code)
            };

            // Nouns
            //if (command.Nouns != null && command.Nouns.Any())
            //{
            //    result.AddRange(command.Nouns.Select(item => serializerFactory.GameComponentsPointersModel.VocabularyNouns.IndexOf(item.Code)));
            //}
            result.Add(Constants.EndToken);

            return result.ToArray();

        }).ToArray();
}
