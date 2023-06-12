using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroAdventureCreator.Infrastructure.Game.Models;

public record AboutModel
{
    public string Name { get; init; } = default!;

    public string Description { get; init; } = default!;

    public string Author { get; init; } = default!;

    public string Copyright { get; init; } = default!;
}
