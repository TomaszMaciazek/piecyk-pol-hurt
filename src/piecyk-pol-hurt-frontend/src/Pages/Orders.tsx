import { Add } from "@mui/icons-material";
import { Button, Grid } from "@mui/material";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import { useCallback, useEffect, useState } from "react";
import { deleteOrder, getOrders } from "../API/Endpoints/Order";
import { Order } from "../API/Models/Order/Order";
import { OrderQuery } from "../API/Models/Order/OrderQuery";
import ConfirmationDialog from "../Common/ConfirmationDialog";
import ProductModal from "../Components/ProductModal";
import { DataGridTranslations } from "../MUI/DataGridTranslations";
import { TableToolbar } from "../MUI/TableToolbar";

const Orders = () => {
  const [openModal, setOpenModal] = useState<boolean>(false);
  const [openDialog, setOpenDialog] = useState<boolean>(false);

  const [pageIndex, setPageIndex] = useState<number>(0);
  const [itemCount, setItemCount] = useState<number>(1);
  const [pageSize, setPageSize] = useState<number>(20);

  const [refresh, setRefresh] = useState<boolean>(false);
  const [orders, setOrders] = useState<Order[]>();
  const [editedOrder, setEditedOrder] = useState<Order | null>(null);

  const getOrdersFromApi = useCallback(() => {
    const orderQuery: OrderQuery = {
      pageNumber: pageIndex + 1,
      pageSize: pageSize,
    };

    getOrders(orderQuery).then((data) => {
      setRefresh(false);
      setOrders(data.items);
      setItemCount(data.totalCount);
    });
  }, [pageIndex, pageSize]);

  useEffect(() => {
    getOrdersFromApi();
  }, [getOrdersFromApi, pageIndex]);

  useEffect(() => {
    if (refresh) {
      getOrdersFromApi();
    }
  }, [getOrdersFromApi, refresh]);

  const handleDelete = () => {
    if (editedOrder) {
      deleteOrder(editedOrder.id).then(() => {
        getOrdersFromApi();
        setEditedOrder(null);
      });
    }
  };

  const columns: GridColDef[] = [
    { field: "id", headerName: "Id zamówienia", type: "number" },
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

  if (!products) {
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
          Dodaj zamówienie
        </Button>
      </Grid>
      <div style={{ height: 400, width: "100%" }}>
        <div style={{ display: "flex", height: "100%" }}>
          <div style={{ flexGrow: 1 }}>
            <DataGrid
              rows={orders}
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
      <ProductModal
        handleClose={() => {
          setEditedOrder(null);
          setOpenModal(false);
        }}
        open={openModal}
        setRefresh={setRefresh}
        editedOrder={editedOrder}
      />
      <ConfirmationDialog
        open={openDialog}
        handleClose={() => setOpenDialog(false)}
        handleConfirm={handleDelete}
      />
    </>
  );
};

export default Orders;
