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
    /// Player Health from 0 to 15
    /// </summary>
    public int Health { get; init; } = 15;

    /// <summary>
    /// Player Experience Points from 0 to 15
    /// </summary>
    public int ExperiencePoints { get; init; } = 0;

    /// <summary>
    /// Player equiped objects
    /// </summary>
    public IEnumerable<ObjectModel>? Objects { get; init; }
}
