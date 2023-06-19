﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroAdventureCreator.Core.Models;

internal record SerializerResultModel(IEnumerable<GameComponentKeyModel> GameComponentKeysModel, byte[] Data);
