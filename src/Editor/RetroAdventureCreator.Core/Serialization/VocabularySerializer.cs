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
/// Header: (3 bytes)
/// Synonyms = 0-8 bits
/// DataAdress = 2 bytes
/// 
/// Data:
/// Synonyms = 0-255 bytes
/// 
/// </remarks>
internal class VocabularySerializer : ISerializer<IEnumerable<VocabularyModel>, VocabularySerializerResultModel>
{
    private record struct Header(byte SynonymsLenght, short DataAddress);

    private record struct Data(string Synonyms);

    public VocabularySerializerResultModel Serialize(IEnumerable<VocabularyModel> @objects)
    {
        var vocabularies = @objects ?? throw new ArgumentNullException(nameof(@objects));

        EnsureHelpers.EnsureMaxLength(vocabularies, Constants.MaxLengthVocabularyAllowed,
            string.Format(Properties.Resources.MaxLengthVocabularyAllowedError, Constants.MaxLengthVocabularyAllowed));

        var verbs = SerializeVocabularies(vocabularies.Where(item => item.WordType == WordType.Verb));

        EnsureHelpers.EnsureMaxLength(verbs.GameComponentKeysModel.Count(), Constants.MaxLengthVocabularyVerbsAllowed,
            string.Format(Properties.Resources.MaxLengthVocabularyVerbsAllowedError, Constants.MaxLengthVocabularyVerbsAllowed));

        var nouns = SerializeVocabularies(vocabularies.Where(item => item.WordType == WordType.Noun));

        return new VocabularySerializerResultModel(verbs, nouns);
    }

    private SerializerResultKeyModel SerializeVocabularies(IEnumerable<VocabularyModel> vocabularies)
    {
        var componentKeys = new List<GameComponentKeyModel>(vocabularies.Count());

        var headerBytes = new List<byte>();
        var dataBytes = new List<byte>();

        foreach (var vocabulary in vocabularies)
        {
            EnsureHelpers.EnsureNotNullOrEmpty(vocabulary.Synonyms, Properties.Resources.SysnonymsAreRequiredError);
            var synonyms = string.Join('|', vocabulary.Synonyms);

            EnsureHelpers.EnsureNotNullOrWhiteSpace(vocabulary.Code, Properties.Resources.CodeIsRequiredError);
            EnsureHelpers.EnsureNotFound(componentKeys, item => item.Code == vocabulary.Code, string.Format(Properties.Resources.DuplicateCodeError, vocabulary.Code));
            EnsureHelpers.EnsureMaxLength(synonyms.Length, Constants.MaxLengthVocabularySynonymsAllowed,
                string.Format(Properties.Resources.MaxLengthVocabularySynonymsError, Constants.MaxLengthVocabularySynonymsAllowed));

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
        header.SynonymsLenght,
        header.DataAddress.GetByte(2),
        header.DataAddress.GetByte(1),
    };

    private static byte[] CreateDataBytes(Data data) => Encoding.ASCII.GetBytes(data.Synonyms);
}
