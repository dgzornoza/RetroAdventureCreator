using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Infrastructure.Builders;

namespace RetroAdventureCreator.Test.Facts.SerializerTests;

public class InputCommandsSerializerTest : SerializerBaseTest
{
    [Fact]
    public void InputCommandsSerializer_Serialize_AsExpected()
    {
        // Arrange
        CreateGame<GameInPawsTutorialBuilder>();
        var serializerFactory = new SerializerFactory(game);

        var expectedDataBytes = GetInputCommandData(game.InputCommands, serializerFactory);

        // Act
        var actual = serializerFactory.Serialize<InputCommandsSerializer>();

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Data);
        Assert.True(actual.Data.Length == expectedDataBytes.Length);
        Assert.Equal(expectedDataBytes, actual.Data);
    }

    private byte[] GetInputCommandData(IEnumerable<InputCommandModel> inputCommands, SerializerFactory serializerFactory) =>
        inputCommands.SortByKey().SelectMany(inputCommand =>
        {
            var result = new List<byte>
            {
                // Verb
                serializerFactory.GameComponentsPointersModel.VocabularyVerbs.IndexOf(inputCommand.Verb.Code)
            };

            // Nouns
            if (inputCommand.Nouns != null && inputCommand.Nouns.Any())
            {
                result.AddRange(inputCommand.Nouns.Select(item => serializerFactory.GameComponentsPointersModel.VocabularyNouns.IndexOf(item.Code)));
            }
            result.Add(Constants.EndToken);

            return result.ToArray();

        }).ToArray();
}
