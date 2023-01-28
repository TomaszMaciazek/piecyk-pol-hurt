/* eslint-disable @typescript-eslint/no-explicit-any */
import axios, { Method } from 'axios';
import data from '../../config.json';

const applicationBase = data.BASE_URL + '/api';

const Client = async (method: Method, endpoint: string, { body }: any = {}, params?: any): Promise<any> => {
  const requestResult = await axios({
    method: method,
    url: `${applicationBase}/${endpoint}`,
    headers: {
      'Content-Type': 'application/json',
      Accept: 'application/json',
    },
    data: JSON.stringify(body),
    responseType: 'json',
    params: new URLSearchParams(params as any),
  });

  return requestResult.data;
};
export { Client };
