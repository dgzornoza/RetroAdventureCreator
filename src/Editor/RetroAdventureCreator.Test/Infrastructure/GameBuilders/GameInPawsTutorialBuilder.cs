﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Core.Extensions;
using RetroAdventureCreator.Infrastructure.Game.Enums;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Test.Infrastructure.Builders
{
    internal class GameInPawsTutorialBuilder : GameBuilder
    {
        protected override string MainSceneCode => "EntradaScene";

        protected override IEnumerable<FlagModel> CreateFlags() => new List<FlagModel>
        {
            new FlagModel { Code = "openEntry", Value = false }
        };

        protected override PlayerModel CreatePlayer() => new()
        {
            Health = 7,
            Objects = Objects,
        };

        protected override SettingsModel CreateSettings() => new()
        {
            Charset = 1,
            BackgroundColor = Color.White,
            BorderColor = Color.BrightPurple,
            Color = Color.White,
        };

        protected override IEnumerable<MessageModel> CreateMessages() => new List<MessageModel>
        {
            new MessageModel { Code = "globDescriptionMessage", Text = "Unos guantes" },
            new MessageModel
            {
                Code = "EntradaSceneDescription1",
                Text = "^^Tras meses de exploración en lo profundo de la selva, y después de haber sido perseguido, mordido, enfermado, pasado hambre y renunciado al todo, ",
            },
            new MessageModel
            {
                Code = "EntradaSceneDescription2",
                Text = "ahora por fin has encontrado lo que buscabas, el Templo de Ok, donde según cuenta la leyenda se guarda el Gran Diamante del Rajá Al-Meredin, rechazado por su amada y condenado a ser encerrado eternamente en lo profundo de la selva.^^ Hasta hoy.",
            },
            new MessageModel
            {
                Code = "InteriorSceneDescription",
                Text = "^^Has finalizado el primer puzzle^^ !!Bien echo.!!",
            }
        };

        protected override IEnumerable<VocabularyModel> CreateVocabulary() => new List<VocabularyModel>
        {
            new VocabularyModel { Code = "GuantesVocabulary", WordType = WordType.Noun, Synonyms = new List<string> { "GUANT" } },
            new VocabularyModel { Code = "BoqueteVocabulary", WordType = WordType.Noun, Synonyms = new List<string> { "BOQUE", "PALAN" }},
            new VocabularyModel { Code = "PalancaVocabulary", WordType = WordType.Noun, Synonyms = new List<string> { "PALAN" }},
            new VocabularyModel { Code = "TemploVocabulary", WordType = WordType.Noun, Synonyms = new List<string> { "TEMPL" }},
            new VocabularyModel { Code = "BloqueVocabulary", WordType = WordType.Noun, Synonyms = new List<string> { "BLOQU", "PIEDR" }},
            new VocabularyModel { Code = "AbreVocabulary", WordType = WordType.Verb, Synonyms = new List<string> { "ABRE"  } },
            new VocabularyModel { Code = "EntraVocabulary", WordType = WordType.Noun, Synonyms = new List<string> { "ENTRA" } },
            new VocabularyModel { Code = "EntraVocabulary", WordType = WordType.Verb, Synonyms = new List<string> { "ENTRA" } },
            new VocabularyModel { Code = "ExaminaVocabulary", WordType = WordType.Verb, Synonyms = new List<string> { "EX" } },
            new VocabularyModel { Code = "MueveVocabulary", WordType = WordType.Verb, Synonyms = new List<string> { "MUEVE" } },
            new VocabularyModel { Code = "EmpujaVocabulary", WordType = WordType.Verb, Synonyms = new List<string> { "EMPUJ" } },
            new VocabularyModel { Code = "TiraVocabulary", WordType = WordType.Verb, Synonyms = new List<string> { "TIRA" } },
        };

        protected override IEnumerable<ObjectModel> CreateObjects() => new List<ObjectModel>
        {
            new ObjectModel
            {
                Code = "GuantesObject",
                Name = Vocabulary.Find("GuantesVocabulary"),
                Description = Messages.Find("globDescriptionMessage"),
                Weight = 1,
            }
        };

        protected override IEnumerable<CommandModel> CreateCommands() => new List<CommandModel>
        {
            //    new CommandModel { Code = "", Token = CommandToken.AT, Arguments = new List<string> { "EntradaScene" } },
            new CommandModel { Code = "IsSetOpenEntry", Token = CommandToken.IsSet, Arguments = new List<string> { "openEntry" } },
            new CommandModel { Code = "IsUnSetOpenEntry", Token = CommandToken.IsUnset, Arguments = new List<string> { "openEntry" } },
            new CommandModel { Code = "SetOpenEntry", Token = CommandToken.Set, Arguments = new List<string> { "openEntry" } },
            new CommandModel { Code = "ShowMessageBloquePiedra", Token = CommandToken.Message, Arguments = new List<string> { "Un enorme bloque de piedra bloquea la entrada." } },
            new CommandModel { Code = "ShowMessageInteriorTemplo", Token = CommandToken.Message, Arguments = new List<string> { "Un siniestro pasaje conduce al interior del templo." } },
            new CommandModel { Code = "ShowMessageFuerza", Token = CommandToken.Message, Arguments = new List<string> { "Ni la fuerza combinada de diez como tú podría mover ese mastodonte de piedra." } },
            new CommandModel { Code = "ShowMessageNoBloque", Token = CommandToken.Message, Arguments = new List<string> { "Ya no hay bloque que abrir." } },
            new CommandModel { Code = "ShowMessageBoquete", Token = CommandToken.Message, Arguments = new List<string> { "Un oscuro boquete cuadrado, excavado por la mano del hombre, en cuyo interior ves una especie de palanca. También crees divisar ojos que te observan desde dentro." } },
            new CommandModel { Code = "ShowMessageAraña", Token = CommandToken.Message, Arguments = new List<string> { "Al meter la mano en el boquete notas un pichazo agudo. Casi al instante tu vista se comienza a nublar y vas perdiendo contacto con la realidad.^^       *** ESTAS MUERTO ***" } },
            new CommandModel { Code = "IsInUseGuantes", Token = CommandToken.InUse, Arguments = new List<string> { "GuantesObject" } },
            new CommandModel { Code = "ShowMessageTiraPalanca", Token = CommandToken.Message, Arguments = new List<string> { "Con no poco repelús metes la mano enguantada y mueves la palanca. Oyes un chasquido, y luego un ruido de correas deslizándose. Al cabo de un rato, el enorme bloque cede y se hunde en el suelo como tragado por el infierno. La entrada está libre." } },
            new CommandModel { Code = "ShowMessagePalancaUsada", Token = CommandToken.Message, Arguments = new List<string> { "No necesitas volver a meter la mano en \"eso\"." } },
            new CommandModel { Code = "GotoInteriorScene", Token = CommandToken.Goto, Arguments = new List<string> { "InteriorScene" } },
            new CommandModel { Code = "EndGame", Token = CommandToken.EndGame },
        };

        protected override IEnumerable<CommandGroupModel> CreateCommandsGroups() => new List<CommandGroupModel>();

        protected override IEnumerable<InputCommandModel> CreateInputCommands() => new List<InputCommandModel>
        {
            new InputCommandModel
            {
                Code = "AbreEntradaInputCommand",
                Verb = Vocabulary.Find("AbreVocabulary"),
                Nouns = new List<VocabularyModel> { Vocabulary.Find("EntraVocabulary") },
            },
            new InputCommandModel
            {
                Code = "AbreBloqueInputCommand",
                Verb = Vocabulary.Find("AbreVocabulary"),
                Nouns = new List<VocabularyModel> { Vocabulary.Find("BloqueVocabulary") },
            },
            new InputCommandModel
            {
                Code = "ExaminarBoquete",
                Verb = Vocabulary.Find("ExaminaVocabulary"),
                Nouns = new List<VocabularyModel> { Vocabulary.Find("BoqueteVocabulary") },
            },
            new InputCommandModel
            {
                Code = "MuevePalanca",
                Verb = Vocabulary.Find("MueveVocabulary"),
                Nouns = new List<VocabularyModel> { Vocabulary.Find("PalancaVocabulary") },
            },
            new InputCommandModel
            {
                Code = "EmpujaPalanca",
                Verb =  Vocabulary.Find("EmpujaVocabulary"),
                Nouns = new List<VocabularyModel> { Vocabulary.Find("PalancaVocabulary") },
            },
            new InputCommandModel
            {
                Code = "TiraPalanca",
                Verb =  Vocabulary.Find("TiraVocabulary"),
                Nouns = new List<VocabularyModel> { Vocabulary.Find("PalancaVocabulary") },
            },
            new InputCommandModel
            {
                Code = "Entra",
                Verb = Vocabulary.Find("EntraVocabulary"),
            },
        };

        protected override IEnumerable<DispatcherModel> CreateDispatchers() => new List<DispatcherModel>
        {
            new DispatcherModel
            {
                Code = $"{MainSceneCode}-1",
                Trigger = Trigger.BeforeInputCommand,
                Commands = new List<CommandModel>
                {
                    Commands.Find("IsSetOpenEntry"),
                    Commands.Find("ShowMessageBloquePiedra"),
                }
            },
            new DispatcherModel
            {
                Code = $"{MainSceneCode}-2",
                Trigger = Trigger.BeforeInputCommand,
                Commands = new List<CommandModel>
                {
                    Commands.Find("IsUnSetOpenEntry"),
                    Commands.Find("ShowMessageInteriorTemplo"),
                }
            },
            new DispatcherModel
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
            new DispatcherModel
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
            new DispatcherModel
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
            new DispatcherModel
            {
                Code = $"{MainSceneCode}-5",
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
                    Commands.Find("ShowMessageAraña"),
                    Commands.Find("EndGame"),
                },
            },
            new DispatcherModel
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
                    Commands.Find("IsInUseGuantes"),
                    Commands.Find("SetOpenEntry"),
                    Commands.Find("ShowMessageTiraPalanca"),
                },
            },
            new DispatcherModel
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
                    Commands.Find("IsSetOpenEntry"),
                    Commands.Find("ShowMessagePalancaUsada"),
                },
            },
            new DispatcherModel
            {
                Code = $"{MainSceneCode}-8",
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
        };

        protected override IEnumerable<SceneModel> CreateScenes() => new List<SceneModel>
        {
            new SceneModel
            {
                Code = MainSceneCode,
                Description = Messages.Where(item => item.Code.StartsWith("EntradaSceneDescription")),
                Dispatchers = Dispatchers.Where(item => item.Code.StartsWith(MainSceneCode))
            },
            new SceneModel
            {
                Code = "InteriorScene",
                Description = Messages.Where(item => item.Code == "InteriorSceneDescription"),
            }
        };
    }
}
