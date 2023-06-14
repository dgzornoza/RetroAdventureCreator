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
public record CommandModel
{
    /// <summary>
    /// Unique Id in game for identify object
    /// </summary>
    public string Id { get; init; } = default!;

    /// <summary>
    /// Logical operator
    /// </summary>
    public LogicalOperator LogicalOperator { get; set; }

    /// <summary>
    /// Command Token
    /// </summary>
    public CommandToken Token { get; set; }

    /// <summary>
    /// Arguments (based in command token)
    /// </summary>
    public IEnumerable<string> Argument { get; set; } = default!;
}
