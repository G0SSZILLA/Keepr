<template>
  <div class="Vault">
  <div>
  <h2>{{vaultData.name}}</h2>
      <button class="btn-small btn-danger" @click="deleteVault(vaultData.id)">&times;</button>
  </div>
  </div>
</template>


<script>
export default {
  name: 'Vault',
  props: ["vaultData"],
  data(){
    return {
    }
  },
  mounted() {
  }, 
  computed:{
    
  },
  methods:{
    goToVault(vaultId){
      console.log("going");
      this.$store.dispatch("getVaultKeeps", this.vaultData.id)
      },
    deleteVault(vaultId){
      if(window.confirm("Are you sure you want to delete this vault?")){
      this.$store.dispatch("deleteVault", vaultId)
      }
    },
  },
  components:{}
}
</script>


<style scoped>
</style>