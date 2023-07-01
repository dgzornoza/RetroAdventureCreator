using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using RetroAdventureCreator.Core.Helpers;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;



//public VocabularyModel Name { get; init; } = default!;
//public MessageModel Description { get; init; } = default!;
//public int Weight { get; init; } = 0;
//public int Health { get; init; } = 7;
//public ObjectProperties Properties { get; init; }
//public IEnumerable<ObjectModel>? ChildObjects { get; init; }
//public IEnumerable<ObjectModel>? RequiredComplements { get; init; }
//public IEnumerable<ObjectModel>? Complements { get; init; }

internal record ObjectsSerializerArguments(IEnumerable<ObjectModel> Objects, SerializerResultKeyModel VocabularySerialized, SerializerResultKeyModel MessagesSerialized);

/// <summary>
/// Object model serializer
/// </summary>
/// <remarks>
/// Format Object serializer:
/// ----------------------------------------------
/// 
/// Header:
/// Name = 8 bits (256 ids vocabulary)
/// Description Size = 8 bits (256)
/// Weight = 5 bits (32)
/// Health = 3 bits (8)
/// Properties = 8 bits (flag 8 properties)
/// ChildObjects = 4 bits (15 ids 2 bytes, relative address Objects) 2 bits (15 ids 2 bytes, absolute address)
/// RequiredComplements = 3 bits (8 ids Objects)
/// Complements = 3 bits (8 ids Objects)
/// 
/// Data:
/// Description = 0-256 bytes
/// ChildObjects = 0-30 bytes
/// RequiredComplements = 0-40 bytes
/// Complements = 0-40 bytes
/// 
/// </remarks>
internal class ObjectsSerializer : ISerializer<ObjectsSerializerArguments, SerializerResultKeyModel>
{
    private record struct Header(byte WordType, byte Synonyms);

    private record struct Data(string Synonyms);

    public SerializerResultKeyModel Serialize(ObjectsSerializerArguments arguments)
    {
        var objects = arguments.Objects ?? throw new ArgumentNullException(nameof(arguments.Objects));

        EnsureHelpers.EnsureMaxLength(objects, Constants.MaxNumberVocabulariesAllowed,
            string.Format(Properties.Resources.MaxNumberVocabularyAllowedError, Constants.MaxNumberVocabulariesAllowed));

        var componentKeys = new List<GameComponentKeyModel>(objects.Count());
        var result = new List<byte>();
        foreach (var @object in objects)
        {
            EnsureHelpers.EnsureNotNullOrWhiteSpace(@object.Code, Properties.Resources.CodeIsRequiredError);
            EnsureHelpers.EnsureNotFound(componentKeys, item => item.Code == @object.Code, string.Format(Properties.Resources.DuplicateCodeError, @object.Code));

            componentKeys.Add(new GameComponentKeyModel(@object.Code, result.Count));

            //var header = new Header((byte)@object.WordType, (byte)synonyms.Length);
            //result.AddRange(CreateHeaderBytes(header));

            //var data = new Data(synonyms);
            //result.AddRange(CreateDataBytes(data));
        }

        return new SerializerResultKeyModel(componentKeys, result.ToArray());
    }

    private static byte[] CreateHeaderBytes(Header header) => new byte[]
    {
                header.WordType,
                header.Synonyms,
    };

    private static byte[] CreateDataBytes(Data data) => Encoding.ASCII.GetBytes(data.Synonyms);
}
