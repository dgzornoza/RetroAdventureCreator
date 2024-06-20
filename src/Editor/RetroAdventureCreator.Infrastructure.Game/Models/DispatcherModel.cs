using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Interfaces;

namespace RetroAdventureCreator.Infrastructure.Game.Models;

/// <summary>
/// Model for dispatch game scene commands
/// </summary>
public class DispatcherModel : IUniqueKey
{
    /// <summary>
    /// Unique Code for identify Command
    /// </summary>
    public string Code { get; init; } = default!;

    /// <summary>
    /// Trigger for launch dispatcher command
    /// </summary>
    public Trigger Trigger { get; init; }

    /// <summary>
    /// Commands to execute. Can be a command or group of commands.
    /// will be executed in cascade as long as one does not return false.
    /// </summary>
    public required IEnumerable<ICommandModel> Commands { get; init; }

    /// <summary>
    /// Input commands (only in triggers AfterInputCommand)
    /// </summary>
    public IEnumerable<InputCommandModel>? InputCommands { get; init; }
}
