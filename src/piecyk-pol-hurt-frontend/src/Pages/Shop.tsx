import { useAuth0 } from "@auth0/auth0-react";
import { Grid } from "@mui/material";
import React, { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { toast } from "react-toastify";
import { getProducts } from "../API/Endpoints/Product";
import { OrderLine } from "../API/Models/Order/OrderLine";
import { Product } from "../API/Models/Product/Product";
import { ProductQuery } from "../API/Models/Product/ProductQuery";
import LoadingScreen from "../Common/LoadingScreen";
import ProductItem from "../Components/ProductItem";
import { addOrderLines } from "../Redux/Reducers/ShoppingCartReducer";

const Shop = () => {
  const [products, setProducts] = useState<Product[]>();
  const { user } = useAuth0();

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
    const productQuery: ProductQuery = {
      pageNumber: 1,
      pageSize: 100,
    };

    getProducts(productQuery).then((data) => {
      setProducts(data.items);
    });
  }, []);

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
