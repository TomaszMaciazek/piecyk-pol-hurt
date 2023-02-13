import { Client } from "../Client/Client";
import { PaginatedList } from "../Models/PaginatedList";
import { CreateReportDefinitionCommand } from "../Models/Reports/CreateReportDefinitionCommand";
import { GeneratorQuery } from "../Models/Reports/GeneratorQuery";
import { ReportDefinitionDto } from "../Models/Reports/ReportDefinitionDto";
import { ReportGenerationPageDto } from "../Models/Reports/ReportGenerationPageDto";
import { ReportListItemDto } from "../Models/Reports/ReportListItemDto";
import { ReportQuery } from "../Models/Reports/ReportQuery";
import { UpdateReportDefinitionCommand } from "../Models/Reports/UpdateReportDefinitionCommand";

const controllerName = "Report";

const getReports = async (
  params: ReportQuery
): Promise<PaginatedList<ReportListItemDto>> => {
  return Client("GET", controllerName, {}, params);
};

const getReportDefinition = async (
  id: number
): Promise<ReportDefinitionDto> => {
  return Client("GET", `${controllerName}/id`);
};

const getReportGenerationPageData = async (
  id: number
): Promise<ReportGenerationPageDto> => {
  return Client("GET", `${controllerName}/page/${id}`);
};

const addReport = async (
  body: CreateReportDefinitionCommand
): Promise<boolean> => {
  return Client("POST", controllerName, { body });
};

const generateReport = async (body: GeneratorQuery): Promise<null> => {
  return Client("POST", `${controllerName}/generate`, { body });
};

const updateReport = async (
  body: UpdateReportDefinitionCommand
): Promise<boolean> => {
  return Client("PUT", controllerName, { body });
};

export {
  generateReport,
  getReportDefinition,
  getReportGenerationPageData,
  updateReport,
  addReport,
  getReports,
};
