import React, { useEffect, useState } from "react";
import { SavePost } from "../services/storyservice";
import { useSelector } from "react-redux";
import { GetAllCategoriesAPI } from "../services/categoryservice";

function AddStory() {
  const [compState, setCompState] = useState({
    SSTitle: "",
    SSDescription: "",
    CategoryId: 0,
  });

  const userObj = useSelector((state) => state.user.userObj);
  const onSave = () => {
    SavePost(compState, userObj.tokenJwt)
      .then((res) => {
        alert("post created");
      })
      .catch((err) => {
        console.log(err);
      });
  };

  const [catList, setCatList] = useState([]);

  useEffect(() => {
    GetAllCategoriesAPI()
      .then((res) => {
        setCatList(res);
      })
      .then((err) => {
        console.log(err);
      });
  }, []);

  return (
    <div className="container">
      <div className="card">
        <div className="card-header">
          <h5 class="card-title"> Add Story</h5>
        </div>
        <div className="card-body">
          <br />
          <input
            className="form-control"
            placeholder="title"
            value={compState.title}
            onChange={(e) =>
              setCompState({ ...compState, SSTitle: e.target.value })
            }
          />
          <br />
          <textarea
            className="form-control"
            placeholder="description"
            value={compState.desc}
            onChange={(e) =>
              setCompState({ ...compState, SSDescription: e.target.value })
            }
          ></textarea>
          <br />
          <select
            className="form-control"
            placeholder=""
            value={compState.CategoryId}
            onChange={(event) =>
              setCompState({
                ...compState,
                CategoryId: event.target.value,
              })
            }
          >
            <option value={0}>Select Category</option>
            {catList.map((item, index) => (
              <option value={item.categoryId} key={index}>
                {item.name}
              </option>
            ))}
          </select>
          <br />
          <button className="btn btn-primary" onClick={() => onSave()}>
            Post Story
          </button>
        </div>
      </div>
    </div>
  );
}

export default AddStory;
