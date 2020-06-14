import Vue from "vue";
import Vuex from "vuex";
import Auth from "./modules/auth";
import User from "./modules/user";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {},
  mutations: {},
  actions: {},
  modules: {
    auth: {
      namespaced: Auth.namespaced,
      state: Auth.state,
      mutations: Auth.mutations,
      getters: Auth.getters,
      actions: Auth.actions
    },
    user: {
      namespaced: User.namespaced,
      state: User.state,
      mutations: User.mutations,
      getters: User.getters,
      actions: User.actions
    }
  }
});
