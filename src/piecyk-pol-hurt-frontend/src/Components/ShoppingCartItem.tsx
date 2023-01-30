/* eslint-disable react-hooks/exhaustive-deps */
import { Delete } from "@mui/icons-material";
import {
  ListItem,
  IconButton,
  ListItemAvatar,
  Avatar,
  CardMedia,
  ListItemText,
} from "@mui/material";
import React, { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { OrderLines } from "../API/Models/OrderLines/OrderLines";
import Counter from "../Common/Counter";
import {
  updateOrderLineQuantity,
  UpdateShoppingCartPayload,
} from "../Redux/Reducers/ShoppingCartReducer";

interface IShoppingCartItem {
  orderLine: OrderLines;
  handleDeleteOrderLine: () => void;
}

const ShoppingCartitem = ({
  orderLine,
  handleDeleteOrderLine,
}: IShoppingCartItem) => {
  const [count, setCount] = useState<number>(orderLine.itemsQuantity);
  const dispatch = useDispatch();

  useEffect(() => {
    const updateShoppingCartPayload: UpdateShoppingCartPayload = {
      productId: orderLine.productId,
      quantity: count,
    };

    dispatch(updateOrderLineQuantity(updateShoppingCartPayload));
  }, [count]);

  return (
    <ListItem
      secondaryAction={
        <IconButton edge="end" onClick={handleDeleteOrderLine}>
          <Delete />
        </IconButton>
      }
    >
      <ListItemAvatar>
        <Avatar>
          <CardMedia
            component="img"
            height="140"
            width="auto"
            image={orderLine.product.imageUrl}
            alt={orderLine.product.name}
          />
        </Avatar>
      </ListItemAvatar>
      <ListItemText
        primary={orderLine.product.name}
      />
      <Counter count={count} setCount={setCount} />
    </ListItem>
  );
};

export default ShoppingCartitem;
