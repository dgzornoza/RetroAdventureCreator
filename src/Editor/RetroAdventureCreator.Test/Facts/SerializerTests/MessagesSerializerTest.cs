using System.Text;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Helpers;
using RetroAdventureCreator.Test.Infrastructure.Builders;

namespace RetroAdventureCreator.Test.Facts.SerializerTests;

public class MessagesSerializerTest : SerializerBaseTest
{
    private static readonly Encoding encoding = Encoding.ASCII;

    [Fact]
    public void MessagesSerializer_Serialize_AsExpected()
    {
        // Arrange
        CreateGame<GameInPawsTutorialBuilder>();
        var serializerFactory = new SerializerFactory(game);

        var gameMessages = game.Messages.SortByKey();
        var expectedDataLength = gameMessages.Sum(item => item.Text.Length);
        var expectedBytes = encoding.GetBytes(string.Join(string.Empty, gameMessages.Select(item => item.Text)));
        var expectedText = encoding.GetString(expectedBytes);

        // Act
        var actual = serializerFactory.Serialize<MessagesSerializer>();
        var splitedData = serializerFactory.GameComponentsPointersModel.Messages.SplitData(actual.Data);

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Data);
        Assert.True(actual.Data.Length == expectedDataLength);
        Assert.Equal(expectedText, encoding.GetString(actual.Data));

        for (int i = 0; i < gameMessages.Count(); i++)
        {
            var asciiText = encoding.GetString(encoding.GetBytes(gameMessages.ElementAt(i).Text));
            Assert.Equal(asciiText, encoding.GetString(splitedData.ElementAt(i)));
        }
    }

    [Fact]
    public void MessagesSerializer_MaxMessages_throwsExcepion()
    {
        // Arrange
        CreateGame<GameMaxLengthLimitsBuilder>();
        var messageError = string.Format(Core.Properties.Resources.MaxLengthMessagesAllowedError, Constants.MaxLengthMessagesAllowed);

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new MessagesSerializer(game.Messages, encoding)).Message == messageError);
    }

    [Fact]
    public void MessagesSerializer_GenerateGameComponentPointers_NullText_throwsExcepion()
    {
        // Arrange
        var messageError = Core.Properties.Resources.TextIsRequiredError;
        var messages = Enumerable.Range(0, 1).Select(item => new MessageModel() { Code = "code1" });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new MessagesSerializer(messages, encoding).GenerateGameComponentPointers()).Message == messageError);
    }

    [Fact]
    public void MessagesSerializer_GenerateGameComponentPointers_CodeNull_throwsExcepion()
    {
        // Arrange        
        CreateGame<GameCodeNullBuilder>();

        var messageError = Core.Properties.Resources.CodeIsRequiredError;

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new MessagesSerializer(game.Messages, encoding).GenerateGameComponentPointers()).Message == messageError);
    }
}
