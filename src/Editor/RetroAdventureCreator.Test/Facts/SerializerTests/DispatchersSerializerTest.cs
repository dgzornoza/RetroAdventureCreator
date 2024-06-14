using System.Text;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Infrastructure;
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
        // TODO: implementar dispatcher test
        // Arrange
        CreateGame<GameInPawsTutorialBuilder>();
        var serializerFactory = new SerializerFactory(game);
        var AfterInputCommandDispatcher = game.Dispatchers.Where(item => item.Trigger == Trigger.AfterInputCommand).SortByKey();

        var verbsVocabularies = game.Vocabulary.Where(item => item.WordType == WordType.Verb).SortByKey();
        var nounsVocabularies = game.Vocabulary.Where(item => item.WordType == WordType.Noun).SortByKey();

        // Act
        var actual = serializerFactory.Serialize<AfterInputCommandDispatchersSerializer>();
        var splitedData = SplitData(serializerFactory.GameComponentsPointersModel.InputCommands, actual.Data);

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Data);

        // validate data
        for (int i = 0; i < AfterInputCommandDispatcher.Count(); i++)
        {
            var element = AfterInputCommandDispatcher.ElementAt(i);
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
}
