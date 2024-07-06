using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Test.Infrastructure.Builders
{
    internal class GameDuplicateCodeBuilder : GameBuilder
    {
        private int elements = 5;

        protected override string MainSceneCode => "MainScene";

        protected override IEnumerable<FlagModel> CreateFlags() =>
            Enumerable.Range(0, elements).Select((item, index) => new FlagModel() { Code = "FlagCodeDuplicated", Value = true });

        protected override IEnumerable<VocabularyModel> CreateVocabulary()
        {
            var nouns = Enumerable.Range(0, elements)
                .Select((item, index) => new VocabularyModel() { Code = "VocabularyNounsCodeDuplicated", WordType = WordType.Noun, Synonyms = new List<string> { "Synonym" } });
            var verbs = Enumerable.Range(0, elements)
                .Select((item, index) => new VocabularyModel() { Code = "VocabularyVerbsCodeDuplicated", WordType = WordType.Verb, Synonyms = new List<string> { "Synonym" } });

            return nouns.Concat(verbs);
        }

        protected override IEnumerable<MessageModel> CreateMessages() =>
            Enumerable.Range(0, elements).Select((item, index) => new MessageModel() { Code = "MessageCodeDuplicated", Text = "test text" });

        protected override IEnumerable<CommandModel> CreateCommands() =>
            Enumerable.Range(0, elements).Select((item, index) => new CommandModel() { Code = "CommandCodeDuplicated" });

        protected override IEnumerable<InputCommandModel> CreateInputCommands() =>
            Enumerable.Range(0, elements).Select((item, index) => new InputCommandModel() { Code = "InputCommandCodeDuplicated" });

        protected override IEnumerable<DispatcherModel> CreateDispatchers()
        {
            var afterDispatchers = Enumerable.Range(0, elements)
                .Select((item, index) => new DispatcherModel() { Code = "AfterDispatcherCodeDuplicated", Trigger = Trigger.AfterInputCommand, Commands = new List<CommandModel>() });
            var beforeDipatchers = Enumerable.Range(0, elements)
                .Select((item, index) => new DispatcherModel() { Code = "BeforeDispatcherCodeDuplicated", Trigger = Trigger.BeforeInputCommand, Commands = new List<CommandModel>() });

            return afterDispatchers.Concat(beforeDipatchers);
        }

        protected override IEnumerable<ObjectModel> CreateObjects() =>
            Enumerable.Range(0, elements).Select((item, index) => new NormalObjectModel() { Code = "ObjectCodeDuplicated" });

        protected override IEnumerable<SceneModel> CreateScenes() =>
            Enumerable.Range(0, elements).Select((item, index) => new SceneModel() { Code = "SceneCodeDuplicated" });

        protected override IEnumerable<ActorModel> CreateActors() =>
            Enumerable.Range(0, elements).Select((item, index) => new ActorModel() { Code = "ActorCodeDuplicated" });

        protected override SettingsModel CreateSettings() => new()
        {
        };

    }
}
