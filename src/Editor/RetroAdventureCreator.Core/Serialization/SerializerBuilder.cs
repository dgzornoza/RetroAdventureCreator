using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

internal class SerializerBuilder
{
    private readonly CommandGroupSerializer commandGroupSerializer;
    private readonly CommandsSerializer commandsSerializer;
    private readonly AfterInputCommandDispatchersSerializer afterInputCommandDispatchersSerializer;
    private readonly BeforeInputCommandDispatchersSerializer beforeInputCommandDispatchersSerializer;
    private readonly FlagsSerializer flagsSerializer;
    private readonly InputCommandsSerializer inputCommandsSerializer;
    private readonly MessagesSerializer messagesSerializer;
    private readonly ObjectsSerializer objectsSerializer;
    private readonly PlayerSerializer playerSerializer;
    private readonly ScenesSerializer scenesSerializer;
    private readonly SettingsSerializer settingsSerializer;
    private readonly VocabularyNounsSerializer vocabularyNounsSerializer;
    private readonly VocabularyVerbsSerializer vocabularyVerbsSerializer;


    public SerializerBuilder(GameModel gameModel)
    {
        commandGroupSerializer = new CommandGroupSerializer(gameModel.CommandsGroups);
        commandsSerializer = new CommandsSerializer(gameModel.Commands);
        afterInputCommandDispatchersSerializer = new AfterInputCommandDispatchersSerializer(gameModel.Dispatchers);
        beforeInputCommandDispatchersSerializer = new BeforeInputCommandDispatchersSerializer(gameModel.Dispatchers);
        flagsSerializer = new FlagsSerializer(gameModel.Flags);
        inputCommandsSerializer = new InputCommandsSerializer(gameModel.InputCommands);
        messagesSerializer = new MessagesSerializer(gameModel.Messages);
        objectsSerializer = new ObjectsSerializer(gameModel.Objects);
        playerSerializer = new PlayerSerializer(gameModel.Player);
        scenesSerializer = new ScenesSerializer(gameModel.Scenes);
        settingsSerializer = new SettingsSerializer(gameModel.Settings);
        vocabularyNounsSerializer = new VocabularyNounsSerializer(gameModel.Vocabulary);
        vocabularyVerbsSerializer = new VocabularyVerbsSerializer(gameModel.Vocabulary);

        GameComponentsPointersModel = new GameComponentsPointersModel(
            Commands: commandsSerializer.GenerateGameComponentPointers(),
            CommandsGroups: commandGroupSerializer.GenerateGameComponentPointers(),
            Flags: flagsSerializer.GenerateGameComponentPointers(),
            InputCommands: inputCommandsSerializer.GenerateGameComponentPointers(),
            Messages: messagesSerializer.GenerateGameComponentPointers(),
            Objects: objectsSerializer.GenerateGameComponentPointers(),
            Scenes: scenesSerializer.GenerateGameComponentPointers(),
            AfterInputCommandDispatchers: afterInputCommandDispatchersSerializer.GenerateGameComponentPointers(),
            BeforeInputCommandDispatchers: beforeInputCommandDispatchersSerializer.GenerateGameComponentPointers(),
            VocabularyNouns: vocabularyNounsSerializer.GenerateGameComponentPointers(),
            VocabularyVerbs: vocabularyVerbsSerializer.GenerateGameComponentPointers());
    }

    public GameComponentsPointersModel GameComponentsPointersModel { get; init; }

    public T GetSerializer<T>() => typeof(T).Name switch
    {
        nameof(CommandGroupSerializer) => (T)Convert.ChangeType(commandGroupSerializer, typeof(T)),
        nameof(CommandsSerializer) => (T)Convert.ChangeType(commandsSerializer, typeof(T)),
        nameof(AfterInputCommandDispatchersSerializer) => (T)Convert.ChangeType(afterInputCommandDispatchersSerializer, typeof(T)),
        nameof(BeforeInputCommandDispatchersSerializer) => (T)Convert.ChangeType(beforeInputCommandDispatchersSerializer, typeof(T)),
        nameof(FlagsSerializer) => (T)Convert.ChangeType(flagsSerializer, typeof(T)),
        nameof(InputCommandsSerializer) => (T)Convert.ChangeType(inputCommandsSerializer, typeof(T)),
        nameof(MessagesSerializer) => (T)Convert.ChangeType(messagesSerializer, typeof(T)),
        nameof(ObjectsSerializer) => (T)Convert.ChangeType(objectsSerializer, typeof(T)),
        nameof(PlayerSerializer) => (T)Convert.ChangeType(playerSerializer, typeof(T)),
        nameof(ScenesSerializer) => (T)Convert.ChangeType(scenesSerializer, typeof(T)),
        nameof(SettingsSerializer) => (T)Convert.ChangeType(settingsSerializer, typeof(T)),
        nameof(VocabularyNounsSerializer) => (T)Convert.ChangeType(vocabularyNounsSerializer, typeof(T)),
        nameof(VocabularyVerbsSerializer) => (T)Convert.ChangeType(vocabularyVerbsSerializer, typeof(T)),
        _ => throw new ArgumentException($"Serializer of type {typeof(T).Name} not found"),
    };
}
