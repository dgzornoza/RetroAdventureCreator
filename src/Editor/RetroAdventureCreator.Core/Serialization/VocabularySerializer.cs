using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

/// <summary>
/// Vocabulary model serializer
/// </summary>
/// <remarks>
/// Format Vocabulary serializer:
/// ----------------------------------------------
/// 
/// Header:
/// WordType = 8 bits (256)
/// Synonyms = 8 bits (256)
/// 
/// Data:
/// Synonyms = 0-255 bytes
/// 
/// </remarks>
internal class VocabularySerializer : ISerializer<IEnumerable<VocabularyModel>, SerializerResultKeyModel>
{
    private record struct Header(byte WordType, byte Synonyms);

    private record struct Data(string Synonyms);

    public SerializerResultKeyModel Serialize(IEnumerable<VocabularyModel> @object)
    {
        var vocabularies = @object ?? throw new ArgumentNullException(nameof(@object));

        if (vocabularies.Count() > Constants.MaxNumberVocabularyCommandsAllowed)
        {
            throw new InvalidOperationException(string.Format(Properties.Resources.MaxNumberVocabularyAllowedError, Constants.MaxNumberVocabularyCommandsAllowed));
        }

        var componentKeys = new List<GameComponentKeyModel>(vocabularies.Count());
        var result = new List<byte>();
        foreach (var vocabulary in vocabularies)
        {
            var synonyms = string.Join('|', vocabulary.Synonyms ?? Enumerable.Empty<string>());

            if (string.IsNullOrEmpty(vocabulary.Code))
            {
                throw new InvalidOperationException(Properties.Resources.CodeIsRequiredError);
            }
            if (componentKeys.Any(item => item.Code == vocabulary.Code))
            {
                throw new InvalidOperationException(string.Format(Properties.Resources.DuplicateCodeError, vocabulary.Code));
            }
            if (synonyms.Length > Constants.MaxNumberVocabularyCommandsAllowed)
            {
                throw new InvalidOperationException(string.Format(Properties.Resources.MaxSizeOfSynonymsError, Constants.MaxNumberVocabularyCommandsAllowed));
            }

            componentKeys.Add(new GameComponentKeyModel(vocabulary.Code, result.Count));

            var header = new Header((byte)vocabulary.WordType, (byte)synonyms.Length);
            result.AddRange(CreateHeaderBytes(header));

            var data = new Data(synonyms);
            result.AddRange(CreateDataBytes(data));
        }

        return new SerializerResultKeyModel(componentKeys, result.ToArray());
    }

    private static byte[] CreateHeaderBytes(Header header) => new byte[]
    {
            header.WordType,
            header.Synonyms,
    };

    private static byte[] CreateDataBytes(Data data) => Encoding.ASCII.GetBytes(data.Synonyms);
}
