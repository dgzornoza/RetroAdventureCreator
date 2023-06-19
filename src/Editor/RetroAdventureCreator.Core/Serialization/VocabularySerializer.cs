using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
/// Id = 8 bits (256)
/// WordType = 3 bits (8)
/// Synonyms = 8 bits (256)
/// 
/// Data:
/// Synonyms = 0-256 bytes
/// 
/// </remarks>
internal class VocabularySerializer : ISerializer
{
    private record struct Header(byte index, byte WordType, byte Synonyms);

    private record struct Data(string Synonyms);

    public byte[] Serialize(GameModel game)
    {
        var vocabularies = game.GetDepthPropertyValuesOfType<VocabularyModel>();

        if (vocabularies.Count() > byte.MaxValue)
        {
            throw new InvalidOperationException(string.Format(Properties.Resources.MaxNumberVocabularyAllowedError, byte.MaxValue));
        }

        byte index = 0;
        var componentKeys = new List<GameComponentKeyModel>(vocabularies.Count());
        foreach (var vocabulary in vocabularies)
        {
            componentKeys.Add(new GameComponentKeyModel(index, vocabulary.Code));

            var synonyms = string.Join('|', vocabulary.Synonyms ?? Enumerable.Empty<string>());
            if (synonyms.Length > byte.MaxValue)
            {
                throw new InvalidOperationException(string.Format(Properties.Resources.MaxSizeOfSynonyms, byte.MaxValue));
            }

            var header = new Header(index, (byte)vocabulary.WordType, (byte)synonyms.Length);
            var data = new Data(synonyms);

            index++;
        }



        return new SerializerResultModel(componentKeys, );
    }


    private byte[] CreateHeaderBytes(Header header)
    {
        var bits = new BitArray(19, false);

        var intBits = 0b_00000000_00000000_00000000_00000000;
        var idMask = 0b_00000000_111_11111111;
        var idBits = new BitArray(header.index);
        
        bits.Xor()
    }

    private byte[] CreateDataBytes(Data data)
    {

    }
}
