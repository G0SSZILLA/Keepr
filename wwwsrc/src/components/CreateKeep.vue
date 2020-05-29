<template>
  <div class="CreateKeep">

<div
      class="modal fade"
      id="orangeModalSubscription"
      tabindex="-1"
      aria-labelledby="myModalLabel"
      aria-hidden="true">
      <div class="modal-dialog modal-notify modal-warning" role="document">
        <!--Content-->
        <div class="modal-content m-auto">
          <!--Header-->
          <div class="modal-header text-center">
            <h4 class="modal-title white-text w-100 font-weight-bold py-2" >Create a Keep</h4>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true" class="white-text">&times;</span>
            </button>
          </div>

          <!--Body-->
          <div class="modal-body">
            <div class="md-form mb-4">
             <label for="name">Name</label>
      <input required v-model="newKeep.name" type="text" name="name" class="form-control" placeholder="Enter Keep Name"/>
            </div>

            <div class="md-form">
              <label data-error="wrong" data-success="right" for="form2">Description</label>
              <textarea
                type="text"
                id="form2"
                class="form-control"
                rows="5"
                cols="20"
                v-model="newKeep.description"
              />
            </div>

            <!-- <div class="md-form justify-content-center mt-4">
              <label data-error="wrong" class data-success="right" for="form4">Add Image</label>
              <input
                type="file"
                id="form4"
                class="form-control p-1"
                accept="image/*"
                @change="encodeImage"
              />
            </div> -->
            <div class="justify-content-center">
              <button
               
                type="button"
                class="btn btn-primary waves-effect my-3 shadow"
                data-dismiss="modal"
                @click.prevent="createKeep()"
              >Post</button>
              <!-- <div v-show="hideAddPost" class="spinner-border mt-3" role="status" />
                <div v-show="hideAddPost">
            <p>Uploading Image...</p>
                </div> -->
           
            <div class="modal-footer" id="imageUpload"></div>
          </div>
          </div>
          <!--Footer-->
        </div>
        <!--/.Content-->
      </div>
    </div>

  <div class="text-center fixed-top mt-5 pt-4 mr-4">
      <a
        href
        class="btn bg-dark text-white shadow float-right"
        data-toggle="modal"
        data-target="#orangeModalSubscription"
        style="border-radius: 50%; z-index:1"
      >&plus;</a>
    </div>
  </div>
</template>


<script>
export default {
  name: 'CreateKeep',
  data(){
    return {
      isPrivate: false,
      newKeep: {}
    }
  },
  mounted() {}, 
  computed:{},
  methods:{
    createKeep(){
      this.$store.dispatch("createKeep", this.newKeep);
      document.getElementById("keepForm").reset();
    }
  },
  components:{}
}
</script>


<style scoped>
</style>