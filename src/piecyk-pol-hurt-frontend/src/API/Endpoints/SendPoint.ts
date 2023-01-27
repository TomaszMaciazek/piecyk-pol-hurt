import { Client } from '../Client/Client';

const controllerName = 'SendPoint';

const getSendPoints = async (): Promise<string[]> => {
  return Client('GET', `${controllerName}/all`);
};
export { getSendPoints };
