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
/// Player model serializer
/// </summary>
/// <remarks>
/// Format Player serializer:
/// ----------------------------------------------
/// 
/// Header:
/// 
/// Data:
/// 
/// </remarks>
internal class PlayerSerializer : ISerializer<PlayerModel>
{
    public SerializerResultModel Serialize(PlayerModel @object)
    {
        throw new NotImplementedException();
    }
}
