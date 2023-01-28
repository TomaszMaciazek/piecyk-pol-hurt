import {
  EditOutlined,
  DeleteOutlineOutlined,
  Add,
  Remove,
} from "@mui/icons-material";
import {
  Grid,
  Card,
  CardMedia,
  CardContent,
  Typography,
  CardActions,
  Button,
  IconButton,
  ButtonGroup,
  TextField,
} from "@mui/material";
import React, { useState } from "react";
import ConfirmationDialog from "../Common/ConfirmationDialog";
import OrderModal from "../Components/OrderModal";
import ProductModal from "../Components/ProductModal";
import "../SCSS/Products.scss";

const Products = () => {
  const [openDialog, setOpenDialog] = useState<boolean>(false);
  const [openProductModal, setOpenProductModal] = useState<boolean>(false);
  const [openOrderModal, setOpenOrderModal] = useState<boolean>(false);
  const [count, setCount] = useState<number>(0);

  return (
    <>
      <Grid justifyContent="flex-end" container marginBottom={2}>
        <Button
          variant="contained"
          endIcon={<Add />}
          onClick={() => setOpenProductModal(true)}
        >
          Dodaj produkt
        </Button>
      </Grid>
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
                <div>
                  <Button size="small" onClick={() => setOpenOrderModal(true)}>
                    Dodaj do koszyka
                  </Button>
                  <ButtonGroup>
                    <Button
                      onClick={() => {
                        setCount(Math.max(count - 1, 0));
                      }}
                    >
                      <Remove fontSize="small" />
                    </Button>
                    <Button className='count-button'>
                    <input
                      value={count}
                      onChange={(e) => {
                        setCount(parseInt(e.target.value));
                      }}
                      className='count'
                      type="tel"
                      />
                      </Button>
                    <Button
                      onClick={() => {
                        setCount(count + 1);
                      }}
                    >
                      <Add fontSize="small" />
                    </Button>
                  </ButtonGroup>
                </div>
                <div>
                  <IconButton
                    onClick={() => {
                      setOpenProductModal(true);
                    }}
                  >
                    <EditOutlined />
                  </IconButton>
                  <IconButton
                    onClick={() => {
                      setOpenDialog(true);
                    }}
                  >
                    <DeleteOutlineOutlined />
                  </IconButton>
                </div>
              </Grid>
            </CardActions>
          </Card>
        </Grid>
      </Grid>
      <ConfirmationDialog
        open={openDialog}
        handleClose={() => setOpenDialog(false)}
        handleConfirm={() => undefined}
      />
      <ProductModal
        handleClose={() => {
          setOpenProductModal(false);
        }}
        open={openProductModal}
      />
      <OrderModal
        handleClose={() => {
          setOpenOrderModal(false);
        }}
        open={openOrderModal}
      />
    </>
  );
};

export default Products;
