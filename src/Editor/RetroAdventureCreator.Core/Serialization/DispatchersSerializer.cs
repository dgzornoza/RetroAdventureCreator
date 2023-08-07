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
/// Dispatcher model serializer
/// </summary>
/// <remarks>
/// Format Dispatcher serializer:
/// ----------------------------------------------
/// 
/// Data:
/// Trigger = 4 bits (16)
/// InputCommands = InputCommand id bytes (end with 0x00)
/// Commands = Command id bytes (end with 0x00)
/// 
/// </remarks>
internal class DispatchersSerializer : ISerializer<IEnumerable<DispatcherModel>>
{
    public IEnumerable<GameComponentPointerModel> GenerateGameComponentKeys(IEnumerable<DispatcherModel> dispatchers)
    {
        throw new NotImplementedException();
    }

    public SerializerResultModel Serialize(GameComponentsPointers gameComponentsIndexes, IEnumerable<DispatcherModel> dispatchers)
    {
        throw new NotImplementedException();
    }
}
