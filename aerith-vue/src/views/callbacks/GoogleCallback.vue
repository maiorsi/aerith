<template lang="pug">
    div
        <v-progress-circular indeterminate color="primary" />
</template>
<script lang="ts">
import Vue from "vue";
export default Vue.extend({
  mounted() {
    console.log(this.$route);
  }
});
</script>
