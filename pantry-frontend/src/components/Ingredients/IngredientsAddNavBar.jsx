import React from 'react';
import { Link } from 'react-router-dom';

const IngredientsAddNavbar = () => (
    <nav className="add-navbar">
        <ul>
            <li><Link to="/ingredients/add">Add New Ingredient</Link></li>
        </ul>
    </nav>
);

export default IngredientsAddNavbar;
