using System.Text.RegularExpressions;
using RetroAdventureCreator.Test.Infrastructure.Builders;

namespace RetroAdventureCreator.Test.Theories.GameTests;

public class GameModelTest
{
    [Theory]
    [InlineData(typeof(GameInPawsTutorialBuilder))]
    public void CreateGameModel(Type gameBuilderType)
    {
        string[] hexValuesArray = new string[] {
            "1022", "0FFA", "3EA3", "42A4"};
        string hexValues = string.Join("", hexValuesArray).Trim();

        var bytes = Regex.Split(hexValues, "(?<=\\G.{2})")
            .Where(item => !string.IsNullOrEmpty(item))
            .Select(item => Convert.ToByte(item, 16))
            .ToArray();

        // Arrange
        var game = (Activator.CreateInstance(gameBuilderType) as GameBuilder)!.BuildGame();

        // Act
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(game);

        // Assert
        Assert.NotNull(game);
    }
}

