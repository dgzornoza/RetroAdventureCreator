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
        var game = FilesHelpers.GetLocalResourceJsonObject<GameModel>("GameInPawsTutorial.json") ?? throw new InvalidOperationException();

        var primeService = new VocabularySerializer().GetDepthPropertyValuesOfType<VocabularyModel>();

        Assert.NotNull(game);
    }
}
