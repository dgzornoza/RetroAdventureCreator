using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroAdventureCreator.Core.Models;

internal record SerializerResultKeyModel(IEnumerable<GameComponentKeyModel> GameComponentKeysModel, byte[] Data) : SerializerResultModel(Data);

internal record SerializerResultModel(byte[] Data);
