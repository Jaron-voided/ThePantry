// src/utils/enumHelpers.js

export const getMeasuredInLabel = (value) => {
    switch (value) {
        case 0: return 'Weight';
        case 1: return 'Volume';
        case 2: return 'Each';
        default: return 'Unknown';
    }
};

export const getIngredientCategoryLabel = (value) => {
    switch (value) {
        case 0: return 'Spice';
        case 1: return 'Meat';
        case 2: return 'Vegetable';
        case 3: return 'Fruit';
        case 4: return 'Dairy';
        case 5: return 'Grain';
        case 6: return 'Liquid';
        case 7: return 'Baking';
        default: return 'Unknown';
    }
};

export const getRecipeCategoryLabel = (value) => {
    switch (value) {
        case 0: return 'Soup';
        case 1: return 'Appetizer';
        case 2: return 'Breakfast';
        case 3: return 'Lunch';
        case 4: return 'Dinner';
        case 5: return 'Dessert';
        case 6: return 'Sauce';
    }
}