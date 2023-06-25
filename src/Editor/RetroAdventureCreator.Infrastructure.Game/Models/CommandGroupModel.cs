using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Interfaces;

namespace RetroAdventureCreator.Infrastructure.Game.Models;

/// <summary>
/// Group command model
/// </summary>
public class CommandGroupModel : ICommandModel
{
    /// <summary>
    /// Logical operator
    /// </summary>
    public LogicalOperator LogicalOperator { get; init; }

    /// <summary>
    /// Commands in group
    /// </summary>
    public IEnumerable<CommandModel> Commands { get; init; } = default!;
}
