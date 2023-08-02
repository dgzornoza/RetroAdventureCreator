using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroAdventureCreator.Core.Infrastructure
{
    public static class Constants
    {
        // Vocabulary
        public const int MaxLengthVocabularyNounsAllowed = byte.MaxValue - 1;
        public const int MaxLengthVocabularyVerbsAllowed = 63;
        public const int MaxLengthVocabularySynonymsAllowed = byte.MaxValue - 1;

        // Messages
        public const int MaxLengthMessagesAllowed = byte.MaxValue - 1;
        public const int MaxLengthMessageTextAllowed = byte.MaxValue - 1;        

        // Objects
        public const int MaxLengthObjectsAllowed = 63;
        public const int MaxLengthObjectWeightAllowed = 31;
        public const int MaxLengthObjectHealthAllowed = 7;
        public const int MaxLengthChildObjectsAllowed = 15;
        public const int MaxLengthRequiredComplementsAllowed = 3;
        public const int MaxLengthComplementsAllowed = 3;

        // Commands
        public const int MaxLengthCommandsAllowed = byte.MaxValue - 1;
        public const int MaxLengthCommandArgumentsAllowed = 3;

        // Input Commands
        public const int MaxLengthInputCommandsAllowed = byte.MaxValue - 1;
        public const int MaxLengthInputCommandsNounsAllowed = 3;

        // Flags
        public const int MaxLengthFlagsAllowed = byte.MaxValue - 1;

        // Dispatcher
        public const int MaxLengthDispatchersAllowed = byte.MaxValue - 1;

        // Scenes
        public const int MaxLengthScenesAllowed = byte.MaxValue - 1;

        // Player
        public const int MaxLengthPlayerObjectsAllowed = 31;
        public const int MaxLengthPlayerHealthAllowed = 7;
    }
}
