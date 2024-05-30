using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroAdventureCreator.Core.Infrastructure
{
    public static class Constants
    {
        public const byte EndToken = 0x00;
        public const byte EndTokenLength = 0x01;

        // Is limited to a byte -1 because indices are used as reference to messages, vocabularies, etc.
        // and index 0 is used as null value.
        public const int MaxLengthIds = byte.MaxValue - 1;

        // Vocabulary
        public const int MaxLengthVocabularyNounsAllowed = MaxLengthIds;
        public const int MaxLengthVocabularyVerbsAllowed = MaxLengthIds;
        public const int MaxLengthVocabularySynonymsAllowed = 16;

        // Messages
        public const int MaxLengthMessagesAllowed = MaxLengthIds;

        // Commands
        public const int MaxLengthCommandsAllowed = MaxLengthIds;

        // Commands Groups
        public const int MaxLengthCommandsGroupsAllowed = MaxLengthIds;

        // Input Commands
        public const int MaxLengthInputCommandsAllowed = MaxLengthIds;

        // Dispatcher
        public const int MaxLengthAfterInputCommandDispatchersAllowed = MaxLengthIds;
        public const int MaxLengthBeforeInputCommandDispatchersAllowed = MaxLengthIds;

        // Objects
        public const int MaxLengthObjectsAllowed = 64;
        public const int MaxLengthObjectWeightAllowed = 32;
        public const int MaxLengthObjectHealthAllowed = 8;
        public const int MaxLengthChildObjectsAllowed = 8;

        // Scenes
        public const int MaxLengthScenesAllowed = MaxLengthIds;

        // Flags
        public const int MaxLengthFlagsAllowed = MaxLengthIds;

        // Player
        public const int MaxLengthPlayerObjectsAllowed = 8;
        public const int MaxLengthPlayerHealthAllowed = 16;
        public const int MaxLengthPlayerExperiencePointsAllowed = 16;
    }
}
