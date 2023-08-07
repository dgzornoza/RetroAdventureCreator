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
/// Data:
/// LogicalOperator = 2 bit
/// Commands = Command id bytes (end with 0x00)
/// 
/// </remarks>
internal class CommandGroupSerializer : ISerializer<IEnumerable<CommandGroupModel>>
{
    public IEnumerable<GameComponentPointerModel> GenerateGameComponentKeys(IEnumerable<CommandGroupModel> commandGroups)
    {
        throw new NotImplementedException();
    }

    public SerializerResultModel Serialize(GameComponentsPointers gameComponentsIndexes, IEnumerable<CommandGroupModel> commandGroups)
    {
        throw new NotImplementedException();
    }
}
