using RetroAdventureCreator.Core.Services;
using RetroAdventureCreator.Test.Infrastructure.Builders;

namespace RetroAdventureCreator.Test.Facts.ServicesTests;

public class GameSerializerServiceTest
{
    [Fact]
    public void FlagsSerializerTest_Serialize_AsExpected()
    {
        // Arrange
        var builder = new GameInPawsTutorialBuilder();
        var game = builder.BuildGame();

        // Act
        var service = new GameSerializerService().Serialize(game);

        // Assert
    }
}
