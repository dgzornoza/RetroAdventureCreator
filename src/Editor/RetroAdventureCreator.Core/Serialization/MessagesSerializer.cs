﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
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
/// Header:
/// Text: 8 bits (256)
/// 
/// Data:
/// Text = 0-255 bytes
/// 
/// </remarks>
internal class MessagesSerializer : ISerializer<IEnumerable<MessageModel>, SerializerResultKeyModel>
{
    private record struct Header(byte Text);

    private record struct Data(string Text);

    public SerializerResultKeyModel Serialize(IEnumerable<MessageModel> @object)
    {
        var messages = @object ?? throw new ArgumentNullException(nameof(@object));

        EnsureHelpers.EnsureMaxLength(messages, Constants.MaxNumberMessagesAllowed,
            string.Format(Properties.Resources.MaxNumberMessagesAllowedError, Constants.MaxNumberMessagesAllowed));

        var componentKeys = new List<GameComponentKeyModel>(messages.Count());
        var result = new List<byte>();
        foreach (var message in messages)
        {
            EnsureHelpers.EnsureNotNullOrWhiteSpace(message.Code, Properties.Resources.CodeIsRequiredError);
            EnsureHelpers.EnsureNotFound(componentKeys, item => item.Code == message.Code, string.Format(Properties.Resources.DuplicateCodeError, message.Code));

            componentKeys.Add(new GameComponentKeyModel(message.Code, result.Count));

            var header = new Header((byte)message.Text.Length);
            result.AddRange(CreateHeaderBytes(header));

            var data = new Data(message.Text);
            result.AddRange(CreateDataBytes(data));
        }

        return new SerializerResultKeyModel(componentKeys, result.ToArray());
    }

    private static byte[] CreateHeaderBytes(Header header) => new byte[]
    {
            header.Text,
    };

    private static byte[] CreateDataBytes(Data data) => Encoding.ASCII.GetBytes(data.Text);
}
