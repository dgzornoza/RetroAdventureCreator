# Game Model Description

- **FlagsModel**: Contains the flags that the game can use. It is used to store the game state.
- **VocabularyModel**: Contains the words that the game can understand. It is used to parse the input commands and to generate the output messages.
- **MessageModel**: Contains the messages that the game can show to the player. It is used to generate the output messages.
- **CommandModel**: Contains execution commands that the game can use.
- **InputCommandModel**: Contains input commands that the game can understand. It is used to parse the input commands.
- **DispatcherModel**: Contains the dispatchers that the game can use to process the input commands. It is used to parse the input commands.
- **ObjectModel**: Contains the objects that the game can use. It is used to store the game player objects.
- **SceneModel**: Contains the scenes that the game can use. It is used to store the game scenes.
- **PlayerModel**: Contains the player data that the game can use. It is used to store the game player.
- **SettingsModel**: Contains the settings that the game can use. It is used to store the game settings.
- **GameModel**: Contains the all game data.

## Game Components Creation Dependencies

    FlagsModel
    VocabularyModel
    MessageModel
    CommandModel
    InputCommandModel -> VocabularyModel
    DispatcherModel -> InputCommandModel, CommandGroupModel
    ObjectModel -> MessageModel, VocabularyModel
    SceneModel -> MessageModel, DispatcherModel, ObjectModel
    PlayerModel -> ObjectModel
    SettingsModel
    GameModel -> PlayerModel, SettingsModel, VocabularyModel, MessageModel, CommandModel, InputCommandModel, DispatcherModel, ObjectModel, SceneModel

## Game Components Serializers

**Remarks:** *All strings are encoded as ASCII*

### FlagsSerializer

    Data:
        Flags = 1 bit per flag
        
    Limnits: 
        MaxLengthFlagsAllowed = 255

### VocabularySerializer

**Remarks:** *Exists two volcabuilary models: 'VocabularyNouns' and 'VocabularyVerbs'*

    Data:
        Synonyms => synonym bytes splitted by '|' (end with 0x00)

    Limits:
        MaxLengthVocabularyNounsAllowed = 255
        MaxLengthVocabularyVerbsAllowed = 255
        MaxLengthVocabularySynonymsAllowed = 16

### MessageSerializer

    Data:
        Text: Mesage text bytes (end with 0x00)

    Limits:
        MaxLengthMessagesAllowed = 255

### CommandSerializer

    Data:
        1 bit = 0 (Command)
        Token = 7 bits (127)
        Arguments = ids bytes (end with 0x00)

    Limits:
        MaxLengthCommandsAllowed = 255;

### InputCommandSerializer

    Data:
        Verb = 8 bits (id verb vocabulary)
        Nouns = vocabulary id bytes (end with 0x00)

    Limits:
        MaxLengthInputCommandsAllowed = 255

### DispatcherSerializer

    Data:
        Commands = Command/commandGroup id bytes (end with 0x00)
        InputCommands = InputCommand id bytes (end with 0x00) (only in AfterInputCommandDispatchers)

    Limits:
        MaxLengthAfterInputCommandDispatchersAllowed = 255
        MaxLengthBeforeInputCommandDispatchersAllowed = 255

### ObjectSerializer

    Data:
        Name = 8 bits (id vocabulary)
        Description = 8 bits (id message)
        Weight = 5 bits (31)
        Health = 3 bits (7)
        Properties = 8 bits (flag 8 properties)
        ChildObjects = (Optional, only if properties has 'IsContainer') 8 object id bytes

    Limits:
        MaxLengthObjectsAllowed = 64
        MaxLengthObjectWeightAllowed = 32
        MaxLengthObjectHealthAllowed = 8
        MaxLengthChildObjectsAllowed = 8

### SceneSerializer

    Data:
        Description = message id byte
        AfterInputCommandDispatchers = dispatcher id bytes (end with 0x00)
        BeforeInputCommandDispatchers = dispatcher id bytes (end with 0x00)
        Objects = object id bytes (end with 0x00)

    Limits:
        MaxLengthScenesAllowed = 255

### PlayerSerializer

    Data (player data can be modified in game for update properties):
        Health: 4 bits (15)
        ExperiencePoints: 4 bits (15)
        Objects: 8 object id bytes

    Limits:
        MaxLengthPlayerObjectsAllowed = 8
        MaxLengthPlayerHealthAllowed = 16
        MaxLengthPlayerExperiencePointsAllowed = 16

### SettingsSerializer

    Data: (2 bytes)
        Charset = 4 bits
        Color = 4 bits
        BackgroundColor = 4 bits
        BorderColor = 4 bits    
