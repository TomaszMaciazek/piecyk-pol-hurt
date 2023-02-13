import { Button, Grid, Typography } from "@mui/material";
import React, { useCallback, useEffect, useState } from "react";
import { getReports } from "../API/Endpoints/Report";
import { ReportListItemDto } from "../API/Models/Reports/ReportListItemDto";
import { ReportQuery } from "../API/Models/Reports/ReportQuery";
import ReportModal from "../Components/ReportModal";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import { DataGridTranslations } from "../MUI/DataGridTranslations";
import { TableToolbar } from "../MUI/TableToolbar";
import LoadingScreen from "../Common/LoadingScreen";
import GenerateReportModal from "../Components/GenerateReportModal";

const Reports = () => {
  const [openReportModal, setOpenReportModal] = useState<boolean>(false);
  const [openGenerateReportModal, setOpenGenerateReportModal] =
    useState<boolean>(false);
  const [pageIndex, setPageIndex] = useState<number>(0);
  const [itemCount, setItemCount] = useState<number>(1);
  const [pageSize, setPageSize] = useState<number>(20);
  const [refresh, setRefresh] = useState<boolean>(false);
  const [reports, setReports] = useState<ReportListItemDto[]>();
  const [editedReportId, setEditedReportId] = useState<number | undefined>(
    undefined
  );

  const getReportsFromApi = useCallback(() => {
    const query: ReportQuery = {
      pageNumber: pageIndex + 1,
      pageSize: pageSize,
    };
    getReports(query).then((data) => {
      setReports(data.items);
      setItemCount(data.totalCount);
    });
  }, [pageSize, pageIndex]);

  useEffect(() => {
    getReportsFromApi();
  }, [getReportsFromApi, refresh]);

  const columns: GridColDef[] = [
    { field: "id", headerName: "Id", type: "number", flex: 0.5 },
    {
      field: "name",
      headerName: "Nazwa",
      flex: 4,
    },
    {
      field: "title",
      headerName: "Tytuł",
      flex: 2,
    },
    {
      field: "group",
      headerName: "Grupa",
      flex: 2,
      type: "number",
    },
    {
      field: "version",
      headerName: "Wersja",
      flex: 1.5,
      type: "number",
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
      field: "Edycja",
      flex: 1.2,
      renderCell: (row) => {
        return (
          <Button
            variant="contained"
            onClick={() => {
              const report = reports?.find((item) => item.id === row.id);
              setEditedReportId(report?.id);
              setOpenReportModal(true);
            }}
          >
            Edytuj
          </Button>
        );
      },
    },
    {
      field: "Raport",
      flex: 1.2,
      renderCell: (row) => {
        return (
          <Button
            variant="contained"
            color="secondary"
            onClick={() => {
              setOpenGenerateReportModal(true);
              const report = reports?.find((item) => item.id === row.id);
              setEditedReportId(report?.id);
            }}
          >
            Generuj
          </Button>
        );
      },
    },
  ];

  if (!reports) {
    return <LoadingScreen />;
  }

  return (
    <>
      <Grid container justifyContent="flex-end" marginBottom={2}>
        <Button variant="contained" onClick={() => setOpenReportModal(true)}>
          Dodaj raport
        </Button>
      </Grid>
      <ReportModal
        open={openReportModal}
        handleClose={() => setOpenReportModal(false)}
        setRefresh={setRefresh}
        editedReportId={editedReportId}
      />
      <div style={{ height: 400, width: "100%" }}>
        <div style={{ display: "flex", height: "100%" }}>
          <div style={{ flexGrow: 1 }}>
            <DataGrid
              rows={reports}
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
      <GenerateReportModal
        handleClose={() => setOpenGenerateReportModal(false)}
        open={openGenerateReportModal}
        editedReportId={editedReportId}
      />
    </>
  );
};

export default Reports;
