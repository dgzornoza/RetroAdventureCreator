using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Infrastructure.Game.Interfaces;

namespace RetroAdventureCreator.Core.Models;

internal record GameComponentsPointers(
    IEnumerable<GameComponentPointerModel> Commands,
    IEnumerable<GameComponentPointerModel> CommandsGroups,
    IEnumerable<GameComponentPointerModel> Dispatchers,
    IEnumerable<GameComponentPointerModel> Flags,
    IEnumerable<GameComponentPointerModel> InputCommands,
    IEnumerable<GameComponentPointerModel> Messages,
    IEnumerable<GameComponentPointerModel> Objects,
    IEnumerable<GameComponentPointerModel> Scenes,
    IEnumerable<GameComponentPointerModel> VocabularyNouns,
    IEnumerable<GameComponentPointerModel> VocabularyVerbs);
