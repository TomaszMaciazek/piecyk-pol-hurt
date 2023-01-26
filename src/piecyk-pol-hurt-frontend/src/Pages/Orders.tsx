import { DataGrid, GridColDef, GridToolbar } from "@mui/x-data-grid";
import React from "react";
import { DataGridTranslations } from "../MUI/DataGridTranslations";

const Orders = () => {
  const columns: GridColDef[] = [
    { field: "id", headerName: "Id zamówienia" },
    {
      field: "product",
      headerName: "Nazwa produktu",
      minWidth: 200,
    },
    {
      field: "price",
      headerName: "Cena",
      minWidth: 90,
      type: "number",
    },
    {
      field: "orderDate",
      headerName: "Data zamówienia",
      type: "date",
      minWidth: 120,
    },
    {
      field: "status",
      headerName: "Status zamówienia",
      description: "This column has a value getter and is not sortable.",
      minWidth: 160,
    },
  ];

  const rows = [
    {
      id: 1,
      product: "Snow",
      price: 123,
      age: 35,
      orderDate: new Date(),
      status: "Odebrano",
    },
    {
      id: 2,
      product: "Lannister",
      price: 123331,
      orderDate: new Date(),
      status: "Odebrano",
    },
    {
      id: 3,
      product: "Lannister",
      price: 123,
      orderDate: new Date(),
      status: "Odebrano",
    },
    {
      id: 4,
      product: "Stark",
      price: 123,
      orderDate: new Date(),
      status: "Odebrano",
    },
    {
      id: 5,
      product: "Targaryen",
      price: 123,
      orderDate: new Date(),
      status: "Odebrano",
    },
    {
      id: 6,
      product: "Melisandre",
      price: 22,
      orderDate: new Date(),
      status: "Odebrano",
    },
  ];

  return (
    <div style={{ height: 400, width: "100%" }}>
      <div style={{ display: "flex", height: "100%" }}>
        <div style={{ flexGrow: 1 }}>
          <DataGrid
            rows={rows}
            columns={columns}
            pageSize={5}
            rowsPerPageOptions={[5]}
            disableSelectionOnClick
            experimentalFeatures={{ newEditingApi: true, }}
            localeText={DataGridTranslations}
            components={{
              Toolbar: GridToolbar,
            }}
          />
        </div>
      </div>
    </div>
  );
};

export default Orders;
