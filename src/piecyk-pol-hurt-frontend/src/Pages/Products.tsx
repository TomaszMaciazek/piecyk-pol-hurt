import { EditOutlined, DeleteOutlineOutlined, Add } from "@mui/icons-material";
import {
  Grid,
  Card,
  CardMedia,
  CardContent,
  Typography,
  CardActions,
  Button,
  IconButton,
} from "@mui/material";
import React, { useState } from "react";
import ConfirmationDialog from "../Common/ConfirmationDialog";
import ProductModal from "../Components/ProductModal";

const Products = () => {
  const [openDialog, setOpenDialog] = useState<boolean>(false);
  const [openModal, setOpenModal] = useState<boolean>(false);
  return (
    <>
      <Grid justifyContent="flex-end" container marginBottom={2}>
        <Button
          variant="contained"
          endIcon={<Add />}
          onClick={() => setOpenModal(true)}
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
                {"3"}
              </Typography>
              <Typography variant="body2" color="text.secondary">
                {"4"}
              </Typography>
            </CardContent>
            <CardActions>
              <Grid container spacing={1} justifyContent="space-between">
                <Button size="small" onClick={() => setOpenModal(true)}>Złóż zamówienie</Button>
                <div>
                  <IconButton
                    onClick={() => {
                      setOpenModal(true);
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
          setOpenModal(false);
        }}
        open={openModal}
      />
    </>
  );
};

export default Products;
