using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Infrastructure.Game.Interfaces;

namespace RetroAdventureCreator.Core.Extensions
{
    public static class LinqExtensions
    {
        public static T Find<T>(this IEnumerable<T> list, string code) where T : IUniqueKey
            => list.First(item => item.Code == code);

        public static IEnumerable<T> SortByKey<T>(this IEnumerable<T> source) where T : IUniqueKey
            => source.OrderBy(item => item.Code);
    }
}
