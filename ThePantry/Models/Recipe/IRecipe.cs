using ThePantry.Models.Extras;

namespace ThePantry.Models.Recipe;

public interface IRecipe
{
    Guid Id { get; set; }
    string Name { get; set; }
    Categories.RecipeCategory RecipeCategory { get; set; }
    List<Guid> MeasurementIds { get; set; }
    List<string> Instructions { get; set; }
    int ServingsPerRecipe { get; set; }
}