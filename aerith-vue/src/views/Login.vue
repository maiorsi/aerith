<template lang="pug">
  <v-container class="fill-height" fluid>
    <v-row justify="center" no-gutters v-if="authLoading">
      <v-col md="auto">
        <v-progress-circular :size="100" color="primary" indeterminate></v-progress-circular>
      </v-col>
    </v-row>
    <v-row justify="center" no-gutters v-if="!authLoading">
      <v-col md="auto">      
        <v-card id="login-card" class="elevation-12">
          <v-toolbar color="primary" dark flat>
            <v-toolbar-title>Login form</v-toolbar-title>
            <v-spacer></v-spacer>
          </v-toolbar>
          <v-card-text>
            <v-form>
              <v-text-field id="login" label="Login" name="login" prepend-icon="mdi-account" type="text" v-model="credentials.username" />
              <v-text-field id="password" label="Password" name="password" prepend-icon="mdi-lock" type="password" v-model="credentials.password" />
            </v-form>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn @click="login" color="primary">Login</v-btn>
          </v-card-actions>
          <v-row justify="center" no-gutters>
            <v-col md="auto">
              div#google-img(@click="googleLogin")
            </v-col>
          </v-row>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import Credentials from "../models/credentials.interface";
import { mapGetters } from "vuex";
export default Vue.extend({
  computed: {
    ...mapGetters("auth", ["isAuthenticated"]),
    ...mapGetters("auth", ["authLoading"])
  },
  data: () => ({
    credentials: {
      username: "",
      password: ""
    } as Credentials
  }),
  methods: {
    login() {
      this.$store.dispatch("auth/authenticate", this.credentials).then(() => {
        const urlParams = new URLSearchParams(window.location.search);
        const redirect = urlParams.get("redirect") || "/home";

        this.$router.push(redirect);
      });
    },
    googleLogin() {
      console.log("Google Login!");
    }
  },
  name: "Login"
});
</script>

<style lang="scss" scoped>
#login-card {
  width: 20rem;
}
#google-img {
  height: 50px;
  width: 200px;
  background: url("~@/assets/btn_google_signin_dark_normal_web@2x.png")
    no-repeat;
  background-size: 100%;
  display: inline-block;
  cursor: pointer;
}
#google-img:hover {
  height: 50px;
  width: 200px;
  background: url("~@/assets/btn_google_signin_dark_focus_web@2x.png") no-repeat;
  background-size: 100%;
}
</style>
