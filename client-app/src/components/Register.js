import React, { useState } from "react";
import "../styles/register.css";
import { registerUser } from "../Service/LoginUser";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";

const Register = () => {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const handleRegister = (event) => {
        event.preventDefault();
        const newUser = {
            userName: username,
            password: password,
            confirmPassword: confirmPassword
        };
        registerUser(newUser, dispatch, navigate);
    };

    return (
        <section className="signin-container br-12">
            <h2 className="signin-title">Sign In</h2>
            <form className="signin-form" onSubmit={handleRegister}>
                <label>Username</label>
                <input className="br-12" type="text" placeholder="Enter your username" onChange={(event) => setUsername(event.target.value)} />
                <label>Password</label>
                <input className="br-12" type="password" placeholder="Enter your password" onChange={(event) => setPassword(event.target.value)} />
                <label>Confirm Password</label>
                <input className="br-12" type="password" placeholder="Confirm your password" onChange={(event) => setConfirmPassword(event.target.value)} />
                <button className="br-12" type="submit">CREATE NEW ACCOUNT</button>
            </form>
        </section>
    )
}

export default Register;