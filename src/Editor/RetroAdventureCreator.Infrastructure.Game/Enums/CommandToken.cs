﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroAdventureCreator.Infrastructure.Game.Enums;

public enum CommandToken
{
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
