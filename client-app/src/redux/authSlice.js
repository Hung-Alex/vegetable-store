import { createSlice } from "@reduxjs/toolkit";

const authuSlice = createSlice({
    name: "auth",
    initialState: {
        login: {
            currentUser: null,
            isFetching: false,
            error: false
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
        },
        loginFail: (state) => {
            state.login.isFetching = false;
            state.login.error = true;
        }
    }
})

export const {
    loginStart,
    loginSuccess,
    loginFail
} = authuSlice.actions;

export default authuSlice.reducer;