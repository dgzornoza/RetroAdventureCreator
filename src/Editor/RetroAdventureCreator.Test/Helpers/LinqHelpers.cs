using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Infrastructure.Game.Interfaces;

namespace RetroAdventureCreator.Test.Helpers
{
    internal static class LinqHelpers
    {
        public static T Find<T>(this IEnumerable<T> list, string code) where T : IUniqueKey
            => list.First(item => item.Code == code);
    }
}
