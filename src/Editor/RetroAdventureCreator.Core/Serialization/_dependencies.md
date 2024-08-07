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

**Remarks:** *All strings are encoded as ASCII by default, can specify encode in SerializerFactory*

### FlagsSerializer

    Data:
        Flags = 1 bit per flag
        
    Limnits: 
        MaxLengthFlagsAllowed = 256

### VocabularySerializer (nouns and verbs)

**Remarks:** *Exists two volcabuilary models: 'VocabularyNouns' and 'VocabularyVerbs'*

    Data:
        Synonyms => synonym bytes splitted by '|'

    Limits:
        MaxLengthVocabularyNounsAllowed = 256   (limited by orderer addresses)
        MaxLengthVocabularyVerbsAllowed = 256   (limited by orderer addresses)
        MaxLengthVocabularySynonymsAllowed = 16

### MessageSerializer

    Data:
        Text: Mesage text bytes

    Limits:
        MaxLengthMessagesAllowed = 256  (can change limits)

### CommandSerializer

    Data:
        Token = 8 bits (256)
        Arguments = ids bytes

    Limits:
        MaxLengthCommandsAllowed = 256; (can change limits)

### InputCommandSerializer

    Data:
        Verbs = 1 byte (index orderer addresses verbs vocabulary)
        Nouns = 1 byte (index orderer addresses nouns vocabulary)

    Limits:
        MaxLengthInputCommandsAllowed = 256 (can change limits)

### DispatcherSerializer

    Data:
        InputCommands = InputCommand pointers (2 bytes each one) (only in AfterInputCommandDispatchers)
        Commands = Commands pointers (2 bytes each one)

    Limits:
        MaxLengthAfterInputCommandDispatchersAllowed = 256 (can change limits)
        MaxLengthBeforeInputCommandDispatchersAllowed = 256 (can change limits)

### ObjectSerializer

    Data:
        Name = index orderer addresses nouns vocabulary (1 byte)
        Description = Message pointer (2 bytes)
        Properties = 1 byte (8 properties flags) (mutable properties)
        OwnerCode = 2 bytes

    Limits:
        MaxLengthObjectsAllowed = 64        // (can change limits)
        MaxLengthObjectWeightAllowed = 16   // not used currently
        MaxLengthObjectHealthAllowed = 16   // not used currently

### SceneSerializer

    Data:
        Description = message id byte
        AfterInputCommandDispatchers = dispatcher id bytes
        BeforeInputCommandDispatchers = dispatcher id bytes
        Objects = object id bytes

    Limits:
        MaxLengthScenesAllowed = 255

### ActorsSerializer

    Data:
        Health: 1 byte  (mutable data)
        ExperiencePoints: 1 byte  (mutable data)
        ActorType: 1 byte

    Limits:
        MaxLengthActorsAllowed = 16;
        MaxLengthPlayerHealthAllowed = 256
        MaxLengthPlayerExperiencePointsAllowed = 256

### SettingsSerializer

    Data: (2 bytes)
        Charset = 4 bits
        Color = 4 bits
        BackgroundColor = 4 bits
        BorderColor = 4 bits    

## Game Components Database

### Global header

Contains pointer to addresses of all components in the database

    Data (2 bytes each one with component address)

    | ----------------- |
    | FlagsModel        |
    | VocabularyModel   |    
    | MessageModel      |
    | CommandModel      |
    | InputCommandModel |    
    | DispatcherModel   |    
    | ObjectModel       |
    | SceneModel        |
    | PlayerModel       |
    | SettingsModel     |   

### Orderer addresses

Orderer addresses for common reused components. This section is used for optimize stored memory in reused components that no require more of 256 (1 byte) of elements in compoent.

This allows you to use an index (1 byte) instead of a memory address (2 bytes) to save memory on commonly common data such as verbs and nouns.

The data of the components that use this section may use a byte indicating the index where the memory address of the data is, so that this section will contain a map from 1 byte index to 2 bytes memory address.

    Data (2 byte each one with component data address)

    | ---------------------- |
    | VocabularyVerbsModel   |
    | VocabularyNounsModel   |

### Components Data

Section with serialized data of all components

    Data (variable length), see each component serializer

    | ----------------- | 
    | FlagsModel        | 
    | VocabularyModel   | 
    | MessageModel      | 
    | CommandModel      | 
    | InputCommandModel |    
    | DispatcherModel   |    
    | ObjectModel       |
    | SceneModel        |
    | PlayerModel       |
    | SettingsModel     |
