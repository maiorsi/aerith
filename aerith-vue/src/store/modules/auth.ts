import { GetterTree, ActionTree, MutationTree } from "vuex";
import Credentials from "@/models/credentials.interface";
import { AuthServiceInstance } from "@/services/auth.service";
import { EventBus } from "@/event-bus";
import Token from "@/models/token.interface";
import { AxiosResponse } from "axios";

export interface AuthState {
  token: string;
  status: string;
}

const state: AuthState = {
  token: localStorage.getItem("auth-token") || "",
  status: ""
};

const getters: GetterTree<AuthState, any> = {
  isAuthenticated: (state: AuthState) => !!state.token,
  authStatus: (state: AuthState) => state.status,
  authToken: (state: AuthState) => state.token
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
          commit("authSuccess", response);
          EventBus.$emit("authenticated");
          resolve(response);
        })
        .catch((exception: Error) => {
          commit("authError", exception);
          localStorage.removeItem("auth-token");
          reject(exception);
        });
    });
  }
};

const mutations: MutationTree<AuthState> = {
  authRequest: (authState: AuthState) => {
    authState.status = "Authenticating...";
  },
  authSuccess: (authState: AuthState, authToken: string) => {
    authState.status = "Successfully authenticated.";
    authState.token = authToken;
  },
  authError: (authState: AuthState) => {
    authState.status = "Authentication failed.";
  },
  authLogout: (authState: AuthState) => {
    authState.token = "";
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
