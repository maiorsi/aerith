import { GetterTree, ActionTree, MutationTree } from "vuex";
import Credentials from "@/models/credentials.interface";
import { AuthServiceInstance } from "@/services/auth.service";
import { EventBus } from "@/event-bus";
import Token from "@/models/token.interface";
import { AxiosResponse } from "axios";
import Profile from "@/models/profile.interface";
import { ProfileServiceInstance } from "@/services/profile.service";

export interface UserState {
  profile: Profile | null;
  loading: boolean;
  status: string;
}

const state: UserState = {
  profile: null,
  loading: false,
  status: ""
};

const getters: GetterTree<UserState, any> = {
  hasRole: (state: UserState) => (role: string) => {
    return state.profile?.roles.indexOf(role) !== -1;
  },
  profile: (state: UserState) => state.profile,
  profileStatus: (state: UserState) => state.status,
  profileLoading: (state: UserState) => state.loading
};

const actions: ActionTree<UserState, any> = {
  userRequest: ({ commit, dispatch }: { commit: any; dispatch: any }) => {
    commit("userRequest");
    ProfileServiceInstance.get()
      .then((response: AxiosResponse<Profile>) => {
        commit("userSuccess", response.data as Profile);
      })
      .catch((exception: Error) => {
        commit("userError");
        dispatch("auth/authLogout", null, { root: true });
      });
  }
};

const mutations: MutationTree<UserState> = {
  userRequest: (userState: UserState) => {
    userState.status = "Fetching user profile...";
  },
  userSuccess: (userState: UserState, profile: Profile) => {
    userState.status = "Successfully fetched user profile.";
    userState.profile = profile;
  },
  userError: (userState: any) => {
    userState.status = "User Profile Fetch Failed!";
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
