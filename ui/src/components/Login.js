import React, { useState } from "react";
import { loginAPI } from "../services/accountservice";
import { useDispatch } from "react-redux";
import { updatetoNewUserData } from "../redux/userSlice";
import { useNavigate } from "react-router-dom";

function Login() {
  const [registerState, setRegisterState] = useState({
    UserName: "",
    Password: "",
  });

  // Generalized input change handler
  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setRegisterState((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const dispatch = useDispatch();
  const navigate = useNavigate();

  const login = () => {
    loginAPI(registerState)
      .then((res) => {
        dispatch(updatetoNewUserData(res));
        navigate("/");
      })
      .catch((err) => {
        console.log(err);
      });
  };
  return (
    <div className="form">
      <div className="form-floating">
        <input
          className="form-control"
          id="floatingInput"
          value={registerState.UserName}
          onChange={handleInputChange}
          name="UserName"
          placeholder="Username"
          required
        />
        <label htmlFor="floatingInput">UserName</label>
      </div>
      <div className="form-floating">
        <input
          type="password"
          className="form-control"
          id="floatingPassword"
          name="Password"
          placeholder="Password"
          value={registerState.Password}
          onChange={handleInputChange}
          required
        />
        <label htmlFor="floatingPassword">Password</label>
      </div>

      <div className="checkbox mb-3 d-none">
        <div className="form-check form-switch">
          <label className="form-check-label" htmlFor="rememberMeSwitch">
            {" "}
            Remember me
          </label>
        </div>
      </div>
      <button onClick={() => login()} className="w-100 btn btn-lg">
        Login
      </button>
    </div>
  );
}

export default Login;
