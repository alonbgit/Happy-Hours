
import Vue from 'vue';

import storageManager from '../../storageManager';
import { router } from '../../main';

const state = {

  userInfo: null,
  logged: false,
  isLoadingSignin: false,
  fetchUserInfo: false

};

const getters = {

  userInfo(state) {
    return state.userInfo;
  },

  fullName(state) {
    return `${state.userInfo.FirstName} ${state.userInfo.LastName}`;
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

    Vue.http.post('api/UserInformation').then((response) => {

      return response.json();

    }).then((data) => {

      state.userInfo = data;

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

      router.push('/');

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
