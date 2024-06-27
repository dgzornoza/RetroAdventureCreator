using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Helpers;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

/// <summary>
/// Object model serializer
/// </summary>
/// <remarks>
/// Format Object serializer:
/// ----------------------------------------------
/// 
/// Data:
/// Name = index orderer addresses nouns vocabulary (1 byte)
/// Description = Message pointer (2 bytes)
/// Weight = 5 bits (31)
/// Health = 3 bits (7)
/// Properties = 8 bits (8 properties) (mutable properties)
/// ChildObjects = (Optional, only if properties has 'IsContainer') 8 object id bytes
/// 
/// </remarks>
internal class ObjectsSerializer : Serializer<IEnumerable<ObjectModel>>
{
    public ObjectsSerializer(IEnumerable<ObjectModel> gameComponent) : base(gameComponent)
    {
        EnsureHelpers.EnsureMaxLength(GameComponent, Constants.MaxLengthObjectsAllowed,
            string.Format(Properties.Resources.MaxLengthObjectsAllowedError, Constants.MaxLengthObjectsAllowed));
    }

    public override IEnumerable<GameComponentPointerModel> GenerateGameComponentPointers()
    {
        var result = new List<GameComponentPointerModel>();
        var pointer = 0;

        foreach (var @object in GameComponent.SortByKey())
        {
            if (pointer > short.MaxValue)
                throw new InvalidOperationException(string.Format(Properties.Resources.MaxPointerExceededError, nameof(CommandsSerializer)));

            EnsureGameComponentProperties(@object, result);

            result.Add(new GameComponentPointerModel(@object.Code, (short)pointer));

            pointer +=
                1 + // Name
                1 + // Description
                1 + // (Weight, Health)
                1 + // Properties
                (@object.Properties.HasFlag(ObjectProperties.IsContainer) ? 8 : 0); // ChildObjects (optional)
        }

        return result;
    }

    public override SerializerResultModel Serialize(GameComponentsPointersModel gameComponentsIndexes)
    {
        var dataBytes = GameComponent.SortByKey().SelectMany(item => CreateDataBytes(item, gameComponentsIndexes));
        return new SerializerResultModel(dataBytes.ToArray());
    }

    private static byte[] CreateDataBytes(ObjectModel @object, GameComponentsPointersModel gameComponentsIndexes)
    {
        var result = new List<byte>
        {
            // description
            gameComponentsIndexes.VocabularyNouns.IndexOf(@object.Name.Code),
            gameComponentsIndexes.Messages.IndexOf(@object.Description.Code),
            (byte)(@object.Weight << 3 | @object.Health),
            (byte)@object.Properties,
        };

        // ChildObjects        
        if (@object.Properties.HasFlag(ObjectProperties.IsContainer))
        {
            if (@object.ChildObjects != null && @object.ChildObjects.Any())
            {
                result.AddRange(@object.ChildObjects.Select(item => gameComponentsIndexes.Objects.IndexOf(item.Code)));
            }
            var objectsCount = @object.ChildObjects?.Count() ?? 0;
            if (objectsCount < Constants.MaxLengthChildObjectsAllowed)
            {
                result.AddRange(Enumerable.Range(0, Constants.MaxLengthChildObjectsAllowed - objectsCount).Select(item => (byte)0x00));
            }
        }

        return result.ToArray();
    }

    private static void EnsureGameComponentProperties(ObjectModel @object, IEnumerable<GameComponentPointerModel> gameComponentPointers)
    {
        EnsureHelpers.EnsureNotFound(gameComponentPointers, item => item.Code == @object.Code, string.Format(Properties.Resources.DuplicateCodeError, @object.Code));
        EnsureHelpers.EnsureNotNullOrWhiteSpace(@object.Code, Properties.Resources.CodeIsRequiredError);

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
    }
}
