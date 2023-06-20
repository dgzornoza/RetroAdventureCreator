using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Helpers;

namespace RetroAdventureCreator.Test.Theories.GameTests;

public class GameModelTest
{
    [Theory]
    [InlineData("GameInPawsTutorial.json")]
    public void CreateGameModel(string jsonFile)
    {
        // Arrange
        var gameJson = FilesHelpers.GetLocalResourceJsonObject<SceneModel>(jsonFile) ?? throw new InvalidOperationException();

        var messages = new List<MessageModel>
        {
            new MessageModel { Code = "globDescriptionMessage", Text = "Unos guantes" },
            new MessageModel
            {
                Code = "EntradaSceneDescription",
                Text = "^^Tras meses de exploración en lo profundo de la selva, y después de haber sido perseguido, mordido, enfermado, pasado hambre y renunciado al todo, ahora por fin has encontrado lo que buscabas, el Templo de Ok, donde según cuenta la leyenda se guarda el Gran Diamante del Rajá Al-Meredin, rechazado por su amada y condenado a ser encerrado eternamente en lo profundo de la selva.^^ Hasta hoy.",
            },
            new MessageModel
            {
                Code = "InteriorSceneDescription",
                Text = "^^Has finalizado el primer puzzle^^ !!Bien echo.!!",
            }
        };

        var vocabulary = new List<VocabularyModel>
        {
            new VocabularyModel { Code = "GuantesVocabulary", WordType = WordType.Noun, Synonyms = new List<string> { "GUANT" } },
            new VocabularyModel { Code = "BoqueteVocabulary", WordType = WordType.Noun, Synonyms = new List<string> { "BOQUE", "PALAN" }},
            new VocabularyModel { Code = "PalancaVocabulary", WordType = WordType.Noun, Synonyms = new List<string> { "PALAN" }},
            new VocabularyModel { Code = "TemploVocabulary", WordType = WordType.Noun, Synonyms = new List<string> { "TEMPL" }},
            new VocabularyModel { Code = "BloqueVocabulary", WordType = WordType.Noun, Synonyms = new List<string> { "BLOQU", "PIEDR" }},
            new VocabularyModel { Code = "AbreVocabulary", WordType = WordType.Verb, Synonyms = new List<string> { "ABRE"  } },
            new VocabularyModel { Code = "EntraVocabulary", WordType = WordType.Noun, Synonyms = new List<string> { "ENTRA" } },
            new VocabularyModel { Code = "ExaminaVocabulary", WordType = WordType.Verb, Synonyms = new List<string> { "EX" } },
            new VocabularyModel { Code = "MueveVocabulary", WordType = WordType.Verb, Synonyms = new List<string> { "MUEVE" } },
            new VocabularyModel { Code = "EmpujaVocabulary", WordType = WordType.Verb, Synonyms = new List<string> { "EMPUJ" } },
            new VocabularyModel { Code = "TiraVocabulary", WordType = WordType.Verb, Synonyms = new List<string> { "TIRA" } },
        };

        var objects = new List<ObjectModel>
        {
            new ObjectModel
            {
                Code = "GuantesObject",
                Name = vocabulary.Find("GuantesVocabulary"),
                Description = messages.Find("globDescriptionMessage"),
                Weight = 1,
            }
        };

        var commands = new List<CommandModel>
        {
            //    new CommandModel { Code = "", Token = CommandToken.AT, Arguments = new List<string> { "EntradaScene" } },
            new CommandModel { Code = "IsSetOpenEntry", Token = CommandToken.IS_SET, Arguments = new List<string> { "openEntry" } },
            new CommandModel { Code = "IsUnSetOpenEntry", Token = CommandToken.IS_UNSET, Arguments = new List<string> { "openEntry" } },
            new CommandModel { Code = "SetOpenEntry", Token = CommandToken.SET, Arguments = new List<string> { "openEntry" } },
            new CommandModel { Code = "ShowMessageBloquePiedra", Token = CommandToken.MESSAGE, Arguments = new List<string> { "Un enorme bloque de piedra bloquea la entrada." } },
            new CommandModel { Code = "ShowMessageInteriorTemplo", Token = CommandToken.MESSAGE, Arguments = new List<string> { "Un siniestro pasaje conduce al interior del templo." } },
            new CommandModel { Code = "ShowMessageFuerza", Token = CommandToken.MESSAGE, Arguments = new List<string> { "Ni la fuerza combinada de diez como tú podría mover ese mastodonte de piedra." } },
            new CommandModel { Code = "ShowMessageNoBloque", Token = CommandToken.MESSAGE, Arguments = new List<string> { "Ya no hay bloque que abrir." } },
            new CommandModel { Code = "ShowMessageBoquete", Token = CommandToken.MESSAGE, Arguments = new List<string> { "Un oscuro boquete cuadrado, excavado por la mano del hombre, en cuyo interior ves una especie de palanca. También crees divisar ojos que te observan desde dentro." } },
            new CommandModel { Code = "ShowMessageAraña", Token = CommandToken.MESSAGE, Arguments = new List<string> { "Al meter la mano en el boquete notas un pichazo agudo. Casi al instante tu vista se comienza a nublar y vas perdiendo contacto con la realidad.^^       *** ESTAS MUERTO ***" } },
            new CommandModel { Code = "IsInUseGuantes", Token = CommandToken.IN_USE, Arguments = new List<string> { "GuantesObject" } },
            new CommandModel { Code = "ShowMessageTiraPalanca", Token = CommandToken.MESSAGE, Arguments = new List<string> { "Con no poco repelús metes la mano enguantada y mueves la palanca. Oyes un chasquido, y luego un ruido de correas deslizándose. Al cabo de un rato, el enorme bloque cede y se hunde en el suelo como tragado por el infierno. La entrada está libre." } },
            new CommandModel { Code = "ShowMessagePalancaUsada", Token = CommandToken.MESSAGE, Arguments = new List<string> { "No necesitas volver a meter la mano en \"eso\"." } },
            new CommandModel { Code = "GotoInteriorScene", Token = CommandToken.GOTO, Arguments = new List<string> { "InteriorScene" } },
            new CommandModel { Code = "EndGame", Token = CommandToken.END_GAME },
        };

        var inputCommands = new List<InputCommandModel>
        {
            new InputCommandModel
            {
                Code = "AbreEntradaInputCommand",
                Verb = vocabulary.Find("AbreVocabulary"),
                Nouns = new List<VocabularyModel> { vocabulary.Find("EntraVocabulary") },
            },
            new InputCommandModel
            {
                Code = "AbreBloqueInputCommand",
                Verb = vocabulary.Find("AbreVocabulary"),
                Nouns = new List<VocabularyModel> { vocabulary.Find("BloqueVocabulary") },
            },
            new InputCommandModel
            {
                Code = "ExaminarBoquete",
                Verb = vocabulary.Find("ExaminaVocabulary"),
                Nouns = new List<VocabularyModel> { vocabulary.Find("BoqueteVocabulary") },
            },
            new InputCommandModel
            {
                Code = "MuevePalanca",
                Verb = vocabulary.Find("MueveVocabulary"),
                Nouns = new List<VocabularyModel> {vocabulary.Find("PalancaVocabulary") },
            },
            new InputCommandModel
            {
                Code = "EmpujaPalanca",
                Verb =  vocabulary.Find("EmpujaVocabulary"),
                Nouns = new List<VocabularyModel> {vocabulary.Find("PalancaVocabulary") },
            },
            new InputCommandModel
            {
                Code = "TiraPalanca",
                Verb =  vocabulary.Find("TiraVocabulary"),
                Nouns = new List<VocabularyModel> {vocabulary.Find("PalancaVocabulary") },
            },
            new InputCommandModel
            {
                Code = "Entra",
                Verb = vocabulary.Find("EntraVocabulary"),
            },
        };

        var game = new GameModel()
        {
            Player = new PlayerModel
            {
                Health = 7,
                Objects = objects,
            },
            Flags = new Dictionary<string, bool> { { "openEntry", false } },
            Settings = new SettingsModel
            {
                Charset = 1,
                BackgroundColor = Color.Black,
                BorderColor = Color.Black,
                Color = Color.White,
            },
            Messages = messages,
            Vocabulary = vocabulary,
            Objects = objects,
            Commands = commands,
            InputCommands = inputCommands,
            MainSceneCode = "EntradaScene",
            Scenes = new List<SceneModel>
            {
                new SceneModel
                {
                    Code = "EntradaScene",
                    Description = messages.Find("EntradaSceneDescription"),
                    Dispatchers = new List<DispatcherModel>
                    {
                        new DispatcherModel
                        {
                            Trigger = Trigger.BeforeInputCommand,
                            Commands = new List<CommandModel>
                            {
                                commands.Find("IsSetOpenEntry"),
                                commands.Find("ShowMessageBloquePiedra"),
                            }
                        },
                        new DispatcherModel
                        {
                            Trigger = Trigger.BeforeInputCommand,
                            Commands = new List<CommandModel>
                            {
                                commands.Find("IsUnSetOpenEntry"),
                                commands.Find("ShowMessageInteriorTemplo"),
                            }
                        },
                        new DispatcherModel
                        {
                            Trigger = Trigger.AfterInputCommand,
                            InputCommands = new List<InputCommandModel>
                            {
                                inputCommands.Find("AbreEntradaInputCommand"),
                                inputCommands.Find("AbreBloqueInputCommand"),
                            },
                            Commands = new List<CommandModel>
                            {
                                commands.Find("IsUnSetOpenEntry"),
                                commands.Find("ShowMessageFuerza"),
                            }
                        },
                        new DispatcherModel
                        {
                            Trigger = Trigger.AfterInputCommand,
                            InputCommands = new List<InputCommandModel>
                            {
                                inputCommands.Find("AbreEntradaInputCommand"),
                                inputCommands.Find("AbreBloqueInputCommand"),
                            },
                            Commands = new List<CommandModel>
                            {
                                commands.Find("IsSetOpenEntry"),
                                commands.Find("ShowMessageNoBloque"),
                            }
                        },
                        new DispatcherModel
                        {
                            Trigger = Trigger.AfterInputCommand,
                            InputCommands = new List<InputCommandModel>
                            {
                                inputCommands.Find("ExaminarBoquete"),
                            },
                            Commands = new List<CommandModel>
                            {
                                commands.Find("ShowMessageBoquete"),
                            }
                        },
                        new DispatcherModel
                        {
                            Trigger = Trigger.AfterInputCommand,
                            InputCommands = new List<InputCommandModel>
                            {
                                inputCommands.Find("MuevePalanca"),
                                inputCommands.Find("EmpujaPalanca"),
                                inputCommands.Find("TiraPalanca"),
                            },
                            Commands = new List<CommandModel>
                            {
                                commands.Find("IsUnSetOpenEntry"),
                                commands.Find("ShowMessageAraña"),
                                commands.Find("EndGame"),
                            },
                        },
                        new DispatcherModel
                        {
                            Trigger = Trigger.AfterInputCommand,
                            InputCommands = new List<InputCommandModel>
                            {
                                inputCommands.Find("MuevePalanca"),
                                inputCommands.Find("EmpujaPalanca"),
                                inputCommands.Find("TiraPalanca"),
                            },
                            Commands = new List<CommandModel>
                            {
                                commands.Find("IsUnSetOpenEntry"),
                                commands.Find("IsInUseGuantes"),
                                commands.Find("SetOpenEntry"),
                                commands.Find("ShowMessageTiraPalanca"),
                            },
                        },
                        new DispatcherModel
                        {
                            Trigger = Trigger.AfterInputCommand,
                            InputCommands = new List<InputCommandModel>
                            {
                                inputCommands.Find("MuevePalanca"),
                                inputCommands.Find("EmpujaPalanca"),
                                inputCommands.Find("TiraPalanca"),
                            },
                            Commands = new List<CommandModel>
                            {
                                commands.Find("IsSetOpenEntry"),
                                commands.Find("ShowMessagePalancaUsada"),
                            },
                        },
                        new DispatcherModel
                        {
                            Trigger = Trigger.AfterInputCommand,
                            InputCommands = new List<InputCommandModel>
                            {
                                inputCommands.Find("Entra"),
                            },
                            Commands = new List<CommandModel>
                            {
                                commands.Find("IsSetOpenEntry"),
                                commands.Find("GotoInteriorScene"),
                            },
                        },
                    }
                },
                new SceneModel
                {
                    Code = "InteriorScene",
                    Description = messages.Find("InteriorSceneDescription"),
                }
            }
        };


        // Act
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(game);

        // Assert
        Assert.NotNull(gameJson);
    }
}

