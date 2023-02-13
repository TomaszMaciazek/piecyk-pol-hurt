import { ReportParamDto } from "./ReportParamDto";

export interface ReportGenerationPageDto {
  reportId: number;
  title: string;
  description: string;
  maxRow: number;
  params: Array<ReportParamDto>;
}
