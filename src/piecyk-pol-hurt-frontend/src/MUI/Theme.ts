import { createTheme } from "@mui/material";
import { plPL } from "@mui/material/locale";

export const theme = createTheme(
  {
    components: {
      MuiButton: {
        defaultProps: {
          variant: "contained",
        },
      },
    },
  },
  plPL
);
