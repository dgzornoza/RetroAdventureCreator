namespace RetroAdventureCreator.Infrastructure.Game.Enums;

/// <summary>
/// Triger types in game
/// </summary>
/// <remarks>All Enums start with 1 because 0 is reserved for end of objects serialization</remarks>
public enum Trigger
{
    /// <summary>After show input command shell</summary>
    AfterInputCommand = 1,
    /// <summary>Before enter input command (this trigger contains input command)<summary>
    BeforeInputCommand,
    ///// <summary>On time interval from enter in Scene<summary>
    //OnSceneInterval,
    ///// <summary>Once time from enter in scene<summary>
    //OnSceneTimeout,
    ///// <summary>On time interval from start game<summary>
    //OnGameInterval,
    ///// <summary>Once time from start game<summary>
    //OnGameTimeout,
}
