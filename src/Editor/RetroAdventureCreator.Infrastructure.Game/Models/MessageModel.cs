using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Infrastructure.Game.Interfaces;

namespace RetroAdventureCreator.Infrastructure.Game.Models;

public class MessageModel : IUniqueKey
{
    /// <summary>
    /// Unique Code for identify Message
    /// </summary>
    public string Code { get; init; } = default!;

    /// <summary>
    /// Message text, can be use <see cref="TextModifier"/> for text
    /// </summary>
    public string Text { get; init; } = default!;
}
