import axios from "axios";

export const SavePost = async (obj) => {
  const promise = axios.post(`Stories/`, {
    ...obj,
  });

  const dataPromise = await promise;

  return dataPromise.data;
}

export const getStoriesByStatusAPI = async (flag) => {
  const promise = axios.get(`Stories/getStoriesByStatus/${flag}`);

  const dataPromise = await promise;

  return dataPromise.data;
};


export const getStoriesByIDAPI = async (id) => {
  const promise = axios.get(`Stories/getStoriesByUserID/${id}`);

  const dataPromise = await promise;

  return dataPromise.data;
};






export const approveStoryAPI = async (id,obj) => {
  const promise = axios.put(`Stories/approveStory/${id}`, {
    ...obj
  } ,{
    withCredentials: true,
  });

  const dataPromise = await promise;

  return dataPromise.data;
};
