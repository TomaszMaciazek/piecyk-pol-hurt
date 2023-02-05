import { useAuth0 } from "@auth0/auth0-react";
import { List, Button, Divider } from "@mui/material";
import { useDispatch, useSelector } from "react-redux";
import { toast } from "react-toastify";
import { createOrder } from "../API/Endpoints/Order";
import { CreateOrderCommand } from "../API/Models/Order/CreateOrderCommand";
import { CreateOrderLineCommand } from "../API/Models/Order/CreateOrderLineCommand";
import ShoppingCartItem from "../Components/ShoppingCartItem";
import {
  clearShoppingCart,
  removeOrderLines,
} from "../Redux/Reducers/ShoppingCartReducer";
import { RootState } from "../Redux/store";
import "../SCSS/ShoppingCart.scss";

const ShoppingCart = () => {
  const { user } = useAuth0();
  const chosenSendPoint = useSelector(
    (state: RootState) => state.shoppingCarts.chosenSendPoint
  );
  const shoppingCart = useSelector((state: RootState) =>
    state.shoppingCarts.shoppingCarts.find(
      (item) =>
        item.email === user?.email && item.sentPointId === chosenSendPoint?.id
    )
  );

  const dispatch = useDispatch();

  const handleDeleteOrderLine = (id: number) => {
    dispatch(removeOrderLines({ email: user?.email, id: id }));
    toast.success("Usunięto produkt z koszyka");
  };

  const submitOrder = () => {
    if (!shoppingCart) {
      return;
    }

    const createOrderLinesCommand: CreateOrderLineCommand[] =
      shoppingCart.orderLines.map((item) => {
        const createOrderLine: CreateOrderLineCommand = {
          itemsQuantity: item.itemsQuantity,
          priceForOneItem: item.priceForOneItem,
          productId: item.product.id,
        };
        return createOrderLine;
      });

    const createOrderCommand: CreateOrderCommand = {
      sendPointId: shoppingCart.sentPointId,
      lines: createOrderLinesCommand,
      requestedReceptionDate: new Date(),
    };

    createOrder(createOrderCommand).then(() => {
      toast.success("Złożono zamówienie");
      dispatch(clearShoppingCart(user?.email));
    });
  };

  if (!shoppingCart) {
    return <></>;
  }

  if (shoppingCart.orderLines.length === 0) {
    return <h2>Koszyk jest pusty</h2>;
  }

  return (
    <>
      <h2>Koszyk</h2>
      <List sx={{ width: "600px" }}>
        {shoppingCart.orderLines.map((orderLine) => (
          <div key={orderLine.product.id}>
            <ShoppingCartItem
              orderLine={orderLine}
              handleDeleteOrderLine={() =>
                handleDeleteOrderLine(orderLine.product.id)
              }
            />
            <Divider />
          </div>
        ))}
      </List>
      <Button onClick={submitOrder} sx={{ mt: 2 }} variant="contained">
        Potwierdź zakup
      </Button>
    </>
  );
};

export default ShoppingCart;
