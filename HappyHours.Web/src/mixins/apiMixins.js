
import { ApiCredentials } from '../ApiData';

export const apiMixins = {

  apiCredentials: null,

  created() {
    this.apiCredentials = ApiCredentials;
  }

};
