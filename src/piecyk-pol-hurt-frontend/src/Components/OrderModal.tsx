import { useAuth0 } from "@auth0/auth0-react";
import { Modal, Box, Button, Grid } from "@mui/material";
import React from "react";
import {
  FormContainer,
  SelectElement,
  TextFieldElement,
} from "react-hook-form-mui";
import { ModalStyle } from "../Styles/ModalStyles";

interface IOrderModal {
  open: boolean;
  handleClose: () => void;
}

const OrderModal = ({ open, handleClose }: IOrderModal) => {
  const { user } = useAuth0();
  const submitOrder = (data: any) => {};
  return (
    <Modal
      open={open}
      onClose={handleClose}
      aria-labelledby="parent-modal-title"
      aria-describedby="parent-modal-description"
    >
      <Box sx={{ ...ModalStyle, width: 400, height: 480 }}>
        <h2>Składanie zamówienia</h2>
        <FormContainer onSuccess={submitOrder}>
          <Box
            sx={{
              "& .MuiTextField-root": { mb: 2, width: "100%" },
            }}
          >
            {user && (
              <TextFieldElement
                name="email"
                label="Adres email"
                required
                type="email"
              />
            )}
            <Grid container flexDirection="row" alignItems='center' justifyContent='space-between'>
              <SelectElement
                name="sentPoint"
                label="Miejsce odbioru"
                required
                style={{width: '60%'}}
              />
              <Button size='small' style={{marginBottom: '16px'}}>Otwórz mapę</Button>
            </Grid>
            <TextFieldElement
              name="description"
              label="Dodatkowy opis"
              required
              multiline
              rows={4}
            />
            <Button
              type="submit"
              variant="contained"
              sx={{ mt: 2 }}
              onClick={() => undefined}
            >
              Zapisz
            </Button>
          </Box>
        </FormContainer>
      </Box>
    </Modal>
  );
};

export default OrderModal;
