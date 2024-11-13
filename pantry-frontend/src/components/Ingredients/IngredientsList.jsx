import React, { useEffect, useState } from 'react';
import { getIngredients } from '../../services/apiService.js';
import {useNavigate} from "react-router-dom";

const IngredientsList = () => {
    const [ingredients, setIngredients] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        getIngredients()
            .then(data => setIngredients(data))
            .catch(error => console.error('Error fetching ingredients:', error));
    }, []);

    const handleDelete = (id) => {
        if (!window.confirm("Are you sure you want to delete this ingredient?")) {
            return;
        }



        // Send delete request to the API
        //fetch(`https://localhost:5001/api/ingredients/${id}`, {
        fetch(`${import.meta.env.VITE_API_URL}/ingredients/${id}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then((response) => {
                if (response.ok) {
                    // Update state to remove the deleted ingredient
                    setIngredients((prevIngredients) => prevIngredients.filter(ingredient => ingredient.id !== id));
                } else {
                    return response.text().then(text => {
                        throw new Error(`Failed to delete ingredient: ${text}`);
                    });
                }
            })
            .catch((error) => console.error('Error deleting ingredient:', error));
    };

    const handleView = (id) => {
        navigate(`/ingredients/view/${id}`);
    };

    const handleEdit = (id) => {
        navigate(`/ingredients/edit/${id}`);
    };

    // JSX rendering
    return (
        <div className="ingredients-list">
            <h1>Ingredients</h1>
            {ingredients.map((ingredient) => (
                <div key={ingredient.id} className="ingredient-item">
                    <span className="ingredient-name">{ingredient.name}</span>
                    <button className={'view-button'}
                        onClick={() => handleView(ingredient.id)}
                    >
                        View
                    </button>
                    <button className={'edit-button'}
                        onClick={() => handleEdit(ingredient.id)}
                    >
                        Edit
                    </button>
                    <button className={'delete-button'}
                        onClick={() => handleDelete(ingredient.id)}
                    >
                        Delete
                    </button>
                </div>
            ))}
        </div>
    );
};
export default IngredientsList;
