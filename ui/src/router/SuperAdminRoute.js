import React from "react";
import { Navigate } from "react-router-dom";
import { getLocalData } from "../utils/localStorage";
import { useSelector } from "react-redux";

function SuperAdminRoute({ children }) {
  const logSuccess = useSelector((state) => state.user.userObj.role);
  return logSuccess === 'Admin' ? children : <Navigate to="/" />;
}
export default SuperAdminRoute;

