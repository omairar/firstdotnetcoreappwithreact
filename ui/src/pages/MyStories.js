import React, { useEffect, useState } from "react";
import {
  getStoriesByIDAPI
} from "../services/storyservice";
import { Col, Row } from "react-bootstrap";
import Story from "../components/Story";
import { useSelector } from "react-redux";

function MyStories() {
  const [storiesArr, setStoriesArr] = useState([]);
  const userObj = useSelector((state) => state.user.userObj);
  useEffect(() => {
    getStoriesByIDAPI(userObj.id)
      .then((res) => {
        setStoriesArr(res);
      })
      .catch((err) => {
        console.log(err);
      });
  }, []);



 


  return (
    <div className="container">
      <Row>
        {storiesArr.map((item, index) => (
          <Col xs={4} key={index}>
            <Story
              item={item}
              canapprove={ false}
              approveStory={null}
            />
          </Col>
        ))}
      </Row>
    </div>
  );
}

export default MyStories;
