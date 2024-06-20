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

        // Act
        var actual = serializerFactory.Serialize<InputCommandsSerializer>();
        var splitedData = SplitDataBytes(serializerFactory.GameComponentsPointersModel.InputCommands, actual.Data);

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Data);

        ValidateSplittedInputCommandsData(inputCommands, game, splitedData);
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

    internal static void ValidateSplittedInputCommandsData(IEnumerable<InputCommandModel> inputCommands,
        GameModel game, 
        IEnumerable<byte[]> splitedData)
    {

        var verbsVocabularies = game.Vocabulary.Where(item => item.WordType == WordType.Verb).SortByKey();
        var nounsVocabularies = game.Vocabulary.Where(item => item.WordType == WordType.Noun).SortByKey();

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
}
