import React, { useEffect, useState } from "react";
import { Col, Row } from "react-bootstrap";
import Story from "../components/Story";
import { getStoriesByStatusAPI } from "../services/storyservice";

function Home() {
  const [storiesArr, setStoriesArr] = useState([]);

  useEffect(() => {
    getStoriesByStatusAPI(true)
      .then((res) => {
        setStoriesArr(res);
      })
      .catch((err) => {
        console.log(err);
      });
  }, []);

  return <div className="container">
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
</div>;
}

export default Home;
