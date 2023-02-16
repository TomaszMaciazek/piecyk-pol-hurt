import { useAuth0 } from "@auth0/auth0-react";
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
import { toast } from "react-toastify";
import { ProductSendPointListItemDto } from "../API/Models/Product/ProductSendPointListItemDto";
import Counter from "../Common/Counter";
interface IProductItem {
  product: ProductSendPointListItemDto;
  handleAddToShoppingCart: (product: ProductSendPointListItemDto, quantity: number) => void;
}
const ProductItem = ({ product, handleAddToShoppingCart }: IProductItem) => {
  const [count, setCount] = useState<number>(0);
  const {user} = useAuth0();

  const onAddToShoppingCartClick = () => {
    if (!user) {
      toast.info("Musisz się zalogować, żeby dodać do koszyka");
      return;
    }
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
