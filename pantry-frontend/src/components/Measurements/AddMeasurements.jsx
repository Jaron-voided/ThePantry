import React, {useState} from 'react';
import Layout from "../../app/layout/layout.jsx";
import AddMeasurementsForm from "../../components/Measurements/AddMeasurementsForm.jsx";
import { useNavigate } from 'react-router-dom';
import PropTypes from "prop-types";
import '../../app/layout/styles/measurements/measurement.css';


const AddMeasurements = ({ recipeId }) => {
    console.log('Recipe ID in AddMeasurements:', recipeId);

    const navigate = useNavigate();
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');

    // Function to handle form submission
    const handleAddMeasurements = (measurementData, finish = false) => {
        setLoading(true); // set loading state to true before making API call
        setError('');

        // Convert Javascript object to the format expected by the API
        const measurementDTO = {
            recipeId:  recipeId,
            ingredientId: measurementData.ingredientId,
            amount: parseInt(measurementData.amount)
        };

        // Log the data being sent to the API for debugging
        console.log('Sending measurement data:', measurementDTO);


        // POST to your backend API to add the measurement
        //fetch('https://localhost:5001/api/measurements', {
        fetch('http://localhost:5000/api/measurements', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(measurementDTO)
        })
            .then((response) => {
                if (response.ok) {
                    return response.json();
                }
                // Log the response text for debugging
                return response.text().then(text => {
                    throw new Error(`Failed to add measurement: ${text}`);
                });
            })
            .then((data) => {
                console.log('Measurement added successfully:', data);
                setLoading(false);

                if (finish) {
                    navigate('/recipes'); // Navigate back to recipes if finished
                }
            })
            .catch((error) => {
                console.error('Error adding measurements:', error);
                setError(error.message);
                setLoading(false);
            });
    };
    return (
        <div className="add-measurement-container">
            <h1>Add New Measurements for Recipe</h1> {/*Eventually make recipe say the actual recipeName*/}
            {/* Error message */}
            {error && <p style={{color: 'red'}}>{error}</p>}

            {/* Show loading text while submitting, otherwise show the form */}
            {loading ? <p>Submitting...</p> : <AddMeasurementsForm onSubmit={handleAddMeasurements} recipeId={recipeId}/>}
        </div>
    );
};

AddMeasurements.propTypes = {
    recipeId: PropTypes.string.isRequired
};

export default AddMeasurements;