using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

/// <summary>
/// Object model serializer
/// </summary>
/// <remarks>
/// Format Object serializer:
/// ----------------------------------------------
/// 
/// Header:
/// Id = 5 bits (32)
/// Name = 6 bits (64 id vocabulary)
/// Description Size = 9 bits (512)
/// Weight = 5 bits (32)
/// Health = 3 bits (8)
/// Properties = 8 bits (flag 8 properties)
/// ChildObjects = 4 bits (16 ids Objects)
/// RequiredComplements = 3 bits (8 ids Objects)
/// Complements = 3 bits (8 ids Objects)
/// 
/// Data:
/// Description = 0-512 bytes
/// ChildObjects = 0-80 bytes
/// RequiredComplements = 0-40 bytes
/// Complements = 0-40 bytes
/// 
/// </remarks>
internal class ObjectModelSerializer : ISerializer<ObjectModel>
{
    public byte[] Serialize(ObjectModel model)
    {
        throw new NotImplementedException();
    }
}
