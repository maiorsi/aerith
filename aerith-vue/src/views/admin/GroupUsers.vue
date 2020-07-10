<template>
  <v-container class="fill-height" fluid>
    <v-row justify="center" no-gutters>
      <v-col>
        <v-data-table
          :headers="headers"
          :items="groupUsers"
          :items-per-page="10"
          :loading="loading"
          class="elevation-1"
        >
          <template v-slot:top>
            <v-toolbar flat>
              <v-toolbar-title>Group Users</v-toolbar-title>
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
                            label="GroupUser name"
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
import GroupUser from "../../models/group.interface";
import Patch from "../../models/meta/patch.interface";
import { GroupUserServiceInstance } from "../../services/groupUser.service";
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
      } as GroupUser,
      dialog: false,
      dialogLoading: false,
      editedIndex: -1,
      editedItem: {
        name: ''
      } as GroupUser,
      headers: [
        {
          text: "ID",
          align: "start",
          sortable: true,
          value: "id"
        },
        { text: "Name", value: "name" },
        { text: "Created", value: "createdDate" },
        { text: "Actions", value: "actions", sortable: false }
      ],
      loading: false,
      groupUsers: [] as GroupUser[]
    };
  },
  name: "GroupUsers",
  methods: {
    close() {
      this.dialog = false;
      this.$nextTick(() => {
        this.editedItem = { ...this.defaultItem };
        this.editedIndex = -1;
        this.dialogLoading = false;
      });
    },
    deleteItem(group: GroupUser) {
      const index = _.findIndex(this.groupUsers, ['id', group.id]);
      confirm("Are you sure you want to delete this item?") &&
        GroupUserServiceInstance.delete(group.id)
          .then(() => {
            this.groupUsers.splice(index, 1);
          })
          .catch((error: Error) => {
            console.error(error);
          });
    },
    editItem(group: GroupUser) {
      this.editedIndex = _.findIndex(this.groupUsers, ['id', group.id]);
      this.editedItem = { ...group };
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

        GroupUserServiceInstance.patch(this.editedItem.id, patches)
          .then((response: AxiosResponse<GroupUser>) => {
            Object.assign(this.groupUsers[this.editedIndex], response.data as GroupUser)
            this.close();
          })
          .catch((error: Error) => {
            console.error(error);
            this.close();
          });
      } else {
        GroupUserServiceInstance.post(this.editedItem as GroupUser)
          .then((response: AxiosResponse<GroupUser>) => {
            this.groupUsers.push(response.data as GroupUser);
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
    GroupUserServiceInstance.get()
      .then((response: AxiosResponse<GroupUser[]>) => {
        this.groupUsers = response.data as GroupUser[];
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
