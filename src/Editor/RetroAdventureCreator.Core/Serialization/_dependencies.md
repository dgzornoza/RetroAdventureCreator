# Game Model Description

- **VocabularyModel**: Contains the words that the game can understand. It is used to parse the input commands and to generate the output messages.
- **MessageModel**: Contains the messages that the game can show to the player. It is used to generate the output messages.
- **CommandModel**: Contains execution commands that the game can use.
- **CommandGroupModel**: Contains execution command groups that the game can use (combined commands).
- **InputCommandModel**: Contains input commands that the game can understand. It is used to parse the input commands.
- **DispatcherModel**: Contains the dispatchers that the game can use to process the input commands. It is used to parse the input commands.
- **ObjectModel**: Contains the objects that the game can use. It is used to store the game player objects.
- **SceneModel**: Contains the scenes that the game can use. It is used to store the game scenes.
- **FlagsModel**: Contains the flags that the game can use. It is used to store the game state.
- **PlayerModel**: Contains the player data that the game can use. It is used to store the game player.
- **SettingsModel**: Contains the settings that the game can use. It is used to store the game settings.
- **GameModel**: Contains the all game data.

## Game Components Creation Dependencies

    VocabularyModel
    MessageModel
    CommandModel
    CommandGroupModel -> CommandModel
    InputCommandModel -> VocabularyModel
    DispatcherModel -> InputCommandModel, CommandGroupModel
    ObjectModel -> MessageModel, VocabularyModel
    SceneModel -> MessageModel, DispatcherModel, ObjectModel
    FlagsModel
    PlayerModel -> ObjectModel
    SettingsModel
    GameModel -> PlayerModel, SettingsModel, VocabularyModel, MessageModel, CommandModel, CommandGroupModel, InputCommandModel, DispatcherModel, ObjectModel, SceneModel

## Game Components Serializer

**Remarks:** *All strings are encoded as ASCII*

### VocabularyModel

**Remarks:** *Exists two volcabuilary models: 'VocabularyNouns' and 'VocabularyVerbs'*

    Data:
        Synonyms => synonym bytes splitted by '|' (end with 0x00)

    Limits:
        MaxLengthVocabularyNounsAllowed = 255
        MaxLengthVocabularyVerbsAllowed = 255
        MaxLengthVocabularySynonymsAllowed = 16

### MessageModel

    Data:
        Text: Mesage text bytes (end with 0x00)

    Limits:
        MaxLengthMessagesAllowed = 255

### CommandModel

    Data:
        1 bit = 0 (Command)
        Token = 7 byte (128)
        Arguments = ids bytes (end with 0x00)

    Limits:
        MaxLengthCommandsAllowed = 255;
