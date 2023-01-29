import { Add } from "@mui/icons-material";
import { Grid, Button, Typography } from "@mui/material";
import React, { useCallback, useEffect, useState } from "react";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import { DataGridTranslations } from "../MUI/DataGridTranslations";
import LoadingScreen from "../Common/LoadingScreen";
import { TableToolbar } from "../MUI/TableToolbar";
import ConfirmationDialog from "../Common/ConfirmationDialog";
import { Product } from "../API/Models/Product/Product";
import { deleteProduct, getProducts } from "../API/Endpoints/Product";
import { ProductQuery } from "../API/Models/Product/ProductQuery";
import ProductModal from "../Components/ProductModal";

const Products = () => {
  const [openModal, setOpenModal] = useState<boolean>(false);
  const [openDialog, setOpenDialog] = useState<boolean>(false);

  const [pageIndex, setPageIndex] = useState<number>(0);
  const [itemCount, setItemCount] = useState<number>(1);
  const [pageSize, setPageSize] = useState<number>(20);

  const [refresh, setRefresh] = useState<boolean>(false);
  const [products, setProducts] = useState<Product[]>();
  const [editedProduct, setEditedProduct] = useState<Product | null>(
    null
  );

  const getProductsFromApi = useCallback(() => {
    const productQuery: ProductQuery = {
      pageNumber: pageIndex + 1,
      pageSize: pageSize,
    };

    getProducts(productQuery).then((data) => {
      setRefresh(false);
      setProducts(data.items);
      setItemCount(data.totalCount);
    });
  }, [pageIndex, pageSize]);

  useEffect(() => {
    getProductsFromApi();
  }, [getProductsFromApi, pageIndex]);

  useEffect(() => {
    if (refresh) {
      getProductsFromApi();
    }
  }, [getProductsFromApi, refresh]);

  const handleDelete = () => {
    if (editedProduct) {
      deleteProduct(editedProduct.id).then(() => {
        getProductsFromApi();
        setEditedProduct(null);
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
      field: "code",
      headerName: "Kod",
      flex: 2,
    },
    {
      field: "price",
      headerName: "Price",
      flex: 2,
      type: 'number'
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
              setEditedProduct(
                products?.find((item) => item.id === row.id) ?? null
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
      field: "Usuwanie",
      flex: 1.2,
      renderCell: (row) => {
        return (
          <Button
            color="error"
            variant="contained"
            onClick={() => {
              setEditedProduct(
                products?.find((item) => item.id === row.id) ?? null
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
          Dodaj produkt
        </Button>
      </Grid>
      <div style={{ height: 400, width: "100%" }}>
        <div style={{ display: "flex", height: "100%" }}>
          <div style={{ flexGrow: 1 }}>
            <DataGrid
              rows={products}
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
          setEditedProduct(null);
          setOpenModal(false);
        }}
        open={openModal}
        setRefresh={setRefresh}
        editedProduct={editedProduct}
      />
      <ConfirmationDialog
        open={openDialog}
        handleClose={() => setOpenDialog(false)}
        handleConfirm={handleDelete}
      />
    </>
  );
};

export default Products;
