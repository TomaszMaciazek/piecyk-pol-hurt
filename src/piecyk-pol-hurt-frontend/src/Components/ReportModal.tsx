import { Modal, Box, Button } from "@mui/material";
import React from "react";
import { FormContainer, TextFieldElement } from "react-hook-form-mui";
import { toast } from "react-toastify";
import { addReport, updateReport } from "../API/Endpoints/Report";
import { CreateReportDefinitionCommand } from "../API/Models/Reports/CreateReportDefinitionCommand";
import { ReportDefinitionDto } from "../API/Models/Reports/ReportDefinitionDto";
import { UpdateReportDefinitionCommand } from "../API/Models/Reports/UpdateReportDefinitionCommand";
import { ModalStyle } from "../Styles/ModalStyles";

interface IReportModal {
  open: boolean;
  handleClose: () => void;
  report: ReportDefinitionDto | undefined;
  setRefresh: React.Dispatch<React.SetStateAction<boolean>>;
}

const ReportModal = ({
  open,
  handleClose,
  report,
  setRefresh,
}: IReportModal) => {
  const submitReport = (
    data: CreateReportDefinitionCommand | UpdateReportDefinitionCommand
  ) => {
    if (report) {
      const request = data as UpdateReportDefinitionCommand;
      request.id = report.id;
      updateReport(request).then((result) => {
        if (result) {
          toast.success("Zaktualizowano raport");
          setRefresh(true);
          handleClose();
        }
      });
    } else {

      addReport(data as CreateReportDefinitionCommand).then((result) => {
        if (result) {
          toast.success("Utworzono raport");
          setRefresh(true);
          handleClose();
        }
      });
    }
  };

  return (
    <Modal
      open={open}
      onClose={handleClose}
      aria-labelledby="parent-modal-title"
      aria-describedby="parent-modal-description"
    >
      <Box sx={{ ...ModalStyle, width: 600, height: "90%", overflow: "auto" }}>
        <h2>{report ? "Edycja" : "Tworzenie raportu"}</h2>
        <FormContainer
          onSuccess={submitReport}
          defaultValues={{
            description: report ? report.description : "",
            group: report ? report.group : "",
            title: report ? report.title : "",
            XmlDefinition: report ? report.xmlDefinition : "",
            maxRow: report ? report.maxRow : 10000,
            version: report ? report.version : 1,
          }}
        >
          <Box
            sx={{
              "& .MuiTextField-root": { mb: 2, width: "100%" },
            }}
          >
            <TextFieldElement name="title" label="Tytuł" required />
            <TextFieldElement name="group" label="Grupa" required />
            <TextFieldElement
              name="description"
              label="Opis"
              required
              multiline
              minRows={3}
            />
            <TextFieldElement
              name="maxRow"
              label="Maksymalna liczba wierszy"
              required
              type="number"
              defaultValue={10000}
            />
            <TextFieldElement
              name="XmlDefinition"
              label="XML"
              required
              multiline
              minRows={5}
            />
            {report !== undefined && (
              <TextFieldElement
                name="version"
                label="Wersja"
                required
                type="number"
              />
            )}

            <Button
              type="submit"
              variant="contained"
              sx={{ mt: 2 }}
              onClick={() => undefined}
            >
              Zapisz
            </Button>
          </Box>
        </FormContainer>
      </Box>
    </Modal>
  );
};

export default ReportModal;
