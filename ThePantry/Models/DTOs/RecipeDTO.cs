using ThePantry.Models.Extras;

namespace ThePantry.Models.DTOs;

public class RecipeDTO
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public Categories.RecipeCategory RecipeCategory { get; init; }
    public List<string> Instructions { get; init; }
    public int ServingsPerRecipe { get; init; }
}