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
/// Message model serializer
/// </summary>
/// <remarks>
/// Format Message serializer:
/// ----------------------------------------------
/// 
/// Header:
/// 
/// Data:
/// 
/// </remarks>
internal class MessagesSerializer : ISerializer
{
    public SerializerResultModel Serialize(GameModel game)
    {
        throw new NotImplementedException();
    }
}
