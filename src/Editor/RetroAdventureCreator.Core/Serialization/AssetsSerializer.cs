using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

/// <summary>
/// Assets model serializer
/// </summary>
/// <remarks>
/// Format Assets serializer:
/// 
/// Data:
///
/// Scenes
/// Vocabulary
/// Messages
/// Objects
/// InputCommands
/// Commands
/// CommandsGroups
/// Dispatchers
/// 
/// </remarks>
internal class AssetsSerializer : ISerializer<AssetsModel>
{
    public IEnumerable<GameComponentPointerModel> GenerateGameComponentKeys(AssetsModel assets)
    {
        throw new NotImplementedException();
    }

    public SerializerResultModel Serialize(GameComponentsPointers gameComponentsIndexes, AssetsModel @object)
    {
        throw new NotImplementedException();
    }
}
