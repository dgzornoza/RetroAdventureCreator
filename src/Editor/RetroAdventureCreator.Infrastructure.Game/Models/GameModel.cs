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
    public string MainSceneId { get; init; } = default!;

    /// <summary>
    /// Game scenes
    /// </summary>
    public IEnumerable<SceneModel>? Scenes { get; init; }

    /// <summary>
    /// Game Vocabulary
    /// </summary>
    public IEnumerable<VocabularyModel>? Vocabulary { get; init; }

    /// <summary>
    /// Game messages
    /// </summary>
    public IEnumerable<MessageModel>? Messages { get; init; }

    /// <summary>
    /// Game Flags
    /// </summary>
    public IDictionary<string, bool>? Flags { get; init; }

    /// <summary>
    /// Default Game Settings
    /// </summary>
    public SettingsModel? Settings { get; init; }
}
