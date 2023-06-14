using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Infrastructure.Game.Enums;

namespace RetroAdventureCreator.Infrastructure.Game.Models;

/// <summary>
/// Model for dispatch game commands
/// </summary>
public class DispatcherModel
{
    /// <summary>
    /// Unique Id in game for identify object
    /// </summary>
    public string Id { get; init; } = default!;

    /// <summary>
    /// Trigger for launch dispatcher command
    /// </summary>
    public Trigger Trigger { get; init; }

    /// <summary>
    /// Input commands (only in triggers AfterInputCommand)
    /// </summary>
    public IEnumerable<InputCommandModel>? InputCommands { get; init; }

    /// <summary>
    /// Commands to execute
    /// </summary>
    public IEnumerable<CommandModel>? Commands { get; init; }
}
