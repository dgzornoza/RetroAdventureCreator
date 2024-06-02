using System.Linq;
using System.Text;
using RetroAdventureCreator.Core;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Core.Services;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Helpers;
using RetroAdventureCreator.Test.Infrastructure.Builders;

namespace RetroAdventureCreator.Test.Facts.SerializerTests;

public class VocabularySerializerTest : SerializerBaseTest
{
    private static readonly Encoding encoding = Encoding.ASCII;

    [Fact]
    public void VocabularyNounsSerializer_Serialize_AsExpected()
    {
        // Arrange
        CreateGame<GameInPawsTutorialBuilder>();
        var serializerFactory = new SerializerFactory(game);

        var vocabularyNouns = game.Vocabulary.Where(item => item.WordType == WordType.Noun).SortByKey();

        var expectedNounsDataLength = vocabularyNouns.Where(item => item.Synonyms != null)
            .Sum(item => string.Join('|', item.Synonyms!).Length);

        var expectedSynonymsBytes = encoding.GetBytes(string.Join(string.Empty, 
            vocabularyNouns.Where(item => item.Synonyms != null)
            .Select(item => string.Join('|', item.Synonyms!))));

        var expectedSynonymsText = encoding.GetString(expectedSynonymsBytes);
        
        // Act
        var actual = serializerFactory.Serialize<VocabularyNounsSerializer>();
        var splitedData = serializerFactory.GameComponentsPointersModel.VocabularyNouns.SplitData(actual.Data);

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Data);
        Assert.True(actual.Data.Length == expectedNounsDataLength);
        Assert.Equal(expectedSynonymsText, encoding.GetString(actual.Data));

