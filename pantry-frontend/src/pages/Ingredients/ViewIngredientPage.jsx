import React, {useEffect, useState} from 'react';
import '/src/app/layout/styles/ingredient/ViewIngredientPage.css';
import Layout from "../../app/layout/layout.jsx";
import ViewIngredient from "../../components/Ingredients/ViewIngredient.jsx";


const ViewIngredientPage = () => (
    <Layout>
        <div className="content">
            <h1> View Ingredient </h1>
            < ViewIngredient />
        </div>
    </Layout>
);

export default ViewIngredientPage;