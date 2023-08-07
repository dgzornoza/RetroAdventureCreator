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
internal class MessagesSerializer : ISerializer<IEnumerable<MessageModel>>
{
    private record struct Header(byte TextLenght, short DataAddress);

    private record struct Data(string Text);

    public IEnumerable<GameComponentPointerModel> GenerateGameComponentKeys(IEnumerable<MessageModel> messages)
    {
        throw new NotImplementedException();
    }

    public SerializerResultModel Serialize(GameComponentsPointers gameComponentsIndexes, IEnumerable<MessageModel> messages)
    {
        EnsureHelpers.EnsureMaxLength(messages, Constants.MaxLengthMessagesAllowed,
            string.Format(Properties.Resources.MaxLengthMessagesAllowedError, Constants.MaxLengthMessagesAllowed));

        var headerBytes = new List<byte>();
        var dataBytes = new List<byte>();

        foreach (var message in messages.SortByKey())
        {
            EnsureHelpers.EnsureNotNullOrWhiteSpace(message.Code, Properties.Resources.CodeIsRequiredError);
            EnsureHelpers.EnsureNotNullOrWhiteSpace(message.Text, Properties.Resources.TextIsRequiredError);
            EnsureHelpers.EnsureMaxLength(message.Text.Length, Constants.MaxLengthMessageTextAllowed,
                string.Format(Properties.Resources.MaxLengthMessageTextError, Constants.MaxLengthMessageTextAllowed));

            var header = new Header((byte)message.Text.Length, (short)dataBytes.Count);
            headerBytes.AddRange(CreateHeaderBytes(header));

            var data = new Data(message.Text);
            dataBytes.AddRange(CreateDataBytes(data));
        }

        return new SerializerResultModel(headerBytes.ToArray(), dataBytes.ToArray());
    }

    private static byte[] CreateHeaderBytes(Header header) => new byte[]
    {
        header.TextLenght,
        header.DataAddress.GetByte(2),
        header.DataAddress.GetByte(1),
    };

    private static byte[] CreateDataBytes(Data data) => Encoding.ASCII.GetBytes(data.Text);
}
