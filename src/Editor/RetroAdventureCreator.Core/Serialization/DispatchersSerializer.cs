using System.Collections.Generic;
using System.Reflection;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Helpers;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

/// <summary>
/// Dispatcher model serializer
/// </summary>
/// <remarks>
/// Format Dispatcher serializer:
/// ----------------------------------------------
/// 
/// Data:
/// InputCommands = InputCommand pointers (2 bytes each one) (only in AfterInputCommandDispatchers)
/// Commands = Commands pointers (2 bytes each one)
/// 
/// </remarks>
internal abstract class DispatchersSerializer : SerializerList<DispatcherModel>
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
            if (pointer > short.MaxValue)
                throw new InvalidOperationException(string.Format(Properties.Resources.MaxPointerExceededError, nameof(CommandsSerializer)));

            EnsureGameComponentProperties(dispatcher, result);

            result.Add(new GameComponentPointerModel(dispatcher.Code, (short)pointer));

            pointer +=
                // InputCommands (only AfterInputCommand, 2 bytes each one)
                (dispatcher.Trigger != Trigger.AfterInputCommand ?
                    0 :
                    (dispatcher.InputCommands?.Count() * 2 ?? 0)) +
                // Commands (2 bytes each one)
                (dispatcher.Commands?.Count() * 2 ?? 0);

        }

        // add end pointer
        result.Add(new GameComponentPointerModel(Constants.EndComponentPointerCode, (short)pointer));

        return result;
    }

    public override SerializerResultModel Serialize(GameComponentsPointersModel gameComponentsPointers)
    {
        var dataBytes = GameComponent.SelectMany(item => CreateDataBytes(item, gameComponentsPointers));
        return new SerializerResultModel(dataBytes.ToArray());
    }

    protected static byte[] CreateDataBytes(DispatcherModel dispatcher, GameComponentsPointersModel gameComponentsPointers)
    {
        var result = new List<byte>();

        // after input commands
        if (dispatcher.Trigger == Trigger.AfterInputCommand && dispatcher.InputCommands != null && dispatcher.InputCommands.Any())
        {
            result.AddRange(GetInputCommandsPointers(dispatcher, gameComponentsPointers));
        }

        // Commands
        if (dispatcher.Commands != null && dispatcher.Commands.Any())
        {
            result.AddRange(GetCommandsPointers(dispatcher, gameComponentsPointers));
        }

        return result.ToArray();
    }

    private static IEnumerable<byte> GetInputCommandsPointers(DispatcherModel dispatcher, GameComponentsPointersModel gameComponentPointers) =>
        dispatcher.InputCommands?.SelectMany(item =>
            gameComponentPointers.InputCommands.Find(item.Code).RelativePointer.GetBytes()) ?? Enumerable.Empty<byte>();

    private static IEnumerable<byte> GetCommandsPointers(DispatcherModel dispatcher, GameComponentsPointersModel gameComponentPointers) =>
        dispatcher.Commands.SelectMany(item =>
            gameComponentPointers.Commands.Find(item.Code).RelativePointer.GetBytes());

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
