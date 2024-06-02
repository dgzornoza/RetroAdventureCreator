using System.Text;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Helpers;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Core.Models;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Serialization;

/// <summary>
/// Vocabulary model serializer
/// </summary>
/// <remarks>
/// Format Vocabulary serializer:
/// ----------------------------------------------
/// 
/// Data:
/// Synonyms = synonym bytes splitted by '|'
/// 
/// NOTE: This component use Orderer addresses with ponters to data, should be use index to orderer addresses for find vocabulary data.
/// </remarks>
internal abstract class VocabularySerializer : Serializer<IEnumerable<VocabularyModel>>
{
    private readonly Encoding encoding;

    protected VocabularySerializer(IEnumerable<VocabularyModel> gameComponent, Encoding encoding) : base(gameComponent)
    {
        this.encoding = encoding;
    }

    public override IEnumerable<GameComponentPointerModel> GenerateGameComponentPointers()
    {
        var result = new List<GameComponentPointerModel>();
        var pointer = 0;

        foreach (var vocabulary in GameComponent.SortByKey())
        {
            EnsureGameComponentProperties(vocabulary, result);

            result.Add(new GameComponentPointerModel(vocabulary.Code, pointer));

            pointer += encoding.GetBytes(JoinSynonyms(vocabulary)).Length;
        }

        // add end vocabulary pointer
        result.Add(new GameComponentPointerModel(Constants.EndComponentPointerCode, pointer));

        return result;
    }

    public override SerializerResultModel Serialize(GameComponentsPointersModel gameComponentsIndexes)
    {
        var dataBytes = GameComponent.SortByKey().SelectMany(CreateDataBytes);
        return new SerializerResultModel(dataBytes.ToArray());
    }

    protected byte[] CreateDataBytes(VocabularyModel vocabulary) => encoding.GetBytes(JoinSynonyms(vocabulary));

    private void EnsureGameComponentProperties(VocabularyModel vocabulary, IEnumerable<GameComponentPointerModel> gameComponentPointers)
    {
        EnsureHelpers.EnsureNotFound(gameComponentPointers, item => item.Code == vocabulary.Code, string.Format(Properties.Resources.DuplicateCodeError, vocabulary.Code));
        EnsureHelpers.EnsureNotNullOrWhiteSpace(vocabulary.Code, Properties.Resources.CodeIsRequiredError);

        EnsureHelpers.EnsureNotNullOrEmpty(vocabulary.Synonyms, Properties.Resources.SysnonymsAreRequiredError);
        EnsureHelpers.EnsureMaxLength(vocabulary.Synonyms.Count(), Constants.MaxLengthVocabularySynonymsAllowed,
            string.Format(Properties.Resources.MaxLengthVocabularySynonymsError, Constants.MaxLengthVocabularySynonymsAllowed));
    }

    private static string JoinSynonyms(VocabularyModel vocabulary) => string.Join('|', vocabulary.Synonyms);
}

internal class VocabularyNounsSerializer : VocabularySerializer
{
    public VocabularyNounsSerializer(IEnumerable<VocabularyModel> gameComponent, Encoding encoding) :
        base(gameComponent.Where(item => item.WordType == WordType.Noun), encoding)
    {
        EnsureHelpers.EnsureMaxLength(GameComponent, Constants.MaxLengthVocabularyNounsAllowed,
            string.Format(Properties.Resources.MaxLengthVocabularyNounsAllowedError, Constants.MaxLengthVocabularyNounsAllowed));
    }
}

internal class VocabularyVerbsSerializer : VocabularySerializer
{
    public VocabularyVerbsSerializer(IEnumerable<VocabularyModel> gameComponent, Encoding encoding) :
        base(gameComponent.Where(item => item.WordType == WordType.Verb), encoding)
    {
        EnsureHelpers.EnsureMaxLength(GameComponent, Constants.MaxLengthVocabularyVerbsAllowed,
            string.Format(Properties.Resources.MaxLengthVocabularyVerbsAllowedError, Constants.MaxLengthVocabularyVerbsAllowed));
    }
}
