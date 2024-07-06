using System.Text;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

/// <summary>
/// Factory class to create serializers for each game component.
/// </summary>
internal class SerializerFactory
{
    private readonly CommandsSerializer commandsSerializer;
    private readonly AfterInputCommandDispatchersSerializer afterInputCommandDispatchersSerializer;
    private readonly BeforeInputCommandDispatchersSerializer beforeInputCommandDispatchersSerializer;
    private readonly FlagsSerializer flagsSerializer;
    private readonly InputCommandsSerializer inputCommandsSerializer;
    private readonly MessagesSerializer messagesSerializer;
    private readonly ObjectsSerializer<ObjectModel> objectsSerializer;
    private readonly ActorsSerializer actorsSerializer;
    private readonly ScenesSerializer scenesSerializer;
    private readonly SettingsSerializer settingsSerializer;
    private readonly VocabularyNounsSerializer vocabularyNounsSerializer;
    private readonly VocabularyVerbsSerializer vocabularyVerbsSerializer;

    public SerializerFactory(GameModel gameModel) : this(gameModel, Encoding.ASCII)
    {
    }

    public SerializerFactory(GameModel gameModel, Encoding encoding)
    {
        commandsSerializer = new CommandsSerializer(gameModel.Commands);
        afterInputCommandDispatchersSerializer = new AfterInputCommandDispatchersSerializer(gameModel.Dispatchers);
        beforeInputCommandDispatchersSerializer = new BeforeInputCommandDispatchersSerializer(gameModel.Dispatchers);
        flagsSerializer = new FlagsSerializer(gameModel.Flags);
        inputCommandsSerializer = new InputCommandsSerializer(gameModel.InputCommands);
        messagesSerializer = new MessagesSerializer(gameModel.Messages, encoding);
        objectsSerializer = new NormalObjectsSerializer(gameModel.Objects);
        actorsSerializer = new ActorsSerializer(gameModel.Actors);
        scenesSerializer = new ScenesSerializer(gameModel.Scenes);
        settingsSerializer = new SettingsSerializer(gameModel.Settings);
        vocabularyNounsSerializer = new VocabularyNounsSerializer(gameModel.Vocabulary, encoding);
        vocabularyVerbsSerializer = new VocabularyVerbsSerializer(gameModel.Vocabulary, encoding);

        GameComponentsPointersModel = new GameComponentsPointersModel(
            Commands: commandsSerializer.GenerateGameComponentPointers(),
            Flags: flagsSerializer.GenerateGameComponentPointers(),            
            InputCommands: inputCommandsSerializer.GenerateGameComponentPointers(),
            Messages: messagesSerializer.GenerateGameComponentPointers(),
            Objects: objectsSerializer.GenerateGameComponentPointers(),
            Actors: actorsSerializer.GenerateGameComponentPointers(),
            Scenes: scenesSerializer.GenerateGameComponentPointers(),
            AfterInputCommandDispatchers: afterInputCommandDispatchersSerializer.GenerateGameComponentPointers(),
            BeforeInputCommandDispatchers: beforeInputCommandDispatchersSerializer.GenerateGameComponentPointers(),
            VocabularyNouns: vocabularyNounsSerializer.GenerateGameComponentPointers(),
            VocabularyVerbs: vocabularyVerbsSerializer.GenerateGameComponentPointers());
    }

    /// <summary>
    /// Serialize the game components.
    /// </summary>
    /// <typeparam name="T">Game component type</typeparam>
    /// <returns>Serialization result model</returns>
    public SerializerResultModel Serialize<T>() where T : ISerializer => GetSerializer<T>().Serialize(GameComponentsPointersModel);

    internal GameComponentsPointersModel GameComponentsPointersModel { get; init; }

    private T GetSerializer<T>() => typeof(T).Name switch
    {
        nameof(CommandsSerializer) => (T)Convert.ChangeType(commandsSerializer, typeof(T)),
        nameof(AfterInputCommandDispatchersSerializer) => (T)Convert.ChangeType(afterInputCommandDispatchersSerializer, typeof(T)),
        nameof(BeforeInputCommandDispatchersSerializer) => (T)Convert.ChangeType(beforeInputCommandDispatchersSerializer, typeof(T)),
        nameof(FlagsSerializer) => (T)Convert.ChangeType(flagsSerializer, typeof(T)),
        nameof(InputCommandsSerializer) => (T)Convert.ChangeType(inputCommandsSerializer, typeof(T)),
        nameof(MessagesSerializer) => (T)Convert.ChangeType(messagesSerializer, typeof(T)),
        nameof(NormalObjectsSerializer) => (T)Convert.ChangeType(objectsSerializer, typeof(T)),
        nameof(ActorsSerializer) => (T)Convert.ChangeType(actorsSerializer, typeof(T)),
        nameof(ScenesSerializer) => (T)Convert.ChangeType(scenesSerializer, typeof(T)),
        nameof(SettingsSerializer) => (T)Convert.ChangeType(settingsSerializer, typeof(T)),
        nameof(VocabularyNounsSerializer) => (T)Convert.ChangeType(vocabularyNounsSerializer, typeof(T)),
        nameof(VocabularyVerbsSerializer) => (T)Convert.ChangeType(vocabularyVerbsSerializer, typeof(T)),
        _ => throw new ArgumentException($"Serializer of type {typeof(T).Name} not found"),
    };


}
