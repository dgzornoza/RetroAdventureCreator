using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Helpers;

namespace RetroAdventureCreator.Test.Theories.SceneTests;

public class SceneModelTest
{
    [Theory]
    [InlineData("SceneBigDescription.json")]
    public void CreateSceneModel(string jsonFile)
    {
        // Arrange
        var scene = FilesHelpers.GetLocalResourceJsonObject<SceneModel>(jsonFile) ?? throw new InvalidOperationException();

        // Act
        var groupedMappedWords = scene.Description.Split(" ").GroupBy(item => item)
            .Where(item => item.Key.Length > 1 && item.Count() > 3)
            .OrderByDescending(item => item.Count())
            .Select((item, index) => new { key = (char)(index + 35), value = item.Key });

        byte[] currentBytes = Encoding.ASCII.GetBytes(scene.Description);

        string newDescription = string.Empty;
        foreach (var mapWord in groupedMappedWords)
        {
            newDescription = scene.Description.Replace(mapWord.value, $"{mapWord.key}");
        }

        byte[] compressedBytes = Encoding.ASCII.GetBytes(newDescription);

        // Assert
        Assert.NotNull(scene);
    }
}
