import Vue from "vue";
import Vuex from "vuex";
import Axios from "axios";
import router from "../router";

Vue.use(Vuex);

let baseUrl = location.host.includes("localhost") ?
    "https://localhost:5001/" :
    "/";

let api = Axios.create({
    baseURL: baseUrl + "api/",
    timeout: 3000,
    withCredentials: true,
});

export default new Vuex.Store({
    state: {
        publicKeeps: [],
        activeKeep: {},
        userKeeps: [],
        vaultKeeps: [],
        userVaults: [],
        vaults: [],
    },
    mutations: {
        setActiveKeep(state, activeKeep) {
            state.activeKeep = activeKeep;
        },

        setPublicKeeps(state, publicKeeps) {
            state.publicKeeps = publicKeeps;
        },

        setUserKeeps(state, userKeeps) {
            state.userKeeps = userKeeps;
        },

        setVaults(state, vaults) {
            state.vaults = vaults
        },

        setUserVaults(state, userVaults) {
            state.userVaults = userVaults;
        },

        setVaultKeeps(state, vaultKeeps) {
            state.vaultKeeps = vaultKeeps;
        },
    },
    actions: {
        setBearer({}, bearer) {
            api.defaults.headers.authorization = bearer;
        },
        resetBearer() {
            api.defaults.headers.authorization = "";
        },

        //#region NOTE VAULTS

        async createVault({ commit, dispatch }, newVault) {
            try {
                let res = await api.post("vaults", newVault);
                dispatch("getVaults");
            } catch (error) {
                console.error(error);
            }
        },

        async getVaults({ commit, dispatch }) {
            try {
                let res = await api.get("vaults");
                console.log(res.data);

                commit("setVaults", res.data);
            } catch (error) {
                console.error(error);
            }
        },

        async deleteVault({ dispatch }, vaultId) {
            try {
                await api.delete("vaults/" + vaultId);
                dispatch("getVaults");
            } catch (error) {
                console.error(error);
            }
        },

        async addKeepToVault({ commit, dispatch }, vaultKeep) {
            try {

                let res = await api.post("vaultKeeps", vaultKeep)

            } catch (error) {
                console.error(error)
            }
        },

        //#endregion

        //#region NOTE KEEPS

        async createKeep({ commit, dispatch }, newKeep) {
            try {
                let res = await api.post("keeps", newKeep);
                dispatch("getPublicKeeps");
            } catch (error) {
                console.error(error);
            }
        },

        async getActiveKeep({ commit }, keepId) {
            try {
                let res = await api.get("keeps/" + keepId);
                commit("setActiveKeep", res.data);
            } catch (error) {
                console.error(error);
            }
        },

        async getKeepsByUser({ commit, dispatch }, userId) {
            try {
                console.log("from getkeepsbyuser", userId)
                let res = await api.get("keeps/user");
                commit("setUserKeeps", res.data);
            } catch (error) {
                console.error(error);
            }
        },

        async getPublicKeeps({ commit, dispatch }) {
            try {
                let res = await api.get("keeps");
                commit("setPublicKeeps", res.data);
            } catch (error) {
                console.error(error);
            }
        },

        async editKeep({ commit, dispatch }, keepEdit) {
            try {
                let res = api.put("keeps/" + keepEdit.id, keepEdit)

            } catch (error) {
                console.error(error)
            }
        },

        async deleteKeep({ dispatch }, keepId) {
            try {
                await api.delete("keeps/" + keepId);
                dispatch("getPublicKeeps");
            } catch (error) {
                console.error(error);
            }
        },

        //#endregion

        //#region NOTE VAULT KEEPS

        async getKeepsByVaultId({ commit, dispatch }, vaultId) {
            let res = await api.get("vaultkeeps/" + vaultId + "/keeps");
            commit("setVaultKeeps", res.data);
        },

        async getVaultKeeps({ commit, dispatch }, vaultId) {
            try {
                let res = await api.get("vaults/" + vaultId + "/keeps");
                commit("setVaultKeeps", res.data);
            } catch (error) {
                console.error(error);
            }
        },

        async deleteVaultKeep({ commit, dispatch }, data) {
            try {
                let res = await api.delete(
                    "vaultkeeps/" + data.vaultId + "/keeps/" + data.keepId);
                dispatch("getKeepsByVaultId", data.vaultId);
            } catch (error) {
                console.error(error);
            }
        },

        //#endregion
    },
});