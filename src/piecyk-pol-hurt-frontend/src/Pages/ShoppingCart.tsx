import { List, Button, Divider } from "@mui/material";
import { useDispatch, useSelector } from "react-redux";
import { toast } from "react-toastify";
import ShoppingCartItem from "../Components/ShoppingCartItem";
import { removeOrderLines } from "../Redux/Reducers/ShoppingCartReducer";
import { RootState } from "../Redux/store";
import "../SCSS/ShoppingCart.scss";

const ShoppingCart = () => {
  const orderLines = useSelector(
    (state: RootState) => state.shoppingCart.orderLines
  );
  const dispatch = useDispatch();

  const handleDeleteOrderLine = (id: number) => {
    dispatch(removeOrderLines(id));
    toast.success("Usunięto produkt z koszyka");
  };

  return (
    <>
      <h2>Koszyk</h2>

      <List sx={{ width: "600px" }}>
        {orderLines.map((orderLine) => (
          <>
            <ShoppingCartItem
              orderLine={orderLine}
              handleDeleteOrderLine={() =>
                handleDeleteOrderLine(orderLine.productId)
              }
            />
            <Divider />
          </>
        ))}
      </List>
      <Button sx={{ mt: 2 }} variant="contained">
        Potwierdź zakup
      </Button>
    </>
  );
};

export default ShoppingCart;
