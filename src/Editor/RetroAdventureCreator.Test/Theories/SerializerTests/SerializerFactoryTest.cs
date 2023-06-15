using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Helpers;

namespace RetroAdventureCreator.Test.Theories.SerializerTests;

//public class SerializerFactoryTest
//{
//    [Theory]
//    [InlineData(typeof(CommandModel), typeof(CommandsSerializer))]
//    [InlineData(typeof(DispatcherModel), typeof(DispatchersSerializer))]
//    [InlineData(typeof(GameModel), typeof(GameSerializer))]
//    [InlineData(typeof(InputCommandModel), typeof(InputCommandsSerializer))]
//    [InlineData(typeof(MessageModel), typeof(MessagesSerializer))]
//    [InlineData(typeof(ObjectModel), typeof(ObjectsSerializer))]
//    [InlineData(typeof(PlayerModel), typeof(PlayerSerializer))]
//    [InlineData(typeof(SceneModel), typeof(ScenesSerializer))]
//    [InlineData(typeof(SettingsModel), typeof(SettingsSerializer))]
//    [InlineData(typeof(VocabularyModel), typeof(VocabularySerializer))]
//    public void CreateSerializers(Type modelType, Type serializerType)
//    {
//        // Arrange
//        dynamic instance = Activator.CreateInstance(modelType) ?? throw new InvalidOperationException();

//        // Act && Assert
//        var serializer = SerializerFactory.GetSerializer(instance);

//        Assert.NotNull(serializer);
//        Assert.IsType(serializerType, serializer);
//    }
//}
