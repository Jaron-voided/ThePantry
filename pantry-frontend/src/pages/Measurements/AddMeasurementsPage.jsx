import React, {useEffect, useState} from 'react';

import '../../app/layout/styles/measurements/measurement.css';

import Layout from "../../app/layout/layout.jsx";
import AddMeasurements from "../../components/Measurements/AddMeasurements.jsx";
import {useParams} from "react-router-dom";

const AddMeasurementsPage = () => {
    const {recipeId} = useParams();

    console.log("Recipe ID in AddMeasurements:", recipeId);

    return (
        <Layout>
            <div className="content">
                <h1> Add Measurements </h1>
                < AddMeasurements recipeId={recipeId} />
            </div>
        </Layout>
    );
};

export default AddMeasurementsPage;