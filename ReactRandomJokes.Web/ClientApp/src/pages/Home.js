import React, { useState, useEffect,useRef } from 'react';
import { Link} from 'react-router-dom';
import getAxios from '../AuthAxios';
import { useAuthContext } from '../AuthContext';

export default function Home() {
    const [joke, setJoke] = useState({});
    const { user } = useAuthContext();     
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {       
        getRandomJoke();        
    }, [])
       
    const getRandomJoke = async () => {
        const { data } = await getAxios().get('/api/jokes/getRandomJoke');
        setJoke(data);
        setIsLoading(false);
        
    }
    const getJokeById = async () => {
        const{data}=await getAxios().get(`/api/jokes/getjokebyid?id=${joke.id}`);
        setJoke(data);        
    }

    function useInterval(callback, delay) {
        const savedCallback = useRef();
        useEffect(() => {
            savedCallback.current = callback;
        }, [callback]);
        useEffect(() => {
            function tick() {
                savedCallback.current();
            }
            if (delay !== null) {
                let id = setInterval(tick, delay);
                return () => clearInterval(id);
            }
        }, [delay]);
    }
    useInterval(() => getJokeById(), 1000)

    const onLikeClick =async(liked)=>{
        const userLiked = { liked: liked, jokeId: joke.id};       
        await getAxios().post('/api/jokes/likeJoke', userLiked);
        getJokeById();       
    }  
    return (
        <div className="row">
            <div className="col-md-6 offset-md-3 card card-body bg-light">
                {isLoading && <h3>Loading....</h3>}
                {!isLoading &&
                    <div>
                        <h4>{joke.setup}</h4>
                        <h4>{joke.punchline}</h4>
                        <div>
                        {!!user && <div>
                            <button className="btn btn-primary" onClick={() => { onLikeClick(true) }}
                                disabled={joke.userLikedJokes.some(ulj => ulj.userId === user.id && (ulj.liked || new Date() >= new Date(new Date(ulj.date).getTime() + 5 * 60000)) )}>Like</button>
                            <button className="btn btn-danger" onClick={() => { onLikeClick(false) }}
                                disabled={joke.userLikedJokes.some(ulj => ulj.userId === user.id && (!ulj.liked || new Date() >= new Date(new Date(ulj.date).getTime() + 5 * 60000)))}>Dislike</button>
                            </div>}
                            {!user &&
                                <div>
                                    <Link to="/login">Login to your account to like/dislike this joke</Link>
                                </div>}
                            <br />
                        <h4>Likes: {joke.userLikedJokes.filter(ulj => ulj.liked).length}</h4>
                        <h4>Dislikes:{joke.userLikedJokes.filter(ulj => !ulj.liked).length}</h4>
                        <h4><button onClick={getRandomJoke} className="btn btn-link">Refresh</button> </h4>                        
                        </div>
                    </div>}
            </div>
        </div>

    )
}