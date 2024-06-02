using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Helpers;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

/// <summary>
/// Command models serializer
/// </summary>
/// <remarks>
/// Format Command serializer:
/// ----------------------------------------------
/// 
/// Data:
/// 1 bit = 0 (Command)
/// Token = 7 byte (128)
/// Arguments = ids bytes (end with 0x00)
/// 
/// </remarks>
internal class CommandsSerializer : Serializer<IEnumerable<CommandModel>>
{
    public CommandsSerializer(IEnumerable<CommandModel> gameComponent) : base(gameComponent)
    {
        EnsureHelpers.EnsureMaxLength(GameComponent, Constants.MaxLengthCommandsAllowed,
            string.Format(Properties.Resources.MaxLengthCommandsAllowedError, Constants.MaxLengthCommandsAllowed));
    }

    public override IEnumerable<GameComponentPointerModel> GenerateGameComponentPointers()
    {
        var result = new List<GameComponentPointerModel>();
        var pointer = 0;

        foreach (var command in GameComponent.SortByKey())
        {
            EnsureGameComponentProperties(command, result);

            result.Add(new GameComponentPointerModel(command.Code, pointer));
            pointer +=
                1 + // flag command + token
                (command.Arguments?.Count() ?? 0) + 1; // Arguments + EndTokenByte
        }

        return result;
    }

    public override SerializerResultModel Serialize(GameComponentsPointersModel gameComponentsIndexes)
    {
        var dataBytes = GameComponent.SortByKey().SelectMany(item => CreateDataBytes(item, gameComponentsIndexes));
        return new SerializerResultModel(dataBytes.ToArray());
    }

    private static byte[] CreateDataBytes(CommandModel command, GameComponentsPointersModel gameComponentsIndexes)
    {
        var result = new List<byte>
        {
            // health + experience points
            (byte)(1 << 7 | (byte)command.Token),
        };

        // TODO: falta implementar argumentos dependiendo del comando
        // ...

        return result.ToArray();
    }

    private static void EnsureGameComponentProperties(CommandModel command, IEnumerable<GameComponentPointerModel> gameComponentPointers)
    {
        EnsureHelpers.EnsureNotFound(gameComponentPointers, item => item.Code == command.Code, string.Format(Properties.Resources.DuplicateCodeError, command.Code));
        EnsureHelpers.EnsureNotNullOrWhiteSpace(command.Code, Properties.Resources.CodeIsRequiredError);
    }
}
