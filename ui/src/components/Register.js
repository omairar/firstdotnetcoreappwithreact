import React, { useState } from "react";
import { registerAPI } from "../services/accountservice";
import Login from "../components/Login";

function Register() {
  const [registerState, setRegisterState] = useState({
    UserName: "",
    Email: "",
    Password: "",
    ConfirmPassword: "",
    DOB: "",
  });

  // Generalized input change handler
  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setRegisterState((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const register = () => {
    const formData = new FormData();
    
    //add Profile Pic TO Form
    formData.append('PP', selectedFile);

    //add Profile Pic TO Form
    formData.append('myModel', JSON.stringify(registerState));


    registerAPI(formData)
      .then((res) => {
        console.log(res);
      })
      .catch((err) => {
        console.log(err);
      });
  };

  const [selectedFile, setSelectedFile] = useState(null);

  const handleFileChange = (event) => {

    const file = event.target.files[0];
    setSelectedFile(file);
  };

  return (
    <div className="form">
      <div className="form-floating">
        <input
          className="form-control"
          name="UserName"
          placeholder="Username"
          value={registerState.UserName}
          onChange={handleInputChange}
          id="floatingInput"
        />
        <label htmlFor="floatingInput">UserName</label>
      </div>
      <div className="form-floating">
        <input
          className="form-control"
          name="Email"
          placeholder="Email"
          value={registerState.Email}
          onChange={handleInputChange}
          id="floatingEmail"
        />
        <label htmlFor="floatingEmail">Email</label>
      </div>
      <div className="form-floating">
        <input
          className="form-control"
          name="Password"
          type="password"
          placeholder="Password"
          value={registerState.Password}
          onChange={handleInputChange}
          id="floatingP"
        />
        <label htmlFor="floatingP">Password</label>
      </div>
      <div className="form-floating">
        <input
          className="form-control"
          name="ConfirmPassword"
          type="password"
          placeholder="Confirm Password"
          value={registerState.ConfirmPassword}
          onChange={handleInputChange}
          id="floatingCP"
        />
        <label htmlFor="floatingCP">Confirm Password</label>
      </div>
      <div className="form-floating">
        <input
          type="date"
          className="form-control"
          placeholder="DOB"
          name="DOB"
          value={registerState.DOB}
          onChange={handleInputChange}
          id="floatingDOB"
        />
        <label htmlFor="floatingDOB">DOB</label>
      </div>
      <div>
        <input type="file" onChange={handleFileChange}/>
      </div>
      <button onClick={() => register()} className="w-100 btn btn-lg">
        Register
      </button>
    </div>
  );
}

export default Register;
