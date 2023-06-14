﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

/// <summary>
/// Dispatcher model serializer
/// </summary>
/// <remarks>
/// Format Dispatcher serializer:
/// ----------------------------------------------
/// 
/// Header:
/// Id = 8 bits (256)
/// Trigger = 6 bits (64)
/// InputCommand = 6 bits (64 ids input command)
/// Commands = 3 bits (8)
/// 
/// Data:
/// Commands = 0-64 bytes
/// 
/// </remarks>
internal class DispatcherModelSerializer : ISerializer<DispatcherModel>
{
    public byte[] Serialize(DispatcherModel model)
    {
        throw new NotImplementedException();
    }
}
