import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { OrderLines } from "../../API/Models/OrderLines/OrderLines";

export interface UpdateShoppingCartPayload {
  quantity: number;
  productId: number;
}

export interface IShoppingCartState {
  orderLines: OrderLines[];
}

const initialState: IShoppingCartState = {
  orderLines: [],
};

export const shoppingCartSlice = createSlice({
  name: "ShoppingCart",
  initialState,
  reducers: {
    addOrderLines: (state, action: PayloadAction<OrderLines>) => {
      state.orderLines.push(action.payload);
    },
    removeOrderLines: (state, action: PayloadAction<number>) => {
      state.orderLines = state.orderLines.filter(
        (item) => item.productId !== action.payload
      );
    },
    updateOrderLineQuantity: (
      state,
      action: PayloadAction<UpdateShoppingCartPayload>
    ) => {
      const index = state.orderLines.findIndex(
        (item) => item.productId === action.payload.productId
      );
      state.orderLines[index].itemsQuantity = action.payload.quantity;
    },
  },
});

export const { addOrderLines, removeOrderLines, updateOrderLineQuantity } =
  shoppingCartSlice.actions;
export default shoppingCartSlice.reducer;
