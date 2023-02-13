import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { UserRole } from "../../Constants/Enums/UserRole";

export interface IPermissionsState {
  permissions?: UserRole;
}

const initialState: IPermissionsState = {
  permissions: undefined,
};

export const orderSlice = createSlice({
  name: "Orders",
  initialState,
  reducers: {
    updatePermission: (state, action: PayloadAction<UserRole>) => {
      state.permissions = action.payload;
    },
  },
});

export const { updatePermission } = orderSlice.actions;
export default orderSlice.reducer;
