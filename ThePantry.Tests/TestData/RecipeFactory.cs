
using ThePantry.Models.Recipe;
using ThePantry.Models.Extras;

namespace ThePantry.Tests.TestData;

public static class RecipeFactory
{
    public static Recipe CreateRecipe(string name, Categories.RecipeCategory category, List<string> instructions, int servings)
    {
        return new Recipe
        {
            Id = Guid.NewGuid(),
            Name = name,
            RecipeCategory = category,
            Instructions = instructions,
            ServingsPerRecipe = servings
        };
    }

    public static Recipe CreateSoupRecipe()
    {
        return CreateRecipe(
            "Tomato Soup",
            Categories.RecipeCategory.Soup,
            new List<string>
            {
                "1. Heat oil in a pan.",
                "2. Add tomatoes and cook until soft.",
                "3. Blend the mixture until smooth.",
                "4. Add seasoning and simmer for 10 minutes."
            },
            4
        );
    }

    public static Recipe CreateAppetizerRecipe()
    {
        return CreateRecipe(
            "Bruschetta",
            Categories.RecipeCategory.Appetizer,
            new List<string>
            {
                "1. Toast slices of bread.",
                "2. Rub garlic onto the toasted bread.",
                "3. Top with chopped tomatoes, basil, and olive oil."
            },
            6
        );
    }

    public static Recipe CreateDessertRecipe()
    {
        return CreateRecipe(
            "Chocolate Cake",
            Categories.RecipeCategory.Dessert,
            new List<string>
            {
                "1. Preheat oven to 350°F.",
                "2. Mix flour, cocoa, sugar, and eggs in a bowl.",
                "3. Pour into a greased pan and bake for 30 minutes.",
                "4. Let it cool and frost with chocolate frosting."
            },
            8
        );
    }

    public static Recipe CreateLunchRecipe()
    {
        return CreateRecipe(
            "Grilled Cheese Sandwich",
            Categories.RecipeCategory.Lunch,
            new List<string>
            {
                "1. Butter the bread slices.",
                "2. Place cheese between two slices of bread.",
                "3. Grill on medium heat until golden brown."
            },
            2
        );
    }

    public static List<Recipe> CreateAllRecipes()
    {
        return new List<Recipe>
        {
            CreateSoupRecipe(),
            CreateAppetizerRecipe(),
            CreateDessertRecipe(),
            CreateLunchRecipe()
        };
    }
}

