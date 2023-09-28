import axios from "axios";

export const CreateCategoryAPI = async (obj,token) => {
  const promise = axios.post(`Categories/`, {
    ...obj,
  },{
    headers: { Authorization: `Bearer  ${token}` },
  });

  const dataPromise = await promise;

  return dataPromise.data;
};

export const GetAllCategoriesAPI = async () => {
  const promise = axios.get(`Categories/`);

  const dataPromise = await promise;

  return dataPromise.data;
};

