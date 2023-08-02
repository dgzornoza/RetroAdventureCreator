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
/// Header:
/// Trigger = 4 bits (16)
/// InputCommands = 5 bits (31 ids input command)
/// Commands = 5 bits (31 ids command)
/// DataAdress = 2 bytes
/// 
/// Data:
/// InputCommands = 0-31 bytes
/// Commands = 0-31 bytes
/// 
/// </remarks>
internal class DispatchersSerializer : Serializer<DispatcherModel>
{
    public DispatchersSerializer(GameComponentsIndexes gameComponentsIndexes) : base(gameComponentsIndexes)
    {
    }

    public override SerializerResultModel Serialize(DispatcherModel @object)
    {
        throw new NotImplementedException();
    }
}
