using ThePantry.Models.Extras;

namespace ThePantry.Models.Recipe;

public class Recipe : IRecipe
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Categories.RecipeCategory RecipeCategory { get; set; }
    public List<string> Instructions { get; set; }
    public int ServingsPerRecipe { get; set; }
    
    // Constructors
    public Recipe(string name, Categories.RecipeCategory recipeCategory, List<string> instructions,
        int servingsPerRecipe)
    {
        Id = Guid.NewGuid();
        Name = name;
        RecipeCategory = recipeCategory;
        Instructions = instructions;
        ServingsPerRecipe = servingsPerRecipe;
    }
    
    // Parameterless constructor
    public Recipe()
    {
        Id = Guid.NewGuid();
    }
}