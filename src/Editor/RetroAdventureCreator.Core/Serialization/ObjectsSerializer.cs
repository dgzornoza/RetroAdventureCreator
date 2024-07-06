using System.Reflection;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Helpers;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

/// <summary>
/// Base Object model serializer
/// </summary>
internal abstract class ObjectsSerializer<T> : SerializerList<T> where T : ObjectModel
{
    public ObjectsSerializer(IEnumerable<T> gameComponent) : base(gameComponent)
    {
        EnsureHelpers.EnsureMaxLength(GameComponent, Constants.MaxLengthActorsAllowed,
            string.Format(Properties.Resources.MaxLengthObjectsAllowedError, Constants.MaxLengthObjectsAllowed));
    }

    public override IEnumerable<GameComponentPointerModel> GenerateGameComponentPointers()
    {
        var result = new List<GameComponentPointerModel>();
        var pointer = 0;

        foreach (var @object in GameComponent)
        {
            if (pointer > short.MaxValue)
                throw new InvalidOperationException(string.Format(Properties.Resources.MaxPointerExceededError, nameof(CommandsSerializer)));

            EnsureGameComponentProperties(@object, result);

            result.Add(new GameComponentPointerModel(@object.Code, (short)pointer));

            pointer += GetPointerSize();
        }

        // add end objects pointer
        result.Add(new GameComponentPointerModel(Constants.EndComponentPointerCode, (short)pointer));

        return result;
    }

    public override SerializerResultModel Serialize(GameComponentsPointersModel gameComponentsIndexes)
    {
        var dataBytes = GameComponent.SelectMany(item => CreateDataBytes(item, gameComponentsIndexes));
        return new SerializerResultModel(dataBytes.ToArray());
    }

    protected virtual int GetPointerSize() => 
        1 + // Name
        2 + // Description
        1 + // Properties
        2;  // OwnerCode

    protected virtual byte[] CreateDataBytes(T @object, GameComponentsPointersModel gameComponentsPointers)
    {
        var result = new List<byte>
        {
            gameComponentsPointers.VocabularyNouns.IndexOf(@object.Name.Code).ToBaseZero()
        };

        result.AddRange(gameComponentsPointers.Messages.Find(@object.Description.Code).RelativePointer.GetBytes());
        result.Add((byte)@object.Properties);
        result.AddRange(FindOwnerPointer(@object, gameComponentsPointers));

        return result.ToArray();
    }

    protected virtual void EnsureGameComponentProperties(T @object, IEnumerable<GameComponentPointerModel> gameComponentPointers)
    {
        EnsureHelpers.EnsureNotFound(gameComponentPointers, item => item.Code == @object.Code, string.Format(Properties.Resources.DuplicateCodeError, @object.Code));
        EnsureHelpers.EnsureNotNullOrWhiteSpace(@object.Code, Properties.Resources.CodeIsRequiredError);

        EnsureHelpers.EnsureNotNull(@object.Name, string.Format(Properties.Resources.NameIsRequiredError, @object.Code));
        EnsureHelpers.EnsureNotNull(@object.Description, string.Format(Properties.Resources.DescriptionIsRequiredError, @object.Code));
        EnsureHelpers.EnsureNotNull(@object.OwnerCode, string.Format(Properties.Resources.OwnerIsRequiredError, @object.Code));
    }

    private static byte[] FindOwnerPointer(T @object, GameComponentsPointersModel gameComponentsPointers)
    {
        var ownerComponents = new List<GameComponentPointerModel>();
        ownerComponents.AddRange(gameComponentsPointers.Actors.Where(item => item.Code != Constants.EndComponentPointerCode));
        ownerComponents.AddRange(gameComponentsPointers.Scenes.Where(item => item.Code != Constants.EndComponentPointerCode));
        ownerComponents.AddRange(gameComponentsPointers.Objects
            .Where(item => item.Code != Constants.EndComponentPointerCode && item.Code != @object.Code));


        EnsureHelpers.EnsureNotFound(ownerComponents, item => item.Code == @object.Code, string.Format(Properties.Resources.DuplicatedOwnerCodeError, @object.Code));

        return ownerComponents.Find(@object.OwnerCode).RelativePointer.GetBytes();
    }
}

/// <summary>
/// Object model serializer
/// </summary>
/// <remarks>
/// Format Object serializer:
/// ----------------------------------------------
/// 
/// Data :
/// Name = index orderer addresses nouns vocabulary (1 byte)
/// Description = Message pointer (2 bytes)
/// Properties = 1 byte (8 properties flags) (mutable properties)
/// OwnerCode = 2 bytes
/// 
/// </remarks>
internal class ObjectsSerializer : ObjectsSerializer<ObjectModel>
{
    public ObjectsSerializer(IEnumerable<ObjectModel> gameComponent) :
        base(gameComponent)
    {
    }
}

/// <summary>
/// TODO: currently not implemented in game engine
/// Complex Object model serializer
/// </summary>
/// <remarks>
/// Format Object serializer:
/// ----------------------------------------------
/// 
/// Data :
/// NormalObjectsSerializer Data +
/// 
/// Weight + Health = 1 byte
/// </remarks>
//internal class ComplexObjectSerializer : ObjectsSerializer<ComplexObjectModel>
//{
//    public ComplexObjectSerializer(IEnumerable<ComplexObjectModel> gameComponent) :
//        base(gameComponent)
//    {
//    }

//    protected override int GetPointerSize() =>
//        base.GetPointerSize() +
//        1; // Weight + Health

//    protected override byte[] CreateDataBytes(ComplexObjectModel @object, GameComponentsPointersModel gameComponentsIndexes)
//    {
//        var bytes = base.CreateDataBytes(@object, gameComponentsIndexes).ToList();
//        bytes.Add((byte)(@object.Weight << 3 | @object.Health));

//        return bytes.ToArray();
//    }

//    protected override void EnsureGameComponentProperties(ComplexObjectModel @object, IEnumerable<GameComponentPointerModel> gameComponentPointers)
//    {
//        base.EnsureGameComponentProperties(@object, gameComponentPointers);

//        EnsureHelpers.EnsureMaxLength(@object.Weight, Constants.MaxLengthObjectWeightAllowed,
//            string.Format(Properties.Resources.MaxLengthObjectWeightError, Constants.MaxLengthObjectWeightAllowed));
//        EnsureHelpers.EnsureMaxLength(@object.Health, Constants.MaxLengthObjectHealthAllowed,
//            string.Format(Properties.Resources.MaxLengthObjectHealthError, Constants.MaxLengthObjectHealthAllowed));
//    }
//}
