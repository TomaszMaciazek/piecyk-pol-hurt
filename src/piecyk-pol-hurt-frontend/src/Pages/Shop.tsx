import { useAuth0 } from "@auth0/auth0-react";
import { Grid } from "@mui/material";
import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { toast } from "react-toastify";
import { getTodaysProductsFromSendPoint } from "../API/Endpoints/Product";
import { OrderLine } from "../API/Models/Order/OrderLine";
import { Product } from "../API/Models/Product/Product";
import { ProductSendPointListItemDto } from "../API/Models/Product/ProductSendPointListItemDto";
import LoadingScreen from "../Common/LoadingScreen";
import ProductItem from "../Components/ProductItem";
import { addOrderLines } from "../Redux/Reducers/ShoppingCartReducer";
import { RootState } from "../Redux/store";

const Shop = () => {
  const [products, setProducts] = useState<ProductSendPointListItemDto[]>();
  const { user } = useAuth0();

  const sendPointId = useSelector(
    (state: RootState) => state.shoppingCarts.chosenSendPoint?.id
  );
  const dispatch = useDispatch();

  const handleAddToShoppingCart = (product: Product, quantity: number) => {
    toast.success("Dodano do koszyka");

    const orderLine: OrderLine = {
      itemsQuantity: quantity,
      product: product,
      priceForOneItem: product.price,
    };
    dispatch(
      addOrderLines({
        email: user?.email,
        orderLine: orderLine,
      })
    );
  };

  useEffect(() => {
    if (sendPointId) {
      getTodaysProductsFromSendPoint(sendPointId).then((data) => {     
        setProducts(data);
      });
    }
  }, [sendPointId]);

  if (!products) {
    return <LoadingScreen />;
  }

  return (
    <Grid container spacing={4}>
      {products.map((product) => (
        <ProductItem
          key={product.id}
          product={product}
          handleAddToShoppingCart={handleAddToShoppingCart}
        />
      ))}
    </Grid>
  );
};

export default Shop;
