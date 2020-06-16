<template lang="pug">
  div.home
    img(alt="Vue Logo",src="../assets/logo.png")
</template>

<script lang="ts">
import Vue from "vue";
import { mapGetters } from "vuex";

export default Vue.extend({
  name: "Home",
  computed: {
    ...mapGetters("user", ["profile"])
  }
});
</script>
