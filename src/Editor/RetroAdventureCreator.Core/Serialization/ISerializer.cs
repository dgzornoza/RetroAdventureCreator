using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;


internal interface ISerializer<T, TResult> where T : class where TResult : SerializerResultModel
{
    TResult Serialize(T @object);
}
