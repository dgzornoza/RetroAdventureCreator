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
/// Header:

/// 
/// Data:

/// 
/// </remarks>
internal class AssetsSerializer : Serializer<AssetsModel>
{
    public AssetsSerializer(GameComponentsIndexes gameComponentsIndexes) : base(gameComponentsIndexes)
    {
    }

    public override SerializerResultModel Serialize(AssetsModel @object)
    {
        throw new NotImplementedException();
    }
}

