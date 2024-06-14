using System;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Helpers;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

/// <summary>
/// InputCommand model serializer
/// </summary>
/// <remarks>
/// Format InputCommand serializer:
/// ----------------------------------------------
/// 
/// Data:
/// Verbs = 1 byte (index orderer addresses verbs vocabulary)
/// Nouns = 1 byte (index orderer addresses nouns vocabulary)
/// 
/// </remarks>
internal class InputCommandsSerializer : Serializer<IEnumerable<InputCommandModel>>
{
    public InputCommandsSerializer(IEnumerable<InputCommandModel> gameComponent) : base(gameComponent)
    {
        EnsureHelpers.EnsureMaxLength(GameComponent, Constants.MaxLengthInputCommandsAllowed,
            string.Format(Properties.Resources.MaxLengthInputCommandsAllowedError, Constants.MaxLengthInputCommandsAllowed));
    }

    public override IEnumerable<GameComponentPointerModel> GenerateGameComponentPointers()
    {
        var result = new List<GameComponentPointerModel>();
        var pointer = 0;

        foreach (var inputCommand in GameComponent.SortByKey())
        {
            if (pointer > short.MaxValue)
                throw new InvalidOperationException(string.Format(Properties.Resources.MaxPointerExceededError, nameof(CommandsSerializer)));

            EnsureGameComponentProperties(inputCommand, result);

            result.Add(new GameComponentPointerModel(inputCommand.Code, (short)pointer));
            pointer +=
                1 + // Verbs (required)
                (inputCommand.Nouns == null ? 0 : 1); // Nouns (optional)
        }

        // add end pointer
        result.Add(new GameComponentPointerModel(Constants.EndComponentPointerCode, (short)pointer));

        return result;
    }

    public override SerializerResultModel Serialize(GameComponentsPointersModel gameComponentsIndexes)
    {
        var dataBytes = GameComponent.SortByKey().SelectMany(item => CreateDataBytes(item, gameComponentsIndexes));
        return new SerializerResultModel(dataBytes.ToArray());
    }

    private static byte[] CreateDataBytes(InputCommandModel inputCommand, GameComponentsPointersModel gameComponentsIndexes)
    {
        var result = new List<byte>
        {
            // Verbs
            gameComponentsIndexes.VocabularyVerbs.IndexOf(inputCommand.Verbs.Code).ToBaseZero(),
        };

        // Nouns
        if (inputCommand.Nouns != null)
        {
            result.Add(gameComponentsIndexes.VocabularyNouns.IndexOf(inputCommand.Nouns.Code).ToBaseZero());
        }

        return result.ToArray();
    }

    private static void EnsureGameComponentProperties(InputCommandModel inputComand, IEnumerable<GameComponentPointerModel> gameComponentPointers)
    {
        EnsureHelpers.EnsureNotFound(gameComponentPointers, item => item.Code == inputComand.Code, string.Format(Properties.Resources.DuplicateCodeError, inputComand.Code));
        EnsureHelpers.EnsureNotNullOrWhiteSpace(inputComand.Code, Properties.Resources.CodeIsRequiredError);
    }
}
