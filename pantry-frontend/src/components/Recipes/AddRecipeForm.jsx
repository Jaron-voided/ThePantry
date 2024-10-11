import React, { useState } from 'react';
import PropTypes from 'prop-types';


// Enums

const recipeCategoryOptions = [
    { value: 0, label: "Soup" },
    { value: 1, label: "Appetizer" },
    { value: 2, label: "Breakfast" },
    { value: 3, label: "Lunch" },
    { value: 4, label: "Dinner" },
    { value: 5, label: "Dessert" },
    { value: 6, label: "Sauce" }
];


const AddRecipeForm = ({ onSubmit }) => {
    // Form state
    const [formData, setFormData] = useState({
        name: '',
        recipeCategory: 0, // Default value to empty string
        instructions: '',
        servingsPerRecipe: ''
    });

// Handle form change and convert value to integer
    const handleChange = (e) => {
        const { name, value } = e.target;

        setFormData({
            ...formData,
            // Convert to integer if the field is `measuredIn` or `recipeCategory`
            [name]: name === "recipeCategory" ? parseInt(value, 10) : value
        });
    };


    // Handle form submission
    const handleSubmit = (e) => {
        e.preventDefault();

        // Split instructions into an array based on newlines
        const formattedInstructions = formData.instructions.split('\n').filter(step => step.trim() !== '');

        // Convert formData to the correct shape to send to the API
        const recipeData = {
            ...formData,
            instructions: formattedInstructions // Pass instructions as an array
        };

        onSubmit(recipeData);
    };

    return (
        <form className="add-recipe-form" onSubmit={handleSubmit}>
            {/* Recipe Name */}
            <div>
                <label htmlFor="name">Recipe Name</label>
                <input
                    type="text"
                    name="name"
                    value={formData.name}
                    onChange={handleChange}
                    required
                />
            </div>

            {/* Recipe Category Dropdown */}
            <div>
                <label htmlFor="recipeCategory">Recipe Category</label>
                <select
                    name="recipeCategory"
                    value={formData.recipeCategory}
                    onChange={handleChange}
                >
                    {recipeCategoryOptions.map((option) => (
                        <option key={option.value} value={option.value}>
                            {option.label}
                        </option>
                    ))}
                </select>
            </div>

            {/* Instructions */}
            <div>
                <label htmlFor="instructions">Instructions</label>
                <textarea
                    name="instructions"
                    value={formData.instructions}
                    onChange={handleChange}
                    placeholder="Enter each instruction on a new line"
                    rows="4"
                    required
                />
            </div>

            {/* Servings Per Recipe */}
            <div>
                <label htmlFor="servingsPerRecipe">Servings Per Recipe</label>
                <input
                    type="number"
                    step="0.01"
                    name="servingsPerRecipe"
                    value={formData.servingsPerRecipe}
                    onChange={handleChange}
                    required
                />
            </div>


            {/* Submit Button */}
            <div>
                <button type="submit">Add Recipe</button>
            </div>
        </form>
    );
};

// Add PropTypes validation
AddRecipeForm.propTypes = {
    onSubmit: PropTypes.func.isRequired
};

export default AddRecipeForm;
