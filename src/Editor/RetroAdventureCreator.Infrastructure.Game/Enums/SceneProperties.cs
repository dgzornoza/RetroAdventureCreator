using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroAdventureCreator.Infrastructure.Game.Enums;

/// <summary>
/// Flag enum for Object properties
/// </summary>
[Flags]
public enum SceneProperties
{
    None = 0x00,
    IsDark = 0x01,
}
