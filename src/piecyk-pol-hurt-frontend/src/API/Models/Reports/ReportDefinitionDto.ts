export interface ReportDefinitionDto {
  id: number;
  title: string;
  group: string;
  description: string;
  maxRow: number;
  created: Date;
  createdBy: string;
  modified: Date;
  isActive?: boolean;
  version: string;
  xmlDefinition: string;
}
