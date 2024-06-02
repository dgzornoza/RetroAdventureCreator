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
    At,
    Message,
    IsSet,
    IsUnset,
    InUse,
    NotInUse,
    EndGame,
    Set,
    Unset,
    Goto,
}
