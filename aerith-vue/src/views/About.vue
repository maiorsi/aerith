<template>
  <div class="about">
    <h1>This is an about page</h1>
    <div id="example-1">
      <button v-on:click="callTest">test</button>
    </div>
    <div class="g-signin2" data-onsuccess="callTest" data-theme="dark"></div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
//import { google } from "googleapis";
export default Vue.extend({
  name: "About",
  methods: {
    callTest() {
      console.log("test");

      /*
      const oauth2Client = new google.auth.OAuth2(
        "612192338081-heaqt6mvei1jd5v7e3ifvka77hl954nv.apps.googleusercontent.com",
        "H9h8lEX0Zy0ig84uJHwhU6MT",
        "http://localhost:8080/"
      );

      // generate a url that asks permissions for Blogger and Google Calendar scopes
      const scopes = ["profile", "email"];

      const url = oauth2Client.generateAuthUrl({
        // 'online' (default) or 'offline' (gets refresh_token)
        access_type: "offline",

        // If you only need one scope you can pass it as a string
        scope: scopes
      });

      console.log(url);
      */

      // Google's OAuth 2.0 endpoint for requesting an access token
      const oauth2Endpoint = "https://accounts.google.com/o/oauth2/v2/auth";

      // Create <form> element to submit parameters to OAuth 2.0 endpoint.
      const form = document.createElement("form");
      form.setAttribute("method", "GET"); // Send as a GET request.
      form.setAttribute("action", oauth2Endpoint);

      // Parameters to pass to OAuth 2.0 endpoint.
      const params = {
        client_id:
          "612192338081-heaqt6mvei1jd5v7e3ifvka77hl954nv.apps.googleusercontent.com",
        redirect_uri: "http://localhost:8080/callback/google",
        response_type: "code",
        scope: "email profile openid",
        include_granted_scopes: "true",
        state: "pass-through value"
      } as any;

      // Add form parameters as hidden input values.
      for (const p in params) {
        const input = document.createElement("input");
        input.setAttribute("type", "hidden");
        input.setAttribute("name", p);
        input.setAttribute("value", params[p]);
        form.appendChild(input);
      }

      // Add form to page and submit it to open the OAuth 2.0 endpoint.
      document.body.appendChild(form);
      form.submit();
    }
  }
});
</script>
