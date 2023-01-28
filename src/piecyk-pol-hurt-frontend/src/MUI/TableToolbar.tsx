import {
  GridToolbarContainer,
  GridToolbarColumnsButton,
  GridToolbarExport,
  GridToolbarDensitySelector,
} from "@mui/x-data-grid";

export const TableToolbar = () => {
  return (
    <GridToolbarContainer >
      <GridToolbarColumnsButton/>
      <GridToolbarDensitySelector/>
      <GridToolbarExport/>
    </GridToolbarContainer>
  );
};
