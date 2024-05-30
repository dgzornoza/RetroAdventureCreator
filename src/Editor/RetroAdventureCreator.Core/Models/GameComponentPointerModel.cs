using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetroAdventureCreator.Infrastructure.Game.Interfaces;

namespace RetroAdventureCreator.Core.Models;

// TODO: dgzornoza, RelativePointer en principio ya no debe hacer falta, ya que se ha implementado un sistema de punteros absolutos
// mediante indices a los elementos y una tabla principal con los punteros a los elementos.
internal record GameComponentPointerModel(string Code, int RelativePointer) : IUniqueKey;
