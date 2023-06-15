using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

/// <summary>
/// Game model serializer
/// </summary>
/// <remarks>
/// Format Game serializer:
/// ----------------------------------------------
/// 
/// Header:
/// 
/// Data:
/// 
/// </remarks>
internal class GameSerializer : ISerializer
{
    public byte[] Serialize(GameModel game)
    {
        throw new NotImplementedException();
    }
}
