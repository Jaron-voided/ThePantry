import React from 'react';
import Layout from "../../app/layout/layout.jsx";
import EditIngredient from "../../components/Ingredients/EditIngredient.jsx";

const EditIngredientPage = () => (
    <Layout>
        <div className="content">
            <h1> Edit Ingredient </h1>
            < EditIngredient />
        </div>
    </Layout>
);

export default EditIngredientPage;
