import React from 'react';
import '../styles/signin.css';
import { NavLink, useNavigate } from 'react-router-dom/dist';
import { useState,useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { loginUser } from '../Service/LoginUser';

const Signin = (props) => {
    const[username,setUsername]=useState('');
    const[password,setPassword]=useState('');
    const dispatch =useDispatch();
    const navigate=useNavigate();
    console.log(props.user, "user")
    const handelLogin=(e)=>{

        e.preventDefault();
        const newUser={
            userName:username,
            password:password,
        };
        loginUser(newUser,dispatch,navigate);
    }
    return (
        <div className="login-form">
            <form onSubmit={handelLogin}>
                <p className="text-center heading">AGRI-CART</p>
                <h2 className="pull-left">Welcome Back</h2>
                <p className="pull-left">Sign in with your email address or mobile number.</p>

                <div className="form-group">
                    <input  type="text" className="form-control" placeholder="Email or mobile number" onChange={(e) =>setUsername(e.target.value)} required="required" />
                </div>
                <div className="form-group">
                    <input type="password" className="form-control" placeholder="Password" onChange={(e) => setPassword(e.target.value)} required="required" />
                </div>
                <div className="form-group">
                    <button type="submit" className="btn btn-block btn-cust" >Log in</button>
                </div>
                {/* <div className="clearfix">
            <label className="pull-left checkbox-inline"><input type="checkbox"/> Remember me</label>
            <a href="#" className="pull-right">Forgot Password?</a> 
        </div>         */}
                <p className="text-center">New to Agri-Cart? <a onClick={() => props.buy("signup")}><NavLink to="/signup">Create an Account</NavLink> </a></p>
            </form>
        </div>

    )
}

export default Signin;