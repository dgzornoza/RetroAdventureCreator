using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
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
/// Code = 8 bits (256)
/// WordType = 3 bits (8)
/// Synonyms = 8 bits (256)
/// 
/// Data:
/// Synonyms = 0-256 bytes
/// 
/// </remarks>
internal class VocabularySerializer : ISerializer
{
    private record struct Header(byte Code, byte WordType, byte Synonyms);

    private record struct Data(byte Synonyms);

    public byte[] Serialize(GameModel game)
    {
        return null;
    }

}
