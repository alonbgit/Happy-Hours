
import Vue from 'vue';

const state = {

  firstName: '',
  lastName: ''

};

const getters = {

  firstName(state) {
    return state.firstName;
  },

  lastName(state) {
    return state.lastName;
  }

};

const mutations = {

  fetchUserDetails(state, payload) {

    debugger;

    Vue.http.get('api/UserInformation').then((data) => {
      debugger;
    });

  }

};

const actions = {

  fetchUserDetails(context, payload) {
    context.commit('fetchUserDetails', payload);
  }

};

export default {
  state,
  getters,
  mutations,
  actions
};
