using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization
{
    /// <summary>
    /// Scene model serializer
    /// </summary>
    /// <remarks>
    /// Format Scene serializer:
    /// 
    /// Header:
    /// Id = 6 bits
    /// Description Size = 10 bits
    /// 
    /// 
    /// 
    /// 
    /// 
    /// Id = 6 bits = 64 scenes
    /// Description = 10 bits (description size) 
    /// 
    /// </remarks>
    internal class SceneModelSerializer : ISerializer<SceneModel>
    {
        public byte[] Serialize(SceneModel model)
        {
            throw new NotImplementedException();
        }
    }
}

public record SceneModel
{
    public string Id { get; init; } = default!;
    public string Description { get; init; } = default!;
    public IDictionary<string, string>? Link { get; init; } = default!;
    public IEnumerable<ObjectModel>? Objects { get; init; } = default!;
}
