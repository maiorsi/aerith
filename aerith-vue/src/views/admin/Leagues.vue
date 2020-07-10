<template>
  <v-container class="fill-height" fluid>
    <v-row justify="center" no-gutters>
      <v-col>
        <v-data-table
          :headers="headers"
          :items="leagues"
          :items-per-page="10"
          :loading="loading"
          class="elevation-1"
        >
          <template v-slot:top>
            <v-toolbar flat>
              <v-toolbar-title>Leagues</v-toolbar-title>
              <v-divider class="mx-4" inset vertical></v-divider>
              <v-spacer></v-spacer>
              <v-dialog v-model="dialog" max-width="500px">
                <template v-slot:activator="{ on, attrs }">
                  <v-btn
                    color="primary"
                    dark
                    class="mb-2"
                    v-bind="attrs"
                    v-on="on"
                    >New Item</v-btn
                  >
                </template>
                <v-card>
                  <v-card-title>
                    <span class="headline">{{ formTitle }}</span>
                  </v-card-title>
                  <v-card-text>
                    <v-container>
                      <v-row>
                        <v-col>
                           <v-text-field
                            v-model="editedItem.name"
                            label="League name"
                          ></v-text-field>
                        </v-col>
                      </v-row>
                    </v-container>
                  </v-card-text>
                  <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="blue darken-1" text @click="close">Cancel</v-btn>
                    <v-btn color="blue darken-1" :loading="dialogLoading" text @click="save">Save</v-btn>
                  </v-card-actions>
                </v-card>
              </v-dialog>
            </v-toolbar>
          </template>
          <template v-slot:item.createdDate="{ item }">
            <span>{{moment(item.createdDate).format()}}</span>
          </template>
          <template v-slot:item.actions="{ item }">
            <v-icon small class="mr-2" @click="editItem(item)">
              fa-pencil-alt
            </v-icon>
            <v-icon small @click="deleteItem(item)" color="red">
              fa-trash-alt
            </v-icon>
          </template>
        </v-data-table>
      </v-col>
    </v-row>
  </v-container>
</template>

<style lang="scss" scoped>
  button.v-icon {
    padding-right: 0.5rem;
  }
</style>

<script lang="ts">
import Vue from "vue";
import League from "../../models/league.interface";
import Patch from "../../models/meta/patch.interface";
import { LeagueServiceInstance } from "../../services/league.service";
import { AxiosResponse } from "axios";
import _ from "lodash";
import moment from "moment";

export default Vue.extend({
  computed: {
    formTitle: function(): string {
      return this.editedIndex === -1 ? "New Item" : "Edit Item";
    },
  },
  data: function() {
    return {
      defaultItem: {
        name: ""
      } as League,
      dialog: false,
      dialogLoading: false,
      editedIndex: -1,
      editedItem: {
        name: ''
      } as League,
      headers: [
        {
          text: "ID",
          align: "start",
          sortable: true,
          value: "id"
        },
        { text: "Name", value: "name" },
        { text: "Code", value: "code.name" },
        { text: "Created", value: "createdDate" },
        { text: "Actions", value: "actions", sortable: false }
      ],
      loading: false,
      leagues: [] as League[]
    };
  },
  name: "Leagues",
  methods: {
    close() {
      this.dialog = false;
      this.$nextTick(() => {
        this.editedItem = { ...this.defaultItem };
        this.editedIndex = -1;
        this.dialogLoading = false;
      });
    },
    deleteItem(league: League) {
      const index = _.findIndex(this.leagues, ['id', league.id]);
      confirm("Are you sure you want to delete this item?") &&
        LeagueServiceInstance.delete(league.id)
          .then(() => {
            this.leagues.splice(index, 1);
          })
          .catch((error: Error) => {
            console.error(error);
          });
    },
    editItem(league: League) {
      this.editedIndex = _.findIndex(this.leagues, ['id', league.id]);
      this.editedItem = { ...league };
      this.dialog = true;
    },
    moment(date: Date) {
      return moment(date);
    },
    save() {
      this.dialogLoading = true;

      if (this.editedIndex > -1) {
        const patches = [] as Patch[];

        patches.push({
          op: "replace",
          path: "/name",
          value: this.editedItem.name
        } as Patch);

        LeagueServiceInstance.patch(this.editedItem.id, patches)
          .then((response: AxiosResponse<League>) => {
            Object.assign(this.leagues[this.editedIndex], response.data as League)
            this.close();
          })
          .catch((error: Error) => {
            console.error(error);
            this.close();
          });
      } else {
        LeagueServiceInstance.post(this.editedItem as League)
          .then((response: AxiosResponse<League>) => {
            this.leagues.push(response.data as League);
            this.close();
          })
          .catch((error: Error) => {
            console.error(error);
            this.close();
          });
      }
    }
  },
  mounted: function() {
    this.loading = true;
    LeagueServiceInstance.get()
      .then((response: AxiosResponse<League[]>) => {
        this.leagues = response.data as League[];
        this.loading = false;
      })
      .catch(() => {
        this.loading = false;
      });
  },
  watch: {
    dialog(val) {
      val || this.close();
    }
  }
});
</script>
