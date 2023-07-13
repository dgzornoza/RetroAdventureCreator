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
/// LogicalOperator = 1 bit
/// Commands = 3 bits (7 ids 8 bits)
/// 
/// Data:
/// Commands = 0-56
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
