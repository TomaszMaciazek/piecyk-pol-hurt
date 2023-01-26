import { Modal, Box, TextField, Button } from "@mui/material";
import React, { useEffect, useState } from "react";

interface IProductModal {
  open: boolean;
  handleClose: () => void;
}

const ProductModal = ({
  open,
  handleClose,
}: IProductModal) => {
  const [name, setName] = useState<string>("");
  const [description, setDescription] = useState<string>("");
  const [price, setPrice] = useState<number>(50);
  const [imageUrl, setImageUrl] = useState<string>("");

 

  const onClose = () => {
    setName("");
    setPrice(50);
    setDescription("");
    setImageUrl("");
    handleClose();
  };

  const style = {
    position: "absolute",
    top: "50%",
    left: "50%",
    transform: "translate(-50%, -50%)",
    bgcolor: "background.paper",
    boxShadow: 24,
    pt: 2,
    px: 4,
    pb: 3,
  };

  return (
    <Modal
      open={open}
      onClose={onClose}
      aria-labelledby="parent-modal-title"
      aria-describedby="parent-modal-description"
    >
      <Box sx={{ ...style, width: 400, height: 480 }}>
        <h2 id="parent-modal-title">
          {true ? "Edycja" : "Dodaj usługę"}
        </h2>
        <Box
          component="form"
          sx={{
            "& .MuiTextField-root": { mb: 2, width: "100%" },
          }}
          noValidate
          autoComplete="off"
        >
          <TextField
            required
            error={false}
            label="Nazwa usługi"
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
          <TextField
            error={false}
            label="Opis"
            multiline
            rows={4}
            value={description}
            onChange={(e) => setDescription(e.target.value)}
          />
          <TextField
            error={false}
            label="Cena"
            InputProps={{ inputProps: { min: 1, step: 1 } }}
            type="number"
            value={price}
            onChange={(e) => setPrice(parseInt(e.target.value))}
          />
          <TextField
            error={false}
            label="Adres Url"
            value={imageUrl}
            onChange={(e) => setImageUrl(e.target.value)}
          />
          <Button variant="contained" sx={{ mt: 2 }} onClick={() => undefined}>
            Zapisz
          </Button>
        </Box>
      </Box>
    </Modal>
  );
};

export default ProductModal;
