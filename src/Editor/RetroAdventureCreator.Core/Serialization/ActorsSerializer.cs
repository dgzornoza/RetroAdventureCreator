using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Helpers;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

/// <summary>
/// Actors models serializer
/// </summary>
/// <remarks>
/// Format Actors serializer:
/// ----------------------------------------------
/// 
/// Data (actor data can be modified in game for update properties):
/// Health: 4 bits (15)
/// ExperiencePoints: 4 bits (15)
/// Objects: 8 object id bytes
///  
/// </remarks>
internal class ActorsSerializer : SerializerList<ActorModel>
{
    public ActorsSerializer(IEnumerable<ActorModel> gameComponent) : base(gameComponent)
    {
        EnsureHelpers.EnsureMaxLength(GameComponent, Constants.MaxLengthActorsAllowed,
    string.Format(Properties.Resources.MaxLengthActorsAllowedError, Constants.MaxLengthActorsAllowed));
    }

    private record struct Data(IEnumerable<byte>? ObjectsIndexes);

    public override IEnumerable<GameComponentPointerModel> GenerateGameComponentPointers()
    {
        var result = new List<GameComponentPointerModel>();
        var pointer = 0;

        foreach (var actor in GameComponent)
        {
            if (pointer > short.MaxValue)
                throw new InvalidOperationException(string.Format(Properties.Resources.MaxPointerExceededError, nameof(CommandsSerializer)));

            EnsureGameComponentProperties(actor, result);

            result.Add(new GameComponentPointerModel(actor.Code, (short)pointer));

            //pointer += GetPointerSize();
        }

        // add end objects pointer
        result.Add(new GameComponentPointerModel(Constants.EndComponentPointerCode, (short)pointer));

        return result;
    }

    public override SerializerResultModel Serialize(GameComponentsPointersModel gameComponentsPointers)
    {
        var dataBytes = GameComponent.SelectMany(item => CreateDataBytes(item, gameComponentsPointers));
        return new SerializerResultModel(dataBytes.ToArray());
    }

    private static byte[] CreateDataBytes(ActorModel actor, GameComponentsPointersModel gameComponentsPointers)
    {
        var result = new List<byte>
        {
            // health + experience points
            (byte)(actor.Health << 4 | actor.ExperiencePoints),
        };

        // Objects        
        if (actor.Objects != null && actor.Objects.Any())
        {
            result.AddRange(actor.Objects.Select(item => gameComponentsPointers.Objects.IndexOf(item.Code)));
        }
        var objectsCount = actor.Objects?.Count() ?? 0;
        if (objectsCount < Constants.MaxLengthPlayerObjectsAllowed)
        {
            result.AddRange(Enumerable.Range(0, Constants.MaxLengthPlayerObjectsAllowed - objectsCount).Select(item => (byte)0x00));
        }

        return result.ToArray();
    }

    private void EnsureGameComponentProperties(ActorModel actor, IEnumerable<GameComponentPointerModel> gameComponentPointers)
    {
        EnsureHelpers.EnsureNotFound(gameComponentPointers, item => item.Code == actor.Code, string.Format(Properties.Resources.DuplicateCodeError, actor.Code));

        EnsureHelpers.EnsureMaxLength(actor.Health, Constants.MaxLengthPlayerHealthAllowed,
            string.Format(Properties.Resources.MaxLengthPlayerHealthAllowedError, Constants.MaxLengthPlayerHealthAllowed));

        EnsureHelpers.EnsureMaxLength(actor.ExperiencePoints, Constants.MaxLengthPlayerExperiencePointsAllowed,
            string.Format(Properties.Resources.MaxLengthPlayerExperiencePointsAllowedError, Constants.MaxLengthPlayerExperiencePointsAllowed));

        if (actor.Objects != null)
        {
            EnsureHelpers.EnsureMaxLength(actor.Objects, Constants.MaxLengthPlayerObjectsAllowed,
                string.Format(Properties.Resources.MaxLengthPlayerObjectsAllowedError, Constants.MaxLengthPlayerObjectsAllowed));
        }
    }
}
