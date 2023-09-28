import axios from "axios";

export const SavePost = async (obj,token) => {
  const promise = axios.post(`Stories/`, {
    ...obj,
  },{
    headers: { Authorization: `Bearer  ${token}` },
  });

  const dataPromise = await promise;

  return dataPromise.data;
}

export const getStoriesByStatusAPI = async (flag) => {
  const promise = axios.get(`Stories/getStoriesByStatus/${flag}`);

  const dataPromise = await promise;

  return dataPromise.data;
};


export const getStoriesByIDAPI = async (id,token) => {
  const promise = axios.get(`Stories/getStoriesByUserID/${id}`,
  {
    headers: { Authorization: `Bearer  ${token}` },
  });

  const dataPromise = await promise;

  return dataPromise.data;
};






export const approveStoryAPI = async (id,obj,token) => {
  const promise = axios.put(`Stories/approveStory/${id}`, {
    ...obj
  } ,{
    headers: { Authorization: `Bearer  ${token}` },
  });

  const dataPromise = await promise;

  return dataPromise.data;
};
