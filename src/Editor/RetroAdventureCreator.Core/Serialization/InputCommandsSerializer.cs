using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
/// Header:
/// Id = 6 bits (64)
/// Verb = 6 bits (64) (id vocabulary)
/// Nouns = 3 bits (8 ids vocabulary)
/// 
/// Data:
/// Nouns = 0-48 bytes
/// 
/// </remarks>
internal class InputCommandsSerializer : ISerializer
{
    public SerializerResultModel Serialize(GameModel game)
    {
        throw new NotImplementedException();
    }
}
