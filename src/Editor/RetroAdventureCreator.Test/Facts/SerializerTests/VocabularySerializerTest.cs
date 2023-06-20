using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Helpers;

namespace RetroAdventureCreator.Test.Facts.SerializerTests;

public class VocabularySerializerTest
{
    [Fact]
    public void VocabularySerializer_Serialize_AsExpected()
    {
        // Arrange
        var game = FilesHelpers.GetLocalResourceJsonObject<GameModel>("GameInPawsTutorial.json") ?? throw new InvalidOperationException();
        var expectedVocabulary = game.GetDepthPropertyValuesOfType<VocabularyModel>();
        var expectedDataSize = expectedVocabulary.Sum(item => 8 + 8 + item.Synonyms?.Count());

        // Act
        var actual = new VocabularySerializer().Serialize(game);

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Data);
        Assert.NotNull(actual.GameComponentKeysModel);
        Assert.True(actual.GameComponentKeysModel.Count() == expectedVocabulary.Count());
        Assert.Collection(actual.GameComponentKeysModel, item => actual.GameComponentKeysModel.Single(component => item.Code == component.Code));
        Assert.True(actual.Data.Length == expectedDataSize);
    }
}
