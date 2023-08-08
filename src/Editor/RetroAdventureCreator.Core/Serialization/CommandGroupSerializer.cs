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

namespace RetroAdventureCreator.Core.Serialization;

/// <summary>
/// CommandGroup models serializer
/// </summary>
/// <remarks>
/// Format CommandGroup serializer:
/// ----------------------------------------------
/// 
/// Data:
/// 1 bit = 1 (CommandGroup)
/// LogicalOperator = 1 bit (and = 0, or = 1)
/// Commands = Command/commandGroup id bytes (end with 0x00)
/// 
/// </remarks>
internal class CommandGroupSerializer : Serializer<IEnumerable<CommandGroupModel>>
{
    public CommandGroupSerializer(IEnumerable<CommandGroupModel> gameComponent) : base(gameComponent)
    {
        EnsureHelpers.EnsureMaxLength(GameComponent, Constants.MaxLengthCommandsGroupsAllowed,
            string.Format(Properties.Resources.MaxLengthCommandsAllowedError, Constants.MaxLengthCommandsGroupsAllowed));
    }

    public override IEnumerable<GameComponentPointerModel> GenerateGameComponentPointers()
    {
        var result = new List<GameComponentPointerModel>();
        var pointer = 0;

        foreach (var commandGroup in GameComponent.SortByKey())
        {
            EnsureGameComponentProperties(commandGroup, result);

            result.Add(new GameComponentPointerModel(commandGroup.Code, pointer));
            pointer +=
                1 + // flag CommandGroup + LogicalOperator
                commandGroup.Commands.Count() + 1; // Commands + EndTokenByte
        }

        return result;
    }

    public override SerializerResultModel Serialize(GameComponentsPointersModel gameComponentsIndexes)
    {
        var dataBytes = GameComponent.SortByKey().SelectMany(item => CreateDataBytes(item, gameComponentsIndexes));
        return new SerializerResultModel(dataBytes.ToArray());
    }

    private static byte[] CreateDataBytes(CommandGroupModel commandGroup, GameComponentsPointersModel gameComponentsIndexes)
    {
        var result = new List<byte>
        {
            // health + experience points
            (byte)(1 << 7 | (byte)commandGroup.LogicalOperator << 6),
        };

        // Commands        
        result.AddRange(GetCommandsIndexes(commandGroup, gameComponentsIndexes));
        result.Add(Constants.EndToken);

        return result.ToArray();
    }
    private static IEnumerable<byte> GetCommandsIndexes(CommandGroupModel commandGroup, GameComponentsPointersModel gameComponentsIndexes) =>
        commandGroup.Commands is IEnumerable<CommandGroupModel> ?
            commandGroup.Commands.Select(item => gameComponentsIndexes.CommandsGroups.IndexOf(item.Code)) :
            commandGroup.Commands.Select(item => gameComponentsIndexes.Commands.IndexOf(item.Code));

    private static void EnsureGameComponentProperties(CommandGroupModel commandGroup, IEnumerable<GameComponentPointerModel> gameComponentPointers)
    {
        EnsureHelpers.EnsureNotFound(gameComponentPointers, item => item.Code == commandGroup.Code, string.Format(Properties.Resources.DuplicateCodeError, commandGroup.Code));
        EnsureHelpers.EnsureNotNullOrWhiteSpace(commandGroup.Code, Properties.Resources.CodeIsRequiredError);

        EnsureHelpers.EnsureNotNullOrEmpty(commandGroup.Commands, Properties.Resources.CommandsAreRequired);
    }
}
