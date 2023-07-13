using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.ConstrainedExecution;
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
internal abstract class VocabularySerializer : Serializer<IEnumerable<VocabularyModel>>
{
    protected record struct Header(byte SynonymsLenght, short DataAddress);

    protected record struct Data(string Synonyms);

    public VocabularySerializer(GameComponentsIndexes gameComponentsIndexes) : base(gameComponentsIndexes)
    {
    }

    protected SerializerResultModel SerializeVocabularies(IEnumerable<VocabularyModel> vocabularies)
    {
        var headerBytes = new List<byte>();
        var dataBytes = new List<byte>();

        foreach (var vocabulary in vocabularies)
        {
            EnsureHelpers.EnsureNotNullOrEmpty(vocabulary.Synonyms, Properties.Resources.SysnonymsAreRequiredError);
            var synonyms = string.Join('|', vocabulary.Synonyms);

            EnsureHelpers.EnsureNotNullOrWhiteSpace(vocabulary.Code, Properties.Resources.CodeIsRequiredError);
            EnsureHelpers.EnsureMaxLength(synonyms.Length, Constants.MaxLengthVocabularySynonymsAllowed,
                string.Format(Properties.Resources.MaxLengthVocabularySynonymsError, Constants.MaxLengthVocabularySynonymsAllowed));

            var header = new Header((byte)synonyms.Length, (short)dataBytes.Count);
            headerBytes.AddRange(CreateHeaderBytes(header));

            var data = new Data(synonyms);
            dataBytes.AddRange(CreateDataBytes(data));
        }

        return new SerializerResultModel(headerBytes.ToArray(), dataBytes.ToArray());
    }


    protected static byte[] CreateHeaderBytes(Header header) => new byte[]
    {
        header.SynonymsLenght,
        header.DataAddress.GetByte(2),
        header.DataAddress.GetByte(1),
    };

    protected static byte[] CreateDataBytes(Data data) => Encoding.ASCII.GetBytes(data.Synonyms);
}

internal class VocabularyNounsSerializer : VocabularySerializer
{
    public VocabularyNounsSerializer(GameComponentsIndexes gameComponentsIndexes) : base(gameComponentsIndexes)
    {
    }

    public override SerializerResultModel Serialize(IEnumerable<VocabularyModel> vocabularies)
    {
        EnsureHelpers.EnsureMaxLength(gameComponentsIndexes.VocabularyNouns, Constants.MaxLengthVocabularyNounsAllowed,
            string.Format(Properties.Resources.MaxLengthVocabularyNounsAllowedError, Constants.MaxLengthVocabularyNounsAllowed));

        var verbs = SerializeVocabularies(vocabularies.Where(item => item.WordType == WordType.Noun).SortByKey());
        return verbs;
    }
}

internal class VocabularyVerbsSerializer : VocabularySerializer
{
    public VocabularyVerbsSerializer(GameComponentsIndexes gameComponentsIndexes) : base(gameComponentsIndexes)
    {
    }

    public override SerializerResultModel Serialize(IEnumerable<VocabularyModel> vocabularies)
    {
        EnsureHelpers.EnsureMaxLength(gameComponentsIndexes.VocabularyVerbs, Constants.MaxLengthVocabularyVerbsAllowed,
            string.Format(Properties.Resources.MaxLengthVocabularyVerbsAllowedError, Constants.MaxLengthVocabularyVerbsAllowed));

        var nouns = SerializeVocabularies(vocabularies.Where(item => item.WordType == WordType.Verb).SortByKey());
        return nouns;
    }
}
