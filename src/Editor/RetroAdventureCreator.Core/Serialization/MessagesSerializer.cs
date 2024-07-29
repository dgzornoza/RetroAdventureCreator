using System.Text;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Helpers;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

/// <summary>
/// Message model serializer
/// </summary>
/// <remarks>
/// Format Message serializer:
/// ----------------------------------------------
/// 
/// Data:
/// Text: Mesage text bytes
/// 
/// </remarks>
internal class MessagesSerializer : SerializerList<MessageModel>
{
    private readonly Encoding encoding;

    public MessagesSerializer(IEnumerable<MessageModel> gameComponent, Encoding encoding) : base(gameComponent)
    {
        EnsureHelpers.EnsureMaxLength(GameComponent, Constants.MaxLengthMessagesAllowed,
            string.Format(Properties.Resources.MaxLengthMessagesAllowedError, Constants.MaxLengthMessagesAllowed));

        this.encoding = encoding;
    }

    public override IEnumerable<GameComponentPointerModel> GenerateGameComponentPointers()
    {
        var result = new List<GameComponentPointerModel>();
        var pointer = 0;

        foreach (var message in GameComponent)
        {
            if (pointer > short.MaxValue)
                throw new InvalidOperationException(string.Format(Properties.Resources.MaxPointerExceededError, nameof(CommandsSerializer)));

            EnsureGameComponentProperties(message, result);

            result.Add(new GameComponentPointerModel(message.Code, (short)pointer));

            pointer += (message.Text).Length;
        }

        // add end messages pointer
        result.Add(new GameComponentPointerModel(Constants.EndComponentPointerCode, (short)pointer));

        return result;
    }

    public override SerializerResultModel Serialize(GameComponentsPointersModel gameComponentsPointers)
    {
        var dataBytes = GameComponent.SelectMany(CreateDataBytes);
        return new SerializerResultModel(dataBytes.ToArray());
    }

    private byte[] CreateDataBytes(MessageModel message) => encoding.GetBytes(message.Text).
        Concat(new[] { Constants.EndToken }).ToArray();

    private static void EnsureGameComponentProperties(MessageModel message, IEnumerable<GameComponentPointerModel> gameComponentPointers)
    {
        EnsureHelpers.EnsureNotFound(gameComponentPointers, item => item.Code == message.Code, string.Format(Properties.Resources.DuplicateCodeError, message.Code));
        EnsureHelpers.EnsureNotNullOrWhiteSpace(message.Code, Properties.Resources.CodeIsRequiredError);

        EnsureHelpers.EnsureNotNullOrEmpty(message.Text, Properties.Resources.TextIsRequiredError);
    }
}
