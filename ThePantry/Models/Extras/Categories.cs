namespace ThePantry.Models.Extras;

public class Categories
{
    public enum IngredientCategory
    {
        Spice = 0,
        Meat = 1,
        Vegetable = 2,
        Fruit = 3,
        Dairy = 4,
        Grain = 5,
        Liquid = 6,
        Baking = 7
    }

    public enum RecipeCategory
    {
        Soup = 0,
        Appetizer = 1,
        Breakfast = 2,
        Lunch = 3,
        Dinner = 4,
        Dessert = 5,
        Sauce = 6
    }

    public enum MeasuredIn
    {
        Weight = 0,
        Volume = 1,
        Each = 2 //Buns would be measured in "eaches"
    }
}