import { Modal, Box, Button, TextField, Select, MenuItem } from "@mui/material";
import { DatePicker, DesktopDatePicker } from "@mui/x-date-pickers";
import { useEffect, useState } from "react";
import {
  generateReport,
  getReportGenerationPageData,
} from "../API/Endpoints/Report";
import { GeneratorParamValue } from "../API/Models/Reports/GeneratorParamValue";
import { GeneratorQuery } from "../API/Models/Reports/GeneratorQuery";
import { ParamType } from "../API/Models/Reports/ParamType";
import { ReportGenerationPageDto } from "../API/Models/Reports/ReportGenerationPageDto";
import { ModalStyle } from "../Styles/ModalStyles";

interface IGenerateReportModal {
  open: boolean;
  handleClose: () => void;
  editedReportId: number | undefined;
}

const GenerateReportModal = ({
  open,
  handleClose,
  editedReportId,
}: IGenerateReportModal) => {
  const [paramValues, setParamValues] = useState<GeneratorParamValue[]>([]);
  const [maxRows, setMaxRows] = useState<number>(10000);
  const [generationPage, setGenerationPage] =
    useState<ReportGenerationPageDto>();

  const handleGenerate = () => {
    if (!editedReportId) {
      return;
    }

    const query: GeneratorQuery = {
      reportid: editedReportId,
      maxRows: maxRows,
      ParamsValues: [
        {
          key: "dateFrom",
          values: ["2023-02-13"],
        },
      ],
    };

    generateReport(query).then((fileBlob) => {
      const link = document.createElement("a");
      link.href = window.URL.createObjectURL(fileBlob);
      link.download = `raport_$${new Date().toLocaleDateString()}.xlsx`;
      link.click();
    });
  };

  const onClose = () => {
    handleClose();
  };

  useEffect(() => {
    if (editedReportId) {
      getReportGenerationPageData(editedReportId).then((data) => {
        setGenerationPage(data);
      });
    }
  }, [editedReportId]);

  return (
    <Modal open={open} onClose={onClose}>
      <Box sx={{ ...ModalStyle, width: 450, height: 600 }}>
        <h2>Generowanie raportu</h2>
        <Box
          sx={{
            "& .MuiTextField-root": { mb: 2, width: "100%" },
          }}
        >
          <TextField
            label="Liczba wierszy"
            type="number"
            value={maxRows}
            onChange={(e) => setMaxRows(parseInt(e.target.value))}
          />
          {generationPage?.params.map((item) => {
            const inputs: any[] = [];
            if (item.type === ParamType.Dis || item.type === ParamType.Lst) {
              inputs.push(
                <>
                  <span>{item.caption}</span>
                  <Select
                    sx={{ mb: 2 }}
                    required={item.isRequired}
                    fullWidth
                    onChange={(e) => console.log(e.target.value)}
                  >
                    {item.availableValues.map((item) => (
                      <MenuItem key={item.value} value={item.value}>
                        {item.label}
                      </MenuItem>
                    ))}
                  </Select>
                </>
              );
            } else if (item.type === ParamType.Lik) {
              inputs.push(<TextField label={item.name} />);
            } else if (item.type === ParamType.Dat) {
              inputs.push(
                <DesktopDatePicker
                  label="Data"
                  value={new Date()}
                  onChange={() => undefined}
                  renderInput={(params: any) => (
                    <TextField sx={{ mb: 2 }} {...params} />
                  )}
                />
              );
            }

            return inputs;
          })}

          <Button variant="contained" onClick={handleGenerate} sx={{ mt: 1 }}>
            Generuj
          </Button>
        </Box>
      </Box>
    </Modal>
  );
};

export default GenerateReportModal;
