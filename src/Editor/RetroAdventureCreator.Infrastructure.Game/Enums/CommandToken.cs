namespace RetroAdventureCreator.Infrastructure.Game.Enums;

/// <summary>
/// Availables command tokens 
/// </summary>
/// <remarks>All Enums start with 1 because 0 is reserved for end of objects serialization</remarks>
public enum CommandToken : byte
{
    /// <summary>Token para agrupar comandos mediante un And:</summary>
    CommandGroupAnd = 1,
    /// <summary>Token para agrupar comandos mediante un Or</summary>
    CommandGroupOr,
    /// <summary>Command to show message</summary>
    ShowMessage,
    /// <summary>Command to verify is set a flag</summary>
    IsSet,
    /// <summary>Command to verify is unset a flag</summary>
    IsUnset,
    /// <summary>Command to verify is in use a object</summary>
    InUse,
    /// <summary>Command to verify is not in use a object</summary>
    NotInUse,
    /// <summary>Command to use a object</summary>
    Use,
    /// <summary>Command to not use a object</summary>
    NotUse,
    /// <summary>Command to end the game</summary>
    EndGame,
    /// <summary>Command to set a flag</summary>
    Set,
    /// <summary>Command to unset a flag</summary>
    Unset,
    /// <summary>Command to navigate to scene</summary>
    Goto,
}
