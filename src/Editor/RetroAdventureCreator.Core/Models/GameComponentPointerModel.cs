using RetroAdventureCreator.Infrastructure.Game.Interfaces;

namespace RetroAdventureCreator.Core.Models;

/// <summary>
/// Store game component (16 bits) pointer
/// </summary>
/// <param name="Code">Codigo del componente</param>
/// <param name="RelativePointer">Relative pointer to component (16 bits)</param>
internal record GameComponentPointerModel(string Code, short RelativePointer) : IUniqueKey;
