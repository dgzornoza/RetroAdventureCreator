using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Helpers;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Enums;
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
/// Text: Mesage text bytes (end with 0x00)
/// 
/// </remarks>
internal class MessagesSerializer : Serializer<IEnumerable<MessageModel>>
{
    public MessagesSerializer(IEnumerable<MessageModel> gameComponent) : base(gameComponent)
    {
        EnsureHelpers.EnsureMaxLength(GameComponent, Constants.MaxLengthMessagesAllowed,
            string.Format(Properties.Resources.MaxLengthMessagesAllowedError, Constants.MaxLengthMessagesAllowed));
    }

    public override IEnumerable<GameComponentPointerModel> GenerateGameComponentPointers()
    {
        var result = new List<GameComponentPointerModel>();
        var pointer = 0;

        foreach (var message in GameComponent.SortByKey())
        {
            EnsureGameComponentProperties(message, result);

            result.Add(new GameComponentPointerModel(message.Code, pointer));

            pointer += (message.Text).Length + Constants.EndTokenLength;
        }

        return result;
    }

    public override SerializerResultModel Serialize(GameComponentsPointersModel gameComponentsIndexes)
    {
        var dataBytes = GameComponent.SortByKey().SelectMany(CreateDataBytes);
        return new SerializerResultModel(dataBytes.ToArray());
    }

    private byte[] CreateDataBytes(MessageModel message) => SerializerEncoding.GetBytes(message.Text).Concat(new byte[] { Constants.EndToken }).ToArray();

    private static void EnsureGameComponentProperties(MessageModel message, IEnumerable<GameComponentPointerModel> gameComponentPointers)
    {
        EnsureHelpers.EnsureNotFound(gameComponentPointers, item => item.Code == message.Code, string.Format(Properties.Resources.DuplicateCodeError, message.Code));
        EnsureHelpers.EnsureNotNullOrWhiteSpace(message.Code, Properties.Resources.CodeIsRequiredError);

        EnsureHelpers.EnsureNotNullOrEmpty(message.Text, Properties.Resources.TextIsRequiredError);
        EnsureHelpers.EnsureNotFound(message.Text, item => item == Constants.EndToken, Properties.Resources.StringEndCharDuplicatedError);
    }
}
