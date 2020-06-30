<template>
  <v-container class="fill-height" fluid>
    <v-row justify="center" no-gutters>
      <v-col>
        <v-data-table
          :headers="headers"
          :items="teams"
          :items-per-page="10"
          :loading="loading"
          class="elevation-1"
        >
          <template v-slot:top>
            <v-toolbar flat>
              <v-toolbar-title>Teams</v-toolbar-title>
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
                            v-model="editedItem.value"
                            label="Team value"
                          ></v-text-field>
                        </v-col>
                        <v-col>
                           <v-text-field
                            v-model="editedItem.name"
                            label="Team name"
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
import Team from "../../models/team.interface";
import Patch from "../../models/meta/patch.interface";
import { TeamServiceInstance } from "../../services/team.service";
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
      } as Team,
      dialog: false,
      dialogLoading: false,
      editedIndex: -1,
      editedItem: {
        name: "",
        value: 0
      } as Team,
      headers: [
        {
          text: "ID",
          align: "start",
          sortable: true,
          value: "id"
        },
        { text: "Value", value: "value" },
        { text: "Name", value: "name" },
        { text: "Created", value: "createdDate" },
        { text: "Actions", value: "actions", sortable: false }
      ],
      loading: false,
      teams: [] as Team[]
    };
  },
  name: "Teams",
  methods: {
    close() {
      this.dialog = false;
      this.$nextTick(() => {
        this.editedItem = { ...this.defaultItem };
        this.editedIndex = -1;
        this.dialogLoading = false;
      });
    },
    deleteItem(team: Team) {
      const index = _.findIndex(this.teams, ['id', team.id]);
      confirm("Are you sure you want to delete this item?") &&
        TeamServiceInstance.delete(team.id)
          .then(() => {
            this.teams.splice(index, 1);
          })
          .catch((error: Error) => {
            console.error(error);
          });
    },
    editItem(team: Team) {
      this.editedIndex = _.findIndex(this.teams, ['id', team.id]);
      this.editedItem = { ...team };
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

        TeamServiceInstance.patch(this.editedItem.id, patches)
          .then((response: AxiosResponse<Team>) => {
            Object.assign(this.teams[this.editedIndex], response.data as Team)
            this.close();
          })
          .catch((error: Error) => {
            console.error(error);
            this.close();
          });
      } else {
        TeamServiceInstance.post(this.editedItem as Team)
          .then((response: AxiosResponse<Team>) => {
            this.teams.push(response.data as Team);
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
    TeamServiceInstance.get()
      .then((response: AxiosResponse<Team[]>) => {
        this.teams = response.data as Team[];
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
