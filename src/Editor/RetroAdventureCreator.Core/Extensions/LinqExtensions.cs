using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Helpers;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Infrastructure.Game.Interfaces;

namespace RetroAdventureCreator.Core.Extensions
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Function for get index (based in 1) of item in list by code
        /// </summary>
        /// <typeparam name="T">type of list elmeent</typeparam>
        /// <param name="list">list of elements</param>
        /// <param name="code">code to find</param>
        /// <returns></returns>
        public static byte IndexOf<T>(this IEnumerable<T> list, string code) where T : IUniqueKey
        {
            EnsureHelpers.EnsureMaxLength(list, Constants.MaxLengthIds, string.Format(Properties.Resources.MaxLengthIdsError, Constants.MaxLengthIds));
            return (byte)(list.Select((item, index) => (item.Code, index)).First(item => item.Code == code).index + 1);
        }
        
        public static T Find<T>(this IEnumerable<T> list, string code) where T : IUniqueKey
            => list.First(item => item.Code == code);

        public static IEnumerable<T> SortByKey<T>(this IEnumerable<T> source) where T : IUniqueKey
            => source.OrderBy(item => item.Code);
    }
}
