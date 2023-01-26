import { Add } from "@mui/icons-material";
import { Grid, Button } from "@mui/material";
import React, { useState } from "react";
import MapModal from "../Components/MapModal";
import {
  DataGrid,
  GridColDef,
  GridToolbar,
} from "@mui/x-data-grid";
import { DataGridTranslations } from "../MUI/DataGridTranslations";

const Locations = () => {
  const [openModal, setOpenModal] = useState<boolean>(false);

  const columns: GridColDef[] = [
    { field: "id", headerName: "Id", width: 90 },
    {
      field: "latitude",
      headerName: "Szerokość",
      width: 120,
      type: "number",
    },
    {
      field: "longitude",
      headerName: "Wysokość",
      type: "number",
      width: 120,
    },
    {
      field: "description",
      headerName: "Opis",
      width: 160,
    },
  ];

  const rows = [
    {
      id: 1,
      latitude: 23.2,
      longitude: 19.2,
      description: "Rudnik",
    },
    {
      id: 2,
      latitude: 23.2,
      longitude: 19.2,
      description: "Rudnik",
    },
    {
      id: 3,
      latitude: 23.2,
      longitude: 19.2,
      description: "Rudnik",
    },
  ];
  return (
    <>
      <Grid justifyContent="flex-end" container marginBottom={2}>
        <Button
          variant="contained"
          endIcon={<Add />}
          onClick={() => setOpenModal(true)}
        >
          Dodaj lokalizację
        </Button>
      </Grid>
      <div style={{ height: 400, width: "100%" }}>
        <div style={{ display: "flex", height: "100%" }}>
          <div style={{ flexGrow: 1 }}>
            <DataGrid
              rows={rows}
              columns={columns}
              pageSize={5}
              rowsPerPageOptions={[5]}
              disableSelectionOnClick
              checkboxSelection
              experimentalFeatures={{ newEditingApi: true }}
              localeText={DataGridTranslations}
              components={{
                Toolbar: GridToolbar,
              }}
            />
          </div>
        </div>
      </div>
      <MapModal
        handleClose={() => {
          setOpenModal(false);
        }}
        open={openModal}
      />
    </>
  );
};

export default Locations;
