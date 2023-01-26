import { Modal, Box } from "@mui/material";
import { useState } from "react";
import { MapContainer, TileLayer, useMapEvents } from "react-leaflet";
import ConfirmationDialog from "../Common/ConfirmationDialog";
import DraggableMarker from "./DraggableMarker";

interface IMapModal {
  open: boolean;
  handleClose: () => void;
}

const MapModal = ({ open, handleClose }: IMapModal) => {
  const [latitude, setLatitude] = useState<number | null>(null);
  const [longitude, setLongitude] = useState<number | null>(null);
  const [newLatitude, setNewLatitude] = useState<number>();
  const [newLongitude, setNewLongitude] = useState<number>();
  const [showDialog, setShowDialog] = useState<boolean>(false);

  const CheckClick = () => {
    useMapEvents({
      click: (e) => {
        setNewLatitude(e.latlng.lat);
        setNewLongitude(e.latlng.lng);
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

  const onClose = () => {
    handleClose();
  };

  const style = {
    position: "absolute",
    top: "50%",
    left: "50%",
    transform: "translate(-50%, -50%)",
    bgcolor: "background.paper",
    boxShadow: 24,
  };

  return (
    <Modal
      open={open}
      onClose={onClose}
      aria-labelledby="parent-modal-title"
      aria-describedby="parent-modal-description"
    >
      <Box sx={{ ...style, width: 600, height: 600 }}>
        {latitude && longitude ? (
          <MapContainer
            center={[latitude, longitude]}
            zoom={10}
            scrollWheelZoom={false}
            style={{ height: 600, width: 600 }}
          >
            <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
            <DraggableMarker
              latitude={latitude}
              setLatitude={setLatitude}
              longitude={longitude}
              setLongitude={setLongitude}
            />
          </MapContainer>
        ) : (
          <MapContainer
            center={[52, 19]}
            zoom={5}
            scrollWheelZoom={false}
            style={{ height: `100%`, width: `100%` }}
          >
            <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
            <CheckClick />
          </MapContainer>
        )}
      <ConfirmationDialog
        open={showDialog}
        handleClose={() => setShowDialog(false)}
        title="Nowy lokalizacja"
        body="Czy chcez dodać nową lokalizację w tym miejscu?"
        handleConfirm={addMarker}
      />
      </Box>
    </Modal>
  );
};

export default MapModal;
