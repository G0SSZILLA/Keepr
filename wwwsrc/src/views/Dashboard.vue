<template>
  <div class="dashboard container-fluid">
    <h1>Vaults</h1>
    <CreateVault class="col-6" v-show="this.$auth.user"></CreateVault>
    <div class="row">
      <div class="col-6">
        <Vault v-for="userVault in userVaults" :key="userVault.id" :vaultData="userVault" />
      </div>
      <div class="col-12">
        <Keep v-for="keep in vaultKeeps" :key="keep.id" :keepData="keep" />
      </div>
    </div>
    <hr />
    <h1>Keeps</h1>
    <div class="card-columns">
      <Keep v-for="userKeep in userKeeps" :key="userKeep.id" :keepData="userKeep" />
    </div>
  </div>
</template>

<script>
import Keep from "../components/Keep.vue";
import Vault from "../components/Vault.vue";
import CreateVault from "../components/CreateVault.vue"
export default {
  name: "dashboard",
  mounted() {
   
    this.$store.dispatch("getUserVaults");
    this.$store.dispatch("getKeepsByUser", this.$auth.user.sub);
  },
  computed: {
 
    userVaults() {
      return this.$store.state.userVaults;
    },
    userKeeps() {
      return this.$store.state.userKeeps;
    },
    vaultKeeps() {
      return this.$store.state.vaultKeeps;
    }
  },
  methods: {
log(){
  console.log(this.$auth)
}

  },
  components: {
    Vault,
    Keep,
    CreateVault
  }
};
</script>

<style></style>
