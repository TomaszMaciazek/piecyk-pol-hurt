import { Add, Remove } from "@mui/icons-material";
import {
  Grid,
  Card,
  CardMedia,
  CardContent,
  Typography,
  CardActions,
  Button,
  ButtonGroup,
  TextField,
} from "@mui/material";
import React, { useState } from "react";
import OrderModal from "../Components/OrderModal";
import "../SCSS/Shop.scss";

const Shop = () => {
  const [openOrderModal, setOpenOrderModal] = useState<boolean>(false);
  const [count, setCount] = useState<number>(0);

  return (
    <>
      <Grid justifyContent="flex-end" container marginBottom={2}></Grid>
      <Grid container spacing={2}>
        <Grid item key={"1"}>
          <Card sx={{ width: 350 }}>
            <CardMedia
              component="img"
              height="140"
              width="auto"
              image="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS2jWd_yGETuMN0NI9QjCydgFMOteG4em2Baw&usqp=CAU"
              alt="coal"
            />
            <CardContent>
              <Typography gutterBottom variant="h5" component="div">
                Węgiel
              </Typography>
              <Typography variant="body2" color="text.secondary">
                węgiel super się pali
              </Typography>
            </CardContent>
            <CardActions>
              <Grid container spacing={1} justifyContent="space-between">
                <Button size="small" onClick={() => setOpenOrderModal(true)}>
                  Dodaj do koszyka
                </Button>
                <ButtonGroup>
                  <Button
                    onClick={() => {
                      setCount(Math.max(count - 1, 0));
                    }}
                    size="small"
                  >
                    <Remove fontSize="small" />
                  </Button>
                  <TextField
                    value={Number.isNaN(count) ? 0 : count}
                    onChange={(e) => {
                      setCount(parseInt(e.target.value));
                    }}
                    className="count"
                    type="number"
                    size="small"
                    inputProps={{style: { textAlign: 'center' }}}
                    sx={{
                      "& fieldset": { border: "none" },
                    }}
                  />
                  <Button
                    variant="contained"
                    onClick={() => {
                      setCount(count + 1);
                    }}
                    size="small"
                  >
                    <Add fontSize="small" />
                  </Button>
                </ButtonGroup>
              </Grid>
            </CardActions>
          </Card>
        </Grid>
      </Grid>
      <OrderModal
        handleClose={() => {
          setOpenOrderModal(false);
        }}
        open={openOrderModal}
      />
    </>
  );
};

export default Shop;
