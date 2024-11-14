import React, {useState} from 'react';
import Layout from "../../app/layout/layout.jsx";
import EditRecipeForm from "../../components/Recipes/EditRecipeForm.jsx";
import {useNavigate, useParams} from 'react-router-dom';



const EditRecipe = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [recipe, setRecipe] = useState(null);
    const [measurements, setMeasurements] = useState([]);
    const [ingredientMap, setIngredientMap] = useState({});
    const [totalPrice, setTotalPrice] = useState(0);
    const [pricePerServing, setPricePerServing] = useState(0);

    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');

    // Function to handle form submission
    const handleEditRecipe = (recipeData) => {
        setLoading(true); // set loading state to true before making API call
        setError('');

        // Convert Javascript object to the format expected by the API
        const recipeDTO = {
            id: recipeData.id,
            name: recipeData.name,
            recipeCategory: parseInt(recipeData.recipeCategory), // Ensure this is an integer
            instructions: recipeData.instructions,
            servingsPerRecipe: parseInt(recipeData.servingsPerRecipe)
        };

        // Log the data being sent to the API for debugging
        console.log('Sending recipe data:', recipeDTO);


        // PUT to your backend API to add the recipe
        fetch(`${import.meta.env.VITE_API_URL}/recipes/${recipeData.id}`, {
            //fetch(`${import.meta.env.VITE_API_URL}/recipes', {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(recipeDTO)
        })
            .then((response) => {
                if (response.ok) {
                    //return response.json();
                    return response.text().then(text => text ? JSON.parse(text) : {});
                } else {
                    // Log the response text for debugging
                    return response.text().then(text => {
                        throw new Error(`Failed to edit recipe: ${text}`);
                    });
                }
            })
            .then((data) => {
                console.log('Recipe added successfully:', data);
                setLoading(false);

                const recipeId = recipeData.id;

                // Delete existing measurements for the recipe
                fetch(`${import.meta.env.VITE_API_URL}/measurements/byRecipe/${recipeId}`, {
                    method: 'DELETE',
                    headers: {
                        'Content-Type': 'application/json'
                    }
                })
                .then((response) => {
                    if (response.ok) {
                        console.log('Measurements deleted successfully');
                    } else {
                        return response.text().then(text => {
                            throw new Error(`Failed to delete measurements: ${text}`);
                        });
                    }
                })
                .catch((error) => {
                    console.error('Error deleting measurements:', error);
                });

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
            <h1>Edit Recipe</h1>
            {/* Error message */}
            {error && <p style={{color: 'red'}}>{error}</p>}

            {/* Show loading text while submitting, otherwise show the form */}
            {loading ? <p>Submitting...</p> : <EditRecipeForm onSubmit={handleEditRecipe}/>}
        </div>
    );
};

export default EditRecipe;