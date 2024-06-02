namespace RetroAdventureCreator.Core.Models;

internal record GameComponentsPointersModel(
    IEnumerable<GameComponentPointerModel> Commands,
    
    IEnumerable<GameComponentPointerModel> Flags,
    IEnumerable<GameComponentPointerModel> InputCommands,
    IEnumerable<GameComponentPointerModel> Messages,
    IEnumerable<GameComponentPointerModel> Objects,
    IEnumerable<GameComponentPointerModel> Scenes,

    IEnumerable<GameComponentPointerModel> AfterInputCommandDispatchers,
    IEnumerable<GameComponentPointerModel> BeforeInputCommandDispatchers,

    IEnumerable<GameComponentPointerModel> VocabularyNouns,
    IEnumerable<GameComponentPointerModel> VocabularyVerbs);
