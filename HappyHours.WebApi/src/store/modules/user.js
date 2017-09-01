
import Vue from 'vue';

import storageManager from '../../storageManager';
import { router } from '../../main';

const state = {

  firstName: '',
  lastName: '',
  logged: false,
  isLoadingSignin: false,
  fetchUserInfo: false

};

const getters = {

  firstName(state) {
    return state.firstName;
  },

  lastName(state) {
    return state.lastName;
  },

  fullName(state) {
    return `${state.firstName} ${state.lastName}`;
  },

  logged(state) {
    return state.logged;
  },

  isLoadingSignin(state) {
    return state.isLoadingSignin;
  },

  fetchUserInfo(state) {
    return state.fetchUserInfo;
  }

};

const mutations = {

  fetchUserDetails(state, payload) {

    if (payload && payload.startup)
      state.fetchUserInfo = true;

    Vue.http.get('api/UserInformation').then((response) => {

      return response.json();

    }).then((data) => {

      state.firstName = data.FirstName;
      state.lastName = data.LastName;

      state.logged  = true;
      state.isLoadingSignin = false;

      state.fetchUserInfo = false;

      // push the user to the main page after the signin
      router.push('/');

    }, (error) => {

      state.fetchUserInfo = false;

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
