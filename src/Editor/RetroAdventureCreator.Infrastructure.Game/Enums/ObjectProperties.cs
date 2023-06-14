﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroAdventureCreator.Infrastructure.Game.Enums;

/// <summary>
/// Flag enum for Object properties
/// </summary>
[Flags]
public enum ObjectProperties
{
    None = 0x00,
    InVisible = 0x01,
    IsEnabled = 0x01 << 1,
    InUse = 0x01 << 2,
    Container = 0x01 << 3,
    Portable = 0x01 << 4,
}
