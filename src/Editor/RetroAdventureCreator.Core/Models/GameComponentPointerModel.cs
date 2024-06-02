using RetroAdventureCreator.Infrastructure.Game.Interfaces;

namespace RetroAdventureCreator.Core.Models;

internal record GameComponentPointerModel(string Code, int RelativePointer) : IUniqueKey;
