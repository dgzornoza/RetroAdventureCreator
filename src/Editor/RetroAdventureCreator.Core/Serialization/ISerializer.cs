using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

internal interface ISerializer<out T> where T : class
{
    T GameComponent {  get; }

    IEnumerable<GameComponentPointerModel> GenerateGameComponentPointers();

    SerializerResultModel Serialize(GameComponentsPointersModel gameComponentsIndexes);
}
