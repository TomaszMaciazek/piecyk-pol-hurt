import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { OrderLine } from "../../API/Models/Order/OrderLine";
import { SendPoint } from "../../API/Models/SendPoint/SendPoint";

const getShoppingCartIndex = (
  state: IShoppingCartState,
  email: string | undefined
) => {
  return state.shoppingCarts.findIndex(
    (item) =>
      item.email === email && item.sentPointId === state.chosenSendPoint?.id
  );
};

export interface UpdateShoppingCartPayload {
  quantity: number;
  productId: number;
  email: string | undefined;
}

export interface AddOrderLinePayload {
  orderLine: OrderLine;
  email: string | undefined;
}

export interface RemoveOrderLinePayload {
  id: number;
  email: string | undefined;
}

export interface UpdateOrderLineQuantityPayload {
  orderLine: OrderLine;
  email: string | undefined;
}

export interface UpdateSentPointPayload {
  id: number;
  email: string | undefined;
}

export interface ShoppingCart {
  orderLines: OrderLine[];
  sentPointId: number;
  email: string | undefined;
}

export interface IShoppingCartState {
  shoppingCarts: ShoppingCart[];
  chosenSendPoint: SendPoint | undefined;
}

const initialState: IShoppingCartState = {
  shoppingCarts: [],
  chosenSendPoint: undefined,
};

export const shoppingCartSlice = createSlice({
  name: "ShoppingCarts",
  initialState,
  reducers: {
    addOrderLines: (state, action: PayloadAction<AddOrderLinePayload>) => {
      if (!state.chosenSendPoint) {
        return;
      }
      const index = getShoppingCartIndex(state, action.payload.email);

      if (index === -1) {
        state.shoppingCarts.push({
          email: action.payload.email,
          sentPointId: state.chosenSendPoint.id,
          orderLines: [action.payload.orderLine],
        });
      } else {
        const searchedOrderLineId = state.shoppingCarts[
          index
        ].orderLines.findIndex(
          (item) =>
            item.product.id === action.payload.orderLine.product.id &&
            item.priceForOneItem === action.payload.orderLine.priceForOneItem
        );

        if (searchedOrderLineId !== -1) {
          state.shoppingCarts[index].orderLines[
            searchedOrderLineId
          ].itemsQuantity += action.payload.orderLine.itemsQuantity;
        } else {
          state.shoppingCarts[index].orderLines.push(action.payload.orderLine);
        }
      }
    },
    removeOrderLines: (
      state,
      action: PayloadAction<RemoveOrderLinePayload>
    ) => {
      const index = getShoppingCartIndex(state, action.payload.email);
      state.shoppingCarts[index].orderLines = state.shoppingCarts[
        index
      ].orderLines.filter((item) => item.product.id !== action.payload.id);
    },
    updateOrderLineQuantity: (
      state,
      action: PayloadAction<UpdateShoppingCartPayload>
    ) => {
      const index = getShoppingCartIndex(state, action.payload.email);

      const orderLineId = state.shoppingCarts[index].orderLines.findIndex(
        (item) => item.product.id === action.payload.productId
      );
      state.shoppingCarts[index].orderLines[orderLineId].itemsQuantity =
        action.payload.quantity;
    },
    updateOrderSentPoint: (
      state,
      action: PayloadAction<UpdateSentPointPayload>
    ) => {
      const index = getShoppingCartIndex(state, action.payload.email);

      if (index === -1) {
        state.shoppingCarts.push({
          email: action.payload.email,
          orderLines: [],
          sentPointId: action.payload.id,
        });
      }
    },
    clearShoppingCart: (state, action: PayloadAction<string | undefined>) => {
      const index = getShoppingCartIndex(state, action.payload);
      state.shoppingCarts[index].orderLines = [];
      state.shoppingCarts[index].sentPointId = 0;
    },
    updateEmail: (state, action: PayloadAction<string | undefined>) => {
      const index = state.shoppingCarts.findIndex(
        (item) =>
          item.email === undefined &&
          state.chosenSendPoint?.id === item.sentPointId
      );

      if (index !== -1) {
        state.shoppingCarts[index].email = action.payload;
      }
    },
    updateChosenSendPoint: (state, action: PayloadAction<SendPoint>) => {
      state.chosenSendPoint = action.payload;
    },
  },
});

export const {
  addOrderLines,
  removeOrderLines,
  updateOrderLineQuantity,
  clearShoppingCart,
  updateOrderSentPoint,
  updateEmail,
  updateChosenSendPoint,
} = shoppingCartSlice.actions;
export default shoppingCartSlice.reducer;
