using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

/// <summary>
/// Scene model serializer
/// </summary>
/// <remarks>
/// Format Scene serializer:
/// 
/// Header:
/// Id = 6 bits (64)
/// Description Size = 9 bits (512)
/// Dispatcher = 5 bits (32 ids dispatchers)
/// Objects = 3 bits (8)
/// 
/// Data:
/// Description = 0-512 bytes
/// Dispatcher = 0-256 bytes
/// Objects = 0-40 bytes
/// 
/// </remarks>
internal class ScenesSerializer : ISerializer<SceneModel>
{
    public SerializerResultModel Serialize(SceneModel @object)
    {
        throw new NotImplementedException();
    }
}

