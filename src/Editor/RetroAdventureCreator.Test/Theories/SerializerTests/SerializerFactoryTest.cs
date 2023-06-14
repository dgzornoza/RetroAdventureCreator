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

public class SerializerFactoryTest
{
    [Theory]
    [InlineData(typeof(AboutModel), typeof(AboutModelSerializer))]
    [InlineData(typeof(CommandModel), typeof(CommandSerializer))]
    [InlineData(typeof(DispatcherModel), typeof(DispatcherModelSerializer))]
    [InlineData(typeof(InputCommandModel), typeof(InputCommandSerializer))]
    [InlineData(typeof(ObjectModel), typeof(ObjectModelSerializer))]
    [InlineData(typeof(SceneModel), typeof(SceneModelSerializer))]
    [InlineData(typeof(SettingsModel), typeof(SettingsModelSerializer))]
    [InlineData(typeof(VocabularyModel), typeof(VocabularyModelSerializer))]
    public void CreateSerializers(Type modelType, Type serializerType)
    {
        // Arrange
        dynamic instance = Activator.CreateInstance(modelType) ?? throw new InvalidOperationException();

        // Act && Assert
        var serializer = SerializerFactory.GetSerializer(instance);

        Assert.NotNull(serializer);
        Assert.IsType(serializerType, serializer);
    }
}
