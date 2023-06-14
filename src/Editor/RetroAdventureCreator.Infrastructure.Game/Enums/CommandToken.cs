using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroAdventureCreator.Infrastructure.Game.Enums;

public enum CommandToken
{
    AT,
    MESSAGE,
    ZERO,
    NOT_ZERO,
    IN_USE,
    NOT_IN_USE,
    END_GAME,
    SET,
    UNSET,
    GOTO,
}
