namespace RetroAdventureCreator.Core.Infrastructure
{
    public static class Constants
    {
        /// <summary>
        /// End component pointer code, for empty component data used for calculate last component data length.
        /// </summary>
        public const string EndComponentPointerCode = "_END_COMPONENT";
        public const byte EndTokenLength = 0x01;
        public const byte EndToken = 0x01;

        public const int MaxLengthIds = byte.MaxValue;

        // Vocabulary
        public const int MaxLengthVocabularyNounsAllowed = MaxLengthIds;
        public const int MaxLengthVocabularyVerbsAllowed = MaxLengthIds;
        public const int MaxLengthVocabularySynonymsAllowed = 16;

        // Messages
        public const int MaxLengthMessagesAllowed = MaxLengthIds;

        // Commands
        public const int MaxLengthCommandsAllowed = MaxLengthIds;

        // Input Commands
        public const int MaxLengthInputCommandsAllowed = MaxLengthIds;

        // Dispatcher
        public const int MaxLengthAfterInputCommandDispatchersAllowed = MaxLengthIds;
        public const int MaxLengthBeforeInputCommandDispatchersAllowed = MaxLengthIds;

        // Objects
        public const int MaxLengthObjectsAllowed = 64;
        public const int MaxLengthObjectWeightAllowed = 16;
        public const int MaxLengthObjectHealthAllowed = 16;

        // Scenes
        public const int MaxLengthScenesAllowed = MaxLengthIds;

        // Flags
        public const int MaxLengthFlagsAllowed = MaxLengthIds;

        // Actors
        public const int MaxLengthActorsAllowed = 16;
        public const int MaxLengthPlayerObjectsAllowed = 8;
        public const int MaxLengthPlayerHealthAllowed = 16;
        public const int MaxLengthPlayerExperiencePointsAllowed = 16;
    }
}
