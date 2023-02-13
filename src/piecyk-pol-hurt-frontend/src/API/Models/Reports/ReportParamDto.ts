import { ParamType } from "./ParamType";

export interface ReportParamDto {
  name: string;
  caption: string;
  type: ParamType;
  isRequired: boolean;
  availableValues: ReportParamValueDto[];
}

interface ReportParamValueDto {
  label: string;
  value: string;
}
