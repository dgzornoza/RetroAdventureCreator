﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Interfaces;

namespace RetroAdventureCreator.Infrastructure.Game.Models;

/// <summary>
/// Command model
/// </summary>
public class CommandModel : IUniqueKey, ICommandModel
{
    /// <summary>
    /// Unique Code for identify Command
    /// </summary>
    public string Code { get; init; } = default!;

    /// <summary>
    /// Command Token
    /// </summary>
    public CommandToken Token { get; init; }

    /// <summary>
    /// Arguments (based in command token)
    /// </summary>
    public IEnumerable<string>? Arguments { get; init; }
}
