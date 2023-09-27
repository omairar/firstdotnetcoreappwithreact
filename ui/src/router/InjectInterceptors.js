import { useEffect } from "react";
import { setupInterceptors } from "../utils/axios";
import { useNavigate } from "react-router-dom";

import { useDispatch } from "react-redux";

function InjectInterceptors() {
  const navigate = useNavigate();
  const dispatch = useDispatch();

  useEffect(() => {
    setupInterceptors(navigate, dispatch);
  }, [navigate]);

  return null;
}

export default InjectInterceptors;
