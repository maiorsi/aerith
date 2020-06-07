<template lang="pug">
    <v-card class="elevation-12" max-width="30rem">
        <v-toolbar color="primary" dark flat>
            <v-toolbar-title>Login form</v-toolbar-title>
            <v-spacer></v-spacer>
        </v-toolbar>
        <v-card-text>
            <v-form>
                <v-text-field label="Login" name="login" prepend-icon="mdi-account" type="text" v-model="credentials.username" />
                <v-text-field id="password" label="Password" name="password" prepend-icon="mdi-lock" type="password" v-model="credentials.password" />
            </v-form>
        </v-card-text>
        <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn @click="login" color="primary">Login</v-btn>
        </v-card-actions>
    </v-card>
</template>

<script lang="ts">
import Vue from "vue";
import Credentials from "../models/credentials.interface";
export default Vue.extend({
  data: () => ({
    credentials: {
      username: "",
      password: ""
    } as Credentials
  }),
  methods: {
    login() {
      this.$store.dispatch("auth/authenticate", this.credentials).then(() => {
          if(this.$route.query.redirect) {
              this.$router.push(this.$route.query.redirect)
          }
          else {
              this.$router.push("/home");
          }
      });
    }
  }
});
</script>
