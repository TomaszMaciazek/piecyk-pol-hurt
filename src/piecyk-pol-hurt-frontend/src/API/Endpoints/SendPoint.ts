import { Client } from "../Client/Client";
import { PaginatedList } from "../Models/PaginatedList";
import { CreateSendPointCommand } from "../Models/SendPoint/CreateSendPointCommand";
import { SendPoint } from "../Models/SendPoint/SendPoint";
import { SendPointQuery } from "../Models/SendPoint/SendPointQuery";
import { UpdateSendPointCommand } from "../Models/SendPoint/UpdateSendPointCommand";

const controllerName = "SendPoint";

const getSendPoints = async (params: SendPointQuery): Promise<PaginatedList<SendPoint>> => {
  return Client("GET", controllerName, {}, params);
};

const getActiveSendPoints = async (): Promise<SendPoint[]> => {
  return Client("GET", `${controllerName}/active`);
};

const deleteSendPoint = async (id: number): Promise<boolean> => {
  return Client("DELETE", `${controllerName}/${id}`);
};

const updateSendPoint = async (
  body: UpdateSendPointCommand
): Promise<boolean> => {
  return Client("PUT", controllerName, { body });
};

const addSendPoint = async (
  body: CreateSendPointCommand
): Promise<boolean> => {
  return Client("POST", controllerName, { body });
};

export { getSendPoints, getActiveSendPoints, deleteSendPoint, updateSendPoint, addSendPoint };
