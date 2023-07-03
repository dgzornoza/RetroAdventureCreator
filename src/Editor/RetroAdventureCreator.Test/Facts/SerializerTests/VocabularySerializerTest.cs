using RetroAdventureCreator.Core;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Infrastructure.Builders;

namespace RetroAdventureCreator.Test.Facts.SerializerTests;

public class VocabularySerializerTest
{
    [Fact]
    public void VocabularySerializer_Serialize_AsExpected()
    {
        // Arrange
        var headersSize = 3; // 3 bytes
        var game = new GameInPawsTutorialBuilder().BuildGame();

        var vocabularyVerbs = game.Assets.Vocabulary.Where(item => item.WordType == WordType.Verb);
        var headerVerbsSize = headersSize * vocabularyVerbs.Count();
        var expectedVerbsDataSize = vocabularyVerbs.Where(item => item.Synonyms != null).Sum(item => string.Join('|', item.Synonyms!).Length);
        var expectedVerbsCount = vocabularyVerbs.Count();

        var vocabularyNouns = game.Assets.Vocabulary.Where(item => item.WordType == WordType.Noun);
        var headerNounsSize = headersSize * vocabularyNouns.Count();
        var expectedNounsDataSize = vocabularyNouns.Where(item => item.Synonyms != null).Sum(item => string.Join('|', item.Synonyms!).Length);
        var expectedNounsCount = vocabularyNouns.Count();

        // Act
        var actual = new VocabularySerializer().Serialize(game.Assets.Vocabulary);

        // Assert
        Assert.NotNull(actual);

        Assert.NotNull(actual.Verbs);
        Assert.NotNull(actual.Verbs.GameComponentKeysModel);
        Assert.True(actual.Verbs.GameComponentKeysModel.Count() == expectedVerbsCount);
        Assert.True(actual.Verbs.GameComponentKeysModel.Select(item => item.Code).Distinct().Count() == expectedVerbsCount);
        Assert.True(actual.Verbs.Data.Length == expectedVerbsDataSize);

        Assert.NotNull(actual.Nouns);
        Assert.NotNull(actual.Nouns.GameComponentKeysModel);
        Assert.True(actual.Nouns.GameComponentKeysModel.Count() == expectedNounsCount);
        Assert.True(actual.Nouns.GameComponentKeysModel.Select(item => item.Code).Distinct().Count() == expectedNounsCount);
        Assert.True(actual.Nouns.Data.Length == expectedNounsDataSize);
    }

    [Fact]
    public void VocabularySerializer_Serialize_MaxVocabularies_throwsExcepion()
    {
        // Arrange
        var messageError = string.Format(RetroAdventureCreator.Core.Properties.Resources.MaxNumberVocabularyAllowedError, Constants.MaxNumberVocabularyAllowed);
        var vocabularies = Enumerable.Range(0, Constants.MaxNumberVocabularyAllowed + 1).Select(item => new VocabularyModel());

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularySerializer().Serialize(vocabularies)).Message == messageError);
    }

    [Fact]
    public void VocabularySerializer_Serialize_DuplicateCode_throwsExcepion()
    {
        // Arrange
        var code = "DuplicateCode";
        var messageError = string.Format(RetroAdventureCreator.Core.Properties.Resources.DuplicateCodeError, code);
        var vocabularies = Enumerable.Range(0, 2).Select(item => new VocabularyModel() { Code = code });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularySerializer().Serialize(vocabularies)).Message == messageError);
    }

    [Fact]
    public void VocabularySerializer_Serialize_MaxSizeSynonims_throwsExcepion()
    {
        // Arrange        
        var messageError = string.Format(RetroAdventureCreator.Core.Properties.Resources.MaxSizeOfSynonymsError, Constants.MaxNumberVocabularyAllowed);
        var vocabularies = Enumerable.Range(0, 2).Select(item => new VocabularyModel() { Code = "code", Synonyms = Enumerable.Range(0, 300).Select(synonym => "Synonym") });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularySerializer().Serialize(vocabularies)).Message == messageError);
    }

    [Fact]
    public void VocabularySerializer_Serialize_CodeNull_throwsExcepion()
    {
        // Arrange        
        var messageError = RetroAdventureCreator.Core.Properties.Resources.CodeIsRequiredError;
        var vocabularies = Enumerable.Range(0, 2).Select(item => new VocabularyModel() { });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new VocabularySerializer().Serialize(vocabularies)).Message == messageError);
    }
}
