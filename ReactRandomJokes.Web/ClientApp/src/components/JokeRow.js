import React from 'react';

export default function JokeRow({ joke }) {
    const { setup, punchline, userLikedJokes } = joke;
    return (
        <div className="card card-body bg-light mb-3">
            <h5>{setup}</h5>
            <h5>{punchline}</h5>
            <span>Likes: {userLikedJokes.filter(ulj=>ulj.liked).length}</span>
            <br />
            <span>Dislikes: {userLikedJokes.filter(ulj => !ulj.liked).length}</span>
        </div>

    )
}

