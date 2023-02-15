import { Add } from "@mui/icons-material";
import { Grid, Button, Typography } from "@mui/material";
import React, { useCallback, useEffect, useState } from "react";
import MapModal from "../Components/MapModal";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import { DataGridTranslations } from "../MUI/DataGridTranslations";
import { deleteSendPoint, getSendPoints } from "../API/Endpoints/SendPoint";
import { SendPointQuery } from "../API/Models/SendPoint/SendPointQuery";
import { SendPoint } from "../API/Models/SendPoint/SendPoint";
import LoadingScreen from "../Common/LoadingScreen";
import { TableToolbar } from "../MUI/TableToolbar";
import ConfirmationDialog from "../Common/ConfirmationDialog";

const Locations = () => {
  const [openModal, setOpenModal] = useState<boolean>(false);
  const [openDialog, setOpenDialog] = useState<boolean>(false);

  const [pageIndex, setPageIndex] = useState<number>(0);
  const [itemCount, setItemCount] = useState<number>(1);
  const [pageSize, setPageSize] = useState<number>(20);

  const [refresh, setRefresh] = useState<boolean>(false);
  const [sendPoints, setSendPoints] = useState<SendPoint[]>();
  const [editedSendPoint, setEditedSendPoint] = useState<SendPoint | null>(
    null
  );

  const getSendPointsFromApi = useCallback(() => {
    const sendPointQuery: SendPointQuery = {
      pageNumber: pageIndex + 1,
      pageSize: pageSize,
    };

    getSendPoints(sendPointQuery).then((data) => {
      setRefresh(false);
      setSendPoints(data.items);
      setItemCount(data.totalCount);
    });
  }, [pageIndex, pageSize]);

  useEffect(() => {
    getSendPointsFromApi();
  }, [getSendPointsFromApi, pageIndex]);

  useEffect(() => {
    if (refresh) {
      getSendPointsFromApi();
    }
  }, [getSendPointsFromApi, refresh]);

  const handleDelete = () => {
    if (editedSendPoint) {
      deleteSendPoint(editedSendPoint.id).then(() => {
        getSendPointsFromApi();
        setEditedSendPoint(null);
      });
    }
  };

  const columns: GridColDef[] = [
    { field: "id", headerName: "Id", type: "number", flex: 0.5 },
    {
      field: "name",
      headerName: "Nazwa",
      flex: 4,
    },
    {
      field: "city",
      headerName: "Miasto",
      flex: 4,
    },
    {
      field: "street",
      headerName: "Ulica",
      flex: 4,
    },
    {
      field: "buildingNumber",
      headerName: "Numer budynku",
      flex: 2,
    },
    {
      field: "code",
      headerName: "Kod",
      flex: 2,
    },
    {
      field: "isActive",
      headerName: "Aktywny",
      flex: 1.5,
      renderCell: (row) => {
        return (
          <Typography
            variant="body1"
            component="span"
            color={row.value ? "green" : "error"}
          >
            {row.value ? "Aktywny" : "Nieaktywny"}
          </Typography>
        );
      },
    },
    {
      field: "edit",
      flex: 1.2,
      renderCell: (row) => {
        return (
          <Button
            variant="contained"
            onClick={() => {
              setEditedSendPoint(
                sendPoints?.find((item) => item.id === row.id) ?? null
              );
              setOpenModal(true);
            }}
          >
            Edytuj
          </Button>
        );
      },
    },
    {
      field: "delete",
      flex: 1.2,
      renderCell: (row) => {
        return (
          <Button
            color="error"
            variant="contained"
            onClick={() => {
              setEditedSendPoint(
                sendPoints?.find((item) => item.id === row.id) ?? null
              );
              setOpenDialog(true);
            }}
          >
            Usuń
          </Button>
        );
      },
    },
  ];

  if (!sendPoints) {
    return <LoadingScreen />;
  }

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
              rows={sendPoints}
              columns={columns}
              disableSelectionOnClick
              experimentalFeatures={{ newEditingApi: true }}
              localeText={DataGridTranslations}
              rowsPerPageOptions={[10, 20, 50, 100, 200]}
              components={{
                Toolbar: TableToolbar,
              }}
              pageSize={pageSize}
              page={pageIndex}
              rowCount={itemCount}
              onPageChange={(page) => setPageIndex(page)}
              onPageSizeChange={(pageSize) => setPageSize(pageSize)}
            />
          </div>
        </div>
      </div>
      <MapModal
        handleClose={() => {
          setEditedSendPoint(null);
          setOpenModal(false);
        }}
        open={openModal}
        setRefresh={setRefresh}
        editedSendPoint={editedSendPoint}
      />
      <ConfirmationDialog
        open={openDialog}
        handleClose={() => setOpenDialog(false)}
        handleConfirm={handleDelete}
      />
    </>
  );
};

export default Locations;
