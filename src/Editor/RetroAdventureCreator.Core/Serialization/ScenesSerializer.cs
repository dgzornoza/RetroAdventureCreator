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
/// Description Size = 2 bits (3 ids messages in data)
/// Dispatcher = 5 bits (32 ids dispatchers)
/// Objects = 3 bits (8)
/// DataAdress = 2 bytes
/// 
/// Data:
/// Description = 0-512 bytes
/// Dispatcher = 0-256 bytes
/// Objects = 0-40 bytes
/// 
/// </remarks>
internal class ScenesSerializer : Serializer<SceneModel>
{
    public ScenesSerializer(GameComponentsIndexes gameComponentsIndexes) : base(gameComponentsIndexes)
    {
    }

    public override SerializerResultModel Serialize(SceneModel @object)
    {
        throw new NotImplementedException();
    }
}
