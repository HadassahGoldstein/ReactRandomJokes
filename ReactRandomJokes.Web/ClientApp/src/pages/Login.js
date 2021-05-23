import React, { useState } from 'react';
import { Link, useHistory } from 'react-router-dom';
import getAxios from '../AuthAxios'
import { useAuthContext } from '../AuthContext';

export default function Login() {
    const [form, setForm] = useState({});
    const [isValidLogin, setIsValidLogin] = useState(true);
    const history = useHistory();
    const { setUser } = useAuthContext();

    const onTextChange = e => {
        const copy = { ...form };
        copy[e.target.name] = e.target.value;
        setForm(copy);
    }
    const onFormSubmit = async e => {
        try {
            e.preventDefault();
            const { data } = await getAxios().post('/api/account/login', form);
            localStorage.setItem('auth-token', data.token);
            setUser(data);
            const { data: user } = await getAxios().get('/api/account/getcurrentuser');
            setUser(user);
            history.push('/')
        }
        catch (e) {
            setIsValidLogin(false);
        }

    }
    return (
        <div className="row">
            <div className="col-md-6 offset-md-3 card card-body bg-light">
                <h3>Log in to your account</h3>
                {!isValidLogin && <span className='text-danger'>Invalid username/password. Please try again.</span>}
                <form onSubmit={onFormSubmit}>
                    <input onChange={onTextChange} value={form.email} type="text" name="email" placeholder="Email" className="form-control" />
                    <br />
                    <input onChange={onTextChange} value={form.password} type="password" name="password" placeholder="Password" className="form-control" />
                    <br />
                    <button className="btn btn-primary">Login</button>
                </form>
                <Link to="/signup">Sign up for a new account</Link>
            </div>
        </div>
    )
}