using System.Text;
using RetroAdventureCreator.Core;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;
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

        var expectedDataLength = game.Messages.SortByKey().Sum(item => item.Text.Length + Constants.EndTokenLength);
        var expectedBytes = encoding.GetBytes(string.Join("\0", game.Messages.SortByKey().Select(item => item.Text)) + "\0");
        var expectedText = encoding.GetString(expectedBytes);

        // Act
        var actual = serializerFactory.Serialize<MessagesSerializer>();

        // Assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Data);
        Assert.True(actual.Data.Length == expectedDataLength);
        Assert.Equal(expectedText, encoding.GetString(actual.Data));
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
