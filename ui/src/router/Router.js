import React, { useEffect, useState } from "react";
import ScrollWrapper from "./ScrollWrapper";
import InjectInterceptors from "./InjectInterceptors";

import { Routes, Route, useLocation } from "react-router-dom";

import { useDispatch, useSelector } from "react-redux";
import Header from "../components/Header";
import Home from "../pages/Home";
import AddStory from "../pages/AddStory";
import ApproveStories from "../pages/ApproveStories";
import Auth from "../pages/Auth";
import { getLocalData } from "../utils/localStorage";
import { addUserFromLocal } from "../redux/userSlice";
import PrivateRoute from "./PrivateRoute";
import SuperAdminRoute from "./SuperAdminRoute";
import MyStories from "../pages/MyStories";

// import PrivateRoute from "./PrivateRoute";

function Router() {
  const dispatch = useDispatch();

  const [initalLoad, setInitalLoad] = useState(false)
  useEffect(() => {
    let userObj = getLocalData("userObj");
    
    if (userObj) {
      dispatch(addUserFromLocal(userObj));
    }else{
      setInitalLoad(true)
    }
  }, []);
  const userObj = useSelector((state) => state.user.userObj);
  useEffect(() => {
    if(userObj && !initalLoad){
      setInitalLoad(true)
    }
  }, [userObj])
  

  return initalLoad && (
    <>
      <ScrollWrapper>
        <InjectInterceptors />

        <div className="d-flex flex-column vh-100 main-clm ">
          <div className="bg"></div>
          <Header />
          <div className="py-5">
            <Routes>
              <Route exact path="/" element={<Home />} />
              <Route exact path="/addstory" element={<PrivateRoute><AddStory /></PrivateRoute>} />
              <Route exact path="/mystory" element={<PrivateRoute><MyStories /></PrivateRoute>} />
              <Route exact path="/register" element={<Auth />} />
              <Route
                exact
                path="/approvestories"
                element={<SuperAdminRoute><ApproveStories /></SuperAdminRoute>}
              />
            </Routes>
          </div>
        </div>
      </ScrollWrapper>
    </>
  );
}

export default Router;
