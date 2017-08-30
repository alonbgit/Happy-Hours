
const tokenKey = 'HH_TOKEN';

export default {

  getTokenBearer() {

    var token = localStorage.getItem(tokenKey);
    return token;

  },

  setTokenBearer(token) {

    localStorage.setItem(tokenKey, token);

  },

  clearTokenBearer() {

    localStorage.removeItem(tokenKey);

  }

}
