using RetroAdventureCreator.Core;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Core.Services;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Helpers;
using RetroAdventureCreator.Test.Infrastructure.Builders;

namespace RetroAdventureCreator.Test.Facts.SerializerTests;

public class VocabularySerializerTest
{
    [Fact]
    public void VocabularySerializer_SerializeNouns_AsExpected()
    {
        // Arrange
        var builder = new GameInPawsTutorialBuilder();
        var game = builder.BuildGame();
        var serializerBuilder = new SerializerBuilder(game);
        var serializer = serializerBuilder.GetSerializer<VocabularyNounsSerializer>();

        var vocabularyNouns = game.Vocabulary.Where(item => item.WordType == WordType.Noun);
        var expectedNounsDataLength = vocabularyNouns.Where(item => item.Synonyms != null).Sum(item => string.Join('|', item.Synonyms!).Length + Constants.EndToken);

        // Act
        var actual = serializer.Serialize(serializerBuilder.GameComponentsPointersModel);

        // Assert
        Assert.NotNull(actual);
        Assert.True(actual.Data.Length == expectedNounsDataLength);
    }

    [Fact]
    public void VocabularySerializer_SerializeVerbs_AsExpected()
    {
        // Arrange
        var builder = new GameInPawsTutorialBuilder();
        var game = builder.BuildGame();
        var serializerBuilder = new SerializerBuilder(game);
        var serializer = serializerBuilder.GetSerializer<VocabularyVerbsSerializer>();

        var vocabularyVerbs = game.Vocabulary.Where(item => item.WordType == WordType.Verb);
        var expectedVerbsDataLength = vocabularyVerbs.Where(item => item.Synonyms != null).Sum(item => string.Join('|', item.Synonyms!).Length + Constants.EndToken);

        // Act
        var actual = serializer.Serialize(serializerBuilder.GameComponentsPointersModel);

        // Assert
        Assert.NotNull(actual);
        Assert.True(actual.Data.Length == expectedVerbsDataLength);
    }

    [Fact]
    public void VocabularySerializer_SerializeNouns_MaxVocabularies_throwsExcepion()
    {
        // Arrange
        var indexes = new GameMaxLengthLimitsBuilder().BuildGameComponentsIndexes();
        var messageError = string.Format(RetroAdventureCreator.Core.Properties.Resources.MaxLengthVocabularyNounsAllowedError, Constants.MaxLengthVocabularyNounsAllowed);
        var vocabularies = Enumerable.Range(0, Constants.MaxLengthVocabularyNounsAllowed + 1).Select(item => new VocabularyModel() { WordType = WordType.Noun });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularyNounsSerializer(indexes).Serialize(vocabularies)).Message == messageError);
    }

    [Fact]
    public void VocabularySerializer_SerializeVerbs_MaxVocabularies_throwsExcepion()
    {
        // Arrange
        var indexes = new GameMaxLengthLimitsBuilder().BuildGameComponentsIndexes();
        var messageError = string.Format(RetroAdventureCreator.Core.Properties.Resources.MaxLengthVocabularyVerbsAllowedError, Constants.MaxLengthVocabularyVerbsAllowed);
        var vocabularies = Enumerable.Range(0, Constants.MaxLengthVocabularyVerbsAllowed + 1).Select(item => new VocabularyModel() { WordType = WordType.Verb });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularyVerbsSerializer(indexes).Serialize(vocabularies)).Message == messageError);
    }

    [Fact]
    public void VocabularySerializer_SerializeNouns_MaxLengthSynonyms_throwsExcepion()
    {
        // Arrange        
        var indexes = new GameInPawsTutorialBuilder().BuildGameComponentsIndexes();
        var messageError = string.Format(RetroAdventureCreator.Core.Properties.Resources.MaxLengthVocabularySynonymsError, Constants.MaxLengthVocabularySynonymsAllowed);
        var vocabularies = Enumerable.Range(0, 2).Select(item => new VocabularyModel() { WordType = WordType.Noun, Code = "code", Synonyms = Enumerable.Range(0, Constants.MaxLengthVocabularySynonymsAllowed + 1).Select(synonym => "Synonym") });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularyNounsSerializer(indexes).Serialize(vocabularies)).Message == messageError);
    }

    [Fact]
    public void VocabularySerializer_SerializeVerbs_MaxLengthSynonyms_throwsExcepion()
    {
        // Arrange        
        var indexes = new GameInPawsTutorialBuilder().BuildGameComponentsIndexes();
        var messageError = string.Format(RetroAdventureCreator.Core.Properties.Resources.MaxLengthVocabularySynonymsError, Constants.MaxLengthVocabularySynonymsAllowed);
        var vocabularies = Enumerable.Range(0, 2).Select(item => new VocabularyModel() { WordType = WordType.Verb, Code = "code", Synonyms = Enumerable.Range(0, Constants.MaxLengthVocabularySynonymsAllowed + 1).Select(synonym => "Synonym") });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularyVerbsSerializer(indexes).Serialize(vocabularies)).Message == messageError);
    }

    [Fact]
    public void VocabularySerializer_SerializeNouns_EmptySynonyms_throwsExcepion()
    {
        // Arrange        
        var indexes = new GameInPawsTutorialBuilder().BuildGameComponentsIndexes();
        var messageError = RetroAdventureCreator.Core.Properties.Resources.SysnonymsAreRequiredError;
        var vocabularies = Enumerable.Range(0, 2).Select(item => new VocabularyModel() { WordType = WordType.Noun, Code = "code", Synonyms = Enumerable.Empty<string>() });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularyNounsSerializer(indexes).Serialize(vocabularies)).Message == messageError);
    }

    [Fact]
    public void VocabularySerializer_SerializeVerbs_EmptySynonyms_throwsExcepion()
    {
        // Arrange        
        var indexes = new GameInPawsTutorialBuilder().BuildGameComponentsIndexes();
        var messageError = RetroAdventureCreator.Core.Properties.Resources.SysnonymsAreRequiredError;
        var vocabularies = Enumerable.Range(0, 2).Select(item => new VocabularyModel() { WordType = WordType.Verb, Code = "code", Synonyms = Enumerable.Empty<string>() });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularyVerbsSerializer(indexes).Serialize(vocabularies)).Message == messageError);
    }

    [Fact]
    public void VocabularySerializer_SerializeNouns_CodeNull_throwsExcepion()
    {
        // Arrange        
        var indexes = new GameInPawsTutorialBuilder().BuildGameComponentsIndexes();
        var messageError = RetroAdventureCreator.Core.Properties.Resources.CodeIsRequiredError;
        var vocabularies = Enumerable.Range(0, 2).Select(item => new VocabularyModel() { WordType = WordType.Noun, Synonyms = Enumerable.Range(0, 1).Select(synonym => "Synonym") });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularyNounsSerializer(indexes).Serialize(vocabularies)).Message == messageError);
    }

    [Fact]
    public void VocabularySerializer_SerializeVerbs_CodeNull_throwsExcepion()
    {
        // Arrange        
        var indexes = new GameInPawsTutorialBuilder().BuildGameComponentsIndexes();
        var messageError = RetroAdventureCreator.Core.Properties.Resources.CodeIsRequiredError;
        var vocabularies = Enumerable.Range(0, 2).Select(item => new VocabularyModel() { WordType = WordType.Verb, Synonyms = Enumerable.Range(0, 1).Select(synonym => "Synonym") });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularyVerbsSerializer(indexes).Serialize(vocabularies)).Message == messageError);
    }
}
