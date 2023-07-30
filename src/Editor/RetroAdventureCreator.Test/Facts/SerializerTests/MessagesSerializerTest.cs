using RetroAdventureCreator.Core;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Serialization;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Infrastructure.Builders;

namespace RetroAdventureCreator.Test.Facts.SerializerTests;

public class MessagesSerializerTest
{
    [Fact]
    public void MessagesSerializer_Serialize_AsExpected()
    {
        // Arrange
        var headersLength = 3; // 3 bytes
        var builder = new GameInPawsTutorialBuilder();
        var game = builder.BuildGame();
        var indexes = builder.BuildGameComponentsIndexes();

        var messages = game.Assets.Messages;
        var headerLength = headersLength * messages.Count();
        var expectedDataLength = messages.Sum(item => item.Text.Length);

        // Act
        var actual = new MessagesSerializer(indexes).Serialize(messages);

        // Assert
        Assert.NotNull(actual);
        Assert.True(actual.Header.Length == headerLength);
        Assert.True(actual.Data.Length == expectedDataLength);
    }

    [Fact]
    public void MessagesSerializer_Serialize_MaxMessages_throwsExcepion()
    {
        // Arrange
        var indexes = new GameInPawsTutorialBuilder().BuildGameComponentsIndexes();
        var messageError = string.Format(Core.Properties.Resources.MaxLengthMessagesAllowedError, Constants.MaxLengthMessagesAllowed);
        var messages = Enumerable.Range(0, Constants.MaxLengthMessagesAllowed + 1).Select(item => new MessageModel());

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new MessagesSerializer(indexes).Serialize(messages)).Message == messageError);
    }

    [Fact]
    public void MessagesSerializer_Serialize_NullText_throwsExcepion()
    {
        // Arrange
        var indexes = new GameInPawsTutorialBuilder().BuildGameComponentsIndexes();
        var messageError = Core.Properties.Resources.TextIsRequiredError;
        var messages = Enumerable.Range(0, 1).Select(item => new MessageModel() { Code = "code1" });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new MessagesSerializer(indexes).Serialize(messages)).Message == messageError);
    }

    [Fact]
    public void MessagesSerializer_Serialize_MaxLengthText_throwsExcepion()
    {
        // Arrange        
        var indexes = new GameInPawsTutorialBuilder().BuildGameComponentsIndexes();
        var messageError = string.Format(RetroAdventureCreator.Core.Properties.Resources.MaxLengthMessageTextError, Constants.MaxLengthMessageTextAllowed);
        var messages = Enumerable.Range(0, 2).Select(item => new MessageModel() { Code = "code1", Text = new string('1', Constants.MaxLengthMessageTextAllowed + 1) });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new MessagesSerializer(indexes).Serialize(messages)).Message == messageError);
    }

    [Fact]
    public void MessagesSerializer_Serialize_CodeNull_throwsExcepion()
    {
        // Arrange        
        var indexes = new GameInPawsTutorialBuilder().BuildGameComponentsIndexes();
        var messageError = Core.Properties.Resources.CodeIsRequiredError;
        var messages = Enumerable.Range(0, 2).Select(item => new MessageModel() { });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new MessagesSerializer(indexes).Serialize(messages)).Message == messageError);
    }
}
