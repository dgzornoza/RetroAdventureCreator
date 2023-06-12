using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization
{
    /// <summary>
    /// Vocabulary model serializer
    /// </summary>
    /// <remarks>
    /// Format Vocabulary serializer:
    /// ----------------------------------------------
    /// 
    /// Header:
    /// Id = 6 bits (64)
    /// WordType = 3 bits (8)
    /// Synonyms = 8 bits (256)
    /// 
    /// Data:
    /// Synonyms = 0-256 bytes
    /// 
    /// </remarks>
    internal class VocabularyModelSerializer : ISerializer<VocabularyModel>
    {
        public byte[] Serialize(VocabularyModel model)
        {
            throw new NotImplementedException();
        }
    }
}
