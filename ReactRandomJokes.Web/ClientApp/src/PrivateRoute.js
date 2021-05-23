import React from 'react';
import { useAuthContext } from './AuthContext.js';
import LoginPage from './pages/Login';
import { Route } from 'react-router-dom';

const PrivateRoute = ({ component, ...options }) => {
    const { user } = useAuthContext();
    const finalComponent = !!user ? component : LoginPage;
    return <Route {...options} component={finalComponent} />;
};

export default PrivateRoute;