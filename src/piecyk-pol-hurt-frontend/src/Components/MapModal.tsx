import { Modal, Box, Grid, Button, TextField } from "@mui/material";
import { useState } from "react";
import {
  CheckboxElement,
  FormContainer,
  TextFieldElement,
} from "react-hook-form-mui";
import { MapContainer, TileLayer, useMapEvents } from "react-leaflet";
import { addSendPoint, updateSendPoint } from "../API/Endpoints/SendPoint";
import { CreateSendPointCommand } from "../API/Models/SendPoint/CreateSendPointCommand";
import { SendPoint } from "../API/Models/SendPoint/SendPoint";
import { UpdateSendPointCommand } from "../API/Models/SendPoint/UpdateSendPointCommand";
import ConfirmationDialog from "../Common/ConfirmationDialog";
import { ModalStyle } from "../Styles/ModalStyles";
import DraggableMarker from "./DraggableMarker";

interface IMapModal {
  open: boolean;
  handleClose: () => void;
  setRefresh: React.Dispatch<React.SetStateAction<boolean>>;
  editedSendPoint: SendPoint | null;
}

const MapModal = ({
  open,
  handleClose,
  setRefresh,
  editedSendPoint,
}: IMapModal) => {
  const [latitude, setLatitude] = useState<number | null>(
    editedSendPoint ? editedSendPoint.latitude : null
  );
  const [longitude, setLongitude] = useState<number | null>(
    editedSendPoint ? editedSendPoint.longitude : null
  );

  const [newLatitude, setNewLatitude] = useState<number | null>(
    editedSendPoint ? editedSendPoint.latitude : null
  );
  const [newLongitude, setNewLongitude] = useState<number | null>(
    editedSendPoint ? editedSendPoint.longitude : null
  );

  const [showDialog, setShowDialog] = useState<boolean>(false);
  const [disableMapEvents, setDisableMapEvents] = useState<boolean>(false);

  const CheckClick = () => {
    useMapEvents({
      click: (e) => {
        if (disableMapEvents) {
          setDisableMapEvents(!disableMapEvents);
          return;
        }

        setNewLongitude(e.latlng.lng);
        setNewLatitude(e.latlng.lat);
        setShowDialog(true);
      },
    });
    return null;
  };

  const addMarker = () => {
    if (newLatitude && newLongitude) {
      setLatitude(newLatitude);
      setLongitude(newLongitude);
      setShowDialog(false);
    }
  };

  const submitSendPoint = (data: CreateSendPointCommand) => {
    data.longitude = longitude ?? 1;
    data.latitude = latitude ?? 1;

    if (editedSendPoint) {
      const request = data as UpdateSendPointCommand;
      request.id = editedSendPoint.id;

      updateSendPoint(request).then((data) => {
        setRefresh(true);
        onClose();
      });
    } else {
      addSendPoint(data).then(() => {
        setRefresh(true);
        onClose();
      });
    }
  };

  const onClose = () => {
    setLatitude(null);
    setLongitude(null);
    setNewLatitude(null);
    setNewLongitude(null);
    handleClose();
  };

  return (
    <Modal open={open} onClose={onClose}>
      <Box sx={{ ...ModalStyle, width: 1000, height: 670 }}>
        <h2>
          {editedSendPoint !== null
            ? "Edytuj lokalizację"
            : "Dodaj lokalizację"}
        </h2>
        <Grid container flexDirection="row" justifyContent="space-between">
          <Grid item sx={{ width: "45%" }}>
            <FormContainer
              onSuccess={submitSendPoint}
              defaultValues={{
                buildingNumber: editedSendPoint
                  ? editedSendPoint.buildingNumber
                  : "",
                city: editedSendPoint ? editedSendPoint.city : "",
                code: editedSendPoint ? editedSendPoint.code : "",
                isActive: editedSendPoint ? editedSendPoint.isActive : false,
                name: editedSendPoint ? editedSendPoint.name : "",
                street: editedSendPoint ? editedSendPoint.street : "",
              }}
            >
              <Box
                sx={{
                  "& .MuiTextField-root": { mb: 2, width: "100%" },
                }}
              >
                <TextFieldElement name="name" label="Nazwa" />
                <TextFieldElement name="code" label="Kod" />
                <TextFieldElement name="city" label="Miasto" />
                <TextFieldElement name="street" label="Ulica" />
                <TextFieldElement name="buildingNumber" label="Numer budynku" />
                <TextField
                  value={latitude ?? ""}
                  onChange={(e) => setLatitude(parseInt(e.target.value))}
                  type="number"
                  label="Szerokość geograficzna"
                />
                <TextField
                  value={longitude ?? ""}
                  onChange={(e) => setLongitude(parseInt(e.target.value))}
                  type="number"
                  label="Długość geograficzna"
                />
                <div>
                  <CheckboxElement
                    name="isActive"
                    label="Aktywuj punkt odbioru"
                  />
                </div>
                <Button variant="contained" type="submit" sx={{ mt: 1 }}>
                  Zapisz
                </Button>
              </Box>
            </FormContainer>
          </Grid>
          <Grid item>
            {latitude && longitude ? (
              <MapContainer
                center={[latitude, longitude]}
                zoom={10}
                scrollWheelZoom={false}
                style={{ height: 530, width: 530 }}
              >
                <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
                <DraggableMarker
                  latitude={latitude}
                  setLatitude={setLatitude}
                  longitude={longitude}
                  setLongitude={setLongitude}
                  setDisableMapEvents={setDisableMapEvents}
                />
              </MapContainer>
            ) : (
              <MapContainer
                center={[52, 19]}
                zoom={5}
                scrollWheelZoom={false}
                style={{ height: 530, width: 530 }}
              >
                <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
                <CheckClick />
              </MapContainer>
            )}
          </Grid>
        </Grid>
        <ConfirmationDialog
          open={showDialog}
          handleClose={() => setShowDialog(false)}
          title="Nowa lokalizacja"
          body="Czy chcez dodać nową lokalizację w tym miejscu?"
          handleConfirm={addMarker}
        />
      </Box>
    </Modal>
  );
};

export default MapModal;
