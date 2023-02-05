import { CreateOrderLineCommand } from "./CreateOrderLineCommand";

export interface CreateOrderCommand {
  lines: CreateOrderLineCommand[];
  sendPointId: number;
  requestedReceptionDate: Date;
}
