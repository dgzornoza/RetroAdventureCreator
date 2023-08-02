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
/// CommandGroup models serializer
/// </summary>
/// <remarks>
/// Format CommandGroup serializer:
/// ----------------------------------------------
/// 
/// Header commandGroup:
/// LogicalOperator = 2 bit
/// Commands = 5 bits (31 ids 8 bits)
/// DataAdress = 2 bytes
/// 
/// Data:
/// Commands = 0-31
/// 
/// </remarks>
internal class CommandGroupSerializer : Serializer<CommandGroupModel>
{
    public CommandGroupSerializer(GameComponentsIndexes gameComponentsIndexes) : base(gameComponentsIndexes)
    {
    }

    public override SerializerResultModel Serialize(CommandGroupModel @object)
    {
        throw new NotImplementedException();
    }
}
