<template>
  <v-container fill-height fluid>
    <v-row align="center" justify="center">
      <v-col>
        <v-data-table
          :headers="headers"
          :items="codes"
          :items-per-page="10"
          :loading="loading"
          class="elevation-1"
        >
        </v-data-table>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import Code from "../../models/code.interface";
import { CodeServiceInstance } from "../../services/code.service";
import { AxiosResponse } from "axios";

export default Vue.extend({
  data: function() {
    return {
      headers: [
        {
          text: "ID",
          align: "start",
          sortable: true,
          value: "id"
        },
        { text: "Name", value: "name" },
        { text: "Created", value: "createdDate" }
      ],
      loading: false,
      codes: [] as Code[]
    };
  },
  name: "Codes",
  mounted: function() {
    this.loading = true;
    CodeServiceInstance.get()
      .then((response: AxiosResponse<Code[]>) => {
        this.codes = response.data as Code[];
        this.loading = false;
      })
      .catch(() => {
        this.loading = false;
      });
  }
});
</script>
