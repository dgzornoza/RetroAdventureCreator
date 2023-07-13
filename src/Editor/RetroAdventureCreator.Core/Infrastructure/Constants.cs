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
        public const int MaxLengthVocabularyNounsAllowed = byte.MaxValue;
        public const int MaxLengthVocabularyVerbsAllowed = 63;
        public const int MaxLengthVocabularySynonymsAllowed = byte.MaxValue;

        // Messages
        public const int MaxLengthMessagesAllowed = byte.MaxValue;
        public const int MaxLengthMessageTextAllowed = byte.MaxValue;        

        // Objects
        public const int MaxLengthObjectsAllowed = 63;
        public const int MaxLengthObjectWeightAllowed = 31;
        public const int MaxLengthObjectHealthAllowed = 7;
        public const int MaxLengthChildObjectsAllowed = 15;
        public const int MaxLengthRequiredComplementsAllowed = 3;
        public const int MaxLengthComplementsAllowed = 3;

        // Commands
        public const int MaxLengthCommandsAllowed = byte.MaxValue;
        public const int MaxLengthCommandArgumentsAllowed = 3;

        // Input Commands
        public const int MaxLengthInputCommandsAllowed = byte.MaxValue;
        public const int MaxLengthInputCommandsNounsAllowed = 3;

        // Flags
        public const int MaxLengthFlagsAllowed = byte.MaxValue;

        // Dispatcher
        public const int MaxLengthDispatchersAllowed = byte.MaxValue;

        // Scenes
        public const int MaxLengthScenesAllowed = byte.MaxValue;
    }
}
