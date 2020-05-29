<template>
  <div class="Keep">
    <div class="card">
      <button
        v-show="this.$auth.user.sub == keepData.userId"
        class="btn-small btn-danger"
        @click="deleteKeep(keepData.id)"
      >&times;</button>
      <!-- <button
        class="border-0 float-right"
        @click="deleteVaultKeep(keepData.id)"
        v-if="this.$route.name == 'dashboard'">
        <i class="fas fa-times-circle" v-if="$auth.isAuthenticated"></i>
      </button>-->
      <!-- <img v-show="keepData.img" class="card-img-top" :src="keepData.img" alt="Error loading image" /> -->
      <div class="card-body">
        <h5 class="card-title">{{keepData.name}}</h5>

        <p class="card-text">{{keepData.description}}</p>
        <i class="fa fa-eye" aria-hidden="true">{{keepData.views}}</i>
        <section></section>
        <i class="fa fa-plus" aria-hidden="true">{{keepData.keeps}}</i>
      </div>

      <!-- NOTE add to vault -->
      <div v-if="$auth.isAuthenticated">

        <div class="dropdown">
  <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
    Dropdown button
  </button>
  <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
    <a class="dropdown-item" @click="addKeepToVault(vault)" v-for="vault in vaults" :value="vault.id" :key="vault.id">{{vault.name}}</a>
   
  </div>
</div>
        <!-- <form>
          <div class="input-group">
            <select class="custom-select" >
              <option value selected disabled>Select Vault</option>
              <option @submit.prevent="addKeepToVault(vault)" v-for="vault in vaults" :value="vault.id" :key="vault.id">{{vault.name}}</option>
            </select>
            <div class="input-group-append">
              <button class="btn btn-success" type="submit">ADD</button>
            </div>
          </div>
        </form> -->
      </div>
    </div>
  </div>
</template>


<script>
export default {
  name: "Keep",
  props: ["keepData"],
  data() {
    return {
      newVault: {},
      vaultKeep: {}
    };
  },
  mounted() {
    this.$store.dispatch("getVaults");
  },
  computed: {
    vaults() {
      return this.$store.state.vaults;
    }
  },
  methods: {
    deleteKeep(keepId) {
      if (window.confirm("Are you sure you want to delete this keep?")) {
        this.$store.dispatch("deleteKeep", keepId);
      }
    },

    deleteVaultKeep(keepId) {
      let vaultKeep = {};
      let vaultId = this.activeVault.id;
      vaultKeep = { vaultId, keepId };
      this.$store.dispatch("deleteVaultKeep", vaultKeep);
    },

    addKeepToVault(vault) {
      this.vaultKeep.vaultId = vault.id
      this.vaultKeep.keepId = this.keepData.id;
      this.$store.dispatch("addKeepToVault", this.vaultKeep);
      this.upKeepCount();
    },

    upKeepCount() {
      this.keepData.keeps++;
      this.$store.dispatch("editKeep", this.keepData);
    }
  },
  components: {}
};
</script>


<style scoped>
</style>