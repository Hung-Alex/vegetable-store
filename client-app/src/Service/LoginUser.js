import axios from "axios";
import { loginStart,loginSuccess,loginFail,registerStart,registerFail,registerSuccess,logoutFailed,logoutStart,logoutSuccess } from "../Redux/authSlice";

export const loginUser = async (user, dispatch, navigate) => {
  dispatch(loginStart());
  try {
    const res = await axios.post(
      "https://localhost:7027/api/Account/Login",
      user
    );
    const data = res.data;
    if (data.isSuccess === false) {
      alert(data.errors);
      return;
    }
    dispatch(loginSuccess(res.data));
    navigate("/");
  } catch (error) {
    dispatch(loginFail());
  }
};
export const registerUser = async (user, dispatch, navigate) => {
  dispatch(registerStart());
  try {
    const res = await axios.post(
      "https://localhost:7027/api/Account/Register",
      user
    );
    const data = res.data;
    if (data.isSuccess === false) {
      alert(data.errors);
      return;
    }
    dispatch(registerSuccess());
    navigate("/signin");
  } catch (error) {
    dispatch(registerFail);
  }
};

export const logOutUser = async (token, dispatch, navigate) => {
  dispatch(logoutStart());

  try {
    fetch("https://localhost:7027/api/Account/Logout", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Authorization": token, // token ở đây là access token được lưu trữ từ trước
      },
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        console.log("Logout successful!");
      })
      .catch((error) => {
        console.error("Error occurred during logout:", error);
      });
     dispatch(logoutSuccess());
    navigate("/");
  } catch (error) {
    dispatch(logoutFailed());
  }
};
