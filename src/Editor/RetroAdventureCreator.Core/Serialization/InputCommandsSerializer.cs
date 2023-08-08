using System;
using System.Collections.Generic;
using System.Linq;
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
using static System.Formats.Asn1.AsnWriter;

namespace RetroAdventureCreator.Core.Serialization;

/// <summary>
/// InputCommand model serializer
/// </summary>
/// <remarks>
/// Format InputCommand serializer:
/// ----------------------------------------------
/// 
/// Data:
/// Verb = 8 bits (id verb vocabulary)
/// Nouns = vocabulary id bytes (end with 0x00)
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
            EnsureGameComponentProperties(inputCommand, result);

            result.Add(new GameComponentPointerModel(inputCommand.Code, pointer));
            pointer +=
                1 + // Verb
                (inputCommand.Nouns?.Count() ?? 0) + 1; // Nouns + EndTokenByte
        }

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
            // Verb
            gameComponentsIndexes.VocabularyVerbs.IndexOf(inputCommand.Verb.Code)
        };

        // Nouns
        if (inputCommand.Nouns != null && inputCommand.Nouns.Any())
        {
            result.AddRange(inputCommand.Nouns.Select(item => gameComponentsIndexes.VocabularyNouns.IndexOf(item.Code)));
        }
        result.Add(Constants.EndToken);

        return result.ToArray();
    }

    private static void EnsureGameComponentProperties(InputCommandModel inputComand, IEnumerable<GameComponentPointerModel> gameComponentPointers)
    {
        EnsureHelpers.EnsureNotFound(gameComponentPointers, item => item.Code == inputComand.Code, string.Format(Properties.Resources.DuplicateCodeError, inputComand.Code));
        EnsureHelpers.EnsureNotNullOrWhiteSpace(inputComand.Code, Properties.Resources.CodeIsRequiredError);
    }
}
