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
        var game = new GameInPawsTutorialBuilder().BuildGame();

        var messages = game.Assets.Messages;
        var headerLength = headersLength * messages.Count();
        var expectedDataLength = messages.Sum(item => item.Text.Length);
        var expectedCount = messages.Count();

        // Act
        var actual = new MessagesSerializer().Serialize(messages);

        // Assert
        Assert.NotNull(actual);

        Assert.NotNull(actual.GameComponentKeysModel);
        Assert.True(actual.GameComponentKeysModel.Count() == expectedCount);
        Assert.True(actual.GameComponentKeysModel.Select(item => item.Code).Distinct().Count() == expectedCount);
        Assert.True(actual.Header.Length == headerLength);
        Assert.True(actual.Data.Length == expectedDataLength);
    }

    [Fact]
    public void MessagesSerializer_Serialize_MaxMessages_throwsExcepion()
    {
        // Arrange
        var messageError = string.Format(Core.Properties.Resources.MaxLengthMessagesAllowedError, Constants.MaxNumberMessagesAllowed);
        var messages = Enumerable.Range(0, Constants.MaxNumberMessagesAllowed + 1).Select(item => new MessageModel());

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new MessagesSerializer().Serialize(messages)).Message == messageError);
    }

    [Fact]
    public void MessagesSerializer_Serialize_DuplicateCode_throwsExcepion()
    {
        // Arrange
        var code = "DuplicateCode";
        var messageError = string.Format(Core.Properties.Resources.DuplicateCodeError, code);
        var messages = Enumerable.Range(0, 2).Select(item => new MessageModel() { Code = code, Text = "MessageText" });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new MessagesSerializer().Serialize(messages)).Message == messageError);
    }

    [Fact]
    public void MessagesSerializer_Serialize_NullText_throwsExcepion()
    {
        // Arrange
        var messageError = Core.Properties.Resources.TextIsRequiredError;
        var messages = Enumerable.Range(0, 1).Select(item => new MessageModel() { Code = "code1" });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new MessagesSerializer().Serialize(messages)).Message == messageError);
    }

    [Fact]
    public void MessagesSerializer_Serialize_MaxLengthText_throwsExcepion()
    {
        // Arrange        
        var messageError = string.Format(RetroAdventureCreator.Core.Properties.Resources.MaxLengthMessageTextError, Constants.MaxLengthMessageTextAllowed);
        var messages = Enumerable.Range(0, 2).Select(item => new MessageModel() { Code = "code1", Text = new string('1', Constants.MaxLengthMessageTextAllowed + 1) });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new MessagesSerializer().Serialize(messages)).Message == messageError);
    }

    [Fact]
    public void MessagesSerializer_Serialize_CodeNull_throwsExcepion()
    {
        // Arrange        
        var messageError = Core.Properties.Resources.CodeIsRequiredError;
        var messages = Enumerable.Range(0, 2).Select(item => new MessageModel() { });

        // Act && Assert
        Assert.True(Assert.Throws<InvalidOperationException>(() => new MessagesSerializer().Serialize(messages)).Message == messageError);
    }
}
