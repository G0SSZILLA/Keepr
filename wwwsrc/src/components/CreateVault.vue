<template>
  <div class="CreateVault">
    <form id="vaultForm" @submit.prevent="createVault()">
    <div class="form-group">
      <label for="name">Name*</label>
      <input required v-model="newVault.name" type="text" name="name" class="form-control" placeholder="Enter Vault Name"/>
    </div>
    <div class="form-group">
      <label for="description">Description*</label>
      <input required v-model="newVault.description" type="text" name="description" class="form-control" placeholder="Enter Vault Description"/>
      <small>*required fields</small>
    </div>
  <div>
  <button type="submit" class="btn btn-light">Create Vault</button>
  </div>
  </form>
</div>
</template>


<script>
export default {
  name: 'CreateVault',
  data(){
    return {
      newVault: {}
    }
  },
  mounted() {}, 
  computed:{},
  methods:{
    createVault(){
      this.$store.dispatch("createVault", this.newVault);
      document.getElementById("vaultForm").reset();
    }
  },
  components:{}
}
</script>


<style scoped>
</style>