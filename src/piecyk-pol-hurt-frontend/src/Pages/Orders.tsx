import { Button, Grid, MenuItem, Select } from "@mui/material";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import { useCallback, useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { toast } from "react-toastify";
import {
  approveOrder,
  cancelOrder,
  finishOrder,
  getOrders,
  getUserOrders,
  rejectOrder,
} from "../API/Endpoints/Order";
import { ApproveOrderCommand } from "../API/Models/Order/ApproveOrderCommand";
import { Order } from "../API/Models/Order/Order";
import { OrderQuery } from "../API/Models/Order/OrderQuery";
import ConfirmationDialog from "../Common/ConfirmationDialog";
import LoadingScreen from "../Common/LoadingScreen";
import { OrderStatus } from "../Constants/Enums/OrderStatus";
import { UserRole } from "../Constants/Enums/UserRole";
import { OrderStatusName } from "../Constants/OrderStatusName";
import { DataGridTranslations } from "../MUI/DataGridTranslations";
import { TableToolbar } from "../MUI/TableToolbar";
import { RootState } from "../Redux/store";

const Orders = () => {
  const permissions = useSelector(
    (state: RootState) => state.orders.permissions
  );

  const [openDialog, setOpenDialog] = useState<boolean>(false);

  const [pageIndex, setPageIndex] = useState<number>(0);
  const [itemCount, setItemCount] = useState<number>(1);
  const [pageSize, setPageSize] = useState<number>(20);

  const [refresh, setRefresh] = useState<boolean>(false);
  const [orders, setOrders] = useState<Order[]>();
  const [editedOrderId, setEditedOrderId] = useState<number | null>(null);

  const getOrdersFromApi = useCallback(() => {
    const orderQuery: OrderQuery = {
      pageNumber: pageIndex + 1,
      pageSize: pageSize,
    };    

    if (permissions === UserRole.LoggedUser) {
      getUserOrders(orderQuery).then((data) => {
        setRefresh(false);
        setOrders(data.items);
        setItemCount(data.totalCount);
      });
    } else {
      getOrders(orderQuery).then((data) => {
        setRefresh(false);
        setOrders(data.items);
        setItemCount(data.totalCount);
      });
    }
  }, [pageIndex, pageSize, permissions]);

  useEffect(() => {
    getOrdersFromApi();
  }, [getOrdersFromApi, pageIndex, permissions]);

  useEffect(() => {
    if (refresh) {
      getOrdersFromApi();
    }
  }, [getOrdersFromApi, refresh]);

  const handleCancelOrder = () => {
    if (editedOrderId) {
      cancelOrder(editedOrderId).then(() => {
        getOrdersFromApi();
        setEditedOrderId(null);
        toast.success("Anulowano zamówienie");
      });
    }
  };

  const onChangedStatus = () => {
    toast.success("Zaktualizowano status");
    getOrdersFromApi();
  };

  const handleChangeStatus = (id: number, status: OrderStatus) => {
    if (status === OrderStatus.Approved) {
      const command: ApproveOrderCommand = {
        receptionDate: new Date(),
        id: id,
      };
      approveOrder(command).then((data) => {
        if (data) {
          onChangedStatus();
        }
      });
    } else if (status === OrderStatus.Finished) {
      finishOrder(id).then((data) => {
        if (data) {
          onChangedStatus();
        }
      });
    } else if (status === OrderStatus.Rejected) {
      rejectOrder(id).then((data) => {
        if (data) {
          onChangedStatus();
        }
      });
    }
  };

  const columns: GridColDef[] = [
    { field: "id", headerName: "Id", type: "number", flex: 0.5 },
    {
      field: "lines",
      headerName: "Nazwa produktu",
      flex: 3,
      renderCell: (row) =>
        row.value.map((item: any) => item.productName).join(", "),
    },
    {
      field: "totalPrice",
      headerName: "Cena",
      flex: 1,
      type: "number",
      renderCell: (row) => `${row.value} zł`,
    },
    {
      field: "orderDate",
      headerName: "Data zamówienia",
      type: "date",
      flex: 1.5,
      renderCell: (row) => new Date(row.value).toLocaleString(),
    },
    {
      field: "status",
      headerName: "Zmiana statusu",
      flex: 2,
      renderCell: (row) => {
        if (permissions === UserRole.LoggedUser) {
          if (
            row.value === OrderStatus.Sent ||
            row.value === OrderStatus.Approved
          ) {
            return (
              <Button
                color="error"
                variant="contained"
                onClick={() => {
                  setOpenDialog(true);
                  setEditedOrderId(row.id as number);
                }}
                size="small"
              >
                Anuluj rezerwację
              </Button>
            );
          }
          return OrderStatusName[row.value];
        } else {
          if (
            row.value === OrderStatus.Canceled ||
            row.value === OrderStatus.Finished
          ) {
            return OrderStatusName[row.value];
          }
          return (
            <Select
              value={
                orders?.find((item) => (item.id = row.id as number))?.status ??
                ""
              }
              onChange={(e) =>
                handleChangeStatus(
                  row.id as number,
                  e.target.value as OrderStatus
                )
              }
              fullWidth
              variant="standard"
            >
              {Object.values(OrderStatus)
                .filter((key) => !isNaN(Number(key)) && key !== OrderStatus.Canceled)
                .map((item) => (
                  <MenuItem key={item} value={item}>
                    {OrderStatusName[item as number]}
                  </MenuItem>
                ))}
            </Select>
          );
        }
      },
    },
  ];

  if (!orders) {
    return <LoadingScreen />;
  }

  return (
    <>
      <Grid justifyContent="flex-end" container marginBottom={2}></Grid>
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
      <ConfirmationDialog
        open={openDialog}
        handleClose={() => setOpenDialog(false)}
        handleConfirm={handleCancelOrder}
      />
    </>
  );
};

export default Orders;
