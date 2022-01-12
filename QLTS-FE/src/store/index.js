import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios';
import * as Resource from './resource'

Vue.use(Vuex);

export const store = new Vuex.Store({
    state: {
        Id: "",
        MapId: {},
        HostApi: "https://localhost:44369/api/v1/",
        Departments: [],
        FixedAssetCategorys: [],
        Resources: [],
        Method: Resource.Method,
        DialogMode: Resource.DialogMode,
        Validate: Resource.Validate,
        ButtonText: Resource.Button,
        Duplicate: "",
        Delete: "",
        MultiDelete: "",
        ErrorDelete: "",
        ErrorMultiDelete: "",
        Required: "",
        GetByIdFail: Resource.Warning.GetByIdFail,
        CancelAdd: Resource.Warning.CancelAdd,
        Changed: Resource.Warning.Changed,
        FixRate: Resource.Warning.FixRate,
        FixDYear: Resource.Warning.FixDYear,
        ErrorDate: Resource.Warning.ErrorDate,
        IsShowDialog: false,
        IsShowDialogForm: false,
        IsShowForm: false,
        IsShowMenuContext: false,
        IsShowToast: false,
        Toast: Resource.Toast,
        ToastContent: "",
        TitleForm: Resource.TitleForm,
        DeleteReceipt: "",
        MultiDeleteReceipt: "",
        TypeDate: "dd-MM-yyyy",
        IsShowReceiptForm: false,
        IsReceiptFormDisplayNone: true,
        IsShowChooseAsset: false,
        AssetNull: Resource.Warning.AssetNull,
        CancelAddReceipt: Resource.Warning.CancelAddReceipt,
        DuplicateReceipt: Resource.Warning.DuplicateReceipt,
        IsShowAssetResourceForm: false
    },
    getters: {
        Id: state => state.Id,
        MapId: state => state.MapId,
        HostApi: state => state.HostApi,
        Departments: state => state.Departments,
        FixedAssetCategorys: state => state.FixedAssetCategorys,
        Resources: state => state.Resources,
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
        ErrorDate: state => state.ErrorDate,
        IsShowDialog: state => state.IsShowDialog,
        IsShowDialogForm: state => state.IsShowDialogForm,
        IsShowForm: state => state.IsShowForm,
        IsShowMenuContext: state => state.IsShowMenuContext,
        IsShowToast: state => state.IsShowToast,
        Toast: state => state.Toast,
        ToastContent: state => state.ToastContent,
        TitleForm: state => state.TitleForm,
        DeleteReceipt: state => state.DeleteReceipt,
        MultiDeleteReceipt: state => state.MultiDeleteReceipt,
        TypeDate: state => state.TypeDate,
        IsShowReceiptForm: state => state.IsShowReceiptForm,
        IsReceiptFormDisplayNone: state => state.IsReceiptFormDisplayNone,
        IsShowChooseAsset: state => state.IsShowChooseAsset,
        AssetNull: state => state.AssetNull,
        CancelAddReceipt: state => state.CancelAddReceipt,
        DuplicateReceipt: state => state.DuplicateReceipt,
        IsShowAssetResourceForm: state => state.IsShowAssetResourceForm,
    },
    mutations: {
        setId: (state, payload) => state.Id = payload,
        setMapId: (state, payload) => state.MapId = {...payload },
        setHostApi: (state, payload) => state.HostApi = payload,
        setDepartments: (state, payload) => state.Departments = payload,
        setFixedAssetCategorys: (state, payload) => state.FixedAssetCategorys = payload,
        setResources: (state, payload) => state.Resources = payload,
        setDuplicate: (state, payload) => state.Duplicate = Resource.Warning.Duplicate(payload),
        setDelete: (state, payload) => state.Delete = Resource.Warning.Delete(payload),
        setMultiDelete: (state, payload) => state.MultiDelete = Resource.Warning.MultiDelete(payload),
        setErrorDelete: (state, payload) => state.ErrorDelete = Resource.Warning.ErrorDelete(payload),
        setErrorMultiDelete: (state, payload) => state.ErrorMultiDelete = Resource.Warning.ErrorMultiDelete(payload),
        setRequired: (state, payload) => state.Required = Resource.Warning.Required(payload),
        setIsShowDialog: (state, payload) => state.IsShowDialog = payload,
        setIsShowDialogForm: (state, payload) => state.IsShowDialogForm = payload,
        setIsShowForm: (state, payload) => state.IsShowForm = payload,
        setIsShowMenuContext: (state, payload) => state.IsShowMenuContext = payload,
        setIsShowToast: (state, payload) => state.IsShowToast = payload,
        setToastContent: (state, payload) => state.ToastContent = payload,
        setDeleteReceipt: (state, payload) => state.DeleteReceipt = Resource.Warning.DeleteReceipt(payload),
        setMultiDeleteReceipt: (state, payload) => state.MultiDeleteReceipt = Resource.Warning.MultiDeleteReceipt(payload),
        setIsShowReceiptForm: (state, payload) => state.IsShowReceiptForm = payload,
        setIsReceiptFormDisplayNone: (state, payload) => state.IsReceiptFormDisplayNone = payload,
        setIsShowChooseAsset: (state, payload) => state.IsShowChooseAsset = payload,
        setCancelAddReceipt: (state, payload) => state.CancelAddReceipt = Resource.Warning.CancelAddReceipt(payload),
        setDuplicateReceipt: (state, payload) => state.DuplicateReceipt = Resource.Warning.DuplicateReceipt(payload),
        setIsShowAssetResourceForm: (state, payload) => state.IsShowAssetResourceForm = payload,

    },
    actions: {
        /**
         * lấy danh sách phòng ban
         * CreatedBy: hadm 12/12/2021
         */
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

        /**
         * lấy danh sách loại tài sản
         * CreatedBy: hadm 12/12/2021
         */
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

        /**
         * lấy danh sách nguồn hình thành
         * CreatedBy: hadm 12/12/2021
         */
        getResources: async context => {
            let HostApi = context.state.HostApi;
            let apiUrl = HostApi + "Resources";
            try {
                let response = await axios.get(apiUrl);
                context.commit("setResources", response.data.Data);
            } catch (error) {
                console.log(error)
            }
        },
    }
})