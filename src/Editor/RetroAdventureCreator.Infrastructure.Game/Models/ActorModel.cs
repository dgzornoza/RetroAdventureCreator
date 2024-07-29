using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Interfaces;

namespace RetroAdventureCreator.Infrastructure.Game.Models;

/// <summary>
/// Game Player Model
/// </summary>
public class ActorModel : IUniqueKey
{
    /// <summary>
    /// Unique Code for identify Object
    /// </summary>
    public string Code { get; init; } = default!;

    /// <summary>
    /// Health 256 max
    /// </summary>
    public byte Health { get; init; } = byte.MaxValue;

    /// <summary>
    /// Experience Points 256 max
    /// </summary>
    public byte ExperiencePoints { get; init; } = byte.MinValue;

    /// <summary>
    /// actor type
    /// </summary>
    public ActorType ActorType { get; init; }
}
