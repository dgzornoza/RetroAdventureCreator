using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Helpers;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

/// <summary>
/// Flags model serializer
/// </summary>
/// <remarks>
/// Format Flags serializer:
/// ----------------------------------------------
/// 
/// Data:
/// Flags = 1 bit per flag
/// 
/// </remarks>
internal class FlagsSerializer : Serializer<IEnumerable<FlagModel>>
{
    public FlagsSerializer(IEnumerable<FlagModel> gameComponent) : base(gameComponent)
    {
        EnsureGameComponentProperties();
    }

    private byte TotalBytes => (byte)Math.Ceiling(GameComponent.Count() / 8M);

    public override IEnumerable<GameComponentPointerModel> GenerateGameComponentPointers() => Enumerable.Empty<GameComponentPointerModel>();

    public override SerializerResultModel Serialize(GameComponentsPointers gameComponentsIndexes)
    {
        var sortedFlags = GameComponent.SortByKey();
        var dataBytes = CreateDataBytes(sortedFlags);
        return new SerializerResultModel(dataBytes.ToArray());
    }

    private byte[] CreateDataBytes(IEnumerable<FlagModel> flags)
    {        
        var bits = new BitArray(flags.Select(item => item.Value).ToArray());
        var result = new byte[TotalBytes];
        bits.CopyTo(result, 0);

        return result;
    }    

    private void EnsureGameComponentProperties()
    {
        EnsureHelpers.EnsureMaxLength(GameComponent, Constants.MaxLengthFlagsAllowed,
            string.Format(Properties.Resources.MaxLengthFlagsAllowedError, Constants.MaxLengthFlagsAllowed));

        foreach (var flag in GameComponent)
        {
            EnsureHelpers.EnsureSingle(GameComponent, item => item.Code == flag.Code, string.Format(Properties.Resources.DuplicateCodeError, flag.Code));
            EnsureHelpers.EnsureNotNullOrWhiteSpace(flag.Code, Properties.Resources.CodeIsRequiredError);
        }
    }
}
