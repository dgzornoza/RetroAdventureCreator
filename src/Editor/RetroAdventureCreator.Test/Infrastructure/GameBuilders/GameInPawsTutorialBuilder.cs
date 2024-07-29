using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Test.Infrastructure.Builders
{
    internal class GameInPawsTutorialBuilder : GameBuilder
    {
        protected override string MainSceneCode => "EntradaScene";

        protected override IEnumerable<FlagModel> CreateFlags() =>
        [
            new() { Code = "openEntry", Value = true }
        ];

        protected override IEnumerable<VocabularyModel> CreateVocabulary() =>
        [
            new() { Code = "GuantesVocabulary", WordType = WordType.Noun, Synonyms = ["GUANT"] },
            new() { Code = "BoqueteVocabulary", WordType = WordType.Noun, Synonyms = ["BOQUE", "PALAN"]},
            new() { Code = "PalancaVocabulary", WordType = WordType.Noun, Synonyms = ["PALAN"]},
            new() { Code = "TemploVocabulary", WordType = WordType.Noun, Synonyms = ["TEMPL"]},
            new() { Code = "BloqueVocabulary", WordType = WordType.Noun, Synonyms = ["BLOQU", "PIEDR"]},
            new() { Code = "AbreVocabulary", WordType = WordType.Verb, Synonyms = ["ABRE"] },
            new() { Code = "EntraVocabulary", WordType = WordType.Noun, Synonyms = ["ENTRA"] },
            new() { Code = "EntraVocabulary", WordType = WordType.Verb, Synonyms = ["ENTRA"] },
            new() { Code = "ExaminaVocabulary", WordType = WordType.Verb, Synonyms = ["EX"] },
            new() { Code = "MueveVocabulary", WordType = WordType.Verb, Synonyms = ["MUEVE"] },
            new() { Code = "EmpujaVocabulary", WordType = WordType.Verb, Synonyms = ["EMPUJ"] },
            new() { Code = "TiraVocabulary", WordType = WordType.Verb, Synonyms = ["TIRA"] },
            new() { Code = "UsarVocabulary", WordType = WordType.Verb, Synonyms = ["USE"] },
        ];

        protected override IEnumerable<MessageModel> CreateMessages() =>
        [
            new() { Code = "globDescriptionMessage", Text = "Unos guantes" },
            new()
            {
                Code = "EntradaSceneDescription",
                Text = "^^Tras meses de exploración en lo profundo de la selva, y después de haber sido perseguido, mordido, enfermado, pasado hambre y renunciado al todo, ahora por fin has encontrado lo que buscabas, el Templo de Ok, donde según cuenta la leyenda se guarda el Gran Diamante del Rajá Al-Meredin, rechazado por su amada y condenado a ser encerrado eternamente en lo profundo de la selva.^^ Hasta hoy.",
            },
            new()
            {
                Code = "InteriorSceneDescription",
                Text = "^^Has finalizado el primer puzzle^^ !!Bien echo.!!",
            },
            new()
            {
                Code = "MessageBloquePiedra",
                Text = "Un enorme bloque de piedra bloquea la entrada.",
            },
            new()
            {
                Code = "MessageInteriorTemplo",
                Text = "Un siniestro pasaje conduce al interior del templo.",
            },
            new()
            {
                Code = "MessageFuerza",
                Text = "Ni la fuerza combinada de diez como tú podría mover ese mastodonte de piedra.",
            },
            new()
            {
                Code = "MessageNoBloque",
                Text = "Ya no hay bloque que abrir.",
            },
            new()
            {
                Code = "MessageBoquete",
                Text = "Un oscuro boquete cuadrado, excavado por la mano del hombre, en cuyo interior ves una especie de palanca. También crees divisar ojos que te observan desde dentro.",
            },
            new()
            {
                Code = "MessageAraña",
                Text = "Al meter la mano en el boquete notas un pichazo agudo. Casi al instante tu vista se comienza a nublar y vas perdiendo contacto con la realidad.^^       *** ESTAS MUERTO ***",
            },
            new()
            {
                Code = "MessageTiraPalanca",
                Text = "Con no poco repelús metes la mano enguantada y mueves la palanca. Oyes un chasquido, y luego un ruido de correas deslizándose. Al cabo de un rato, el enorme bloque cede y se hunde en el suelo como tragado por el infierno. La entrada está libre.",
            },
            new()
            {
                Code = "MessagePalancaUsada",
                Text = "No necesitas volver a meter la mano en \"eso\".",
            },
        ];

        protected override IEnumerable<CommandModel> CreateCommands() =>
        [
            new() { Code = "IsSetOpenEntry", Token = CommandToken.IsSet, Arguments = new List<string> { "openEntry" } },
            new() { Code = "IsUnSetOpenEntry", Token = CommandToken.IsUnset, Arguments = new List<string> { "openEntry" } },
            new() { Code = "SetOpenEntry", Token = CommandToken.Set, Arguments = new List<string> { "openEntry" } },
            new() { Code = "ShowMessageBloquePiedra", Token = CommandToken.ShowMessage, Arguments = new List<string> { "MessageBloquePiedra" } },
            new() { Code = "ShowMessageInteriorTemplo", Token = CommandToken.ShowMessage, Arguments = new List<string> { "MessageInteriorTemplo" } },
            new() { Code = "ShowMessageFuerza", Token = CommandToken.ShowMessage, Arguments = new List<string> { "MessageFuerza" } },
            new() { Code = "ShowMessageNoBloque", Token = CommandToken.ShowMessage, Arguments = new List<string> { "MessageNoBloque" } },
            new() { Code = "ShowMessageBoquete", Token = CommandToken.ShowMessage, Arguments = new List<string> { "MessageBoquete" } },
            new() { Code = "ShowMessageAraña", Token = CommandToken.ShowMessage, Arguments = new List<string> { "MessageAraña" } },
            new() { Code = "ShowMessageTiraPalanca", Token = CommandToken.ShowMessage, Arguments = new List<string> { "MessageTiraPalanca" } },
            new() { Code = "ShowMessagePalancaUsada", Token = CommandToken.ShowMessage, Arguments = new List<string> { "MessagePalancaUsada" } },
            new() { Code = "IsInUseGuantes", Token = CommandToken.InUse, Arguments = new List<string> { "GuantesObject" } },
            new() { Code = "IsNotInUseGuantes", Token = CommandToken.NotInUse, Arguments = new List<string> { "GuantesObject" } },
            new() { Code = "UseGuantes", Token = CommandToken.Use, Arguments = new List<string> { "GuantesObject" } },
            new() { Code = "GotoInteriorScene", Token = CommandToken.Goto, Arguments = new List<string> { "InteriorScene" } },
            new() { Code = "EndGame", Token = CommandToken.EndGame },
        ];

        protected override IEnumerable<InputCommandModel> CreateInputCommands() =>
        [
            new() {
                Code = "AbreEntradaInputCommand",
                Verbs = Vocabulary.Find("AbreVocabulary"),
                Nouns = Vocabulary.Find("EntraVocabulary"),
            },
            new() {
                Code = "UsaGuantesInputCommand",
                Verbs = Vocabulary.Find("UsarVocabulary"),
                Nouns = Vocabulary.Find("GuantesVocabulary"),
            },
            new()
            {
                Code = "AbreBloqueInputCommand",
                Verbs = Vocabulary.Find("AbreVocabulary"),
                Nouns = Vocabulary.Find("BloqueVocabulary"),
            },
            new()
            {
                Code = "ExaminarBoquete",
                Verbs = Vocabulary.Find("ExaminaVocabulary"),
                Nouns = Vocabulary.Find("BoqueteVocabulary"),
            },
            new()
            {
                Code = "MuevePalanca",
                Verbs = Vocabulary.Find("MueveVocabulary"),
                Nouns = Vocabulary.Find("PalancaVocabulary"),
            },
            new()
            {
                Code = "EmpujaPalanca",
                Verbs = Vocabulary.Find("EmpujaVocabulary"),
                Nouns = Vocabulary.Find("PalancaVocabulary"),
            },
            new()
            {
                Code = "TiraPalanca",
                Verbs = Vocabulary.Find("TiraVocabulary"),
                Nouns = Vocabulary.Find("PalancaVocabulary"),
            },
            new()
            {
                Code = "Entra",
                Verbs = Vocabulary.Find("EntraVocabulary"),
            },
        ];

        protected override IEnumerable<DispatcherModel> CreateDispatchers() =>
        [
            new()
            {
                Code = $"{MainSceneCode}-1",
                Trigger = Trigger.BeforeInputCommand,
                Commands = new List<CommandModel>
                {
                    Commands.Find("IsSetOpenEntry"),
                    Commands.Find("ShowMessageBloquePiedra"),
                }
            },
            new()
            {
                Code = $"{MainSceneCode}-2",
                Trigger = Trigger.BeforeInputCommand,
                Commands = new List<CommandModel>
                {
                    Commands.Find("IsUnSetOpenEntry"),
                    Commands.Find("ShowMessageInteriorTemplo"),
                }
            },
            new()
            {
                Code = $"{MainSceneCode}-3",
                Trigger = Trigger.AfterInputCommand,
                InputCommands = new List<InputCommandModel>
                {
                    InputCommands.Find("AbreEntradaInputCommand"),
                    InputCommands.Find("AbreBloqueInputCommand"),
                },
                Commands = new List<CommandModel>
                {
                    Commands.Find("IsUnSetOpenEntry"),
                    Commands.Find("ShowMessageFuerza"),
                }
            },
            new()
            {
                Code = $"{MainSceneCode}-4",
                Trigger = Trigger.AfterInputCommand,
                InputCommands = new List<InputCommandModel>
                {
                    InputCommands.Find("AbreEntradaInputCommand"),
                    InputCommands.Find("AbreBloqueInputCommand"),
                },
                Commands = new List<CommandModel>
                {
                    Commands.Find("IsSetOpenEntry"),
                    Commands.Find("ShowMessageNoBloque"),
                }
            },
            new()
            {
                Code = $"{MainSceneCode}-5",
                Trigger = Trigger.AfterInputCommand,
                InputCommands = new List<InputCommandModel>
                {
                    InputCommands.Find("ExaminarBoquete"),
                },
                Commands = new List<CommandModel>
                {
                    Commands.Find("ShowMessageBoquete"),
                }
            },
            new()
            {
                Code = $"{MainSceneCode}-6",
                Trigger = Trigger.AfterInputCommand,
                InputCommands = new List<InputCommandModel>
                {
                    InputCommands.Find("MuevePalanca"),
                    InputCommands.Find("EmpujaPalanca"),
                    InputCommands.Find("TiraPalanca"),
                },
                Commands = new List<CommandModel>
                {
                    Commands.Find("IsUnSetOpenEntry"),
                    Commands.Find("IsNotInUseGuantes"),
                    Commands.Find("ShowMessageAraña"),
                    Commands.Find("EndGame"),
                },
            },
            new()
            {
                Code = $"{MainSceneCode}-7",
                Trigger = Trigger.AfterInputCommand,
                InputCommands = new List<InputCommandModel>
                {
                    InputCommands.Find("MuevePalanca"),
                    InputCommands.Find("EmpujaPalanca"),
                    InputCommands.Find("TiraPalanca"),
                },
                Commands = new List<CommandModel>
                {
                    Commands.Find("IsUnSetOpenEntry"),
                    Commands.Find("IsInUseGuantes"),
                    Commands.Find("SetOpenEntry"),
                    Commands.Find("ShowMessageTiraPalanca"),
                },
            },
            new()
            {
                Code = $"{MainSceneCode}-8",
                Trigger = Trigger.AfterInputCommand,
                InputCommands = new List<InputCommandModel>
                {
                    InputCommands.Find("MuevePalanca"),
                    InputCommands.Find("EmpujaPalanca"),
                    InputCommands.Find("TiraPalanca"),
                },
                Commands = new List<CommandModel>
                {
                    Commands.Find("IsSetOpenEntry"),
                    Commands.Find("ShowMessagePalancaUsada"),
                },
            },
            new()
            {
                Code = $"{MainSceneCode}-9",
                Trigger = Trigger.AfterInputCommand,
                InputCommands = new List<InputCommandModel>
                {
                    InputCommands.Find("Entra"),
                },
                Commands = new List<CommandModel>
                {
                    Commands.Find("IsSetOpenEntry"),
                    Commands.Find("GotoInteriorScene"),
                },
            },
            new()
            {
                Code = $"Common-1",
                Trigger = Trigger.AfterInputCommand,
                InputCommands = new List<InputCommandModel>
                {
                    InputCommands.Find("UsaGuantesInputCommand"),
                },
                Commands = new List<CommandModel>
                {
                    Commands.Find("UseGuantes"),
                },
            },
        ];

        protected override IEnumerable<ObjectModel> CreateObjects() =>
        [
            new ObjectModel()
            {
                Code = "GuantesObject",
                Name = Vocabulary.Find("GuantesVocabulary"),
                Description = Messages.Find("globDescriptionMessage"),
                OwnerCode = "PlayerActor",
                Properties = ObjectProperties.None,
            }
        ];

        protected override IEnumerable<SceneModel> CreateScenes() =>
        [
            new()
            {
                Code = MainSceneCode,
                Description = Messages.Find("EntradaSceneDescription"),
                AfterInputCommandDispatchers = Dispatchers.Where(item => item.Code.StartsWith(MainSceneCode) && item.Trigger == Trigger.AfterInputCommand),
                BeforeInputCommandDispatchers = Dispatchers.Where(item => item.Code.StartsWith(MainSceneCode) && item.Trigger == Trigger.BeforeInputCommand)
            },
            new()
            {
                Code = "InteriorScene",
                Description = Messages.Find("InteriorSceneDescription"),
            }
        ];

        protected override IEnumerable<ActorModel> CreateActors() =>
        [
            new()
            {
                Code = "PlayerActor",
                Health = 7,
                // Objects = Objects,
            }
        ];

        protected override SettingsModel CreateSettings() => new()
        {
            Charset = 1,
            BackgroundColor = Color.White,
            BorderColor = Color.BrightPurple,
            Color = Color.White,
        };
    }
}
