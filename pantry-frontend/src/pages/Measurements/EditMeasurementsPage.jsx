import React, {useEffect, useState} from 'react';
import '../../app/layout/styles/measurements/measurement.css';
import Layout from "../../app/layout/layout.jsx";
import EditMeasurements from "../../components/Measurements/EditMeasurements.jsx";
import {useParams} from "react-router-dom";

const EditMeasurementsPage = () => {
    const {recipeId} = useParams();

    console.log("Recipe ID in EditMeasurements:", recipeId);

    return (
        <Layout>
            <div className="content">
                <h1> Edit Measurements </h1>
                < EditMeasurements recipeId={recipeId} />
            </div>
        </Layout>
    );
};

export default EditMeasurementsPage;