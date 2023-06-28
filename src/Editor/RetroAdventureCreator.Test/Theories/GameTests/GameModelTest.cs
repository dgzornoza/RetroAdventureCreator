using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Helpers;
using RetroAdventureCreator.Test.Infrastructure.Builders;

namespace RetroAdventureCreator.Test.Theories.GameTests;

public class GameModelTest
{
    [Theory]
    [InlineData(typeof(GameInPawsTutorialBuilder))]
    public void CreateGameModel(Type gameBuilderType)
    {
        string[] hexValues = new string[] {
            "1022", "0FFA", "3EA3", "42A4"};

        byte[] result = hexValues
          .Select(value => Convert.ToByte(value, 16))
          .ToArray();


        // Arrange
        var game = (Activator.CreateInstance(gameBuilderType) as GameBuilder)!.BuildGame();

        // Act
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(game);

        // Assert
        Assert.NotNull(game);
    }
}

