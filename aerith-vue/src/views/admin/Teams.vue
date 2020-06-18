<template lang="pug">
  div 
    p testing
</template>

<script lang="ts">
import Vue from "vue";
import Team from "../../models/team.interface";
import { TeamServiceInstance } from "../../services/team.service";
import { AxiosResponse } from "axios";

export default Vue.extend({
  data: () => ({
    teams: [] as string[]
  }),
  name: "Teams",
  mounted: () => {
    TeamServiceInstance.get().then((response: AxiosResponse<Team>) => {
      console.log(response);
    });
  }
});
</script>
