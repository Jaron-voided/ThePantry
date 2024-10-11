import React from 'react';
import { Link } from 'react-router-dom';
import '../app/layout/styles/styles.css';

const Navbar = () => (
    <nav>
        <ul>
            <li><Link to="/">Home</Link></li>
            <li><Link to="/ingredients">Ingredients</Link></li>
            <li><Link to="/recipes">Recipes</Link></li>
        </ul>
    </nav>
);

export default Navbar;