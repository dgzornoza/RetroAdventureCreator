using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Helpers;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

internal record ObjectsSerializerArguments(IEnumerable<ObjectModel> Objects, SerializerResultKeyModel VocabularyNounsSerialized, SerializerResultKeyModel MessagesSerialized);

/// <summary>
/// Object model serializer
/// </summary>
/// <remarks>
/// Format Object serializer:
/// ----------------------------------------------
/// 
/// Header: (7 bytes)
/// Name = 8 bits (id vocabulary)
/// Description Size = 8 bits (id message)
/// Weight = 5 bits (32)
/// Health = 3 bits (8)
/// Properties = 8 bits (flag 8 properties)
/// ChildObjects = 4 bits (15 ids objects in data)
/// RequiredComplements = 2 bits (3 ids objects in data)
/// Complements = 2 bits (3 ids objects in data)
/// DataAdress = 2 bytes
/// 
/// Data:
/// ChildObjects = 0-15 bytes
/// RequiredComplements = 0-3 bytes
/// Complements = 0-3 bytes
/// 
/// </remarks>
internal class ObjectsSerializer : ISerializer<ObjectsSerializerArguments, SerializerResultKeyModel>
{
    private record struct Header(byte NameIndex, byte DescriptionIndex, byte Weight, byte Health, byte Properties, byte ChildObjects, byte RequiredComplements, byte Complements, short DataAddress);

    private record struct Data(IEnumerable<byte>? ChildObjectsIndexes, IEnumerable<byte>? RequiredComplementsIndexes, IEnumerable<byte>? ComplementsIndexes);

    public SerializerResultKeyModel Serialize(ObjectsSerializerArguments arguments)
    {
        var objects = arguments.Objects ?? throw new ArgumentNullException(nameof(arguments.Objects));
        var vocabularySerialized = arguments.VocabularyNounsSerialized ?? throw new InvalidOperationException(nameof(arguments.VocabularyNounsSerialized));
        var messagesSerialized = arguments.MessagesSerialized ?? throw new InvalidOperationException(nameof(arguments.MessagesSerialized));

        EnsureHelpers.EnsureMaxLength(objects, Constants.MaxNumberObjectsAllowed,
            string.Format(Properties.Resources.MaxLengthObjectsAllowedError, Constants.MaxNumberObjectsAllowed));

        var componentKeys = objects.Select((item, index) => new GameComponentKeyModel(item.Code, index));

        var headerBytes = new List<byte>();
        var dataBytes = new List<byte>();

        foreach (var @object in objects)
        {
            EnsureObjectProperties(@object, componentKeys);

            var header = new Header()
            {
                NameIndex = (byte)vocabularySerialized.GameComponentKeysModel.Find(@object.Name.Code).HeaderIndex,
                DescriptionIndex = (byte)messagesSerialized.GameComponentKeysModel.Find(@object.Description.Code).HeaderIndex,
                Weight = (byte)@object.Weight,
                Health = (byte)@object.Health,
                Properties = (byte)@object.Properties,
                ChildObjects = (byte)(@object.ChildObjects?.Count() ?? 0),
                RequiredComplements = (byte)(@object.RequiredComplements?.Count() ?? 0),
                Complements = (byte)(@object.Complements?.Count() ?? 0),
                DataAddress = (short)dataBytes.Count,
            };

            headerBytes.AddRange(CreateHeaderBytes(header));

            var childObjectIndexes = @object.ChildObjects?.Select(item => (byte)componentKeys.Find(item.Code).HeaderIndex);
            var requiredComplementsIndexes = @object.RequiredComplements?.Select(item => (byte)componentKeys.Find(item.Code).HeaderIndex);
            var complementsIndexes = @object.Complements?.Select(item => (byte)componentKeys.Find(item.Code).HeaderIndex);

            var data = new Data(childObjectIndexes, requiredComplementsIndexes, complementsIndexes);
            dataBytes.AddRange(CreateDataBytes(data));
        }

        return new SerializerResultKeyModel(componentKeys, headerBytes.ToArray(), dataBytes.ToArray());
    }

    private static void EnsureObjectProperties(ObjectModel @object, IEnumerable<GameComponentKeyModel> componentKeys)
    {
        EnsureHelpers.EnsureNotNullOrWhiteSpace(@object.Code, Properties.Resources.CodeIsRequiredError);
        EnsureHelpers.EnsureSingle(componentKeys, item => item.Code == @object.Code, string.Format(Properties.Resources.DuplicateCodeError, @object.Code));

        EnsureHelpers.EnsureNotNull(@object.Name, Properties.Resources.NameIsRequiredError);
        EnsureHelpers.EnsureNotNull(@object.Description, Properties.Resources.DescriptionIsRequiredError);

        EnsureHelpers.EnsureMaxLength(@object.Weight, Constants.MaxLengthObjectWeightAllowed, 
            string.Format(Properties.Resources.MaxLengthObjectWeightError, Constants.MaxLengthObjectWeightAllowed));
        EnsureHelpers.EnsureMaxLength(@object.Health, Constants.MaxLengthObjectHealthAllowed,
            string.Format(Properties.Resources.MaxLengthObjectHealthError, Constants.MaxLengthObjectHealthAllowed));

        if (@object.ChildObjects != null)
        {
            EnsureHelpers.EnsureMaxLength(@object.ChildObjects, Constants.MaxLengthChildObjectsAllowed,
                string.Format(Properties.Resources.MaxLengthChildObjectsAllowedError, Constants.MaxLengthChildObjectsAllowed));
        }

        if (@object.RequiredComplements != null)
        {
            EnsureHelpers.EnsureMaxLength(@object.RequiredComplements, Constants.MaxLengthRequiredComplementsAllowed,
                string.Format(Properties.Resources.MaxLengthRequiredComplementsAllowedError, Constants.MaxLengthRequiredComplementsAllowed));
        }

        if (@object.Complements != null)
        {
            EnsureHelpers.EnsureMaxLength(@object.Complements, Constants.MaxLengthComplementsAllowed,
                string.Format(Properties.Resources.MaxLengthComplementsAllowedError, Constants.MaxLengthComplementsAllowed));
        }
    }

    /// </summary>
    /// <param name="header"></param>
    /// <returns></returns>
    private static byte[] CreateHeaderBytes(Header header) => new byte[]
    {
        header.NameIndex,
        header.DescriptionIndex,
        (byte)(header.Weight << 3 | header.Health),
        header.Properties,
        (byte)(header.ChildObjects << 4 | header.RequiredComplements << 2 | header.Complements),
        header.DataAddress.GetByte(2),
        header.DataAddress.GetByte(1),
    };

    private static byte[] CreateDataBytes(Data data)
    {
        var result = new List<byte>();
        if (data.ChildObjectsIndexes != null)
        {
            result.AddRange(data.ChildObjectsIndexes);
        }
        if (data.RequiredComplementsIndexes != null)
        {
            result.AddRange(data.RequiredComplementsIndexes);
        }
        if (data.ComplementsIndexes != null)
        {
            result.AddRange(data.ComplementsIndexes);
        }
        
        return result.ToArray();
    }
}
