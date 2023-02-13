import { createSlice } from "@reduxjs/toolkit";

export interface IPermissionsState {
    shoppingCarts: string;
  }

const initialState: IPermissionsState = {
    shoppingCarts: '',
  };

export const orderSlice = createSlice({
    name: "Orders",
    initialState,
    reducers:{
        
    }
})


export const {
    updatePermission
  } = orderSlice.actions;
export default orderSlice.reducer;