using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Helpers;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

/// <summary>
/// Actors models serializer
/// </summary>
/// <remarks>
/// Format Actors serializer:
/// ----------------------------------------------
/// 
/// Data:
/// 
/// Health: 1 byte  (mutable data)
/// ExperiencePoints: 1 byte  (mutable data)
/// ActorType: 1 byte
///  
/// </remarks>
internal class ActorsSerializer : SerializerList<ActorModel>
{
    public ActorsSerializer(IEnumerable<ActorModel> gameComponent) : base(gameComponent)
    {
        EnsureHelpers.EnsureMaxLength(GameComponent, Constants.MaxLengthActorsAllowed,
            string.Format(Properties.Resources.MaxLengthActorsAllowedError, Constants.MaxLengthActorsAllowed));

        EnsureHelpers.EnsureSingle(GameComponent, item => item.ActorType == ActorType.Player, Properties.Resources.DuplicatedPlayerTypeError);
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

            pointer +=
                1 + // Health
                1 + // ExperiencePoints
                1;  // ActorType
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
            actor.Health,
            actor.ExperiencePoints,
            (byte)actor.ActorType,
        };

        return result.ToArray();
    }

    private void EnsureGameComponentProperties(ActorModel actor, IEnumerable<GameComponentPointerModel> gameComponentPointers)
    {
        EnsureHelpers.EnsureNotFound(gameComponentPointers, item => item.Code == actor.Code, string.Format(Properties.Resources.DuplicateCodeError, actor.Code));

        EnsureHelpers.EnsureMaxLength(actor.Health, Constants.MaxLengthActorHealthAllowed,
            string.Format(Properties.Resources.MaxLengthPlayerHealthAllowedError, Constants.MaxLengthActorHealthAllowed));

        EnsureHelpers.EnsureMaxLength(actor.ExperiencePoints, Constants.MaxLengthctorExperiencePointsAllowed,
            string.Format(Properties.Resources.MaxLengthPlayerExperiencePointsAllowedError, Constants.MaxLengthctorExperiencePointsAllowed));
    }
}
