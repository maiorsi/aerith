import { GetterTree, ActionTree, MutationTree } from "vuex";
import Credentials from "@/models/credentials.interface";
import { AuthServiceInstance } from "@/services/auth.service";
import { EventBus } from "@/event-bus";
import Token from "@/models/token.interface";
import { AxiosResponse } from "axios";

export interface AuthState {
  token: string;
  refreshToken: string;
  status: string;
  loading: boolean;
}

const state: AuthState = {
  token: localStorage.getItem("auth-token") || "",
  refreshToken: localStorage.getItem("refresh-token") || "",
  status: "",
  loading: false
};

const getters: GetterTree<AuthState, any> = {
  isAuthenticated: (state: AuthState) => !!state.token,
  authStatus: (state: AuthState) => state.status,
  authToken: (state: AuthState) => state.token,
  authRefreshToken: (state: AuthState) => state.token,
  authLoading: (state: AuthState) => state.loading
};

const actions: ActionTree<AuthState, any> = {
  async authenticate(
    { dispatch, commit }: { dispatch: any; commit: any },
    credentials: Credentials
  ) {
    return new Promise((resolve, reject) => {
      commit("authRequest");
      AuthServiceInstance.login(credentials)
        .then((response: AxiosResponse<Token>) => {
          localStorage.setItem("auth-token", response.data.token);
          localStorage.setItem("refresh-token", response.data.refreshToken);
          commit("authSuccess", {
            token: response.data.token,
            refreshToken: response.data.refreshToken
          });
          EventBus.$emit("authenticated");
          dispatch("user/userRequest", null, { root: true });
          resolve(response);
        })
        .catch((exception: Error) => {
          commit("authError", exception);
          localStorage.removeItem("auth-token");
          localStorage.removeItem("refresh-token");
          reject(exception);
        });
    });
  },
  logout({ dispatch, commit }: { dispatch: any; commit: any }) {
    return new Promise(resolve => {
      commit("authLogout");
      localStorage.removeItem("auth-token");
      localStorage.removeItem("refresh-token");
      resolve();
    });
  },
  refresh({ dispatch, commit }: { dispatch: any; commit: any }) {
    return new Promise((resolve, reject) => {
      const token: Token = {
        token: localStorage.getItem("auth-token") || "",
        refreshToken: localStorage.getItem("refresh-token") || ""
      };

      commit("authRequest");
      AuthServiceInstance.refresh(token)
        .then((response: AxiosResponse<Token>) => {
          localStorage.setItem("auth-token", response.data.token);
          localStorage.setItem("refresh-token", response.data.refreshToken);
          commit("authSuccess", {
            token: response.data.token,
            refreshToken: response.data.refreshToken
          });
          EventBus.$emit("authenticated");
          dispatch("user/userRequest", null, { root: true });
          resolve(response);
        })
        .catch((exception: Error) => {
          commit("authError", exception);
          localStorage.removeItem("auth-token");
          localStorage.removeItem("refresh-token");
          reject(exception);
        });
    });
  }
};

const mutations: MutationTree<AuthState> = {
  authRequest: (authState: AuthState) => {
    authState.status = "Authenticating...";
    authState.loading = true;
  },
  authSuccess: (
    authState: AuthState,
    authToken: { token: string; refreshToken: string }
  ) => {
    authState.status = "Successfully authenticated.";
    authState.token = authToken.token;
    authState.refreshToken = authToken.refreshToken;
    authState.loading = false;
  },
  authError: (authState: AuthState) => {
    authState.status = "Authentication failed!";
    authState.loading = false;
  },
  authLogout: (authState: AuthState) => {
    authState.token = "";
    authState.loading = false;
  }
};

const namespaced = true;

export default {
  namespaced,
  state,
  getters,
  actions,
  mutations
};
