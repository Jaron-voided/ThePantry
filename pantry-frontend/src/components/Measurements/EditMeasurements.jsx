import React, { useState, useEffect } from 'react';
import EditMeasurementsForm from './EditMeasurementsForm';  // Assuming the form is in a separate file
import { useParams } from 'react-router-dom';  // For getting the recipeId from the route

const EditMeasurements = () => {
    const { recipeId } = useParams();  // Get the recipeId from the URL params
    const [existingMeasurements, setExistingMeasurements] = useState([]);
    const [ingredients, setIngredients] = useState([]);
    const [error, setError] = useState('');

    // Fetch the existing measurements for the recipe
    useEffect(() => {
        fetch(`${import.meta.env.VITE_API_URL}/recipes/${recipeId}/measurements`)
            .then(response => response.json())
            .then(data => setExistingMeasurements(data))
            .catch((error) => {
                console.error('Error fetching measurements:', error);
                setError('Failed to load measurements');
            });
    }, [recipeId]);

    // Fetch the ingredients to display in the dropdowns
    useEffect(() => {
        fetch('https://localhost:5001/ingredients')
            .then(response => response.json())
            .then(data => setIngredients(data))
            .catch((error) => {
                console.error('Error fetching ingredients:', error);
                setError('Failed to load ingredients');
            });
    }, []);

    // Handle submitting the updated measurements
    const handleSubmit = (updatedMeasurements) => {
        return fetch(`${import.meta.env.VITE_API_URL}/recipes/${recipeId}/measurements`, {
            method: 'PUT',  // Or POST, depending on your backend logic
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(updatedMeasurements)
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Failed to update measurements');
                }
                return response.json();
            })
            .catch((error) => {
                console.error('Error updating measurements:', error);
                setError('Failed to update measurements');
            });
    };

    return (
        <div>
            <h2>Edit Measurements</h2>
            {error && <p className="error-message">{error}</p>}
            {existingMeasurements.length > 0 && ingredients.length > 0 ? (
                <EditMeasurementsForm
                    recipeId={recipeId}
                    existingMeasurements={existingMeasurements}
                    ingredients={ingredients}
                    handleSubmit={handleSubmit}
                />
            ) : (
                <p>Loading measurements and ingredients...</p>
            )}
        </div>
    );
};

export default EditMeasurements;
