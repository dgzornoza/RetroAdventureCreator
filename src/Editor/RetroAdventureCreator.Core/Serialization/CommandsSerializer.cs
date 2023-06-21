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
/// Command model serializer
/// </summary>
/// <remarks>
/// Format Command serializer:
/// ----------------------------------------------
/// 
/// Header:
/// Id = 8 bits (256)
/// LogicalOperator = 1 bit
/// Token = 6 bits (64)
/// Arguments = 2 bits (4 ids max value)
/// 
/// Data:
/// Arguments = 0-32
/// 
/// </remarks>
internal class CommandsSerializer : ISerializer<CommandModel>
{
    public SerializerResultModel Serialize(CommandModel @object)
    {
        throw new NotImplementedException();
    }
}
