using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Interfaces;

namespace RetroAdventureCreator.Infrastructure.Game.Models;

/// <summary>
/// Model for dispatch game scene commands
/// </summary>
public class DispatcherModel
{
    /// <summary>
    /// Owner scene code for dispatcher. if not specified, is for all scenes.
    /// </summary>
    public string OwnerSceneCode { get; init; } = default!;

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
