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
    [Fact]
    public void VocabularyNounsSerializer_Serialize_AsExpected()
    {
        // Arrange
        CreateGame<GameInPawsTutorialBuilder>();
        var serializerFactory = new SerializerFactory(game);
        var serializer = serializerFactory.GetSerializer<VocabularyNounsSerializer>();

        var vocabularyNouns = game.Vocabulary.Where(item => item.WordType == WordType.Noun);
        var expectedNounsDataLength = vocabularyNouns.Where(item => item.Synonyms != null).SortByKey().Sum(item => string.Join('|', item.Synonyms!).Length + Constants.EndTokenLength);
        var expectedSynonymsBytes = Encoding.ASCII.GetBytes(string.Join(EndTokenString, 
            vocabularyNouns.Where(item => item.Synonyms != null)
            .SortByKey().Select(item => string.Join('|', item.Synonyms!))) + EndTokenString);
        var expectedSynonymsText = Encoding.ASCII.GetString(expectedSynonymsBytes);
        
        // Act
        var actual = serializer.Serialize(serializerFactory.GameComponentsPointersModel);

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Data);
        Assert.True(actual.Data.Length == expectedNounsDataLength);
        Assert.Equal(expectedSynonymsText, Encoding.ASCII.GetString(actual.Data));
    }

    [Fact]
    public void VocabularyVerbsSerializer_Serialize_AsExpected()
    {
        // Arrange
        CreateGame<GameInPawsTutorialBuilder>();
        var serializerFactory = new SerializerFactory(game);
        var serializer = serializerFactory.GetSerializer<VocabularyVerbsSerializer>();

        var vocabularyVerbs = game.Vocabulary.Where(item => item.WordType == WordType.Verb);
        var expectedVerbsDataLength = vocabularyVerbs.Where(item => item.Synonyms != null).SortByKey().Sum(item => string.Join('|', item.Synonyms!).Length + Constants.EndTokenLength);
        var expectedSynonyms = string.Join(EndTokenString, vocabularyVerbs.Where(item => item.Synonyms != null).SortByKey().Select(item => string.Join('|', item.Synonyms!))) + EndTokenString;

        // Act
        var actual = serializer.Serialize(serializerFactory.GameComponentsPointersModel);

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Data);
        Assert.True(actual.Data.Length == expectedVerbsDataLength);
        Assert.Equal(expectedSynonyms, Encoding.ASCII.GetString(actual.Data));
    }
    
    [Fact]
    public void VocabularyNounsSerializer_MaxVocabularies_throwsExcepion()
    {
        // Arrange
        CreateGame<GameMaxLengthLimitsBuilder>();

        var messageError = string.Format(RetroAdventureCreator.Core.Properties.Resources.MaxLengthVocabularyNounsAllowedError, Constants.MaxLengthVocabularyNounsAllowed);

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularyNounsSerializer(game.Vocabulary)).Message == messageError);
    }

    [Fact]
    public void VocabularyVerbsSerializer_MaxVocabularies_throwsExcepion()
    {
        // Arrange
        CreateGame<GameMaxLengthLimitsBuilder>();

        var messageError = string.Format(RetroAdventureCreator.Core.Properties.Resources.MaxLengthVocabularyVerbsAllowedError, Constants.MaxLengthVocabularyVerbsAllowed);

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularyVerbsSerializer(game.Vocabulary)).Message == messageError);
    }

    [Fact]
    public void VocabularyNounsSerializer_GenerateGameComponentPointers_MaxLengthSynonyms_throwsExcepion()
    {
        // Arrange        
        var messageError = string.Format(RetroAdventureCreator.Core.Properties.Resources.MaxLengthVocabularySynonymsError, Constants.MaxLengthVocabularySynonymsAllowed);
        var vocabularies = Enumerable.Range(0, 2).Select(item => new VocabularyModel() { WordType = WordType.Noun, Code = "code", Synonyms = Enumerable.Range(0, Constants.MaxLengthVocabularySynonymsAllowed + 1).Select(synonym => "Synonym") });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularyNounsSerializer(vocabularies).GenerateGameComponentPointers()).Message == messageError);
    }

    [Fact]
    public void VocabularyVerbsSerializer_GenerateGameComponentPointers_MaxLengthSynonyms_throwsExcepion()
    {
        // Arrange        
        var messageError = string.Format(RetroAdventureCreator.Core.Properties.Resources.MaxLengthVocabularySynonymsError, Constants.MaxLengthVocabularySynonymsAllowed);
        var vocabularies = Enumerable.Range(0, 2).Select(item => new VocabularyModel() { WordType = WordType.Verb, Code = "code", Synonyms = Enumerable.Range(0, Constants.MaxLengthVocabularySynonymsAllowed + 1).Select(synonym => "Synonym") });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularyVerbsSerializer(vocabularies).GenerateGameComponentPointers()).Message == messageError);
    }

    [Fact]
    public void VocabularyNounsSerializer_GenerateGameComponentPointers_EmptySynonyms_throwsExcepion()
    {
        // Arrange        
        var messageError = RetroAdventureCreator.Core.Properties.Resources.SysnonymsAreRequiredError;
        var vocabularies = Enumerable.Range(0, 2).Select(item => new VocabularyModel() { WordType = WordType.Noun, Code = "code", Synonyms = Enumerable.Empty<string>() });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularyNounsSerializer(vocabularies).GenerateGameComponentPointers()).Message == messageError);
    }

    [Fact]
    public void VocabularyVerbsSerializer_GenerateGameComponentPointers_EmptySynonyms_throwsExcepion()
    {
        // Arrange        
        var messageError = RetroAdventureCreator.Core.Properties.Resources.SysnonymsAreRequiredError;
        var vocabularies = Enumerable.Range(0, 2).Select(item => new VocabularyModel() { WordType = WordType.Verb, Code = "code", Synonyms = Enumerable.Empty<string>() });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularyVerbsSerializer(vocabularies).GenerateGameComponentPointers()).Message == messageError);
    }

    [Fact]
    public void VocabularyNounsSerializer_GenerateGameComponentPointers_CodeNull_throwsExcepion()
    {
        // Arrange        
        CreateGame<GameCodeNullBuilder>();

        var messageError = RetroAdventureCreator.Core.Properties.Resources.CodeIsRequiredError;

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularyNounsSerializer(game.Vocabulary).GenerateGameComponentPointers()).Message == messageError);
    }

    [Fact]
    public void VocabularyVerbsSerializer_GenerateGameComponentPointers_CodeNull_throwsExcepion()
    {
        // Arrange        
        CreateGame<GameCodeNullBuilder>();

        var messageError = RetroAdventureCreator.Core.Properties.Resources.CodeIsRequiredError;        

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularyVerbsSerializer(game.Vocabulary).GenerateGameComponentPointers()).Message == messageError);
    }
}
