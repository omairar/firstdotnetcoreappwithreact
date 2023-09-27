import axios from "axios";

export const registerAPI = async (obj) => {
  const promise = axios.post(`accounts/register/`, {
    ...obj,
  });

  const dataPromise = await promise;

  return dataPromise.data;
};

export const loginAPI = async (obj) => {
  const promise = axios.post(`accounts/login/`, {
    ...obj,
  });

  const dataPromise = await promise;

  return dataPromise.data;
};

export const LogoutAPI = async () => {
  const promise = axios.post(`accounts/logout/`);

  const dataPromise = await promise;

  return dataPromise.data;
};
