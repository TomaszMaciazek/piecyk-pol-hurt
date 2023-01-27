import { Client } from '../Client/Client';

const controllerName = 'Product';

const getSendPoints = async (): Promise<string[]> => {
  return Client('GET', `${controllerName}/all`);
};
export { getSendPoints };
