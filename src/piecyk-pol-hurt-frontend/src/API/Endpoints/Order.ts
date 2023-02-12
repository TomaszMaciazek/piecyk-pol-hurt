import { Client } from "../Client/Client";
<<<<<<< HEAD
import { Order } from "../Models/Order/Order";
import { OrderQuery } from "../Models/Order/OrderQuery";
import { PaginatedList } from "../Models/PaginatedList";

const controllerName = "Order";

const getOrders = async (
    params: OrderQuery
  ): Promise<PaginatedList<Order>> => {
    return Client("GET", controllerName, {}, params);
  };

  const addOrder = async (body: any): Promise<null> => {
    return Client("POST", controllerName, { body });
  };
  
  const updateOrder = async (body: any): Promise<null> => {
    return Client("PUT", controllerName, { body });
  };
  
  const deleteOrder = async (id: number): Promise<null> => {
    return Client("DELETE", `${controllerName}/${id}`);
  };

  export { getOrders, addOrder, updateOrder, deleteOrder };
=======
import { CreateOrderCommand } from "../Models/Order/CreateOrderCommand";

const controllerName = "Order";

const createOrder = async (body: CreateOrderCommand): Promise<null> => {
  return Client("POST", controllerName, { body });
};

export { createOrder };
>>>>>>> main
