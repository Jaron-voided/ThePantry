import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import PropTypes from "prop-types";

// Import the necessary API functions
import { getRecipes, getRecipesByCategory, getRecipesByIngredient, getRecipesByPrice } from "../../services/apiService.js";

const RecipesList = ({ sortByCategory, priceSortOrder, selectedIngredient }) => {
    const [recipes, setRecipes] = useState([]);
    const navigate = useNavigate();
    const [measurements, setMeasurements] = useState([]);


    useEffect(() => {
        const fetchRecipes = async () => {
            try {
                let data;

                // Fetch recipes based on the props
                if (sortByCategory) {
                    data = await getRecipesByCategory(sortByCategory);
                } else if (selectedIngredient) {
                    data = await getRecipesByIngredient(selectedIngredient);
                } else if (priceSortOrder === "asc") {
                    data = await getRecipesByPrice();
                } else {
                    // Default fetch all recipes if no filters are set
                    data = await getRecipes();
                }

                setRecipes(data);
            } catch (error) {
                console.error("Error fetching recipes:", error);
            }
        };

        fetchRecipes();
    }, [sortByCategory, priceSortOrder, selectedIngredient]);


/*    useEffect(() => {
        getRecipes()
            .then(data => setRecipes(data))
            .catch(error => console.error('Error fetching recipes:', error));
    }, []);

    // Filter and sort logic
    const filteredRecipes = recipes
        .filter(recipe =>
            (!sortByCategory || recipe.category === sortByCategory) &&
            (!selectedIngredient || recipe.ingredients.some(ing => ing.id === selectedIngredient))
        )
        .sort((a, b) => {
            if (priceSortOrder === "asc") {
                return a.pricePerServing - b.pricePerServing;
            }
            return 0;
        });*/

    const handleView = (id) => {
        navigate(`/recipes/view/${id}`);
    };

    const handleEdit = (id) => {
        navigate(`/recipes/edit/${id}`);
    };

    const handleDelete = (id) => {
        if (!window.confirm("Are you sure you want to delete this recipe?")) {
            return;
        }

        // Delete the measurements for the recipe

        fetch(`${import.meta.env.VITE_API_URL}/api/measurements?recipeId=${id}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then((response) => {
                if (response.ok) {
                    // Update state to remove the deleted ingredient
                    setMeasurements((prevMeasurements) =>
                        prevMeasurements.filter(measurement => measurement.recipeId !== id));
                } else {
                    return response.text().then(text => {
                        throw new Error(`Failed to delete measurements: ${text}`);
                    });
                }
            })
            .catch((error) => console.error('Error deleting measurement:', error));

        // Then delete the recipe itself
        fetch(`https://localhost:5001/api/recipes/${id}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then((response) => {
                if (response.ok) {
                    // Update state to remove the deleted ingredient
                    setRecipes((prevRecipes) => prevRecipes.filter(ingredient => ingredient.id !== id));
                } else {
                    return response.text().then(text => {
                        throw new Error(`Failed to delete recipe: ${text}`);
                    });
                }
            })
            .catch((error) => console.error('Error deleting recipe:', error));

        // Delete related measurements

    };

    return (
        <div className="recipes-list">
            <h1>Recipes</h1>
            {recipes.map((recipe) => (
                <div key={recipe.id} className="recipe-item">
                    <span className="recipe-name">{recipe.name}</span>
                    <button className="view-button" onClick={() => handleView(recipe.id)}>View</button>
                    <button className="edit-button" onClick={() => handleEdit(recipe.id)}>Edit</button>
                    <button className="delete-button" onClick={() => handleDelete(recipe.id)}>Delete</button>
                </div>
            ))}
        </div>
    );
};

// Add PropTypes validation
RecipesList.propTypes = {
    sortByCategory: PropTypes.func,
    priceSortOrder: PropTypes.string,
    selectedIngredient: PropTypes.bool,

};


export default RecipesList;




/*
import React, { useEffect, useState } from "react";
import {useNavigate} from "react-router-dom";
import {getRecipes} from "../../services/apiService.js";

const RecipesList = () => {
    const [recipes, setRecipes] = useState([]);
    const [measurements, setMeasurements] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        getRecipes()
            .then(data => setRecipes(data))
            .catch(error => console.error('Error fetching recipes:', error));
    }, [])

    const handleDelete = (id) => {
        if (!window.confirm("Are you sure you want to delete this recipe?")) {
            return;
        }

        // Delete the measurements for the recipe

        fetch(`https://localhost:5001/api/measurements?recipeId=${id}`, {
            method: 'DELETE',
                headers: {
                'Content-Type': 'application/json'
            }
        })
            .then((response) => {
                if (response.ok) {
                    // Update state to remove the deleted ingredient
                    setMeasurements((prevMeasurements) =>
                        prevMeasurements.filter(measurement => measurement.recipeId !== id));
                } else {
                    return response.text().then(text => {
                        throw new Error(`Failed to delete measurements: ${text}`);
                    });
                }
            })
            .catch((error) => console.error('Error deleting measurement:', error));

        // Then delete the recipe itself
        fetch(`https://localhost:5001/api/recipes/${id}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then((response) => {
                if (response.ok) {
                    // Update state to remove the deleted ingredient
                    setRecipes((prevRecipes) => prevRecipes.filter(ingredient => ingredient.id !== id));
                } else {
                    return response.text().then(text => {
                        throw new Error(`Failed to delete recipe: ${text}`);
                    });
                }
            })
            .catch((error) => console.error('Error deleting recipe:', error));

        // Delete related measurements

    };

    const handleView = (id) => {
        navigate(`/recipes/view/${id}`);
    };

    const handleEdit = (id) => {
        navigate(`/recipes/edit/${id}`);
    };

    // JSX rendering
    return (
        <div className="recipes-list">
            <h1>Recipes</h1>
            {recipes.map((recipe) => (
                <div key={recipe.id} className="recipe-item">
                    <span className="recipe-name">{recipe.name}</span>
                    <button className={'view-button'}
                            onClick={() => handleView(recipe.id)}
                    >
                        View
                    </button>
                    <button className={'edit-button'}
                            onClick={() => handleEdit(recipe.id)}
                    >
                        Edit
                    </button>
                    <button className={'delete-button'}
                            onClick={() => handleDelete(recipe.id)}
                    >
                        Delete
                    </button>
                </div>
            ))}
        </div>
    );
};
export default RecipesList;

*/
