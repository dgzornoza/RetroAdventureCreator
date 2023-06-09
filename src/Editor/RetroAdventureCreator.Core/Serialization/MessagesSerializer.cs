﻿using System;
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
/// Header: (3 bytes)
/// Text: 8 bits
/// DataAdress = 2 bytes
/// 
/// Data:
/// Text = 0-255 bytes
/// 
/// </remarks>
internal class MessagesSerializer : Serializer<IEnumerable<MessageModel>>
{
    private record struct Header(byte TextLenght, short DataAddress);

    private record struct Data(string Text);

    public MessagesSerializer(GameComponentsIndexes gameComponentsIndexes) : base(gameComponentsIndexes)
    {
    }

    public override SerializerResultModel Serialize(IEnumerable<MessageModel> messages)
    {
        EnsureHelpers.EnsureMaxLength(messages, Constants.MaxLengthMessagesAllowed,
            string.Format(Properties.Resources.MaxLengthMessagesAllowedError, Constants.MaxLengthMessagesAllowed));

        var componentKeys = new List<GameComponentKeyModel>(messages.Count());
        var headerBytes = new List<byte>();
        var dataBytes = new List<byte>();

        foreach (var message in messages)
        {
            EnsureHelpers.EnsureNotNullOrWhiteSpace(message.Code, Properties.Resources.CodeIsRequiredError);
            EnsureHelpers.EnsureNotFound(componentKeys, item => item.Code == message.Code, string.Format(Properties.Resources.DuplicateCodeError, message.Code));
            EnsureHelpers.EnsureNotNullOrWhiteSpace(message.Text, Properties.Resources.TextIsRequiredError);
            EnsureHelpers.EnsureMaxLength(message.Text.Length, Constants.MaxLengthMessageTextAllowed,
                string.Format(Properties.Resources.MaxLengthMessageTextError, Constants.MaxLengthMessageTextAllowed));

            componentKeys.Add(new GameComponentKeyModel(message.Code, componentKeys.Count));

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
