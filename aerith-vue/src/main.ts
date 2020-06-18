import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import vuetify from "./plugins/vuetify";
import axios, { AxiosRequestConfig, AxiosResponse } from "axios";

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

axios.interceptors.response.use(
  (response: AxiosResponse) => response,
  (error: any) => {
    // Return any error which is not due to authentication back to the calling service
    if (error.response.status !== 401) {
      return new Promise((resolve, reject) => {
        reject(error);
      });
    }

    const originalRequest = error.config;

    store
      .dispatch("auth/refresh")
      .then(() => {
        axios(originalRequest);
      })
      .catch(() => {
        console.log("Failed to refresh token!");
      });
  }
);

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount("#app");
