/* eslint-disable react-hooks/exhaustive-deps */
import { useAuth0 } from "@auth0/auth0-react";
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
import { OrderLine } from "../API/Models/Order/OrderLine";
import Counter from "../Common/Counter";
import {
  updateOrderLineQuantity,
  UpdateShoppingCartPayload,
} from "../Redux/Reducers/ShoppingCartReducer";

interface IShoppingCartItem {
  orderLine: OrderLine;
  handleDeleteOrderLine: () => void;
}

const ShoppingCartitem = ({
  orderLine,
  handleDeleteOrderLine,
}: IShoppingCartItem) => {
  const [count, setCount] = useState<number>(orderLine.itemsQuantity);
  const dispatch = useDispatch();
  const {user} = useAuth0();

  useEffect(() => {
    const updateShoppingCartPayload: UpdateShoppingCartPayload = {
      productId: orderLine.product.id,
      quantity: count,
      email: user?.email
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
