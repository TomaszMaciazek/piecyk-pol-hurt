import { Client } from "../Client/Client";
import { PaginatedList } from "../Models/PaginatedList";
import { CreateProductCommand } from "../Models/Product/CreateProductCommand";
import { Product } from "../Models/Product/Product";
import { ProductQuery } from "../Models/Product/ProductQuery";
import { ProductSendPointListItemDto } from "../Models/Product/ProductSendPointListItemDto";
import { UpdateProductCommand } from "../Models/Product/UpdateProductCommand";

const controllerName = "Product";

const getProducts = async (
  params: ProductQuery
): Promise<PaginatedList<Product>> => {
  return Client("GET", controllerName, {}, params);
};

const addProduct = async (body: CreateProductCommand): Promise<null> => {
  return Client("POST", controllerName, { body });
};

const updateProduct = async (body: UpdateProductCommand): Promise<null> => {
  return Client("PUT", controllerName, { body });
};

const deleteProduct = async (id: number): Promise<null> => {
  return Client("DELETE", `${controllerName}/${id}`);
};

const getTodaysProductsFromSendPoint = async (
  id: number
): Promise<ProductSendPointListItemDto[]> => {
  return Client("GET", `${controllerName}/sendPoint/${id}`);
};

export {
  getProducts,
  addProduct,
  deleteProduct,
  updateProduct,
  getTodaysProductsFromSendPoint,
};
