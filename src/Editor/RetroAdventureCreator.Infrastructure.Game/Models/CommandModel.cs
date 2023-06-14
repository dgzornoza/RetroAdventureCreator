using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Infrastructure.Game.Enums;

namespace RetroAdventureCreator.Infrastructure.Game.Models;

/// <summary>
/// Command model
/// </summary>
public class CommandModel
{
    /// <summary>
    /// Unique Id in game for identify object
    /// </summary>
    public string Id { get; init; } = default!;

    /// <summary>
    /// Logical operator
    /// </summary>
    public LogicalOperator LogicalOperator { get; init; }

    /// <summary>
    /// Command Token
    /// </summary>
    public CommandToken Token { get; init; }

    /// <summary>
    /// Arguments (based in command token)
    /// </summary>
    public IEnumerable<string> Argument { get; init; } = default!;
}
