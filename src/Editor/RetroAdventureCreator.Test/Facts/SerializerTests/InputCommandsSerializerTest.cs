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


        for (int i = 0; i < inputCommands.Count(); i++)
        {
            var element = inputCommands.ElementAt(i);
            List<string> codes = [element.Verb.Code];
            if (element.Nouns != null)
            {
                codes.AddRange(element.Nouns.Select(item => item.Code));
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

    private byte[] GetInputCommandData(IEnumerable<InputCommandModel> inputCommands, SerializerFactory serializerFactory) =>
        inputCommands.SortByKey().SelectMany(inputCommand =>
        {
            var result = new List<byte>
            {
                // Verb
                serializerFactory.GameComponentsPointersModel.VocabularyVerbs.IndexOf(inputCommand.Verb.Code).ToBaseZero()
            };

            // Nouns
            if (inputCommand.Nouns != null && inputCommand.Nouns.Any())
            {
                result.AddRange(inputCommand.Nouns.Select(item => serializerFactory.GameComponentsPointersModel.VocabularyNouns.IndexOf(item.Code).ToBaseZero()));
            }
            result.Add(Constants.EndToken);

            return result.ToArray();

        }).ToArray();
}
