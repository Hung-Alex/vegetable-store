import React, { useState } from "react";
import '../styles/login.css';
import { NavLink, useNavigate } from "react-router-dom";
import { loginUser } from "../redux/apiRequest";
import { useDispatch } from "react-redux";
const Login = () => {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const handleLogin = (event) => {
        event.preventDefault();
        const newUser = {
            userName: username,
            password: password
        };
        loginUser(newUser, dispatch, navigate);
    };

    return (
        <section className="login-container br-12">
            <h2 className="login-title">Log In</h2>
            <form className="login-form" onSubmit={handleLogin}>
                <label>Username</label>
                <input className="br-12" type="text" placeholder="Enter your username" onChange={(event) => setUsername(event.target.value)} />
                <label>Password</label>
                <input className="br-12" type="password" placeholder="Enter your password" onChange={(event) => setPassword(event.target.value)} />
                <button className="br-12" type="submit">LOG IN</button>
            </form>
            <div className="login-register-container">
                <div className="login-register">Don't have an account yet?</div>
                <NavLink to="/register" className="login-register-link">Register one for free</NavLink>
            </div>
        </section>
    )
}

export default Login;