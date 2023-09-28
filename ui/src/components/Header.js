import React from "react";
import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import Form from "react-bootstrap/Form";
import NavDropdown from "react-bootstrap/NavDropdown";
import { Link, useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { LogoutAPI } from "../services/accountservice";
import { signoutUser } from "../redux/userSlice";

function Header() {
  const userObj = useSelector((state) => state.user.userObj);
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const onLogout = () => {
    console.log("yo");
    LogoutAPI()
      .then((res) => {
        console.log(res);
        navigate("/");
        dispatch(signoutUser());
      })
      .catch((err) => {
        console.log(err);
      });
  };
  return (
    <Navbar
      bg="dark"
      data-bs-theme="dark"
      expand="lg"
      className="bg-body-tertiary"
    >
      <Container>
        <Link className="navbar-brand" to="/">
          Short Story
        </Link>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        {userObj && (
          <Navbar.Collapse id="basic-navbar-nav">
            <Nav className="me-auto">
              <Link className="nav-link" to="/addstory">
                Add Story
              </Link>
              {userObj.role === "Admin" && (
                <>
                  <Link className="nav-link" to="/approvestories">
                  Approve Story
                </Link>
                <Link className="nav-link" to="/category">
                Categories
                </Link>
                </>
                
              )}
              <Link className="nav-link" to='/mystory'>
                My Stories
              </Link>
            </Nav>
          </Navbar.Collapse>
        )}

        <div className="d-flex align-items-center">
          {userObj ? (
            <>
              <div
                className="userlink h-100 pe-3"
                style={{ color: "#fff" }}
                to="/approvestories"
              >
                Welcome {userObj.role === "Admin" ? "Admin" : userObj.userName}{" "}
                !
              </div>
              <button onClick={() => onLogout()} className="btn btn-danger">
                Logout
              </button>
            </>
          ) : (
            <Link to="/register" className="btn btn-warning">
              Signup/SignIn
            </Link>
          )}
        </div>
      </Container>
    </Navbar>
  );
}

export default Header;
