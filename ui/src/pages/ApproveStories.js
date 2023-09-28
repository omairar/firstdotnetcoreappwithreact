import React, { useEffect, useState } from "react";
import {
  approveStoryAPI,
  getStoriesByStatusAPI,
} from "../services/storyservice";
import { Col, Row } from "react-bootstrap";
import Story from "../components/Story";
import { useSelector } from "react-redux";

function ApproveStories() {
  const [storiesArr, setStoriesArr] = useState([]);

  useEffect(() => {
    getStoriesByStatusAPI(false)
      .then((res) => {
        setStoriesArr(res);
      })
      .catch((err) => {
        console.log(err);
      });
  }, []);
  const userObj = useSelector((state) => state.user.userObj);

  const approveStory = (id, obj) => {
    approveStoryAPI(id, obj , userObj.tokenJwt)
      .then((res) => {
        console.log(res);
        getStoriesByStatusAPI(false)
          .then((res) => {
            setStoriesArr(res);
          })
          .catch((err) => {
            console.log(err);
          });
      })
      .catch((err) => {
        console.log(err);
      });
  };




  return (
    <div className="container">
      <Row>
        {storiesArr.map((item, index) => (
          <Col xs={4} key={index}>
            <Story
              item={item}
              canapprove={userObj.role === "Admin" ? true : false}
              approveStory={approveStory}
            />
          </Col>
        ))}
      </Row>
    </div>
  );
}

export default ApproveStories;
