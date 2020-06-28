<template>
  <v-app id="inspire">
    <v-navigation-drawer v-model="drawer" v-if="isAuthenticated" app clipped>
      <v-list>
        <v-list-item link to="/">
          <v-list-item-icon>
            <v-icon>mdi-home</v-icon>
          </v-list-item-icon>
          <v-list-item-content>
            <v-list-item-title>Home</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <template v-if="hasRole('Administrators')">
          <v-list-group
            v-for="item in adminItems"
            :key="item.title"
            v-model="item.active"
            :prepend-icon="item.icon"
            no-action
          >
            <template v-slot:activator>
              <v-list-item-content>
                <v-list-item-title v-text="item.title"></v-list-item-title>
              </v-list-item-content>
            </template>

            <v-list-item
              v-for="subItem in item.items"
              :key="subItem.title"
              link
              :to="subItem.route"
            >
              <v-list-item-content>
                <v-list-item-title v-text="subItem.title"></v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </v-list-group>
        </template>
      </v-list>
    </v-navigation-drawer>

    <v-app-bar app clipped-left v-if="isAuthenticated">
      <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
      <v-toolbar-title>Aerith</v-toolbar-title>
    </v-app-bar>

    <v-content>
      <router-view />
    </v-content>

    <v-footer app>
      <span>&copy; 2020</span>
    </v-footer>
  </v-app>
</template>

<script lang="ts">
import { Vue } from "vue-property-decorator";
import { EventBus } from "./event-bus";
import { mapGetters } from "vuex";
import item from "./models/item.interface";

export default Vue.extend({
  computed: {
    ...mapGetters("auth", ["isAuthenticated"]),
    ...mapGetters("user", ["hasRole"])
  },
  created() {
    this.$vuetify.theme.dark = true;

    EventBus.$on("authenticated", (payLoad: any) => {
      console.log({
        message: "'authenticated' message received...",
        payLoad: payLoad
      });
    });

    const isAuthenticated = this.$store.getters[
      "auth/isAuthenticated"
    ] as boolean;

    if (isAuthenticated) {
      // Already authenticated. We need to now fetch profile
      this.$store.dispatch("user/userRequest", null, { root: true });
    }
  },
  data: function() {
    return {
      drawer: false,
      adminItems: [
        {
          icon: "fa-cogs",
          title: "Administration",
          items: [
            { title: "Codes", route: "/admin/codes" },
            { title: "Leagues", route: "/admin/leagues" },
            { title: "Tournaments", route: "/admin/tournaments" },
            { title: "Teams", route: "/admin/teams" },
            { title: "Competitions", route: "/admin/competitions" },
            { title: "Groups", route: "/admin/groups" },
            { title: "Users", route: "/admin/users" }
          ]
        }
      ] as item[]
    };
  },
  destroyed: () => {
    EventBus.$off("authenticated");
  },
  name: "App",
  props: {
    source: String
  }
});
</script>
