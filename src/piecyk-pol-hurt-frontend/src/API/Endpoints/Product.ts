import { Client } from '../Client/Client';

const controllerName = 'Product';

const getProducts = async (): Promise<string[]> => {
  return Client('GET', `${controllerName}/all`);
};
export { getProducts };
