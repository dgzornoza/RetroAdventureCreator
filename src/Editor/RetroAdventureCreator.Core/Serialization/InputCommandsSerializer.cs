using System;
using System.Collections.Generic;
using System.Linq;
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
/// InputCommand model serializer
/// </summary>
/// <remarks>
/// Format InputCommand serializer:
/// ----------------------------------------------
/// 
/// Data:
/// Verb = 6 bits (id verb vocabulary)
/// Nouns = vocabulary id bytes (end with 0x00)
/// 
/// </remarks>
internal class InputCommandsSerializer : ISerializer<IEnumerable<InputCommandModel>>
{
    private record struct Header(byte VerbIndex, byte Nouns, short DataAddress);

    private record struct Data(IEnumerable<byte>? NounsIndexes);

    public IEnumerable<GameComponentPointerModel> GenerateGameComponentKeys(IEnumerable<InputCommandModel> inputCommands)
    {
        throw new NotImplementedException();
    }

    public SerializerResultModel Serialize(GameComponentsPointers gameComponentsIndexes, IEnumerable<InputCommandModel> inputCommands)
    {
        EnsureHelpers.EnsureMaxLength(inputCommands, Constants.MaxLengthInputCommandsAllowed,
            string.Format(Properties.Resources.MaxLengthInputCommandsAllowedError, Constants.MaxLengthInputCommandsAllowed));

        var headerBytes = new List<byte>();
        var dataBytes = new List<byte>();

        foreach (var inputCommand in inputCommands.SortByKey())
        {
            EnsureHelpers.EnsureNotNullOrWhiteSpace(inputCommand.Code, Properties.Resources.CodeIsRequiredError);
            EnsureHelpers.EnsureNotNull(inputCommand.Verb, Properties.Resources.InputCommandVerbIsRequired);
            if (inputCommand.Nouns != null)
            {
                EnsureHelpers.EnsureMaxLength(inputCommand.Nouns.Count(), Constants.MaxLengthInputCommandsNounsAllowed,
                    string.Format(Properties.Resources.MaxLengthInputCommandsNounsAllowedError, Constants.MaxLengthInputCommandsNounsAllowed));
            }

            var header = new Header
            {
                VerbIndex = (byte)gameComponentsIndexes.VocabularyVerbs.Find(inputCommand.Verb.Code).RelativePointer,
                Nouns = (byte)(inputCommand.Nouns?.Count() ?? 0),
                DataAddress = (short)dataBytes.Count,
            };
            headerBytes.AddRange(CreateHeaderBytes(header));

            var nounsIndexes = inputCommand.Nouns?.Select(item => (byte)gameComponentsIndexes.VocabularyNouns.Find(item.Code).RelativePointer);
            var data = new Data(nounsIndexes);
            dataBytes.AddRange(CreateDataBytes(data));
        }

        return new SerializerResultModel(headerBytes.ToArray(), dataBytes.ToArray());
    }

    private static byte[] CreateHeaderBytes(Header header) => new byte[]
    {
        (byte)(header.VerbIndex << 2 | header.Nouns),
        header.DataAddress.GetByte(2),
        header.DataAddress.GetByte(1),
    };

    private static byte[] CreateDataBytes(Data data) => data.NounsIndexes?.ToArray() ?? Array.Empty<byte>();
}
