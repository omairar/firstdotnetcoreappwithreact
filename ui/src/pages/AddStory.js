import React, { useState } from "react";
import { SavePost } from "../services/storyservice";
import { useSelector } from "react-redux";

function AddStory() {
  const [compState, setCompState] = useState({
    SSTitle: "",
    SSDescription: "",
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
  return (
    <div className="container">
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
      <button className="btn btn-primary" onClick={() => onSave()}>
        Post Story
      </button>
    </div>
  );
}

export default AddStory;
