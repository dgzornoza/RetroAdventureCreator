using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Helpers;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

/// <summary>
/// Player model serializer
/// </summary>
/// <remarks>
/// Format Player serializer:
/// ----------------------------------------------
/// 
/// Data (player data ban be modified in game for update properties):
/// Health: 4 bits (15)
/// ExperiencePoints: 4 bits (15)
/// Objects: 8 object id bytes
///  
/// </remarks>
internal class PlayerSerializer : Serializer<PlayerModel>
{
    public PlayerSerializer(PlayerModel gameComponent) : base(gameComponent)
    {
        EnsureGameComponentProperties();
    }

    private record struct Data(IEnumerable<byte>? ObjectsIndexes);

    public override IEnumerable<GameComponentPointerModel> GenerateGameComponentPointers() => Enumerable.Empty<GameComponentPointerModel>();

    public override SerializerResultModel Serialize(GameComponentsPointersModel gameComponentsIndexes)
    {
        var dataBytes = CreateDataBytes(GameComponent, gameComponentsIndexes);
        return new SerializerResultModel(dataBytes.ToArray());
    }

    private static byte[] CreateDataBytes(PlayerModel player, GameComponentsPointersModel gameComponentsIndexes)
    {
        var result = new List<byte>
        {
            // health + experience points
            (byte)(player.Health << 4 | player.ExperiencePoints),
        };

        // Objects        
        if (player.Objects != null && player.Objects.Any())
        {
            result.AddRange(player.Objects.Select(item => gameComponentsIndexes.Objects.IndexOf(item.Code)));
        }
        var objectsCount = player.Objects?.Count() ?? 0;
        if (objectsCount < Constants.MaxLengthPlayerObjectsAllowed)
        {
            result.AddRange(Enumerable.Range(0, Constants.MaxLengthPlayerObjectsAllowed - objectsCount).Select(item => (byte)0x00));
        }

        return result.ToArray();
    }

    private void EnsureGameComponentProperties()
    {
        EnsureHelpers.EnsureMaxLength(GameComponent.Health, Constants.MaxLengthPlayerHealthAllowed,
            string.Format(Properties.Resources.MaxLengthPlayerHealthAllowedError, Constants.MaxLengthPlayerHealthAllowed));

        EnsureHelpers.EnsureMaxLength(GameComponent.ExperiencePoints, Constants.MaxLengthPlayerExperiencePointsAllowed,
            string.Format(Properties.Resources.MaxLengthPlayerExperiencePointsAllowedError, Constants.MaxLengthPlayerExperiencePointsAllowed));

        if (GameComponent.Objects != null)
        {
            EnsureHelpers.EnsureMaxLength(GameComponent.Objects, Constants.MaxLengthPlayerObjectsAllowed,
                string.Format(Properties.Resources.MaxLengthPlayerObjectsAllowedError, Constants.MaxLengthPlayerObjectsAllowed));
        }
    }
}
