
import Vue from 'vue';

import storageManager from '../../storageManager';
import { router } from '../../main';

const state = {

  firstName: '',
  lastName: '',
  logged: false,
  isLoadingSignin: false

};

const getters = {

  firstName(state) {
    return state.firstName;
  },

  lastName(state) {
    return state.lastName;
  },

  logged(state) {
    return state.logged;
  },

  isLoadingSignin(state) {
    return state.isLoadingSignin;
  }

};

const mutations = {

  fetchUserDetails(state, payload) {

    Vue.http.get('api/UserInformation').then((data) => {

      state.logged  = true;
      state.isLoadingSignin = false;

      // push the user to the main page after the signin
      router.push('/');

    }, (error) => {

      state.isLoadingSignin = false;

    });

  },

  setIsLoadingSignin(state, isLoading) {
    state.isLoadingSignin = isLoading;
  },

  setLogged(state, payload) {
    state.logged = payload;
  },

  logout(state, payload) {

    Vue.http.post('api/Logout').then(() => {

      storageManager.clearTokenBearer();
      state.logged = false;

    }, (error) => {

      console.log(error);

    });

  }

};

const actions = {

  fetchUserDetails(context, payload) {
    context.commit('fetchUserDetails', payload);
  },

  setIsLoadingSignin(context, payload) {
    context.commit('setIsLoadingSignin', payload);
  },

  setLogged(context, payload) {
    context.commit('setLogged', payload);
  },

  logout(context, payload) {
    context.commit('logout', payload);
  }

};

export default {
  state,
  getters,
  mutations,
  actions
};
