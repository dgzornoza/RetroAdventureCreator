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
public class CommandGroupModel : IUniqueKey, ICommandModel
{
    /// <summary>
    /// Unique Code for identify Command group
    /// </summary>
    public string Code { get; init; } = default!;

    /// <summary>
    /// Logical operator
    /// </summary>
    public LogicalOperator LogicalOperator { get; init; }

    /// <summary>
    /// Commands in group
    /// </summary>
    public IEnumerable<ICommandModel> Commands { get; init; } = default!;
}

