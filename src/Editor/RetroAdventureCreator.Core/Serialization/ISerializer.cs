using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;


internal interface ISerializer<in T, out TResult> where T : class where TResult : class
{
    TResult Serialize(GameComponentsIndexes gameComponentsIndexes, T @object);
}
