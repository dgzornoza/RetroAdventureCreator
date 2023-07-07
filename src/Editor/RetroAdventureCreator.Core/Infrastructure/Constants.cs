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
        public const int MaxLengthVocabularyAllowed = byte.MaxValue;
        public const int MaxLengthVocabularyVerbsAllowed = 64;
        public const int MaxLengthVocabularySynonymsAllowed = byte.MaxValue;

        // Messages
        public const int MaxLengthMessagesAllowed = byte.MaxValue;
        public const int MaxLengthMessageTextAllowed = byte.MaxValue;
        

        // Objects
        public const int MaxLengthObjectsAllowed = 64;
        public const int MaxLengthObjectWeightAllowed = 32;
        public const int MaxLengthObjectHealthAllowed = 8;
        public const int MaxLengthChildObjectsAllowed = 15;
        public const int MaxLengthRequiredComplementsAllowed = 3;
        public const int MaxLengthComplementsAllowed = 3;

        // Commands
        public const int MaxLengthCommandsAllowed = byte.MaxValue;
        public const int MaxLengthCommandArgumentsAllowed = 3;

        // Commands
        public const int MaxLengthInputCommandsAllowed = byte.MaxValue;
        public const int MaxLengthInputCommandsNounsAllowed = 3;
    }
}
