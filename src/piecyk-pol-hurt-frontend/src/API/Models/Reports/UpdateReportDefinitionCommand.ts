export interface UpdateReportDefinitionCommand {
  id: number;
  title: string;
  group: string;
  description: string;
  maxRow: number;
  version: string;
  isActive?: boolean;
  xmlDefinition: string;
}
