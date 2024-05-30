using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Helpers;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;
using static System.Formats.Asn1.AsnWriter;

namespace RetroAdventureCreator.Core.Serialization;

/// <summary>
/// Dispatcher model serializer
/// </summary>
/// <remarks>
/// Format Dispatcher serializer:
/// ----------------------------------------------
/// 
/// Data:
/// Commands = Command id bytes (end with 0x00)
/// InputCommands = InputCommand id bytes (end with 0x00) (only in AfterInputCommandDispatchers)
/// 
/// </remarks>
internal class DispatchersSerializer : Serializer<IEnumerable<DispatcherModel>>
{
    public DispatchersSerializer(IEnumerable<DispatcherModel> gameComponent) : base(gameComponent)
    {
    }

    public override IEnumerable<GameComponentPointerModel> GenerateGameComponentPointers()
    {
        var result = new List<GameComponentPointerModel>();
        var pointer = 0;

        foreach (var dispatcher in GameComponent)
        {
            EnsureGameComponentProperties(dispatcher, result);

            result.Add(new GameComponentPointerModel(dispatcher.Code, pointer));

            pointer +=
                (dispatcher.Commands?.Count() ?? 0) + 1 + // Commands + EndTokenByte 
                (dispatcher.Trigger != Trigger.AfterInputCommand ? 0 : (dispatcher.InputCommands?.Count() ?? 0) + 1); // InputCommands + EndTokenByte
        }

        return result;
    }

    public override SerializerResultModel Serialize(GameComponentsPointersModel gameComponentsIndexes)
    {
        var dataBytes = GameComponent.SortByKey().SelectMany(item => CreateDataBytes(item, gameComponentsIndexes));
        return new SerializerResultModel(dataBytes.ToArray());
    }

    protected static byte[] CreateDataBytes(DispatcherModel dispatcher, GameComponentsPointersModel gameComponentsIndexes)
    {
        var result = new List<byte>();

        // Commands
        if (dispatcher.Commands != null && dispatcher.Commands.Any())
        {
            result.AddRange(GetCommandsIndexes(dispatcher, gameComponentsIndexes));
        }
        result.Add(Constants.EndToken);

        // after input commands
        if (dispatcher.Trigger == Trigger.AfterInputCommand)
        {
            if (dispatcher.InputCommands != null && dispatcher.InputCommands.Any())
            {
                result.AddRange(dispatcher.InputCommands.Select(item => gameComponentsIndexes.InputCommands.IndexOf(item.Code)));
            }
            result.Add(Constants.EndToken);
        }

        return result.ToArray();
    }

    private static IEnumerable<byte> GetCommandsIndexes(DispatcherModel dispatcher, GameComponentsPointersModel gameComponentsIndexes) =>
        dispatcher.Commands.Select(item => gameComponentsIndexes.Commands.IndexOf(item.Code));

    protected static void EnsureGameComponentProperties(DispatcherModel dispatcher, IEnumerable<GameComponentPointerModel> gameComponentPointers)
    {
        EnsureHelpers.EnsureNotFound(gameComponentPointers, item => item.Code == dispatcher.Code, string.Format(Properties.Resources.DuplicateCodeError, dispatcher.Code));
        EnsureHelpers.EnsureNotNullOrWhiteSpace(dispatcher.Code, Properties.Resources.CodeIsRequiredError);
    }
}

internal class AfterInputCommandDispatchersSerializer : DispatchersSerializer
{
    public AfterInputCommandDispatchersSerializer(IEnumerable<DispatcherModel> gameComponent) :
        base(gameComponent.Where(item => item.Trigger == Trigger.AfterInputCommand))
    {
        EnsureHelpers.EnsureMaxLength(GameComponent, Constants.MaxLengthAfterInputCommandDispatchersAllowed,
            string.Format(Properties.Resources.MaxLengthAfterInputCommandDispatchersAllowedError, Constants.MaxLengthAfterInputCommandDispatchersAllowed));
    }
}

internal class BeforeInputCommandDispatchersSerializer : DispatchersSerializer
{
    public BeforeInputCommandDispatchersSerializer(IEnumerable<DispatcherModel> gameComponent) :
        base(gameComponent.Where(item => item.Trigger == Trigger.BeforeInputCommand))
    {
        EnsureHelpers.EnsureMaxLength(GameComponent, Constants.MaxLengthBeforeInputCommandDispatchersAllowed,
            string.Format(Properties.Resources.MaxLengthBeforeInputCommandDispatchersAllowedError, Constants.MaxLengthBeforeInputCommandDispatchersAllowed));
    }
}
