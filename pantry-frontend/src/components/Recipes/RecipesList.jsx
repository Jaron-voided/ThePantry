import React, { useEffect, useState } from "react";
import {useNavigate} from "react-router-dom";
import {getRecipes} from "../../services/apiService.js";

const RecipesList = () => {
    const [recipes, setRecipes] = useState([]);
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

        // Send delete request to the API
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

