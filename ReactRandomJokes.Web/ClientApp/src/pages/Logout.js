import React, { useEffect } from 'react';
import { useAuthContext } from '../AuthContext';
import { useHistory } from 'react-router-dom';

const Logout = () => {
    const { logout }= useAuthContext();
    const history = useHistory();

    useEffect(() => {
        const executeLogout =async () => {           
            logout();
            history.push('/');
        }
        executeLogout();
    }, []);
    return (<></>);
}

export default Logout;