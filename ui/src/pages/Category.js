import React, { useEffect, useState } from "react";

import { Col, Row } from "react-bootstrap";
import { CreateCategoryAPI, GetAllCategoriesAPI } from "../services/categoryservice";
import { useSelector } from "react-redux";
function Category() {
  const [createCat, setCreateCat] = useState({
    Name: "",
    Description: "",
  });

  const [catList, setCatList] = useState([])
  const userObj = useSelector((state) => state.user.userObj);
  const onSave = () => {
    CreateCategoryAPI(createCat, userObj.tokenJwt).then((res)=>{
        alert("Category Created")
        setCreateCat({
            Name: "",
            Description: "",
        })
       getAllCats()
    }).then((err)=>{
        console.log(err)
    })
  }

  useEffect(() => {
    getAllCats()
    
  }, [])

  const getAllCats = () => { 
    GetAllCategoriesAPI().then((res)=>{
        setCatList(res)
    }).then((err)=>{
        console.log(err)
    })
   }
  
  return (
    <div className="container">
      <div className="card">
        <div className="card-header">
          <h5 class="card-title"> Create Category</h5>
        </div>
        <div className="card-body">
          <input
            className="form-control"
            placeholder="title"
            value={createCat.Name}
            onChange={(e) =>
              setCreateCat({ ...createCat, Name: e.target.value })
            }
          />
          <br />
          <textarea
            className="form-control"
            placeholder="description"
            value={createCat.Description}
            onChange={(e) =>
              setCreateCat({ ...createCat, Description: e.target.value })
            }
          ></textarea>
          <br />
          <button className="btn btn-primary" onClick={() => onSave()}>
            Post Story
          </button>
        </div>
      </div>
      <div className="card mt-5">
        <div className="card-header">
          <h5 class="card-title"> Category List</h5>
        </div>
        <div className="card-body">
                {
                    catList.map((item,index)=>(
                        <Row key={index}>
                            <Col xs={2}>
                                {
                                    item.name
                                }
                            </Col>
                            <Col xs={10}>
                                {
                                    item.description
                                }
                            </Col>
                        </Row>
                    ))
                }
            
        </div>
      </div>
    </div>
  );
}

export default Category;
