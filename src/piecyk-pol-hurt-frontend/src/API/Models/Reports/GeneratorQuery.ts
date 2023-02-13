import { GeneratorParamValue } from "./GeneratorParamValue";

export interface GeneratorQuery {
  reportid: number;
  maxRows?: number;
  ParamsValues: GeneratorParamValue[];
}
