import axios from 'axios';

const getAxios = () => {
    const headers = {};
    const token = localStorage.getItem('auth-token');
    headers['Authorization'] = `Bearer ${token}`;
    return axios.create({
        headers
    });
}

export default getAxios;