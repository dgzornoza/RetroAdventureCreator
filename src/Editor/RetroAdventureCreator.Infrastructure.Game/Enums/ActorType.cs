using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroAdventureCreator.Infrastructure.Game.Enums;

public enum ActorType : byte
{
    Player = 0,
    WeakEnemy = 1,
    NormalEnemy = 2,
    StrongEnemy = 3,
    Boss = 4,
    Merchant = 6,
}
