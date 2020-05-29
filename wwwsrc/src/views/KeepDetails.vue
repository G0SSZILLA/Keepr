<template>
<div class="KeepDetails container-fluid">
  <div class="row">
    <div class="col-7 mx-auto">
      <h2 class="text-center" v-if="!editingName" @click="editingName = true">
        <b>{{activeKeep.name}}</b>
      </h2>
      <div v-else class="mb-2 text-center">
        <input class="text-center" type="text" width="30" @keyup.enter="editKeepName()" v-model="activeKeep.name" />
      </div>

      <h4 class="text-center">{{activeKeep.description}}</h4>

  <img v-show=activeKeep.img class="card-img-top" :src=activeKeep.img alt="Error loading image">
<div class="row">
      <div class="col-6 text-right"><b>Views</b></div>
      <div class="col-6"><b>Keeps</b></div>
      <div class="col-6 text-right">{{activeKeep.views}}</div>
      <div class="col-6">{{activeKeep.keeps}}</div>
      <div class="col-12 text-center"></div>
</div>

          
</div>
  </div>



</div>
</template>


<script>
export default {
  name: 'keepDetails',
  data(){
    return {
    editingName: false,
    editingDescription: false,
    }
  },
  mounted() {
    this.$store.dispatch("getActiveKeep", this.$route.params.keepId);
  }, 
  computed:{
    activeKeep(){return this.$store.state.activeKeep
    },
  },
  methods:{
    editKeepName() {
      console.log(this.activeKeep);
      this.$store.dispatch("editKeepName", this.activeKeep)
      this.editingName = false
    },
      
  },
  components:{}
}
</script>


<style scoped>
</style>