export interface GeneratorQuery {
  reportId: number;
  maxRows?: number;
  paramsValue: GeneretorParamValue[];
}

export interface GeneretorParamValue {
  key: string;
  values: string[];
}
