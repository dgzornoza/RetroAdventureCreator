using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroAdventureCreator.Infrastructure.Game.Models;

public record LinkModel
{
    /// <summary>
    /// Vocabulary Id for link
    /// </summary>
    public string VocabularyId { get; init; } = default!;

    /// <summary>
    /// Scene Id to navigate
    /// </summary>
    public string SceneId { get; set; } = default!;

    /// <summary>
    /// Command to validate if can navigate
    /// </summary>
    public string CanNavigateCommand { get; set; } = default!;
}
