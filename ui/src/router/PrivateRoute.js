import React from "react";
import { Navigate } from "react-router-dom";
import { getLocalData } from "../utils/localStorage";
import { useSelector } from "react-redux";

function PrivateRoute({ children }) {
  const logSuccess = useSelector((state) => state.user.userObj);
  return logSuccess ? children : <Navigate to="/" />;
}
export default PrivateRoute;
