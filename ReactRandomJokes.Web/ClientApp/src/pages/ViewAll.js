import axios from 'axios';
import React, { useState, useEffect } from 'react';
import JokeRow from '../components/JokeRow';

export default function ViewAll() {
    const [jokes, setJokes] = useState([]);
    useEffect(() => {
        const getJokes = async () => {
            const { data } = await axios.get('/api/jokes/viewJokes');
            setJokes(data);
        }
        getJokes();
    }, [])
    return(
        <div className='row'>
            <div className='col-md-6 offset-md-3'>
            {jokes.map(j=><JokeRow joke={j}/>)}
            </div>
        </div>
    )
}
