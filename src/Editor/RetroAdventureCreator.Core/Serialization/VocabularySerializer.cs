﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.ConstrainedExecution;
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

/// <summary>
/// Vocabulary model serializer
/// </summary>
/// <remarks>
/// Format Vocabulary serializer:
/// ----------------------------------------------
/// 
/// Data:
/// Synonyms = synonym bytes splitted by '|' (end with 0x00)
/// 
/// </remarks>
internal abstract class VocabularySerializer : Serializer<IEnumerable<VocabularyModel>>
{
    protected VocabularySerializer(IEnumerable<VocabularyModel> gameComponent) : base(gameComponent)
    {
    }

    public override IEnumerable<GameComponentPointerModel> GenerateGameComponentPointers()
    {
        var result = new List<GameComponentPointerModel>();
        var pointer = 0;

        foreach (var vocabulary in GameComponent)
        {
            EnsureGameComponentProperties(vocabulary, result);

            result.Add(new GameComponentPointerModel(vocabulary.Code, pointer));

            pointer += JoinSynonyms(vocabulary).Length;
        }

        return result;
    }

    public override SerializerResultModel Serialize(GameComponentsPointers gameComponentsIndexes)
    {
        var dataBytes = GameComponent.SelectMany(CreateDataBytes);
        return new SerializerResultModel(dataBytes.ToArray());
    }

    protected byte[] CreateDataBytes(VocabularyModel vocabulary) => SerializerEncoding.GetBytes(JoinSynonyms(vocabulary));

    private static void EnsureGameComponentProperties(VocabularyModel vocabulary, IEnumerable<GameComponentPointerModel> gameComponentPointers)
    {
        EnsureHelpers.EnsureNotFound(gameComponentPointers, item => item.Code == vocabulary.Code, string.Format(Properties.Resources.DuplicateCodeError, vocabulary.Code));
        EnsureHelpers.EnsureNotNullOrWhiteSpace(vocabulary.Code, Properties.Resources.CodeIsRequiredError);

        EnsureHelpers.EnsureNotNullOrEmpty(vocabulary.Synonyms, Properties.Resources.SysnonymsAreRequiredError);
        EnsureHelpers.EnsureMaxLength(vocabulary.Synonyms.Count(), Constants.MaxLengthVocabularySynonymsAllowed,
            string.Format(Properties.Resources.MaxLengthVocabularySynonymsError, Constants.MaxLengthVocabularySynonymsAllowed));

        var synonymsChars = JoinSynonyms(vocabulary).TrimEnd(Convert.ToChar(EndToken));
        EnsureHelpers.EnsureNotFound(synonymsChars, item => item == EndToken, Properties.Resources.StringEndCharDuplicatedError);
    }

    private static string JoinSynonyms(VocabularyModel vocabulary) => string.Join('|', vocabulary.Synonyms) + EndToken;
}

internal class VocabularyNounsSerializer : VocabularySerializer
{
    public VocabularyNounsSerializer(IEnumerable<VocabularyModel> gameComponent) :
        base(gameComponent.Where(item => item.WordType == WordType.Noun))
    {
        EnsureHelpers.EnsureMaxLength(GameComponent, Constants.MaxLengthVocabularyNounsAllowed,
            string.Format(Properties.Resources.MaxLengthVocabularyNounsAllowedError, Constants.MaxLengthVocabularyNounsAllowed));
    }
}

internal class VocabularyVerbsSerializer : VocabularySerializer
{
    public VocabularyVerbsSerializer(IEnumerable<VocabularyModel> gameComponent) :
        base(gameComponent.Where(item => item.WordType == WordType.Verb))
    {
        EnsureHelpers.EnsureMaxLength(GameComponent, Constants.MaxLengthVocabularyVerbsAllowed,
            string.Format(Properties.Resources.MaxLengthVocabularyVerbsAllowedError, Constants.MaxLengthVocabularyVerbsAllowed));
    }
}
