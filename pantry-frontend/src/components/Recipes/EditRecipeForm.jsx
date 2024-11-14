import React, {useEffect, useState} from 'react';
import PropTypes from 'prop-types';
import {useParams} from "react-router-dom";


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


const EditRecipeForm = ({ onSubmit }) => {
    const { id } = useParams();
    const [recipe, setRecipe] = useState(null)
    const [measurements, setMeasurements] = useState([]);
    const [ingredientMap, setIngredientMap] = useState({});

    // Form state
    const [formData, setFormData] = useState({
        name: '',
        recipeCategory: 0, // Default value to empty string
        instructions: '',
        servingsPerRecipe: ''
    });

    useEffect(() => {
        // Fetch Recipe details from the API
        fetch(`${import.meta.env.VITE_API_URL}/recipes/${id}`)
            .then((response) => {
                if (response.ok) {
                    return response.json();
                }
                throw new Error('Failed to fetch recipe');
            })
            .then((data) => {
                setRecipe(data);
                // Initialize formData once the recipe is fetched
                setFormData({
                    name: data.name,
                    recipeCategory: data.recipeCategory,
                    //instructions: data.instructions.split(',').join('\n'),
                    instructions: data.instructions,
                    servingsPerRecipe: data.servingsPerRecipe
                });
            })
            .catch((error) => console.error('Error fetching recipe:', error));
    }, [id]);

    // Fetch the measurements for the recipe
    useEffect(() => {
        if (recipe && recipe.id) {
            fetch(`${import.meta.env.VITE_API_URL}/measurements?recipeId=${recipe.id}`)//this looks different then normal because its passing data to the api
                //recipeId=${recipe.id} is the actual query parameter, where recipeId is the key and ${recipe.id} is the value you're passing (using template literals to dynamically insert recipe.id).
                .then((response) => {
                    if (response.ok) {
                        return response.json();
                    }
                    throw new Error('Failed to fetch measurements');
                })
                .then((data) => {
                    setMeasurements(data)

                    // Extract unique ingredientIds from the measurements
                    const ingredientIds = [...new Set(data.map(m => m.ingredientId))];

                    // Fetch ingredient names based on their IDs
                    fetchIngredients(ingredientIds);
                })
                .catch((error) => console.error('Error fetching measurement:', error));
        }
    }, [recipe]);

    // Fetch ingredient names based on their IDs
    const fetchIngredients = (ingredientIds) => {
        Promise.all(ingredientIds.map(id =>
            fetch(`${import.meta.env.VITE_API_URL}/ingredients/${id}`)
                .then(response => response.json())
        )).then(ingredientData => {
            const ingredientMap = ingredientData.reduce((map, ingredient) => {
                map[ingredient.id] = ingredient.name;
                return map;
            }, {});
            setIngredientMap(ingredientMap);
        }).catch(error => console.error('Error fetching ingredients:', error));
    };

    // Loading state
    if (!recipe) {
        return <div>Loading...</div>
    }

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

        //const formattedInstructions = formData.instructions.split('\n').filter(step => step.trim() !== '');
        const formattedInstructions = formData.instructions.split('/n').join(',');

        // Convert formData to the correct shape to send to the API
        const recipeData = {
            id,
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
                <h2>You have to manually remove commas and hit enter, sorry</h2>
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

            {/* Ingredients and Measurements */}
            <h2 className="recipe-row">Ingredients and Measurements</h2>
            {measurements.length > 0 ? (
                <ul className="recipe-measurements">
                    {measurements.map((measurement) => (
                        <li key={measurement.id} className="recipe-row">
                            <strong>Ingredient:</strong> {ingredientMap[measurement.ingredientId] || 'Loading...'} -
                            <strong>Amount:</strong> {measurement.amount}
                        </li>
                    ))}
                </ul>
            ) : (
                <p className="recipe-row">No measurements found for this recipe.</p>
            )}


            {/* Submit Button */}
            <div>
                <button type="submit">Edit Recipe</button>
            </div>
        </form>
    );
};

// Add PropTypes validation
EditRecipeForm.propTypes = {
    onSubmit: PropTypes.func.isRequired
};

export default EditRecipeForm;
