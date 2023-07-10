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
/// Header: --
/// 
/// Data:
/// Flags = 1 bit per flag
/// 
/// </remarks>
internal class FlagsSerializer : ISerializer<IEnumerable<FlagModel>, SerializerResultKeyModel>
{
    public SerializerResultKeyModel Serialize(GameComponentsIndexes gameComponentsIndexes, IEnumerable<FlagModel> flags)
    {
        throw new NotImplementedException();
    }
}
