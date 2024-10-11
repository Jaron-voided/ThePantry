import React from 'react';
import PropTypes from 'prop-types';
import Navbar from "../../components/Navbar.jsx";

const Layout = ({ children }) => (
    <div>
        <Navbar />

        {/* Main Content */}
        <main>
            {children}
        </main>

        {/* Footer or any other common elements */}
        <footer>
            <p>&copy; 2024 The Pantry App</p>
        </footer>
    </div>
);

// Add PropTypes validation for children
Layout.propTypes = {
    children: PropTypes.node,
};


export default Layout;
