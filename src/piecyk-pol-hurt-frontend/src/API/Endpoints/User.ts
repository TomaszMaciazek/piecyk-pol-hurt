import { Client } from '../Client/Client';

const controllerName = 'User';

const getPermissions = async (): Promise<string[]> => {
  return Client('GET', controllerName);
};

export { getPermissions };
