import {
  Button,
  Grid,
  MenuItem,
  Select,
  SelectChangeEvent,
  TextField,
} from "@mui/material";
import { MapContainer, Marker, Popup, TileLayer } from "react-leaflet";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { getActiveSendPoints } from "../API/Endpoints/SendPoint";
import { SendPoint } from "../API/Models/SendPoint/SendPoint";
import { RootState } from "../Redux/store";
import { LatLng } from "leaflet";
import { updateChosenSendPoint } from "../Redux/Reducers/ShoppingCartReducer";
import LoadingScreen from "../Common/LoadingScreen";

const LocationChoosing = () => {
  const [sendPoints, setSendPoints] = useState<SendPoint[]>();
  const chosenSendPoint = useSelector(
    (state: RootState) => state.shoppingCarts.chosenSendPoint
  );
  const dispatch = useDispatch();
  const navigate = useNavigate();

  useEffect(() => {
    const L = require("leaflet");
    delete L.Icon.Default.prototype._getIconUrl;
    L.Icon.Default.mergeOptions({
      iconRetinaUrl: require("../Images/Warehouse.png"),
      iconUrl: require("../Images/Warehouse.png"),
      shadowUrl: require("leaflet/dist/images/marker-shadow.png"),
    });
  }, []);

  useEffect(() => {
    getActiveSendPoints().then((data) => {
      setSendPoints(data);
    });
  }, []);

  const onChangeLocationOnMap = (location: LatLng) => {
    const sendPoint = sendPoints?.find(
      (item) => item.latitude === location.lat && item.logitude === location.lng
    );

    if (!sendPoint) {
      return;
    }
    dispatch(updateChosenSendPoint(sendPoint));
  };

  const onSelectLocation = (e: SelectChangeEvent) => {
    const id = parseInt(e.target.value);
    const sendPoint = sendPoints?.find((item) => item.id === id);

    if (!sendPoint) {
      return;
    }
    dispatch(updateChosenSendPoint(sendPoint));
  };

  if (!sendPoints) {
    return <LoadingScreen />;
  }

  return (
    <Grid container justifyContent="space-between" sx={{ height: "80vh" }}>
      <div style={{ width: "25%" }}>
        <Grid container flexDirection="column">
          <h2>Wybierz lokalizację</h2>
          <Select
            value={chosenSendPoint ? chosenSendPoint.id.toString() : ""}
            onChange={onSelectLocation}
          >
            ;
            {sendPoints.map((item) => (
              <MenuItem key={item.id} value={item.id}>
                {item.name}
              </MenuItem>
            ))}
          </Select>
          {chosenSendPoint && (
            <>
              <TextField
                sx={{ mt: 2 }}
                label="Miasto"
                value={chosenSendPoint.city}
                inputProps={{ readOnly: true }}
              />
              <TextField
                sx={{ mt: 2 }}
                label="Adres"
                value={`${chosenSendPoint.street} ${chosenSendPoint.buildingNumber}`}
                inputProps={{ readOnly: true }}
              />
              <TextField
                sx={{ mt: 2 }}
                label="Kod"
                value={chosenSendPoint.code}
                inputProps={{ readOnly: true }}
              />
            </>
          )}
          <Button
            disabled={!chosenSendPoint}
            variant="contained"
            onClick={() => navigate("/Sklep")}
            sx={{ mt: 3 }}
          >
            Przejdź do sklepu
          </Button>
        </Grid>
      </div>
      <MapContainer
        center={[52, 19]}
        zoom={6}
        scrollWheelZoom={false}
        style={{ width: "70%" }}
      >
        <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
        {sendPoints &&
          sendPoints.map((item) => (
            <Marker
              key={item.id}
              position={[item.latitude, item.logitude]}
              eventHandlers={{
                click: (e) => {
                  onChangeLocationOnMap(e.latlng);
                },
              }}
            >
              <Popup>{(item.name)}</Popup>
            </Marker>
          ))}
      </MapContainer>
    </Grid>
  );
};

export default LocationChoosing;
