import { Grid, Select, MenuItem, SelectChangeEvent } from "@mui/material";
import React, { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { toast } from "react-toastify";
import { getProducts } from "../API/Endpoints/Product";
import { getActiveSendPoints } from "../API/Endpoints/SendPoint";
import { SimpleSendPoint } from "../API/Endpoints/SimpleSendPoint";
import { OrderLines } from "../API/Models/OrderLines/OrderLines";
import { Product } from "../API/Models/Product/Product";
import { ProductQuery } from "../API/Models/Product/ProductQuery";
import LoadingScreen from "../Common/LoadingScreen";
import ProductItem from "../Components/ProductItem";
import { addOrderLines } from "../Redux/Reducers/ShoppingCartReducer";

const Shop = () => {
  const [sendPoints, setSendPoints] = useState<SimpleSendPoint[]>();
  const [chosenSendPoint, setChosenSendPoint] = useState<SimpleSendPoint>();
  const [products, setProducts] = useState<Product[]>([]);
  const dispatch = useDispatch();

  const handleChangeSendPoint = (e: SelectChangeEvent) => {
    const id = parseInt(e.target.value);
    setChosenSendPoint(sendPoints?.find((item) => item.id === id));
  };

  const handleAddToShoppingCart = (product: Product, quantity: number) => {
    toast.success("Dodano do koszyka");  

    const orderLine: OrderLines = {
      itemsQuantity: quantity,
      product: product,
      productId: product.id
    };
    dispatch(addOrderLines(orderLine));
  };

  useEffect(() => {
    getActiveSendPoints().then((data) => {
      setSendPoints(data);
    });
  }, []);

  useEffect(() => {
    const productQuery: ProductQuery = {
      pageNumber: 1,
      pageSize: 100,
    };

    getProducts(productQuery).then((data) => {
      setProducts(data.items);
    });
  }, []);

  if (!sendPoints) {
    return <LoadingScreen />;
  }

  if (!chosenSendPoint) {
    return (
      <>
        <h2>Wybierz lokalizację</h2>
        <Select
          sx={{ width: "250px" }}
          value=""
          onChange={handleChangeSendPoint}
        >
          ;
          {sendPoints.map((item) => (
            <MenuItem key={item.id} value={item.id}>
              {item.name}
            </MenuItem>
          ))}
        </Select>
      </>
    );
  }

  return (
    <>
      <Grid container marginBottom={2} alignItems="center">
        <h4>Wybierz lokalizację</h4>
        <Select
          size="small"
          sx={{ width: "250px", height: "45px", ml: 2 }}
          value={chosenSendPoint.id.toString()}
          onChange={handleChangeSendPoint}
        >
          {sendPoints.map((item) => (
            <MenuItem key={item.id} value={item.id}>
              {item.name}
            </MenuItem>
          ))}
        </Select>
      </Grid>
      <Grid container spacing={4}>
        {products.map((product) => (
          <ProductItem
            key={product.id}
            product={product}
            handleAddToShoppingCart={handleAddToShoppingCart}
          />
        ))}
      </Grid>
    </>
  );
};

export default Shop;
