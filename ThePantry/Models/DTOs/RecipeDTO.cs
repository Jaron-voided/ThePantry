using ThePantry.Models.Extras;

namespace ThePantry.Models.DTOs;

public class RecipeDTO
{
    Guid Id { get; set; }
    string Name { get; set; }
    Categories.RecipeCategory RecipeCategory { get; set; }
    List<Guid> MeasurementIds { get; set; }
    List<string> Instructions { get; set; }
    int ServingsPerRecipe { get; set; }
}