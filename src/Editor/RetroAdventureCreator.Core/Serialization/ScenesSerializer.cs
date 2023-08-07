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
/// Data:
/// Description = message id bytes (end with 0x00)
/// Dispatcher = dispatcher id bytes (end with 0x00)
/// Objects = object id bytes (end with 0x00)
/// 
/// </remarks>
internal class ScenesSerializer : Serializer<IEnumerable<SceneModel>>
{
    public ScenesSerializer(IEnumerable<SceneModel> gameComponent) : base(gameComponent)
    {
    }

    public override IEnumerable<GameComponentPointerModel> GenerateGameComponentPointers()
    {
        throw new NotImplementedException();
    }

    public override SerializerResultModel Serialize(GameComponentsPointers gameComponentsIndexes)
    {
        throw new NotImplementedException();
    }
}
