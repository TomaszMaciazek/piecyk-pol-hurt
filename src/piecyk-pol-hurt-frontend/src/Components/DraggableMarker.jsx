import { Button } from "@mui/material";
import { useMemo, useRef, useEffect } from "react";
import { Marker, Popup } from "react-leaflet";

const DraggableMarker = (props) => {
  useEffect(() => {
    const L = require("leaflet");
    delete L.Icon.Default.prototype._getIconUrl;
    L.Icon.Default.mergeOptions({
      iconRetinaUrl: require("leaflet/dist/images/marker-icon-2x.png"),
      iconUrl: require("leaflet/dist/images/marker-icon.png"),
      shadowUrl: require("leaflet/dist/images/marker-shadow.png"),
    });
  }, []);
  const markerRef = useRef(null);
  const eventHandlers = useMemo(
    () => ({
      dragend() {
        const marker = markerRef.current;
        if (marker != null) {
          const localization = marker.getLatLng();
          props.setLatitude(localization.lat);
          props.setLongitude(localization.lng);
        }
      },
    }),
    [props]
  );

  const deleteMarker = () => {
    props.setLatitude(null);
    props.setLongitude(null);
    props.setDisableMapEvents(true);
  };

  return (
    <Marker
      draggable
      eventHandlers={eventHandlers}
      position={{ lat: props.latitude, lng: props.longitude }}
      ref={markerRef}
    >
      <Popup minWidth={90}>
        <Button variant="contained" color="error" onClick={deleteMarker}>
          Usuń marker
        </Button>
      </Popup>
    </Marker>
  );
};

export default DraggableMarker;
