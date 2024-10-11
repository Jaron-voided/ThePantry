import React, {useEffect, useState} from 'react';
import '/src/app/layout/styles/ingredient/ViewIngredientPage.css';
import Layout from "../../app/layout/layout.jsx";
import AddIngredient from "../../components/Ingredients/AddIngredient.jsx";


const AddIngredientPage = () => (
    <Layout>
        <div className="content">
            <h1> Add Ingredient </h1>
            < AddIngredient />
        </div>
    </Layout>
);

export default AddIngredientPage;