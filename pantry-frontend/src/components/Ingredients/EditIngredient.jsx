import React from 'react';
import Layout from "../../app/layout/layout.jsx";
import EditIngredientForm from "../../components/Ingredients/EditIngredientForm.jsx";
import {useNavigate, useParams} from 'react-router-dom';

const EditIngredient = () => {
    const navigate = useNavigate();
    const { id } = useParams();

    // Function to handle form submission
    const handleEditIngredient = (ingredientData) => {
        // Convert Javascript object to the format expected by the API
        const ingredientDTO = {
            name: ingredientData.name,
            measuredIn: parseInt(ingredientData.measuredIn), // Ensure this is an integer
            ingredientCategory: parseInt(ingredientData.ingredientCategory), // Ensure this is an integer
            pricePerPackage: parseFloat(ingredientData.pricePerPackage),
            measurementsPerPackage: parseInt(ingredientData.measurementsPerPackage)
        };

        // Log the data being sent to the API for debugging
        console.log('Sending ingredient data:', ingredientDTO);


        // POST to your backend API to edit the ingredient
        //fetch(`https://localhost:5001/ingredients/${id}`, {
        fetch(`${import.meta.env.VITE_API_URL}/ingredients/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(ingredientDTO)
        })
/*            .then((response) => {
                if (response.ok) {
                    return response.text().then(text => text ? JSON.parse(text) : null);
                }
                // Log the response text for debugging
                return response.text().then(text => {
                    throw new Error(`Failed to edit ingredient: ${text}`);
                });
            })*/
            .then((data) => {
                console.log('Ingredient edited successfully:', data);
                // Redirect to IngredientsPage after successful editition
                navigate('/ingredients');
            })
            .catch((error) => console.error('Error editing ingredients:', error));
    };
    return (
        <div className="add-ingredient-container">
            <h1>Edit New Ingredient</h1>
            <EditIngredientForm onSubmit={handleEditIngredient} />
        </div>
    );
};

export default EditIngredient;