import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";

import { deleteFromLocalData, setLocalData } from "../utils/localStorage";

// export const getUserDetailsRedux = createAsyncThunk(
//   "user/getUserDetailsRedux",
//   async (payload, thunkAPI) => {
//     try {
//       let response = await clientLogin(payload);

//       return response;
//     } catch (err) {
//       if (!err.response) {
//         throw err;
//       }
//       return thunkAPI.rejectWithValue(err.response);
//     }
//   }
// );

const initialState = {
  userObj: null,
  isSuccess: false,
  loading: false,
  message: "",
};

export const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    addUserFromLocal: (state, action) => {
      state.userObj = action.payload;
      state.isSuccess = true;
    },
    signoutUser: (state, action) => {
      state.userObj = null;
      state.isSuccess = false;
      deleteFromLocalData("userObj");
    },
    updatetoNewUserData: (state, action) => {
      state.userObj = action.payload;
      let tempObj = {
        ...state.userObj,
      };
      setLocalData("userObj", tempObj);
    },
  },

  extraReducers: (builder) => {
    //builder
    // .addCase(getUserDetailsRedux.pending, (state, action) => {
    //   state.loading = true;
    // })
    // .addCase(getUserDetailsRedux.fulfilled, (state, action) => {
    //   const { payload } = action;
    //   setLocalData("userObj", payload);
    //   state.userObj = payload;
    //   state.loading = false;
    //   state.isSuccess = true;
    // })
    // .addCase(getUserDetailsRedux.rejected, (state, action) => {
    //   const { payload } = action;
    //   state.message = payload;
    //   state.loading = false;
    //   state.isSuccess = false;
    // });
  },
});

// Action creators are generated for each case reducer function
export const { addUserFromLocal, signoutUser, updatetoNewUserData } =
  userSlice.actions;

export default userSlice.reducer;
