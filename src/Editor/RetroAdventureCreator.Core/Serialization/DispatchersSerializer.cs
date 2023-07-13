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
/// Id = 8 bits (256)
/// Trigger = 6 bits (64)
/// InputCommand = 6 bits (64 ids input command)
/// Commands = 3 bits (8)
/// 
/// Data:
/// Commands = 0-63 bytes
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
