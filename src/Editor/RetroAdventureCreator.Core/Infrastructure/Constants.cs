using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroAdventureCreator.Core.Infrastructure
{
    public static class Constants
    {
        public const int MaxLengthIds = byte.MaxValue - 1;
        // Vocabulary
        public const int MaxLengthVocabularyNounsAllowed = MaxLengthIds;
        public const int MaxLengthVocabularyVerbsAllowed = MaxLengthIds;
        public const int MaxLengthVocabularySynonymsAllowed = 16;

        // Messages
        public const int MaxLengthMessagesAllowed = MaxLengthIds;

        // Objects
        public const int MaxLengthObjectsAllowed = 63;
        public const int MaxLengthObjectWeightAllowed = 31;
        public const int MaxLengthObjectHealthAllowed = 7;
        public const int MaxLengthChildObjectsAllowed = 8;

        // Commands
        public const int MaxLengthCommandsAllowed = MaxLengthIds;
        public const int MaxLengthCommandArgumentsAllowed = 3;

        // Input Commands
        public const int MaxLengthInputCommandsAllowed = MaxLengthIds;
        public const int MaxLengthInputCommandsNounsAllowed = 3;

        // Flags
        public const int MaxLengthFlagsAllowed = MaxLengthIds;

        // Dispatcher
        public const int MaxLengthDispatchersAllowed = MaxLengthIds;

        // Scenes
        public const int MaxLengthScenesAllowed = MaxLengthIds;

        // Player
        public const int MaxLengthPlayerObjectsAllowed = 8;
        public const int MaxLengthPlayerHealthAllowed = 15;
        public const int MaxLengthPlayerExperiencePointsAllowed = 15;
    }
}
