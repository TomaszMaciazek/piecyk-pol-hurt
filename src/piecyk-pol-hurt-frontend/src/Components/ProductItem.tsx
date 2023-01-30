import {
  Grid,
  Card,
  CardMedia,
  CardContent,
  Typography,
  CardActions,
  Button,
} from "@mui/material";
import React, { useState } from "react";
import { Product } from "../API/Models/Product/Product";
import Counter from "../Common/Counter";
interface IProductItem {
  product: Product;
  handleAddToShoppingCart: (product: Product, quantity: number) => void;
}
const ProductItem = ({ product, handleAddToShoppingCart }: IProductItem) => {
  const [count, setCount] = useState<number>(0);

  const onAddToShoppingCartClick = () => {
    handleAddToShoppingCart(product, count);
    setCount(0);
  };

  return (
    <Grid item key={product.id}>
      <Card sx={{ width: 350 }}>
        <CardMedia
          component="img"
          height="140"
          width="auto"
          image={product.imageUrl}
          alt={product.name}
        />
        <CardContent>
          <Grid container justifyContent="space-between">
            <Typography gutterBottom variant="h5" component="div">
              {product.name}
            </Typography>
            <div>{`${product.price} zł`}</div>
          </Grid>
          <Typography variant="body2" color="text.secondary">
            {product.description}
          </Typography>
        </CardContent>
        <CardActions>
          <Grid container spacing={1} justifyContent="space-between">
            <Button size="small" onClick={onAddToShoppingCartClick}>
              Dodaj do koszyka
            </Button>
          <Counter count={count} setCount={setCount}/>
          </Grid>
        </CardActions>
      </Card>
    </Grid>
  );
};

export default ProductItem;
