using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Test.Infrastructure.Builders
{
    internal class GameCodeNullBuilder : GameBuilder
    {
        private int elements = 5;

        protected override string MainSceneCode => "MainScene";

        protected override IEnumerable<FlagModel> CreateFlags() =>
            Enumerable.Range(0, elements).Select((item, index) => new FlagModel() { });

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
                .Select((item, index) => new DispatcherModel() { Trigger = Trigger.AfterInputCommand, Commands = new List<CommandModel>() });
            var beforeDipatchers = Enumerable.Range(0, elements)
                .Select((item, index) => new DispatcherModel() { Trigger = Trigger.BeforeInputCommand, Commands = new List<CommandModel>() });

            return afterDispatchers.Concat(beforeDipatchers);
        }

        protected override IEnumerable<ObjectModel> CreateObjects() =>
            Enumerable.Range(0, elements).Select((item, index) => new ObjectModel() { });

        protected override IEnumerable<SceneModel> CreateScenes() =>
            Enumerable.Range(0, elements).Select((item, index) => new SceneModel() { });

        protected override IEnumerable<ActorModel> CreateActors() => 
            Enumerable.Range(0, elements).Select((item, index) => new ActorModel() { });

        protected override SettingsModel CreateSettings() => new()
        {
        };

    }
}
