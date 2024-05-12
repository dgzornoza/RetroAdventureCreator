using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Infrastructure.Builders;

namespace RetroAdventureCreator.Test.Facts.SerializerTests;

public class PlayerSerializerTest : SerializerBaseTest
{
    [Fact]
    public void PlayerSerializerTest_Serialize_AsExpected()
    {
        // Arrange
        CreateGame<GameInPawsTutorialBuilder>();
        var serializerFactory = new SerializerFactory(game);

        var expectedDataBytes = GetPlayerData(game.Player, serializerFactory);

        // Act
        var actual = serializerFactory.Serialize<PlayerSerializer>();

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Data);
        Assert.True(actual.Data.Length == expectedDataBytes.Length);
        Assert.Equal(expectedDataBytes, actual.Data);
    }

    private byte[] GetPlayerData(PlayerModel player, SerializerFactory serializerFactory)
    {
        var result = new List<byte>
        {
            // health + experience points
            (byte)(player.Health << 4 | player.ExperiencePoints),
        };

        if (player.Objects != null && player.Objects.Any())
        {
            result.AddRange(player.Objects.Select(item => serializerFactory.GameComponentsPointersModel.Objects.IndexOf(item.Code)));
        }

        var objectsCount = player.Objects?.Count() ?? 0;
        if (objectsCount < Constants.MaxLengthPlayerObjectsAllowed)
        {
            result.AddRange(Enumerable.Range(0, Constants.MaxLengthPlayerObjectsAllowed - objectsCount).Select(item => (byte)0x00));
        }

        return result.ToArray();
    }
}
