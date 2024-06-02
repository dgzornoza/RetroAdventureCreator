using System.Collections;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Helpers;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
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
/// GenerateGameComponentPointers returns a list of pointers with:
/// Code: Flag code
/// RelativePointer: index to the flag bit, relative to the Flags component bytes.
/// </remarks>
internal class FlagsSerializer : Serializer<IEnumerable<FlagModel>>
{
    public FlagsSerializer(IEnumerable<FlagModel> gameComponent) : base(gameComponent)
    {
    }

    private byte TotalBytes => (byte)Math.Ceiling(GameComponent.Count() / 8M);

    public override IEnumerable<GameComponentPointerModel> GenerateGameComponentPointers()
    {
        var result = new List<GameComponentPointerModel>();
        var pointer = 0;

        EnsureGameComponentProperties();        

        foreach (var flag in GameComponent.SortByKey())
        {

            result.Add(new GameComponentPointerModel(flag.Code, pointer));
            pointer += 1;
        }

        return result;
    }

    public override SerializerResultModel Serialize(GameComponentsPointersModel gameComponentsIndexes)
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
            EnsureHelpers.EnsureNotNullOrWhiteSpace(flag.Code, Properties.Resources.CodeIsRequiredError);
            EnsureHelpers.EnsureSingle(GameComponent, item => item.Code == flag.Code, string.Format(Properties.Resources.DuplicateCodeError, flag.Code));
        }
    }
}
