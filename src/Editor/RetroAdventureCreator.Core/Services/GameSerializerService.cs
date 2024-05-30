using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Helpers;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Interfaces;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Services;

/// <summary>
/// Service to serialize a game model to a byte array.
/// </summary>
internal class GameSerializerService
{
    /// <summary>
    /// Method for serialize entire game
    /// </summary>
    /// <param name="gameModel">Game model to serialize</param>
    /// <returns>Byte[] with game serialized</returns>
    /// <remarks>
    /// The serialization will performed as follows:
    ///
    /// When the factory is created, the model 'GameComponentsPointersModel' will be created with a reference to all the elements of the game.
    /// Then the elements will be serialized in any order since internally the 'GameComponentsPointersModel' references will be used.
    /// These references are implemented by creating bytes with the indexes of the different elements, this limits the number of elements of each type to one byte,
    /// Initially this is done to occupy as little memory as possible, in the future it is possible that 16-bit indices will be used for more powerful machines.
    ///
    /// The serialized bytes will be united into a single array and a header will be created indicating the memory address of each element following
    /// the order of dependencies indicated in the '_dependencies.md' file.
    ///
    /// The objective is to use as little memory as possible, since initially the engine is implemented for 8-bit computers.
    /// </remarks>
    public byte[] Serialize(GameModel gameModel)
    {
        var serializerBuilder = new SerializerFactory(gameModel, Encoding.ASCII);

        var commandsSerializer = serializerBuilder.Serialize<CommandsSerializer>();
        var afterInputCommandDispatchersSerializer = serializerBuilder.Serialize<AfterInputCommandDispatchersSerializer>();
        var beforeInputCommandDispatchersSerializer = serializerBuilder.Serialize<BeforeInputCommandDispatchersSerializer>();
        var flagsSerializer = serializerBuilder.Serialize<FlagsSerializer>();
        var inputCommandsSerializer = serializerBuilder.Serialize<InputCommandsSerializer>();
        var messagesSerializer = serializerBuilder.Serialize<MessagesSerializer>();
        var objectsSerializer = serializerBuilder.Serialize<ObjectsSerializer>();
        var playerSerializer = serializerBuilder.Serialize<PlayerSerializer>();
        var scenesSerializer = serializerBuilder.Serialize<ScenesSerializer>();
        var settingsSerializer = serializerBuilder.Serialize<SettingsSerializer>();
        var vocabularyNounsSerializer = serializerBuilder.Serialize<VocabularyNounsSerializer>();
        var vocabularyVerbsSerializer = serializerBuilder.Serialize<VocabularyVerbsSerializer>();

        return null;
    }
}
