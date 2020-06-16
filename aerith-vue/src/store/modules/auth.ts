import { GetterTree, ActionTree, MutationTree } from "vuex";
import Credentials from "@/models/credentials.interface";
import { AuthServiceInstance } from "@/services/auth.service";
import { EventBus } from "@/event-bus";
import Token from "@/models/token.interface";
import { AxiosResponse } from "axios";

export interface AuthState {
  token: string;
  status: string;
  loading: boolean;
}

const state: AuthState = {
  token: localStorage.getItem("auth-token") || "",
  status: "",
  loading: false
};

const getters: GetterTree<AuthState, any> = {
  isAuthenticated: (state: AuthState) => !!state.token,
  authStatus: (state: AuthState) => state.status,
  authToken: (state: AuthState) => state.token,
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
          commit("authSuccess", response.data.token);
          EventBus.$emit("authenticated");
          dispatch("user/userRequest", null, { root: true });
          resolve(response);
        })
        .catch((exception: Error) => {
          commit("authError", exception);
          localStorage.removeItem("auth-token");
          reject(exception);
        });
    });
  },
  logout({ dispatch, commit }: { dispatch: any; commit: any }) {
    return new Promise(resolve => {
      commit("authLogout");
      localStorage.removeItem("auth-token");
      resolve();
    });
  }
};

const mutations: MutationTree<AuthState> = {
  authRequest: (authState: AuthState) => {
    authState.status = "Authenticating...";
    authState.loading = true;
  },
  authSuccess: (authState: AuthState, authToken: string) => {
    authState.status = "Successfully authenticated.";
    authState.token = authToken;
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
