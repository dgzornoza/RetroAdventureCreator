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
        public const int MaxNumberVocabularyAllowed = byte.MaxValue;
        public const int MaxSizeOfVocabularySynonymsAllowed = byte.MaxValue;

        // Messages
        public const int MaxNumberMessagesAllowed = byte.MaxValue;
        public const int MaxSizeOfMessageTextAllowed = byte.MaxValue;
        

        // Objects
        public const int MaxNumberObjectsAllowed = 64;
        public const int MaxSizeOfObjectWeightAllowed = 32;
        public const int MaxSizeOfObjectHealthAllowed = 8;
        public const int MaxSizeOfChildObjectsAllowed = 15;
        public const int MaxSizeOfRequiredComplementsAllowed = 3;
        public const int MaxSizeOfComplementsAllowed = 3;

        // Commands
        public const int MaxNumberCommandsAllowed = byte.MaxValue;
        public const int MaxNumberCommandArgumentsAllowed = 3;
    }
}
