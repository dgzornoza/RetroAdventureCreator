using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroAdventureCreator.Infrastructure.Game.Models;

/// <summary>
/// Game Player Model
/// </summary>
public class PlayerModel
{
    /// <summary>
    /// Player Health from 0 to 7
    /// </summary>
    public int Health { get; init; } = 7;

    /// <summary>
    /// Player equiped objects
    /// </summary>
    public IEnumerable<ObjectModel>? Objects { get; init; }
}
