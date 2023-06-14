using System;
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
/// Trigger = 6 bits (64)
/// InputCommands = 3 bits (8)
/// Commands = 3 bits (8)
/// 
/// Data:
/// Commands = 0-256 bytes
/// 
/// </remarks>
internal class DispatcherModelSerializer : ISerializer<DispatcherModel>
{
    public byte[] Serialize(DispatcherModel model)
    {
        throw new NotImplementedException();
    }
}

///// <summary>
///// Trigger for launch dispatcher command
///// </summary>
//public Trigger Trigger { get; init; }

///// <summary>
///// Input commands (only in triggers AfterInputCommand)
///// </summary>
//public IEnumerable<string>? InputCommands { get; init; }

///// <summary>
///// Command to execute
///// </summary>
//public IEnumerable<string>? Commands { get; init; }
