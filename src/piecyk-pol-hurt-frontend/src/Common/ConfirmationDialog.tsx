import React from "react";
import {
  Dialog,
  DialogTitle,
  DialogActions,
  Button,
  DialogContent,
  DialogContentText,
} from "@mui/material";

interface IConfirmationDialog {
  open: boolean;
  handleConfirm: () => void;
  handleClose: () => void;
  title?: string;
  body?: string;
}

const ConfirmationDialog = ({
  open,
  handleConfirm,
  handleClose,
  title,
  body,
}: IConfirmationDialog) => {
  return (
    <Dialog
      open={open}
      onClose={handleClose}
      aria-labelledby="alert-dialog-title"
      aria-describedby="alert-dialog-description"
    >
      <DialogTitle id="alert-dialog-title">
        {title ? title : "Czy na pewno chcesz usunąć"}
      </DialogTitle>
      <DialogContent>
        <DialogContentText id="alert-dialog-description">
          {body ? body : "Tej akcji nie można cofnąć"}
        </DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button
          onClick={() => {
            handleConfirm();
            handleClose();
          }}
          color="secondary"
          variant="contained"
        >
          Tak
        </Button>
        <Button variant="contained" onClick={handleClose} autoFocus>
          Anuluj
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default ConfirmationDialog;
