import React, { useState } from "react";
import { registerAPI } from "../services/accountservice";
import Login from "../components/Login";
import Register from "../components/Register";

function Auth() {
  const [isLoginActivated, setIsLoginActivated] = useState(true);

  return (
    <main className="form-signin">
      <h1 className="h3 d-flex">
        <div onClick={() => setIsLoginActivated(true)}>Login</div>/
        <div onClick={() => setIsLoginActivated(false)}>Register</div>
      </h1>
      {isLoginActivated ? <Login /> : <Register />}
    </main>
  );
}

export default Auth;
