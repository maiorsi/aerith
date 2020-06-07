import Vue from "vue";
import Vuex from "vuex";
import Auth from "./modules/auth";

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
    }
  }
});
