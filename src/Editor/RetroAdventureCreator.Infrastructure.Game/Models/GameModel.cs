using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroAdventureCreator.Infrastructure.Game.Models;

/// <summary>
/// Main Game model
/// </summary>
public class GameModel
{
    /// <summary>
    /// Game player
    /// </summary>
    public PlayerModel Player { get; init; } = default!;

    /// <summary>
    /// Main game scene
    /// </summary>
    public string MainSceneCode { get; init; } = default!;

    /// <summary>
    /// Default Game Settings
    /// </summary>
    public SettingsModel Settings { get; init; } = default!;

    /// <summary>
    /// Game Assets
    /// </summary>
    public AssetsModel Assets { get; init; } = default!;
}
