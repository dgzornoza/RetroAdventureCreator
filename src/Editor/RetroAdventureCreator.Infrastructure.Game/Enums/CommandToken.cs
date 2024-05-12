using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroAdventureCreator.Infrastructure.Game.Enums;

/// <summary>
/// Aavailables command tokens 
/// </summary>
/// <remarks>All Enums start with 1 because 0 is reserved for end of objects serialization</remarks>
public enum CommandToken : byte
{
    At = 1,
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
