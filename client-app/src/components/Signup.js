import React from 'react';
import '../styles/signin.css';
import { NavLink, useNavigate } from 'react-router-dom';
import { useState } from 'react';
import { registerUser } from '../Service/LoginUser';
import { useDispatch } from 'react-redux';

const Signup = (props) => {
  const[username,setUsername]=useState('');
  const[password,setPassword]=useState('');
  const[confirmPassword,setConfirmPassword]=useState('');
  const dispatch=useDispatch();
  const navigate=useNavigate();
  
const handleRegister=(e)=>{
  e.preventDefault();
  const newRegister={
    userName:username,
    password:password,
    confirmPassword:confirmPassword
    
  };
  registerUser(newRegister,dispatch,navigate);
}


  return (
    <div className="login-form">
      <form onSubmit={handleRegister} >
        <p className="text-center heading">AGRI-CART</p>
        <h2 className="pull-left">Let's Get Started</h2>
        <p className="pull-left">Enter your email address or mobile number.</p>
        <div className="form-group">
          <input type="text" className="form-control" placeholder="Email or mobile number" onChange={(e) => setUsername(e.target.value)} required="required" />
        </div>
        <div className="form-group">
          <input type="password" className="form-control" placeholder="Password" onChange={(e) => setPassword(e.target.value)} required="required" />
        </div>
        <div className="form-group">
          <input type="password" className="form-control" placeholder="Confirm Password" onChange={(e) => setConfirmPassword(e.target.value)} required="required" />
        </div>

        <div className="form-group">
          <label className="pull-left checkbox-inline"><input type="checkbox" /> Accept Terms & Conditions</label>

          <button type="submit" className="btn btn-block btn-cust" onClick={(e) => props.signUp(e)}>Signup</button>
        </div>
        {/* <div className="clearfix">
            <label className="pull-left checkbox-inline"><input type="checkbox"/> Remember me</label>
            <a href="#" className="pull-right">Forgot Password?</a> 
        </div>         */}
        <p className="text-center">Already a Member? <a onClick={() => props.buy("signin")}><NavLink to="/signin">Sign in</NavLink></a></p>
      </form>
    </div>

  )
}

export default Signup;