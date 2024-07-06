namespace RetroAdventureCreator.Core.Models;

internal record GameComponentsPointersModel(
    IEnumerable<GameComponentPointerModel> Commands,    
    IEnumerable<GameComponentPointerModel> InputCommands,
    
    IEnumerable<GameComponentPointerModel> Actors,
    IEnumerable<GameComponentPointerModel> Flags,
    IEnumerable<GameComponentPointerModel> Objects,
    IEnumerable<GameComponentPointerModel> Scenes,

    IEnumerable<GameComponentPointerModel> AfterInputCommandDispatchers,
    IEnumerable<GameComponentPointerModel> BeforeInputCommandDispatchers,

    IEnumerable<GameComponentPointerModel> Messages,
    IEnumerable<GameComponentPointerModel> VocabularyNouns,
    IEnumerable<GameComponentPointerModel> VocabularyVerbs);
