using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Infrastructure.Game.Models;

namespace RetroAdventureCreator.Core.Models;

internal record InputCommandsSerializerArgumentsModel(IEnumerable<InputCommandModel> InputCommands, VocabularySerializerResultModel VocabularySerialized);
