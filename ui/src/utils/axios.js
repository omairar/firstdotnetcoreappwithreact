import axios from "axios";

import moment from "moment";
// const instance = axios.create({
//   baseURL: `${process.env.REACT_APP_API}api/v1/`,
// });

const instance = axios.create();

export const setupInterceptors = (navigate, dispatch) => {
  axios.defaults.baseURL = `${process.env.REACT_APP_API}api/`;
  //axios.defaults.headers.common["Accept-Language"] =getLocalData("en");
  //axios.defaults.headers.common["DateTimeOffset"] = moment().utcOffset();
  
  axios.interceptors.request.use(
    (request) => {
      return request;
    },
    (err) => {
      const { status } = err.response;
      console.log(status);
    }
  );

  axios.interceptors.response.use(
    (response) => {
      return response;
    },
    (err) => {
      const { status } = err.response;

      if (status === 401) {
        // here we have access of the useHistory() from current Router
        console.log("unauthorized");
        // dispatch(signoutUser());
        navigate("/");
      }

      return Promise.reject(err);
    }
  );
};

export default instance;
