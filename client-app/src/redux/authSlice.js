import { createSlice } from "@reduxjs/toolkit";

const authuSlice = createSlice({
    name: "auth",
    initialState: {
        login: {
            currentUser: null,
            isFetching: false,
            error: false
        },
        register: {
            isFetching: false,
            error: false,
            success: false
        },
        logout:{
            isFetching:false,
            error:false,
        }
    },
    reducers: {
        loginStart: (state) => {
            state.login.isFetching = true;
        },
        loginSuccess: (state, action) => {
            state.login.isFetching = false;
            state.login.currentUser = action.payload;
            state.login.error = false;
            localStorage.setItem('token', action.payload.result.token)
            
        },
        loginFail: (state) => {
            state.login.isFetching = false;
            state.login.error = true;
        },
        registerStart: (state) => {
            state.register.isFetching = true;
        },
        registerSuccess: (state) => {
            state.register.isFetching = false;
            state.register.error = false;
            state.register.success = true;
        },
        registerFail: (state) => {
            state.register.isFetching = false;
            state.register.error = true;
            state.register.success = false;
        },
        logoutSuccess:(state)=>{
            state.logout.isFetching=false;
            state.login.currentUser=null;
            state.logout.error=false;
        },
        logoutFailed:(state)=>{
            state.logout.isFetching=false;
            state.logout.error=true;
        },
        logoutStart:(state)=>{
            state.logout.isFetching=true;
        },
    }
})

export const {
    loginStart,
    loginSuccess,
    loginFail,
    registerStart,
    registerSuccess,
    registerFail,
    logoutSuccess,
    logoutStart,
    logoutFailed
} = authuSlice.actions;

export default authuSlice.reducer;