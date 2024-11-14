import React, {useState} from 'react';
import Layout from "../../app/layout/layout.jsx";
import AddRecipeForm from "../../components/Recipes/AddRecipeForm.jsx";
import { useNavigate } from 'react-router-dom';

const AddRecipe = () => {
    const navigate = useNavigate();

    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');

    // Function to handle form submission
    const handleAddRecipe = (recipeData) => {
        setLoading(true); // set loading state to true before making API call
        setError('');

        // Convert Javascript object to the format expected by the API
        const recipeDTO = {
            name: recipeData.name,
            recipeCategory: parseInt(recipeData.recipeCategory), // Ensure this is an integer
            instructions: recipeData.instructions,
            servingsPerRecipe: parseInt(recipeData.servingsPerRecipe)
        };

        // Log the data being sent to the API for debugging
        console.log('Sending recipe data:', recipeDTO);


        // POST to your backend API to add the recipe
        //fetch('https://localhost:5001/recipes', {
        fetch(`${import.meta.env.VITE_API_URL}/recipes`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(recipeDTO)
        })
            .then((response) => {
                if (response.ok) {
                    return response.json();
                }
                // Log the response text for debugging
                return response.text().then(text => {
                    throw new Error(`Failed to add recipe: ${text}`);
                });
            })
            .then((data) => {
                console.log('Recipe added successfully:', data);
                setLoading(false);
                const recipeId = data.id;
                // Redirect to RecipesPage after successful addition
                navigate(`/recipes/measurements/${recipeId}`);
            })
            .catch((error) => {
                console.error('Error adding recipes:', error);
                setError(error.message);
                setLoading(false);
            });
    };
    return (
        <div className="add-recipe-container">
            <h1>Add New Recipe</h1>
            {/* Error message */}
            {error && <p style={{color: 'red'}}>{error}</p>}

            {/* Show loading text while submitting, otherwise show the form */}
            {loading ? <p>Submitting...</p> : <AddRecipeForm onSubmit={handleAddRecipe}/>}
        </div>
    );
};

export default AddRecipe;