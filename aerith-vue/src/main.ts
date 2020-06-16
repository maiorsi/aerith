import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import vuetify from "./plugins/vuetify";
import axios, { AxiosRequestConfig } from "axios";

Vue.config.productionTip = false;

axios.interceptors.request.use(
  (config: AxiosRequestConfig) => {
    const authToken = store.getters["auth/authToken"];
    if (authToken) {
      config.headers.Authorization = `Bearer ${authToken}`;
    }
    return config;
  },
  (err: Error) => {
    return Promise.reject(err);
  }
);

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount("#app");