        for (int i = 0; i < vocabularyNouns.Count(); i++)
        {
            Assert.Equal(string.Join('|', vocabularyNouns.ElementAt(i).Synonyms), encoding.GetString(splitedData.ElementAt(i)));
        }
    }

    [Fact]
    public void VocabularyVerbsSerializer_Serialize_AsExpected()
    {
        // Arrange
        CreateGame<GameInPawsTutorialBuilder>();
        var serializerFactory = new SerializerFactory(game);

        var vocabularyVerbs = game.Vocabulary.Where(item => item.WordType == WordType.Verb).SortByKey();
        var expectedVerbsDataLength = vocabularyVerbs.Where(item => item.Synonyms != null)
            .Sum(item => string.Join('|', item.Synonyms!).Length);

        var expectedSynonyms = string.Join(string.Empty, 
            vocabularyVerbs.Where(item => item.Synonyms != null).Select(item => string.Join('|', item.Synonyms!)));

        // Act
        var actual = serializerFactory.Serialize<VocabularyVerbsSerializer>();
        var splitedData = serializerFactory.GameComponentsPointersModel.VocabularyVerbs.SplitData(actual.Data);

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Data);
        Assert.True(actual.Data.Length == expectedVerbsDataLength);
        Assert.Equal(expectedSynonyms, encoding.GetString(actual.Data));

        for (int i = 0; i < vocabularyVerbs.Count(); i++)
        {
            Assert.Equal(string.Join('|', vocabularyVerbs.ElementAt(i).Synonyms), encoding.GetString(splitedData.ElementAt(i)));
        }
    }
    
    [Fact]
    public void VocabularyNounsSerializer_MaxVocabularies_throwsExcepion()
    {
        // Arrange
        CreateGame<GameMaxLengthLimitsBuilder>();

        var messageError = string.Format(RetroAdventureCreator.Core.Properties.Resources.MaxLengthVocabularyNounsAllowedError, Constants.MaxLengthVocabularyNounsAllowed);

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularyNounsSerializer(game.Vocabulary, encoding)).Message == messageError);
    }

    [Fact]
    public void VocabularyVerbsSerializer_MaxVocabularies_throwsExcepion()
    {
        // Arrange
        CreateGame<GameMaxLengthLimitsBuilder>();

        var messageError = string.Format(RetroAdventureCreator.Core.Properties.Resources.MaxLengthVocabularyVerbsAllowedError, Constants.MaxLengthVocabularyVerbsAllowed);

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularyVerbsSerializer(game.Vocabulary, encoding)).Message == messageError);
    }

    [Fact]
    public void VocabularyNounsSerializer_GenerateGameComponentPointers_MaxLengthSynonyms_throwsExcepion()
    {
        // Arrange        
        var messageError = string.Format(RetroAdventureCreator.Core.Properties.Resources.MaxLengthVocabularySynonymsError, Constants.MaxLengthVocabularySynonymsAllowed);
        var vocabularies = Enumerable.Range(0, 2).Select(item => new VocabularyModel() { WordType = WordType.Noun, Code = "code", Synonyms = Enumerable.Range(0, Constants.MaxLengthVocabularySynonymsAllowed + 1).Select(synonym => "Synonym") });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularyNounsSerializer(vocabularies, encoding).GenerateGameComponentPointers()).Message == messageError);
    }

    [Fact]
    public void VocabularyVerbsSerializer_GenerateGameComponentPointers_MaxLengthSynonyms_throwsExcepion()
    {
        // Arrange        
        var messageError = string.Format(RetroAdventureCreator.Core.Properties.Resources.MaxLengthVocabularySynonymsError, Constants.MaxLengthVocabularySynonymsAllowed);
        var vocabularies = Enumerable.Range(0, 2).Select(item => new VocabularyModel() { WordType = WordType.Verb, Code = "code", Synonyms = Enumerable.Range(0, Constants.MaxLengthVocabularySynonymsAllowed + 1).Select(synonym => "Synonym") });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularyVerbsSerializer(vocabularies, encoding).GenerateGameComponentPointers()).Message == messageError);
    }

    [Fact]
    public void VocabularyNounsSerializer_GenerateGameComponentPointers_EmptySynonyms_throwsExcepion()
    {
        // Arrange        
        var messageError = RetroAdventureCreator.Core.Properties.Resources.SysnonymsAreRequiredError;
        var vocabularies = Enumerable.Range(0, 2).Select(item => new VocabularyModel() { WordType = WordType.Noun, Code = "code", Synonyms = Enumerable.Empty<string>() });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularyNounsSerializer(vocabularies, encoding).GenerateGameComponentPointers()).Message == messageError);
    }

    [Fact]
    public void VocabularyVerbsSerializer_GenerateGameComponentPointers_EmptySynonyms_throwsExcepion()
    {
        // Arrange        
        var messageError = RetroAdventureCreator.Core.Properties.Resources.SysnonymsAreRequiredError;
        var vocabularies = Enumerable.Range(0, 2).Select(item => new VocabularyModel() { WordType = WordType.Verb, Code = "code", Synonyms = Enumerable.Empty<string>() });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularyVerbsSerializer(vocabularies, encoding).GenerateGameComponentPointers()).Message == messageError);
    }

    [Fact]
    public void VocabularyNounsSerializer_GenerateGameComponentPointers_CodeNull_throwsExcepion()
    {
        // Arrange        
        CreateGame<GameCodeNullBuilder>();

        var messageError = RetroAdventureCreator.Core.Properties.Resources.CodeIsRequiredError;

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularyNounsSerializer(game.Vocabulary, encoding).GenerateGameComponentPointers()).Message == messageError);
    }

    [Fact]
    public void VocabularyVerbsSerializer_GenerateGameComponentPointers_CodeNull_throwsExcepion()
    {
        // Arrange        
        CreateGame<GameCodeNullBuilder>();

        var messageError = RetroAdventureCreator.Core.Properties.Resources.CodeIsRequiredError;        

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularyVerbsSerializer(game.Vocabulary, encoding).GenerateGameComponentPointers()).Message == messageError);
    }
}
