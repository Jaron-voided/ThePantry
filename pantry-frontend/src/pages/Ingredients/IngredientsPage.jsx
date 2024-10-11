import React from 'react';
import IngredientsList from '../../components/Ingredients/IngredientsList.jsx';
import Layout from "../../app/layout/layout.jsx";
import IngredientsAddNavbar from "../../components/Ingredients/IngredientsAddNavBar.jsx";

const IngredientsPage = () => (
    <Layout>
        < IngredientsAddNavbar />
        <div className="content">
            <h1>Ingredients Page</h1>
            <IngredientsList/>
        </div>
    </Layout>
);

export default IngredientsPage;