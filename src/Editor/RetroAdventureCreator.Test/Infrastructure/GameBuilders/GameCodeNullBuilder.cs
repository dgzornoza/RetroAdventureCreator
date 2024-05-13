using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Core.Infrastructure;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Test.Infrastructure.Builders
{
    internal class GameCodeNullBuilder : GameBuilder
    {
        private int elements = 5;

        protected override string MainSceneCode => "MainScene";

        protected override IEnumerable<VocabularyModel> CreateVocabulary()
        {
            var nouns = Enumerable.Range(0, elements)
                .Select((item, index) => new VocabularyModel() { WordType = WordType.Noun, Synonyms = new List<string> { "Synonym" } });
            var verbs = Enumerable.Range(0, elements)
                .Select((item, index) => new VocabularyModel() { WordType = WordType.Verb, Synonyms = new List<string> { "Synonym" } });

            return nouns.Concat(verbs);
        }

        protected override IEnumerable<MessageModel> CreateMessages() =>
            Enumerable.Range(0, elements).Select((item, index) => new MessageModel() { });

        protected override IEnumerable<CommandModel> CreateCommands() =>
            Enumerable.Range(0, elements).Select((item, index) => new CommandModel() { });

        protected override IEnumerable<InputCommandModel> CreateInputCommands() =>
            Enumerable.Range(0, elements).Select((item, index) => new InputCommandModel() { });

        protected override IEnumerable<DispatcherModel> CreateDispatchers()
        {
            var afterDispatchers = Enumerable.Range(0, elements)
                .Select((item, index) => new DispatcherModel() { Trigger = Trigger.AfterInputCommand });
            var beforeDipatchers = Enumerable.Range(0, elements)
                .Select((item, index) => new DispatcherModel() { Trigger = Trigger.BeforeInputCommand });

            return afterDispatchers.Concat(beforeDipatchers);
        }

        protected override IEnumerable<ObjectModel> CreateObjects() =>
            Enumerable.Range(0, elements).Select((item, index) => new ObjectModel() { });

        protected override IEnumerable<SceneModel> CreateScenes() =>
            Enumerable.Range(0, elements).Select((item, index) => new SceneModel() { });

        protected override IEnumerable<FlagModel> CreateFlags() =>
            Enumerable.Range(0, elements).Select((item, index) => new FlagModel() { });

        protected override PlayerModel CreatePlayer() => new()
        {
        };

        protected override SettingsModel CreateSettings() => new()
        {
        };

    }
}
