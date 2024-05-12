using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroAdventureCreator.Infrastructure.Game.Enums;

/// <summary>
/// Flag enum for Object properties
/// </summary>
/// <remarks>All Enums start with 1 because 0 is reserved for end of objects serialization</remarks>
[Flags]
public enum ObjectProperties : byte
{
    None = 0x01,
    InVisible = 0x01 << 1,
    IsEnabled = 0x01 << 2,
    InUse = 0x01 << 3,
    Portable = 0x01 << 4,
    IsContainer = 0x01 << 5,
}
