import { Modal, Box, Button } from "@mui/material";
import React from "react";
import {
  FormContainer,
  TextFieldElement,
  CheckboxElement,
} from "react-hook-form-mui";
import { toast } from "react-toastify";
import { addProduct, updateProduct } from "../API/Endpoints/Product";
import { CreateProductCommand } from "../API/Models/Product/CreateProductCommand";
import { Product } from "../API/Models/Product/Product";
import { UpdateProductCommand } from "../API/Models/Product/UpdateProductCommand";
import { ModalStyle } from "../Styles/ModalStyles";

interface IProductModal {
  open: boolean;
  handleClose: () => void;
  setRefresh: React.Dispatch<React.SetStateAction<boolean>>;
  editedProduct: Product | null;
}

const ProductModal = ({
  open,
  handleClose,
  setRefresh,
  editedProduct,
}: IProductModal) => {
  const sumbitProduct = (data: CreateProductCommand) => {
    if (editedProduct) {
      const request = data as UpdateProductCommand;
      request.id = editedProduct.id;

      updateProduct(request)
        .then(() => {
          setRefresh(true);
          onClose();
        })
        .catch((reason) => {
          if (reason.response.status === 409) {
            toast.error("Kod musi być unikalny");
          }
        });
    } else {
      addProduct(data)
        .then(() => {
          setRefresh(true);
          onClose();
        })
        .catch((reason) => {
          if (reason.response.status === 409) {
            toast.error("Kod musi być unikalny");
          }
        });
    }
  };

  const onClose = () => {
    handleClose();
  };

  return (
    <Modal open={open} onClose={onClose}>
      <Box sx={{ ...ModalStyle, width: 450, height: 600 }}>
        <h2>{editedProduct ? "Edytuj produkt" : "Dodaj produkt"}</h2>
        <FormContainer
          onSuccess={sumbitProduct}
          defaultValues={{
            code: editedProduct ? editedProduct.code : "",
            isActive: editedProduct ? editedProduct.isActive : false,
            name: editedProduct ? editedProduct.name : "",
            description: editedProduct ? editedProduct.description : "",
            imageUrl: editedProduct ? editedProduct.description : "",
            price: editedProduct ? editedProduct.price : undefined,
          }}
        >
          <Box
            sx={{
              "& .MuiTextField-root": { mb: 2, width: "100%" },
            }}
          >
            <TextFieldElement name="name" label="Nazwa" />
            <TextFieldElement name="code" label="Kod" />
            <TextFieldElement
              name="description"
              label="Opis"
              multiline
              rows={4}
            />
            <TextFieldElement name="price" label="Cena" type="number" />
            <TextFieldElement name="imageUrl" label="Adres URL zdjęcia" />
            <div>
              <CheckboxElement name="isActive" label="Aktywuj punkt odbioru" />
            </div>
            <Button variant="contained" type="submit" sx={{ mt: 1 }}>
              Zapisz
            </Button>
          </Box>
        </FormContainer>
      </Box>
    </Modal>
  );
};

export default ProductModal;
