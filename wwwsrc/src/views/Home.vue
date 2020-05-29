<template>
  <div class="home container-fluid">
    <div class="row">
      
    <CreateKeep v-show="this.$auth.user"></CreateKeep>
    </div>
    <hr>
    <div class="card-columns">
    <Keep v-for="publicKeep in publicKeeps" :key="publicKeep.id" :keepData="publicKeep"/>
    </div>

  </div>
</template>

<script>
import Keep from "../components/Keep.vue"
import CreateKeep from "../components/CreateKeep.vue"
export default {
  name: "home",
  computed: {
    user() {
      return this.$store.state.user;
    },
    publicKeeps(){return this.$store.state.publicKeeps},
  },
  mounted() {
    this.$store.dispatch("getPublicKeeps")
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    }
  },
  components:{
    Keep,CreateKeep
    }
};
</script>