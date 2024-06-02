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
/// AfterInputCommandDispatchers = dispatcher id bytes (end with 0x00)
/// BeforeInputCommandDispatchers = dispatcher id bytes (end with 0x00)
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
                (scene.AfterInputCommandDispatchers?.Count() ?? 0) + 1 + // AfterInputCommandDispatchers + EndTokenByte 
                (scene.BeforeInputCommandDispatchers?.Count() ?? 0) + 1 + // BeforeInputCommandDispatchers + EndTokenByte 
                (scene.Objects?.Count() ?? 0) + 1; // objects + EndTokenByte
        }

        return result;
    }

    public override SerializerResultModel Serialize(GameComponentsPointersModel gameComponentsIndexes)
    {
        var dataBytes = GameComponent.SortByKey().SelectMany(item => CreateDataBytes(item, gameComponentsIndexes));
        return new SerializerResultModel(dataBytes.ToArray());
    }

    private static byte[] CreateDataBytes(SceneModel scene, GameComponentsPointersModel gameComponentsIndexes)
    {
        var result = new List<byte>
        {
            // description
            gameComponentsIndexes.Messages.IndexOf(scene.Description.Code),
        };

        // dispatchers
        if (scene.AfterInputCommandDispatchers != null && scene.AfterInputCommandDispatchers.Any())
        {
            result.AddRange(scene.AfterInputCommandDispatchers.Select(item => gameComponentsIndexes.AfterInputCommandDispatchers.IndexOf(item.Code)));            
        }
        result.Add(Constants.EndToken);
        
        if (scene.BeforeInputCommandDispatchers != null && scene.BeforeInputCommandDispatchers.Any())
        {
            result.AddRange(scene.BeforeInputCommandDispatchers.Select(item => gameComponentsIndexes.BeforeInputCommandDispatchers.IndexOf(item.Code)));
        }
        result.Add(Constants.EndToken);

        // objects
        if (scene.Objects != null && scene.Objects.Any())
        {
            result.AddRange(scene.Objects.Select(item => gameComponentsIndexes.Objects.IndexOf(item.Code)));            
        }
        result.Add(Constants.EndToken);

        return result.ToArray();
    }  

    private static void EnsureGameComponentProperties(SceneModel scene, IEnumerable<GameComponentPointerModel> gameComponentPointers)
    {
        EnsureHelpers.EnsureNotFound(gameComponentPointers, item => item.Code == scene.Code, string.Format(Properties.Resources.DuplicateCodeError, scene.Code));
        EnsureHelpers.EnsureNotNullOrWhiteSpace(scene.Code, Properties.Resources.CodeIsRequiredError);
    }
}
