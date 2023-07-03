using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Helpers;
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
/// Synonyms = 0-8 bits (256)
/// DataAdress = 2 bytes
/// 
/// Data:
/// Synonyms = 0-255 bytes
/// 
/// </remarks>
internal class VocabularySerializer : ISerializer<IEnumerable<VocabularyModel>, VocabularySerializerResultModel>
{
    private record struct Header(byte Synonyms, short DataAddress);

    private record struct Data(string Synonyms);

    public VocabularySerializerResultModel Serialize(IEnumerable<VocabularyModel> @objects)
    {
        var vocabularies = @objects ?? throw new ArgumentNullException(nameof(@objects));

        EnsureHelpers.EnsureMaxLength(vocabularies, Constants.MaxNumberVocabularyAllowed,
            string.Format(Properties.Resources.MaxNumberVocabularyAllowedError, Constants.MaxNumberVocabularyAllowed));

        var verbs = SerializeVocabularies(vocabularies.Where(item => item.WordType == WordType.Verb));
        var nouns = SerializeVocabularies(vocabularies.Where(item => item.WordType == WordType.Noun));

        return new VocabularySerializerResultModel(verbs, nouns);
    }

    private SerializerResultKeyModel SerializeVocabularies(IEnumerable<VocabularyModel> vocabularies)
    {
        var vocabularySize = vocabularies.Count();
        var componentKeys = new List<GameComponentKeyModel>(vocabularySize);

        var headerBytes = new List<byte>();
        var dataBytes = new List<byte>();

        foreach (var vocabulary in vocabularies)
        {
            var synonyms = string.Join('|', vocabulary.Synonyms ?? Enumerable.Empty<string>());

            EnsureHelpers.EnsureNotNullOrWhiteSpace(vocabulary.Code, Properties.Resources.CodeIsRequiredError);
            EnsureHelpers.EnsureNotFound(componentKeys, item => item.Code == vocabulary.Code, string.Format(Properties.Resources.DuplicateCodeError, vocabulary.Code));
            EnsureHelpers.EnsureMaxLength(synonyms.Length, Constants.MaxSizeOfSynonymsAllowed,
                string.Format(Properties.Resources.MaxSizeOfSynonymsError, Constants.MaxSizeOfSynonymsAllowed));

            componentKeys.Add(new GameComponentKeyModel(vocabulary.Code, componentKeys.Count));

            var header = new Header((byte)synonyms.Length, (short)dataBytes.Count);
            headerBytes.AddRange(CreateHeaderBytes(header));

            var data = new Data(synonyms);
            dataBytes.AddRange(CreateDataBytes(data));
        }

        return new SerializerResultKeyModel(componentKeys, headerBytes.ToArray(), dataBytes.ToArray());
    }


    private static byte[] CreateHeaderBytes(Header header) => new byte[]
    {
        header.Synonyms,
        header.DataAddress.GetByte(2),
        header.DataAddress.GetByte(1),
    };

    private static byte[] CreateDataBytes(Data data) => Encoding.ASCII.GetBytes(data.Synonyms);
}
