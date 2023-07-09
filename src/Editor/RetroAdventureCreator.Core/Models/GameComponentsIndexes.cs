using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Infrastructure.Game.Interfaces;

namespace RetroAdventureCreator.Core.Models;

internal record GameComponentsIndexes(
    IEnumerable<GameComponentKeyModel> Commands,
    IEnumerable<GameComponentKeyModel> CommandsGroups,
    IEnumerable<GameComponentKeyModel> Dispatchers,
    IEnumerable<GameComponentKeyModel> Flags,
    IEnumerable<GameComponentKeyModel> InputCommands,
    IEnumerable<GameComponentKeyModel> Messages,
    IEnumerable<GameComponentKeyModel> Objects,
    IEnumerable<GameComponentKeyModel> Scenes,
    IEnumerable<GameComponentKeyModel> VocabularyNouns,
    IEnumerable<GameComponentKeyModel> VocabularyVerbs);
