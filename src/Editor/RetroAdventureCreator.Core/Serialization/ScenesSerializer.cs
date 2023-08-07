using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Helpers;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

/// <summary>
/// Scene model serializer
/// </summary>
/// <remarks>
/// Format Scene serializer:
/// 
/// Data:
/// Description = message id byte
/// Dispatcher = dispatcher id bytes (end with 0x00)
/// Objects = object id bytes (end with 0x00)
/// 
/// </remarks>
internal class ScenesSerializer : Serializer<IEnumerable<SceneModel>>
{
    public ScenesSerializer(IEnumerable<SceneModel> gameComponent) : base(gameComponent)
    {
        EnsureHelpers.EnsureMaxLength(GameComponent, Constants.MaxLengthScenesAllowed,
            string.Format(Properties.Resources.MaxLengthScenesAllowedError, Constants.MaxLengthScenesAllowed));
    }

    public override IEnumerable<GameComponentPointerModel> GenerateGameComponentPointers()
    {
        var result = new List<GameComponentPointerModel>();
        var pointer = 0;

        foreach (var scene in GameComponent.SortByKey())
        {
            EnsureGameComponentProperties(scene, result);

            result.Add(new GameComponentPointerModel(scene.Code, pointer));

            pointer +=
                1 + // description
                (scene.Dispatchers?.Count() ?? 0) + 1 + // dispatcher + EndTokenByte 
                (scene.Objects?.Count() ?? 0) + 1; // objects + EndTokenByte
        }

        return result;
    }

    public override SerializerResultModel Serialize(GameComponentsPointers gameComponentsIndexes)
    {
        var dataBytes = GameComponent.SortByKey().SelectMany(item => CreateDataBytes(item, gameComponentsIndexes));
        return new SerializerResultModel(dataBytes.ToArray());
    }

    private static byte[] CreateDataBytes(SceneModel scene, GameComponentsPointers gameComponentsIndexes)
    {
        var result = new List<byte>
        {
            // description
            gameComponentsIndexes.Messages.IndexOf(scene.Description.Code),
        };

        // dispatcher
        if (scene.Dispatchers != null && scene.Dispatchers.Any())
        {
            result.AddRange(scene.Dispatchers.Select(item => gameComponentsIndexes.Dispatchers.IndexOf(item.Code)));            
        }
        result.Add(EndToken);

        // objects
        if (scene.Objects != null && scene.Objects.Any())
        {
            result.AddRange(scene.Objects.Select(item => gameComponentsIndexes.Objects.IndexOf(item.Code)));            
        }
        result.Add(EndToken);

        return result.ToArray();
    }  

    private static void EnsureGameComponentProperties(SceneModel scene, IEnumerable<GameComponentPointerModel> gameComponentPointers)
    {
        EnsureHelpers.EnsureNotFound(gameComponentPointers, item => item.Code == scene.Code, string.Format(Properties.Resources.DuplicateCodeError, scene.Code));
        EnsureHelpers.EnsureNotNullOrWhiteSpace(scene.Code, Properties.Resources.CodeIsRequiredError);
    }
}
