import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios';
import * as Resource from './Resource'

Vue.use(Vuex);

export const store = new Vuex.Store({
    state: {
        Id: "",
        MapId: {},
        HostApi: "https://localhost:44369/api/v1/",
        Departments: [],
        FixedAssetCategorys: [],
        Method: Resource.Method,
        DialogMode: Resource.DialogMode,
        Validate: Resource.Validate,
        ButtonText: Resource.Button,
        Duplicate: "",
        Delete: "",
        MultiDelete: "",
        ErrorDelete: Resource.Warning.ErrorDelete,
        ErrorMultiDelete: "",
        Required: "",
        GetByIdFail: Resource.Warning.GetByIdFail,
        CancelAdd: Resource.Warning.CancelAdd,
        Changed: Resource.Warning.Changed,
        FixRate: Resource.Warning.FixRate,
        FixDYear: Resource.Warning.FixDYear,
        IsShowDialog: false,
        IsShowDialogForm: false,
        IsShowForm: false,
        IsShowMenuContext: false,
        IsShowToast: false,
        Toast: Resource.Toast,
        ToastContent: ""
    },
    getters: {
        Id: state => state.Id,
        MapId: state => state.MapId,
        HostApi: state => state.HostApi,
        Departments: state => state.Departments,
        FixedAssetCategorys: state => state.FixedAssetCategorys,
        Method: state => state.Method,
        DialogMode: state => state.DialogMode,
        ButtonText: state => state.ButtonText,
        Validate: state => state.Validate,
        Duplicate: state => state.Duplicate,
        Delete: state => state.Delete,
        MultiDelete: state => state.MultiDelete,
        ErrorDelete: state => state.ErrorDelete,
        ErrorMultiDelete: state => state.ErrorMultiDelete,
        Required: state => state.Required,
        GetByIdFail: state => state.GetByIdFail,
        CancelAdd: state => state.CancelAdd,
        Changed: state => state.Changed,
        FixRate: state => state.FixRate,
        FixDYear: state => state.FixDYear,
        IsShowDialog: state => state.IsShowDialog,
        IsShowDialogForm: state => state.IsShowDialogForm,
        IsShowForm: state => state.IsShowForm,
        IsShowMenuContext: state => state.IsShowMenuContext,
        IsShowToast: state => state.IsShowToast,
        Toast: state => state.Toast,
        ToastContent: state => state.ToastContent,
    },
    mutations: {
        setId: (state, payload) => state.Id = payload,
        setMapId: (state, payload) => state.MapId = {...payload },
        setHostApi: (state, payload) => state.HostApi = payload,
        setDepartments: (state, payload) => state.Departments = payload,
        setFixedAssetCategorys: (state, payload) => state.FixedAssetCategorys = payload,
        setDuplicate: (state, payload) => state.Duplicate = Resource.Warning.Duplicate(payload),
        setDelete: (state, payload) => state.Delete = Resource.Warning.Delete(payload),
        setMultiDelete: (state, payload) => state.MultiDelete = Resource.Warning.MultiDelete(payload),
        setErrorMultiDelete: (state, payload) => state.ErrorMultiDelete = Resource.Warning.ErrorMultiDelete(payload),
        setRequired: (state, payload) => state.Required = Resource.Warning.Required(payload),
        setIsShowDialog: (state, payload) => state.IsShowDialog = payload,
        setIsShowDialogForm: (state, payload) => state.IsShowDialogForm = payload,
        setIsShowForm: (state, payload) => state.IsShowForm = payload,
        setIsShowMenuContext: (state, payload) => state.IsShowMenuContext = payload,
        setIsShowToast: (state, payload) => state.IsShowToast = payload,
        setToastContent: (state, payload) => state.ToastContent = payload,
    },
    actions: {
        getDepartments: async context => {
            let HostApi = context.state.HostApi;
            let apiUrl = HostApi + "Departments";
            try {
                let response = await axios.get(apiUrl);
                context.commit("setDepartments", response.data.Data);
            } catch (error) {
                console.log(error)
            }
        },

        getFixedAssetCategorys: async context => {
            let HostApi = context.state.HostApi;
            let apiUrl = HostApi + "FixedAssetCategorys";
            try {
                let response = await axios.get(apiUrl);
                context.commit("setFixedAssetCategorys", response.data.Data);
            } catch (error) {
                console.log(error)
            }
        },
    }
})