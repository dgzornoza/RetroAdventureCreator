using RetroAdventureCreator.Core;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Infrastructure.Builders;

namespace RetroAdventureCreator.Test.Facts.SerializerTests;

public class VocabularySerializerTest
{
    [Fact]
    public void VocabularySerializer_Serialize_AsExpected()
    {
        // Arrange
        var game = new GameInPawsTutorialBuilder().BuildGame();
        // header = 2 bytes
        var headersSize = 2 * game.Assets.Vocabulary.Count(); // 2 bytes
        var dataSize = game.Assets.Vocabulary.Where(item => item.Synonyms != null).Sum(item => string.Join('|', item.Synonyms!).Length);
        var expectedDataSize = headersSize + dataSize;

        // Act
        var actual = new VocabularySerializer().Serialize(game.Assets.Vocabulary);

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Data);
        Assert.NotNull(actual.GameComponentKeysModel);
        Assert.True(actual.GameComponentKeysModel.Count() == game.Assets.Vocabulary.Count());
        Assert.True(actual.GameComponentKeysModel.Select(item => item.Code).Distinct().Count() == game.Assets.Vocabulary.Count());
        Assert.True(actual.Data.Length == expectedDataSize);
    }

    [Fact]
    public void VocabularySerializer_Serialize_MaxVocabularies_throwsExcepion()
    {
        // Arrange
        var messageError = string.Format(RetroAdventureCreator.Core.Properties.Resources.MaxNumberVocabularyAllowedError, Constants.MaxNumberVocabularyCommandsAllowed);
        var vocabularies = Enumerable.Range(0, Constants.MaxNumberVocabularyCommandsAllowed + 1).Select(item => new VocabularyModel());

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
        var messageError = string.Format(RetroAdventureCreator.Core.Properties.Resources.MaxSizeOfSynonymsError, Constants.MaxNumberVocabularyCommandsAllowed);
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
