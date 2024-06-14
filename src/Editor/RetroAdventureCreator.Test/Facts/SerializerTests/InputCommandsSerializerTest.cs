using System.Text;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Infrastructure.Game.Enums;
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
        var inputCommands = game.InputCommands.SortByKey();

        var verbsVocabularies = game.Vocabulary.Where(item => item.WordType == WordType.Verb).SortByKey();
        var nounsVocabularies = game.Vocabulary.Where(item => item.WordType == WordType.Noun).SortByKey();

        var expectedDataBytes = GetInputCommandData(game.InputCommands, serializerFactory);

        // Act
        var actual = serializerFactory.Serialize<InputCommandsSerializer>();
        var splitedData = SplitData(serializerFactory.GameComponentsPointersModel.InputCommands, actual.Data);

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Data);
        Assert.True(actual.Data.Length == expectedDataBytes.Length);
        Assert.Equal(expectedDataBytes, actual.Data);

        // validate data
        for (int i = 0; i < inputCommands.Count(); i++)
        {
            var element = inputCommands.ElementAt(i);
            List<string> codes = [element.Verbs.Code];
            if (element.Nouns != null)
            {
                codes.Add(element.Nouns.Code);
            }

            for (int j = 0; j < codes.Count; j++)
            {
                if (j == 0)
                {
                    Assert.Equal(verbsVocabularies.IndexOf(codes[j]).ToBaseZero(), splitedData.ElementAt(i)[j]);
                }
                else
                {
                    Assert.Equal(nounsVocabularies.IndexOf(codes[j]).ToBaseZero(), splitedData.ElementAt(i)[j]);
                }
            }   
        }
    }

    [Fact]
    public void InputCommandsSerializer_MaxInputCommands_throwsExcepion()
    {
        // Arrange
        CreateGame<GameMaxLengthLimitsBuilder>();
        var messageError = string.Format(Core.Properties.Resources.MaxLengthInputCommandsAllowedError, Constants.MaxLengthInputCommandsAllowed);

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new InputCommandsSerializer(game.InputCommands)).Message == messageError);
    }

    [Fact]
    public void InputCommandsSerializer_GenerateGameComponentPointers_CodeNull_throwsExcepion()
    {
        // Arrange        
        CreateGame<GameCodeNullBuilder>();

        var messageError = Core.Properties.Resources.CodeIsRequiredError;

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new InputCommandsSerializer(game.InputCommands).GenerateGameComponentPointers()).Message == messageError);
    }

    [Fact]
    public void InputCommandsSerializer_GenerateGameComponentPointers_DuplicateCode_throwsExcepion()
    {
        // Arrange        
        CreateGame<GameDuplicateCodeBuilder>();

        var messageError = string.Format(Core.Properties.Resources.DuplicateCodeError, "InputCommandCodeDuplicated");

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new InputCommandsSerializer(game.InputCommands).GenerateGameComponentPointers()).Message == messageError);
    }

    private byte[] GetInputCommandData(IEnumerable<InputCommandModel> inputCommands, SerializerFactory serializerFactory) =>
        inputCommands.SortByKey().SelectMany(inputCommand =>
        {
            var result = new List<byte>
            {
                // Verb
                serializerFactory.GameComponentsPointersModel.VocabularyVerbs.IndexOf(inputCommand.Verbs.Code).ToBaseZero()
            };

            // Nouns
            if (inputCommand.Nouns != null)
            {
                result.Add(serializerFactory.GameComponentsPointersModel.VocabularyNouns.IndexOf(inputCommand.Nouns.Code).ToBaseZero());
            }

            return result.ToArray();

        }).ToArray();
}
