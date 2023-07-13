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
    internal class GameMaxLengthLimitsBuilder : GameBuilder
    {
        protected override string MainSceneCode => "MainScene";

        protected override IEnumerable<FlagModel> CreateFlags() =>
            Enumerable.Range(0, Constants.MaxLengthFlagsAllowed + 1).Select((item, index) => new FlagModel() { Code = $"Flag{index}" });

        protected override PlayerModel CreatePlayer() => new()
        {
        };

        protected override SettingsModel CreateSettings() => new()
        {
        };

        protected override IEnumerable<MessageModel> CreateMessages() =>
            Enumerable.Range(0, Constants.MaxLengthMessagesAllowed + 1).Select((item, index) => new MessageModel() { Code = $"Message{index}" }); 

        protected override IEnumerable<VocabularyModel> CreateVocabulary()
        {
            var nouns = Enumerable.Range(0, Constants.MaxLengthVocabularyNounsAllowed + 1)
                .Select((item, index) => new VocabularyModel() { Code = $"Noun{index}", WordType = WordType.Noun, Synonyms = new List<string> { "Synonym" } });
            var verbs = Enumerable.Range(0, Constants.MaxLengthVocabularyVerbsAllowed + 1)
                .Select((item, index) => new VocabularyModel() { Code = $"Verb{index}", WordType = WordType.Verb, Synonyms = new List<string> { "Synonym" } });

            return nouns.Concat(verbs);
        }

        protected override IEnumerable<ObjectModel> CreateObjects() =>
            Enumerable.Range(0, Constants.MaxLengthObjectsAllowed + 1).Select((item, index) => new ObjectModel() { Code = $"Object{index}" });

        protected override IEnumerable<CommandModel> CreateCommands() =>
            Enumerable.Range(0, Constants.MaxLengthCommandsAllowed + 1).Select((item, index) => new CommandModel() { Code = $"Commmand{index}" });

        protected override IEnumerable<CommandGroupModel> CreateCommandsGroups() => new List<CommandGroupModel>();

        protected override IEnumerable<InputCommandModel> CreateInputCommands() =>
            Enumerable.Range(0, Constants.MaxLengthInputCommandsAllowed + 1).Select((item, index) => new InputCommandModel() { Code = $"InpuCommmand{index}" });

        protected override IEnumerable<DispatcherModel> CreateDispatchers() =>
            Enumerable.Range(0, Constants.MaxLengthDispatchersAllowed + 1).Select((item, index) => new DispatcherModel() { Code = $"Dispatcher{index}" });

        protected override IEnumerable<SceneModel> CreateScenes() =>
            Enumerable.Range(0, Constants.MaxLengthScenesAllowed + 1).Select((item, index) => new SceneModel() { Code = $"Scene{index}" }); 
    }
}
