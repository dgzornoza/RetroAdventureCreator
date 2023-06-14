using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;
using RetroAdventureCreator.Test.Helpers;

namespace RetroAdventureCreator.Test.Theories.SceneTests;

public class GameModelTest
{
    [Theory]
    [InlineData("GameBasic.json")]
    public void CreateGameModel(string jsonFile)
    {
        // Arrange
        //var game = FilesHelpers.GetLocalResourceJsonObject<SceneModel>(jsonFile) ?? throw new InvalidOperationException();

        // Act
        var game = new GameModel()
        {
            Player = new PlayerModel
            {
                Health = 7,
                Objects = new List<ObjectModel>
                {
                    new ObjectModel
                    {
                        Id = "GuantesObject",
                        Name = new VocabularyModel { Id = "", WordType = WordType.Noun, Synonyms = new List<string> { "GUANT" } },
                        Description = new MessageModel { Id = "", Text = "Unos guantes" },
                        Weight = 1,
                    }
                }
            },
            Flags = new Dictionary<string, bool> { { "openEntry", false } },
            Settings = new SettingsModel
            {
                Charset = 1,
                BackgroundColor = Color.Black,
                BorderColor = Color.Black,
                Color = Color.White,
            },
            Vocabulary = new List<VocabularyModel>
            {
                new VocabularyModel { Id = "BoqueteVocabulary", WordType = WordType.Noun, Synonyms = new List<string> { "BOQUE", "PALAN" }},
                new VocabularyModel { Id = "TemploVocabulary", WordType = WordType.Noun, Synonyms = new List<string> { "TEMPL" }},
                new VocabularyModel { Id = "BloqueVocabulary", WordType = WordType.Noun, Synonyms = new List<string> { "BLOQU", "PIEDR" }},
            },
            MainSceneId = "EntradaScene",            
            Scenes = new List<SceneModel>
            {
                new SceneModel
                {
                    Id = "EntradaScene",
                    Description = new MessageModel
                    {
                        Id = "EntradaSceneDescription",
                        Text = "^^Tras meses de exploración en lo profundo de la selva, y después de haber sido perseguido, mordido, enfermado, pasado hambre y renunciado al todo, ahora por fin has encontrado lo que buscabas, el Templo de Ok, donde según cuenta la leyenda se guarda el Gran Diamante del Rajá Al-Meredin, rechazado por su amada y condenado a ser encerrado eternamente en lo profundo de la selva.^^ Hasta hoy.",
                    },
                    Dispatchers = new List<DispatcherModel>
                    {
                        new DispatcherModel
                        {
                            Id = "",
                            Trigger = Trigger.BeforeInputCommand,
                            Commands = new List<CommandModel>
                            {
                                new CommandModel { Id = "", Token = CommandToken.AT, Arguments = new List<string> { "EntradaScene" } },
                                new CommandModel { Id = "", Token = CommandToken.ZERO, Arguments = new List<string> { "openEntry" } },
                                new CommandModel { Id = "", Token = CommandToken.MESSAGE, Arguments = new List<string> { "Un enorme bloque de piedra bloquea la entrada." } }
                            }
                        },
                        new DispatcherModel
                        {
                            Id = "",
                            Trigger = Trigger.BeforeInputCommand,
                            Commands = new List<CommandModel>
                            {
                                new CommandModel { Id = "", Token = CommandToken.AT, Arguments = new List<string> { "EntradaScene" } },
                                new CommandModel { Id = "", Token = CommandToken.NOT_ZERO, Arguments = new List<string> { "openEntry" } },
                                new CommandModel { Id = "", Token = CommandToken.MESSAGE, Arguments = new List<string> { "Un siniestro pasaje conduce al interior del templo." } }
                            }
                        },
                        new DispatcherModel
                        {
                            Id = "",
                            Trigger = Trigger.AfterInputCommand,
                            InputCommands = new List<InputCommandModel>
                            {
                                new InputCommandModel
                                {
                                    Id = "",
                                    Verb = new VocabularyModel { Id = "", WordType = WordType.Verb, Synonyms = new List<string> { "ABRE"  } },
                                    Nouns = new List<VocabularyModel> { new VocabularyModel { Id = "", WordType = WordType.Noun, Synonyms = new List<string> { "ENTRA" } } },
                                },
                                new InputCommandModel
                                {
                                    Id = "",
                                    Verb = new VocabularyModel { Id = "", WordType = WordType.Verb, Synonyms = new List<string> { "ABRE"  } },
                                    Nouns = new List<VocabularyModel> { new VocabularyModel { Id = "", WordType = WordType.Noun, Synonyms = new List<string> { "BLOQU" } } },
                                },
                            },
                            Commands = new List<CommandModel>
                            {
                                new CommandModel { Id = "", Token = CommandToken.AT, Arguments = new List<string> { "EntradaScene" } },
                                new CommandModel { Id = "", Token = CommandToken.ZERO, Arguments = new List<string> { "openEntry" } },
                                new CommandModel { Id = "", Token = CommandToken.MESSAGE, Arguments = new List<string> { "Ni la fuerza combinada de diez como tú podría mover\r\n//\tese mastodonte de piedra." } }
                            }
                        },
                        new DispatcherModel
                        {
                            Id = "",
                            Trigger = Trigger.AfterInputCommand,
                            InputCommands = new List<InputCommandModel>
                            {
                                new InputCommandModel
                                {
                                    Id = "",
                                    Verb = new VocabularyModel { Id = "", WordType = WordType.Verb, Synonyms = new List<string> { "ABRE"  } },
                                    Nouns = new List<VocabularyModel> { new VocabularyModel { Id = "", WordType = WordType.Noun, Synonyms = new List<string> { "ENTRA" } } },
                                },
                                new InputCommandModel
                                {
                                    Id = "",
                                    Verb = new VocabularyModel { Id = "", WordType = WordType.Verb, Synonyms = new List<string> { "ABRE"  } },
                                    Nouns = new List<VocabularyModel> { new VocabularyModel { Id = "", WordType = WordType.Noun, Synonyms = new List<string> { "BLOQU" } } },
                                },
                            },
                            Commands = new List<CommandModel>
                            {
                                new CommandModel { Id = "", Token = CommandToken.AT, Arguments = new List<string> { "EntradaScene" } },
                                new CommandModel { Id = "", Token = CommandToken.NOT_ZERO, Arguments = new List<string> { "openEntry" } },
                                new CommandModel { Id = "", Token = CommandToken.MESSAGE, Arguments = new List<string> { "Ya no hay bloque que abrir." } }
                            }
                        },
                        new DispatcherModel
                        {
                            Id = "",
                            Trigger = Trigger.AfterInputCommand,
                            InputCommands = new List<InputCommandModel>
                            {
                                new InputCommandModel
                                {
                                    Id = "",
                                    Verb = new VocabularyModel { Id = "", WordType = WordType.Verb, Synonyms = new List<string> { "EX" } },
                                    Nouns = new List<VocabularyModel> { new VocabularyModel { Id = "", WordType = WordType.Noun, Synonyms = new List<string> { "BOQUE" } } },
                                },
                            },
                            Commands = new List<CommandModel>
                            {
                                new CommandModel { Id = "", Token = CommandToken.AT, Arguments = new List<string> { "EntradaScene" } },
                                new CommandModel { Id = "", Token = CommandToken.MESSAGE, Arguments = new List<string> { "Un oscuro boquete cuadrado, excavado por la mano del hombre, en cuyo interior ves una especie de palanca. También crees divisar ojos que te observan desde dentro." } }
                            }
                        },
                        new DispatcherModel
                        {
                            Id = "",
                            Trigger = Trigger.AfterInputCommand,
                            InputCommands = new List<InputCommandModel>
                            {
                                new InputCommandModel
                                {
                                    Id = "",
                                    Verb = new VocabularyModel { Id = "", WordType = WordType.Verb, Synonyms = new List<string> { "MUEVE" } },
                                    Nouns = new List<VocabularyModel> { new VocabularyModel { Id = "", WordType = WordType.Noun, Synonyms = new List<string> { "PALAN" } } },
                                },
                                new InputCommandModel
                                {
                                    Id = "",
                                    Verb = new VocabularyModel { Id = "", WordType = WordType.Verb, Synonyms = new List<string> { "EMPUJ" } },
                                    Nouns = new List<VocabularyModel> { new VocabularyModel { Id = "", WordType = WordType.Noun, Synonyms = new List<string> { "PALAN" } } },
                                },
                                new InputCommandModel
                                {
                                    Id = "",
                                    Verb = new VocabularyModel { Id = "", WordType = WordType.Verb, Synonyms = new List<string> { "TIRA" } },
                                    Nouns = new List<VocabularyModel> { new VocabularyModel { Id = "", WordType = WordType.Noun, Synonyms = new List<string> { "PALAN" } } },
                                },
                            },
                            Commands = new List<CommandModel>
                            {
                                new CommandModel { Id = "", Token = CommandToken.AT, Arguments = new List<string> { "EntradaScene" } },
                                new CommandModel { Id = "", Token = CommandToken.ZERO, Arguments = new List<string> { "openEntry" } },
                                new CommandModel { Id = "", Token = CommandToken.NOT_IN_USE, Arguments = new List<string> { "GuantesObject" } },
                                new CommandModel { Id = "", Token = CommandToken.MESSAGE, Arguments = new List<string> { "Al meter la mano en el boquete notas un pichazo agudo. Casi al instante tu vista se comienza a nublar y vas perdiendo contacto con la realidad.^^       *** ESTAS MUERTO ***" } },
                                new CommandModel { Id = "", Token = CommandToken.END_GAME }
                            },
                        },
                        new DispatcherModel
                        {
                            Id = "",
                            Trigger = Trigger.AfterInputCommand,
                            InputCommands = new List<InputCommandModel>
                            {
                                new InputCommandModel
                                {
                                    Id = "",
                                    Verb = new VocabularyModel { Id = "", WordType = WordType.Verb, Synonyms = new List<string> { "MUEVE" } },
                                    Nouns = new List<VocabularyModel> { new VocabularyModel { Id = "", WordType = WordType.Noun, Synonyms = new List<string> { "PALAN" } } },
                                },
                                new InputCommandModel
                                {
                                    Id = "",
                                    Verb = new VocabularyModel { Id = "", WordType = WordType.Verb, Synonyms = new List<string> { "EMPUJ" } },
                                    Nouns = new List<VocabularyModel> { new VocabularyModel { Id = "", WordType = WordType.Noun, Synonyms = new List<string> { "PALAN" } } },
                                },
                                new InputCommandModel
                                {
                                    Id = "",
                                    Verb = new VocabularyModel { Id = "", WordType = WordType.Verb, Synonyms = new List<string> { "TIRA" } },
                                    Nouns = new List<VocabularyModel> { new VocabularyModel { Id = "", WordType = WordType.Noun, Synonyms = new List<string> { "PALAN" } } },
                                },
                            },
                            Commands = new List<CommandModel>
                            {
                                new CommandModel { Id = "", Token = CommandToken.AT, Arguments = new List<string> { "EntradaScene" } },
                                new CommandModel { Id = "", Token = CommandToken.ZERO, Arguments = new List<string> { "openEntry" } },
                                new CommandModel { Id = "", Token = CommandToken.IN_USE, Arguments = new List<string> { "GuantesObject" } },
                                new CommandModel { Id = "", Token = CommandToken.SET, Arguments = new List<string> { "openEntry" } },
                                new CommandModel { Id = "", Token = CommandToken.MESSAGE, Arguments = new List<string> { "Con no poco repelús metes la mano enguantada y mueves la palanca. Oyes un chasquido, y luego un ruido de correas deslizándose. Al cabo de un rato, el enorme bloque cede y se hunde en el suelo como tragado por el infierno. La entrada está libre." } },
                            },
                        },
                        new DispatcherModel
                        {
                            Id = "",
                            Trigger = Trigger.AfterInputCommand,
                            InputCommands = new List<InputCommandModel>
                            {
                                new InputCommandModel
                                {
                                    Id = "",
                                    Verb = new VocabularyModel { Id = "", WordType = WordType.Verb, Synonyms = new List<string> { "MUEVE" } },
                                    Nouns = new List<VocabularyModel> { new VocabularyModel { Id = "", WordType = WordType.Noun, Synonyms = new List<string> { "PALAN" } } },
                                },
                                new InputCommandModel
                                {
                                    Id = "",
                                    Verb = new VocabularyModel { Id = "", WordType = WordType.Verb, Synonyms = new List<string> { "EMPUJ" } },
                                    Nouns = new List<VocabularyModel> { new VocabularyModel { Id = "", WordType = WordType.Noun, Synonyms = new List<string> { "PALAN" } } },
                                },
                                new InputCommandModel
                                {
                                    Id = "",
                                    Verb = new VocabularyModel { Id = "", WordType = WordType.Verb, Synonyms = new List<string> { "TIRA" } },
                                    Nouns = new List<VocabularyModel> { new VocabularyModel { Id = "", WordType = WordType.Noun, Synonyms = new List<string> { "PALAN" } } },
                                },
                            },
                            Commands = new List<CommandModel>
                            {
                                new CommandModel { Id = "", Token = CommandToken.AT, Arguments = new List<string> { "EntradaScene" } },
                                new CommandModel { Id = "", Token = CommandToken.NOT_ZERO, Arguments = new List<string> { "openEntry" } },
                                new CommandModel { Id = "", Token = CommandToken.MESSAGE, Arguments = new List<string> { "No necesitas volver a meter la mano en \"eso\"." } },
                            },
                        },
                        new DispatcherModel
                        {
                            Id = "",
                            Trigger = Trigger.AfterInputCommand,
                            InputCommands = new List<InputCommandModel>
                            {
                                new InputCommandModel
                                {
                                    Id = "",
                                    Verb = new VocabularyModel { Id = "", WordType = WordType.Verb, Synonyms = new List<string> { "ENTRA" } },
                                },
                            },
                            Commands = new List<CommandModel>
                            {
                                new CommandModel { Id = "", Token = CommandToken.AT, Arguments = new List<string> { "EntradaScene" } },
                                new CommandModel { Id = "", Token = CommandToken.NOT_ZERO, Arguments = new List<string> { "openEntry" } },
                                new CommandModel { Id = "", Token = CommandToken.GOTO, Arguments = new List<string> { "InteriorScene" } },
                            },
                        },
                    }
                },
                new SceneModel
                {
                    Id = "InteriorScene",
                    Description = new MessageModel
                    {
                        Id = "InteriorSceneSceneDescription",
                        Text = "^^Has finalizado el primer puzzle^^ !!Bien echo.!!",
                    },
                }
            }
        };

        var json = Newtonsoft.Json.JsonConvert.SerializeObject(game);

        // Assert
        Assert.NotNull(game);
    }
}
