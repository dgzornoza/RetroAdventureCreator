using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Infrastructure.Game.Interfaces;

namespace RetroAdventureCreator.Infrastructure.Game.Models;

public class FlagModel : IUniqueKey
{
    /// <summary>
    /// Unique Code for identify InputCommand
    /// </summary>
    public string Code { get; init; } = default!;

    /// <summary>
    /// Flag value
    /// </summary>
    public bool Value { get; set; }
}
