using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Helpers;
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
}
