import { Client } from "../Client/Client";
import { CreateOrderCommand } from "../Models/Order/CreateOrderCommand";

const controllerName = "Order";

const createOrder = async (body: CreateOrderCommand): Promise<null> => {
  return Client("POST", controllerName, { body });
};

export { createOrder };
