using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroAdventureCreator.Infrastructure.Game.Models;

public class MessageModel
{
    /// <summary>
    /// Unique Id in game for identify scene
    /// </summary>
    public string Id { get; init; } = default!;

    /// <summary>
    /// Message text, can be use <see cref="TextModifier"/> for text description
    /// </summary>
    public string Text { get; init; } = default!;
}
