using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Extensions;
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
/// Synonyms = 0-256 bytes
/// 
/// </remarks>
internal class VocabularySerializer : ISerializer<IEnumerable<VocabularyModel>>
{
    private record struct Header(byte WordType, byte Synonyms);

    private record struct Data(string Synonyms);

    public SerializerResultModel Serialize(IEnumerable<VocabularyModel> @object)
    {
        var vocabularies = @object ?? throw new ArgumentNullException(nameof(@object));

        if (vocabularies.Count() > byte.MaxValue)
        {
            throw new InvalidOperationException(string.Format(Properties.Resources.MaxNumberVocabularyAllowedError, byte.MaxValue));
        }

        var componentKeys = new List<GameComponentKeyModel>(vocabularies.Count());
        var result = new List<byte>();
        foreach (var vocabulary in vocabularies)
        {
            if (componentKeys.Any(item => item.Code == vocabulary.Code))
            {
                throw new InvalidOperationException(string.Format(Properties.Resources.DuplicateCodeError, vocabulary.Code));
            }

            componentKeys.Add(new GameComponentKeyModel(vocabulary.Code, result.Count));

            var synonyms = string.Join('|', vocabulary.Synonyms ?? Enumerable.Empty<string>());
            if (synonyms.Length > byte.MaxValue)
            {
                throw new InvalidOperationException(string.Format(Properties.Resources.MaxSizeOfSynonyms, byte.MaxValue));
            }

            var header = new Header((byte)vocabulary.WordType, (byte)synonyms.Length);
            result.AddRange(CreateHeaderBytes(header));

            var data = new Data(synonyms);
            result.AddRange(CreateDataBytes(data));
        }

        return new SerializerResultModel(componentKeys, result.ToArray());
    }

    private byte[] CreateHeaderBytes(Header header) => new byte[]
    {
            header.WordType,
            header.Synonyms
    };

    private byte[] CreateDataBytes(Data data) => Encoding.ASCII.GetBytes(data.Synonyms);
}
