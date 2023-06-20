using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Helpers;

namespace RetroAdventureCreator.Test.Theories.ExtensionsTests;

public class ObjectExtensionsTest
{
    [Theory]
    [InlineData(typeof(CommandModel), 27)]
    [InlineData(typeof(DispatcherModel), 9)]
    [InlineData(typeof(InputCommandModel), 27)]
    [InlineData(typeof(MessageModel), 129)]
    [InlineData(typeof(ObjectModel), 123)]
    [InlineData(typeof(PlayerModel), 1)]
    [InlineData(typeof(SceneModel), 3)]
    [InlineData(typeof(SettingsModel), 1)]
    [InlineData(typeof(VocabularyModel), 234)]
    public void GetDepthPropertyValuesOfType_AsExpected(Type type, int objectCount)
    {
        // Arrange
        var game = FilesHelpers.GetLocalResourceJsonObject<GameModel>("GameRandom.json") ?? throw new InvalidOperationException();

        // Act 
        MethodInfo method = typeof(ObjectExtensions).GetMethod(nameof(ObjectExtensions.GetDepthPropertyValuesOfType)) ?? throw new InvalidOperationException();
        MethodInfo generic = method.MakeGenericMethod(type);
        var properties = generic.Invoke(null, new object[] { game }) as IEnumerable<object> ?? throw new InvalidOperationException();

        // Assert
        Assert.NotNull(properties);
        Assert.NotEmpty(properties);
        Assert.True(properties.Count() == objectCount);
        Assert.All(properties, item => Assert.IsType(type, item));
    }
}
