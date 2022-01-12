<template>
  <div class="m-form">
    <div
      class="form-site receipt-form"
      id="receiptForm"
      v-show="this.$store.getters.IsReceiptFormDisplayNone"
    >
      <div class="f-header" @mousedown="moveForm($event)">
        <div class="f-title">{{ titleForm }}</div>
        <div
          class="icon-24 icon-close m-tooltip-site"
          @click="closeForm('check')"
          v-shortkey="['esc']"
          @shortkey="closeForm('check')"
        >
          <Tooltip :content="'Đóng (ESC)'" />
        </div>
      </div>
      <div class="f-content f-receipt-content">
        <div class="receipt-site">
          <div class="title">Thông tin chứng từ</div>
          <div class="data-site">
            <div class="row p-x-16 dis-flex align-items-center m-b-20">
              <div class="w-1/3">
                <div class="label m-b-8">
                  Mã chứng từ <span style="color: red">*</span>
                </div>
                <div class="m-textbox">
                  <input
                    type="text"
                    placeholder="Nhập mã chứng từ"
                    tabindex="1"
                    class="required"
                    maxlength="10"
                    v-model="receipt.ReceiptCode"
                    ref="ReceiptCode"
                    @focus="focusSelected('ReceiptCode')"
                    @blur="removeError('ReceiptCode')"
                  />
                </div>
                <div class="tooltip">{{ tooltip }}</div>
              </div>
              <div class="w-1/3 m-x-16">
                <div class="label m-b-8">
                  Ngày bắt đầu sử dụng <span style="color: red">*</span>
                </div>
                <DxDateBox
                  class="required"
                  :show-clear-button="true"
                  :use-mask-behavior="true"
                  :value="new Date()"
                  placeholder="dd/mm/yyyy"
                  :display-format="typeDate"
                  type="date"
                  :tabIndex="2"
                  v-model="receipt.ReceiptDate"
                  ref="ReceiptDate"
                  @focusOut="removeError('ReceiptDate')"
                />
                <div class="tooltip">{{ tooltip }}</div>
              </div>
              <div class="w-1/3">
                <div class="label m-b-8">
                  Ngày ghi tăng <span style="color: red">*</span>
                </div>
                <DxDateBox
                  class="required"
                  :show-clear-button="true"
                  :use-mask-behavior="true"
                  :value="new Date()"
                  placeholder="dd/mm/yyyy"
                  :display-format="typeDate"
                  type="date"
                  :tabIndex="3"
                  v-model="receipt.IncreaseDate"
                  ref="IncreaseDate"
                  @focusOut="removeError('IncreaseDate')"
                />
                <div class="tooltip">{{ tooltip }}</div>
              </div>
            </div>
            <div class="row p-x-16 dis-flex align-items-center m-b-20">
              <div class="w-full">
                <div class="label m-b-8">Ghi chú</div>
                <div class="m-textbox">
                  <input
                    type="text"
                    placeholder="Nội dung"
                    tabindex="4"
                    v-model="receipt.Note"
                    ref="Note"
                    @focus="focusSelected('Note')"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="asset-site">
          <div class="title">Thông tin chi tiết</div>
          <div class="data-site">
            <div class="tool p-x-16">
              <div class="tool-left" style="width: 280px">
                <div class="m-textbox-icon">
                  <div class="icon-16 icon-search"></div>
                  <input
                    type="text"
                    placeholder="Tìm kiếm theo mã, tên tài sản"
                    v-model="keyword"
                    @keyup.enter="search()"
                  />
                </div>
              </div>
              <div class="tool-right">
                <button class="m-btn m-btn-outline" @click="showChooseAsset()">
                  <div class="label">Chọn tài sản</div>
                </button>
              </div>
            </div>
            <div class="tbl-site" id="asset-list">
              <table class="m-table" id="tbl-asset-list">
                <thead>
                  <tr>
                    <th class="very-short-width text-center">
                      <div class="text">STT</div>
                    </th>
                    <th class="medium-width">
                      <div class="text">Mã tài sản</div>
                    </th>
                    <th class="long-width">
                      <div class="text">Tên tài sản</div>
                    </th>
                    <th
                      style="width: 146px; max-width: 146px; min-width: 130px"
                    >
                      <div class="text">Bộ phận sử dụng</div>
                    </th>
                    <th class="medium-width text-right">
                      <div class="text">Nguyên giá</div>
                    </th>
                    <th class="medium-width text-right">
                      <div class="text">Hao mòn năm</div>
                    </th>
                    <th class="medium-width text-right">
                      <div class="text">Giá trị còn lại</div>
                    </th>
                  </tr>
                </thead>
                <tbody>
                  <tr
                    v-for="(asset, index) in assetsViews"
                    :key="asset.FixedAssetId"
                    @dblclick="
                      showAssetResourceForm(
                        asset.FixedAssetId,
                        asset.FixedAssetName,
                        asset.DepartmentName
                      )
                    "
                  >
                    <td class="very-short-width text-center">
                      <div class="text">
                        {{ index + 1 + (pageIndex - 1) * pageSize }}
                      </div>
                    </td>
                    <td class="medium-width">
                      <div class="text">{{ asset.FixedAssetCode }}</div>
                    </td>
                    <td class="long-width">
                      <div class="text">{{ asset.FixedAssetName }}</div>
                      <Tooltip
                        :content="asset.FixedAssetName"
                        :bottom="'-16px'"
                      />
                    </td>
                    <td
                      style="width: 146px; max-width: 146px; min-width: 146px"
                    >
                      <div class="text">{{ asset.DepartmentName }}</div>
                      <Tooltip
                        :content="asset.DepartmentName"
                        :bottom="'-16px'"
                      />
                    </td>
                    <td class="medium-width text-right">
                      <div class="text">{{ asset.Cost | handleNumer }}</div>
                    </td>
                    <td class="medium-width text-right">
                      <div class="text">
                        {{
                          setDepreciation(asset.Cost, asset.DepreciationRate)
                            | handleNumer
                        }}
                      </div>
                    </td>
                    <td class="medium-width text-right last-col">
                      <div class="text">
                        {{
                          (asset.Cost -
                            setDepreciation(asset.Cost, asset.DepreciationRate))
                            | handleNumer
                        }}
                      </div>
                      <div class="tool">
                        <div
                          class="icon-24 action m-tooltip-site icon-edit m-l-6"
                          @click="
                            showAssetResourceForm(
                              asset.FixedAssetId,
                              asset.FixedAssetName,
                              asset.DepartmentName
                            )
                          "
                        >
                          <Tooltip :content="'Sửa'" :left="'-6px'" />
                        </div>
                        <div
                          class="icon-24 action m-tooltip-site icon-trash m-l-6"
                          @click="removeAsset(asset.FixedAssetId)"
                        >
                          <Tooltip :content="'Xóa'" :left="'-6px'" />
                        </div>
                      </div>
                    </td>
                  </tr>
                </tbody>
              </table>
              <div
                class="m-fit-content"
                id="fit-content-asset"
                :class="assetsViews.length < 1 ? 'no-data' : ''"
                :style="
                  assetsViews.length < 1 ? 'height: calc(100% - 80px)' : ''
                "
              ></div>
              <div class="general">
                <table style="border-collapse: collapse">
                  <tbody>
                    <td
                      class="dis-flex align-items-center"
                      style="min-width: 528px; width: 528px; max-width: 528px"
                    >
                      <div style="width: 180px; padding-left: 16px">
                        Tổng số:
                        <span style="font-family: MISARoboto-Bold">{{
                          recordNum
                        }}</span>
                        bản ghi
                      </div>
                      <DxSelectBox
                        :data-source="paginationSource"
                        :search-enabled="false"
                        :value="paginationSource[1].key"
                        display-expr="key"
                        value-expr="key"
                        width="68"
                        height="28"
                        @value-changed="setPageSize($event.value)"
                      >
                      </DxSelectBox>
                      <div class="paging">
                        <div
                          class="paging-item icon-24 pointer icon-previous"
                          @click="
                            pageIndex === 1 ? '' : setPageIndex(pageIndex - 1)
                          "
                        ></div>
                        <div
                          class="paging-item"
                          v-for="(item, index) in listIndexs"
                          :key="index"
                          :class="
                            item === '...'
                              ? ''
                              : item === pageIndex
                              ? 'current pointer'
                              : 'pointer'
                          "
                          @click="setPageIndex(item)"
                        >
                          {{ item }}
                        </div>
                        <div
                          class="paging-item icon-24 pointer icon-next"
                          @click="
                            pageIndex === totalPageIndex
                              ? ''
                              : setPageIndex(pageIndex + 1)
                          "
                        ></div>
                      </div>
                    </td>
                    <td
                      :class="assetsViews.length > 0 ? '' : 'sum'"
                      class="text-right p-x-10"
                      style="
                        min-width: 135px;
                        width: 135px;
                        font-family: MISARoboto-Bold;
                      "
                    >
                      <div class="text">
                        {{ totals.TotalCost | handleNumer }}
                      </div>
                    </td>
                    <td
                      :class="assetsViews.length > 0 ? '' : 'sum'"
                      class="text-right p-x-10"
                      style="
                        min-width: 135px;
                        width: 135px;
                        font-family: MISARoboto-Bold;
                      "
                    >
                      <div class="text">
                        {{ totals.TotalDepreciation | handleNumer }}
                      </div>
                    </td>
                    <td
                      :class="assetsViews.length > 0 ? '' : 'sum'"
                      class="text-right p-x-10"
                      style="
                        min-width: 135px;
                        width: 135px;
                        font-family: MISARoboto-Bold;
                      "
                    >
                      <div class="text">
                        {{ totals.TotalResidual | handleNumer }}
                      </div>
                    </td>
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="f-footer">
        <div></div>
        <div class="button-field dis-flex">
          <button class="m-btn m-btn-outline m-r-16" @click="closeForm(null)">
            <div class="label">Hủy</div>
          </button>
          <button
            class="m-btn m-tooltip-site"
            @click="saveData()"
            v-shortkey="['ctrl', 's']"
            @shortkey="saveData()"
          >
            <div class="label">Lưu</div>
            <Tooltip :content="'Lưu (Ctrl + S)'" />
          </button>
        </div>
      </div>
    </div>
    <ChooseAsset
      v-if="this.$store.getters.IsShowChooseAsset"
      :chosenAssets="assets"
      @chosenAssets="setChosenAsset($event)"
    />
    <Loading v-if="isLoading" />
    <Dialog
      :dialogMode="dialogMode"
      :itemCode="receipt.ReceiptCode"
      :nameRequired="nameRequired"
      v-if="this.$store.getters.IsShowDialogForm"
      @funct="saveData()"
      @required="focusToError()"
    />
    <AssetResource
      v-if="this.$store.getters.IsShowAssetResourceForm"
      :idAsset="idAsset"
      :nameAsset="nameAsset"
      :departmentNameAsset="departmentNameAsset"
      @funct="refreshAsset($event)"
    />
  </div>
</template>

<script>
import DxSelectBox from "devextreme-vue/select-box";
import Tooltip from "../../base/MTooltip.vue";
import DxDateBox from "devextreme-vue/date-box";
import ChooseAsset from "./MChooseAsset.vue";
import axios from "axios";
import moment from "moment";
import Dialog from "../../base/MDialog.vue";
import Loading from "../../base/MLoading.vue";
import AssetResource from "./MAssetDetail.vue";
export default {
  props: ["id"],
  components: {
    Tooltip,
    DxDateBox,
    ChooseAsset,
    Loading,
    Dialog,
    DxSelectBox,
    AssetResource,
  },
  data() {
    return {
      typeDate: this.$store.getters.TypeDate,
      hostApi: this.$store.getters.HostApi,
      endPoint: "Receipts/",
      tooltip: "Không được bỏ trống!",
      assets: [],
      assetsViews: [],
      paginationSource: [
        { key: 10 },
        { key: 20 },
        { key: 30 },
        { key: 50 },
        { key: 100 },
      ],
      isLoading: false,
      receipt: {
        ReceiptCode: "",
        ReceiptDate: moment(new Date()).format("YYYY-MM-DD"),
        IncreaseDate: moment(new Date()).format("YYYY-MM-DD"),
        TotalCost: 0,
        Note: null,
        FixedAssets: null,
        CreatedDate: null,
        CreatedBy: null,
        ModifiedDate: null,
        ModifiedBy: null,
      },
      receiptClone: null,
      method: "",
      dialogMode: "",
      nameRequired: "",
      focusRequired: "",
      temp: 0,
      titleForm: "",
      listAssetId: [],
      idReceipt: this.id,
      flag: true,
      recordNum: 0,
      totalPageIndex: 0,
      listIndexs: [],
      pageIndex: 1,
      pageSize: 20,
      totals: {
        TotalCost: 0,
        TotalDepreciation: 0,
        TotalResidual: 0,
      },
      keyword: "",
      idAsset: "",
      nameAsset: "",
      departmentNameAsset: "",
      assetClones: [],
    };
  },
  filters: {
    /**
     * xử lý dữ liệu tiền
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    handleNumer(salary) {
      if (salary == null || salary == undefined) salary = 0;
      let result = salary.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
      return result;
    },
  },
  created() {
    this.method = this.$store.getters.Method.Add;
    if (this.idReceipt) {
      this.bindData();
      this.method = this.$store.getters.Method.Edit;
      this.endPoint += this.idReceipt;
      this.titleForm = this.$store.getters.TitleForm.EditReceipt;
      this.$store.commit(
        "setToastContent",
        this.$store.getters.Toast.EditReceipt
      );
    } else {
      this.bindCode();
      this.titleForm = this.$store.getters.TitleForm.AddReceipt;
      this.$store.commit(
        "setToastContent",
        this.$store.getters.Toast.AddReceipt
      );
    }
    this.$store.dispatch("getResources");
  },
  mounted() {
    this.$refs.ReceiptCode.focus();
  },
  updated() {
    this.setTooltip();
    this.setFitContentAssetList();
  },
  methods: {
    /**
     * làm mới dữ liệu
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    refreshAsset(cost) {
      let index = 0;
      for (let i = 0; i < this.assets.length; i++) {
        if (this.assets[i].FixedAssetId == this.idAsset) index = i;
      }
      if (this.assetClones.length > 0) {
        for (let i = 0; i < this.assetClones.length; i++) {
          if (this.assetClones[i].FixedAssetId == this.idAsset) {
            this.assetClones[i].Cost = cost;
          }
          break;
        }
      }
      if (index >= 0) this.assets[index].Cost = cost;

      this.assetsViews = this.assets.filter(this.filterAssetsPaging);
      this.calculateTotal();
    },

    /**
     * show form sửa nguồn tài sản
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    showAssetResourceForm(id, name, departmentName) {
      this.idAsset = id;
      this.nameAsset = name;
      this.departmentNameAsset = departmentName;
      this.$store.commit("setIsShowAssetResourceForm", true);
    },

    /**
     * tìm kiếm
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    search() {
      this.keyword = this.keyword.trim();
      this.assetsViews = this.assets.filter(this.filterSearch);
      this.recordNum = this.assetsViews.length;
      this.calculateTotal();
      this.genPagination();
    },

    /**
     * filter tìm kiếm
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    filterSearch(asset) {
      let flag = false;
      if (asset.FixedAssetCode.includes(this.keyword)) flag = true;
      if (asset.FixedAssetName.includes(this.keyword)) flag = true;
      return flag;
    },

    /**
     * bỏ chọn tài sản
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    removeAsset(id) {
      event.stopPropagation();
      let index = 0;
      for (let i = 0; i < this.assets.length; i++) {
        if (this.assets[i].FixedAssetId == id) index = i;
      }
      this.assets.splice(index, 1);
      this.recordNum = this.assets.length;
      this.assetsViews = this.assets.filter(this.filterAssetsPaging);
      this.calculateTotal();
      this.genPagination();
    },

    /**
     * tính tổng số các dữ liệu
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    calculateTotal() {
      (this.totals.TotalCost = 0),
        (this.totals.TotalDepreciation = 0),
        (this.totals.TotalResidual = 0);
      if (this.assetsViews && this.assetsViews.length > 0) {
        this.assetsViews.forEach((ele) => {
          this.totals.TotalCost += ele.Cost;
          this.totals.TotalDepreciation += this.setDepreciation(
            ele.Cost,
            ele.DepreciationRate
          );
        });
        this.totals.TotalResidual =
          this.totals.TotalCost - this.totals.TotalDepreciation;
      }
    },
    /**
     * validate các trường bắt buộc
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    validateRequired() {
      let flagRequired = true;
      let inputVal = "";
      let name = "";
      let count = 0;
      this.nameRequired = "";
      let requireds = document.querySelectorAll(".required");
      let tooltip = document.querySelectorAll(".tooltip");
      for (let i = 0; i < requireds.length; i++) {
        if (i == 1 || i == 2 || i == 4 || i == 9) {
          inputVal = requireds[i].querySelector("input").value;
          if (inputVal == null || inputVal == "") {
            requireds[i].classList.add("error");
            tooltip[i].style.display = "block";
            flagRequired = false;
            name =
              requireds[i].parentElement.querySelector(".label").textContent;
            this.nameRequired += name.slice(0, name.length - 1) + ",";
            count++;
            if (count == 1) {
              this.focusRequired = requireds[i].querySelector(
                "input.dx-texteditor-input"
              );
            }
          }
        } else {
          inputVal = requireds[i].value;
          if (inputVal == null || inputVal == "") {
            requireds[i].classList.add("error");
            tooltip[i].style.display = "block";
            flagRequired = false;
            name =
              requireds[i].parentElement.parentElement.querySelector(
                ".label"
              ).textContent;
            this.nameRequired += name.slice(0, name.length - 1) + ",";
            count++;
            if (count == 1) {
              this.focusRequired = requireds[i];
            }
          }
        }
      }
      return flagRequired;
    },

    /**
     * validate asset list
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    validateAssets() {
      if (this.assets.length < 1) {
        this.dialogMode = this.$store.getters.DialogMode.AssetNull;
        this.$store.commit("setIsShowDialogForm", true);
        return false;
      }
      return true;
    },

    /**
     * xóa error khi focus
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    removeError(refName) {
      let requireds = document.querySelectorAll(".required");
      let tooltip = document.querySelectorAll(".tooltip");
      switch (refName) {
        case "ReceiptDate":
          requireds[1].classList.remove("error");
          tooltip[1].style.display = "none";

          break;
        case "IncreaseDate":
          requireds[2].classList.remove("error");
          tooltip[2].style.display = "none";

          break;
        default:
          this.$refs[refName].classList.remove("error");
          this.$refs[refName].parentElement.parentElement.querySelector(
            ".tooltip"
          ).style.display = "none";
          break;
      }
    },

    /**
     * focus vào lỗi
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    focusToError() {
      this.focusRequired.focus();
    },

    /**
     * đóng form
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    closeForm(check) {
      this.nameRequired = "";
      let flag = false;
      if (JSON.stringify(this.assetClones) !== JSON.stringify(this.assets))
        flag = true;
      if (JSON.stringify(this.receiptClone) !== JSON.stringify(this.receipt))
        flag = true;

      if (flag && check !== null) {
        if (this.method === this.$store.getters.Method.Add) {
          this.dialogMode = this.$store.getters.DialogMode.CancelAddReceipt;
          this.$store.commit("setIsShowDialogForm", true);
        } else {
          this.dialogMode = this.$store.getters.DialogMode.Changed;
          this.$store.commit("setIsShowDialogForm", true);
        }
      } else {
        this.$store.commit("setIsShowReceiptForm", false);
      }
    },

    /**
     * lưu dữ liệu
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    async saveData() {
      if (!this.validateRequired()) {
        this.focusToError();
        return;
      }
      if (!this.validateAssets()) {
        return;
      }
      this.beforeSave();
      await this.saveReceipt();
      if (!this.flag) return;
      if (this.method == this.$store.getters.Method.Add) {
        await this.getLastId();
        if (!this.flag) return;
      }
      await this.saveAssetByReceiptId();
      this.isLoading = false;
      if (!this.flag) return;
      this.closeForm(null);
      this.$emit("loadData");
      this.$store.commit("setIsShowToast", true);
    },

    /**
     * event trước khi lưu
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    beforeSave() {
      this.isLoading = true;
      for (let i = 0; i < this.assets.length; i++) {
        this.listAssetId.push(this.assets[i].FixedAssetId);
        this.receipt.TotalCost += this.assets[i].Cost;
      }
    },

    /**
     * lưu chứng từ
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    async saveReceipt() {
      const config = {
        url: this.hostApi + this.endPoint,
        method: this.method,
        data: this.receipt,
      };
      await axios
        .request(config)
        .then(() => {
          this.flag = true;
        })
        .catch((error) => {
          const validateInfo = error.response.data.ValidateInfo;
          this.isLoading = false;
          // check duplicate
          if (validateInfo && validateInfo.length) {
            validateInfo.forEach((ele) => {
              if (ele.column === "ReceiptCode" && ele.state === "duplicate") {
                this.dialogMode =
                  this.$store.getters.DialogMode.DuplicateReceipt;
                this.$store.commit("setIsShowDialogForm", true);
                this.focusRequired = this.$refs.ReceiptCode;
              }
            });
          }
          this.flag = false;
        });
    },

    /**
     * lấy Id mới nhất
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    async getLastId() {
      let urlApi = `${this.hostApi}Receipts/lastId`;
      await axios
        .get(urlApi)
        .then((res) => {
          this.idReceipt = res.data.Data;
          this.flag = true;
        })
        .catch((error) => {
          this.isLoading = false;
          console.log(error);
          this.flag = false;
        });
    },

    /**
     * lưu asset theo id
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    async saveAssetByReceiptId() {
      const config = {
        url: this.hostApi + "FixedAssets/Receipt/" + this.idReceipt,
        method: this.$store.getters.Method.Edit,
        data: this.listAssetId,
      };
      await axios
        .request(config)
        .then(() => {
          this.flag = true;
        })
        .catch((error) => {
          this.isLoading = false;
          console.log(error);
          this.flag = false;
        });
    },

    /**
     * gán dữ liệu chứng từ
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    bindData() {
      this.isLoading = true;
      let urlApi = `${this.hostApi}${this.endPoint}${this.idReceipt}`;
      axios
        .get(urlApi)
        .then((res) => {
          this.isLoading = false;
          this.receipt = res.data.Data;
          this.assets = this.receipt.FixedAssets;
          this.recordNum = this.assets.length;
          this.receiptClone = { ...this.receipt };
          this.assetClones = [...this.assets];
          this.assetsViews = this.assets.filter(this.filterAssetsPaging);
          this.genPagination();
          this.calculateTotal();
        })
        .catch(() => {
          this.isLoading = false;
          this.dialogMode = this.$store.getters.DialogMode.GetByIdFail;
          console.log(this.dialogMode);
          this.$store.commit("setIsShowDialogForm", true);
        });
    },
    /**
     * sinh mã mới
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    bindCode() {
      this.isLoading = true;
      let urlApi = `${this.hostApi}${this.endPoint}new-code`;
      axios
        .get(urlApi)
        .then((res) => {
          this.isLoading = false;
          this.receipt.ReceiptCode = res.data.Data;
          this.receiptClone = { ...this.receipt };
          this.assetClones = [...this.assets];
          this.$refs.ReceiptCode.focus();
        })
        .catch((error) => {
          this.isLoading = false;
          console.log(error);
        });
    },

    /**
     * reset text
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    focusSelected(refName) {
      this.$refs[refName].select();
    },

    /**
     * format số
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    formatNum(value) {
      if (value == null || value == 0 || value == "") return "0";
      let num = value.replace(/[^0-9-]+/g, "");
      return parseInt(num);
    },

    /**
     * format tiền
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    formatCurrency(value) {
      if (value == null || value == 0 || value == "") return "0";
      let num = this.formatNum(value + "") + "";
      return num.replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    },

    /**
     * thêm asset đc chọn
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    setChosenAsset(asset) {
      if (this.assets.length <= 0) {
        this.assets = [...asset];
      } else {
        this.assets = this.assets.concat(asset);
      }
      this.recordNum = this.assets.length;
      this.assetsViews = this.assets.filter(this.filterAssetsPaging);
      this.genPagination();
      this.calculateTotal();
    },

    /**
     * hiển thị form chọn tài sản
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    showChooseAsset() {
      this.$store.commit("setIsReceiptFormDisplayNone", false);
      this.$store.commit("setIsShowChooseAsset", true);
    },

    /**
     * tính khấu hao lũy kế
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    setDepreciation(cost, rate) {
      return parseInt((cost * rate) / 100);
    },

    /**
     * set Tooltip nếu text dài quá
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */ setTooltip() {
      let cells = document.querySelectorAll("#receiptForm tbody tr td");
      for (let i = 0; i < cells.length; i++) {
        const cell = cells[i];
        let textWidth = cell.querySelector(".text")?.offsetWidth;
        let scrollWidth = cell.querySelector(".text")?.scrollWidth;
        if (scrollWidth > textWidth) {
          cell.classList.add("m-tooltip-site");
        }
      }
    },

    /**
     * set chiều cao div fit content
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    setFitContentAssetList() {
      let heightContent = document.getElementById("asset-list").offsetHeight;
      let height = document.getElementById("tbl-asset-list").offsetHeight;
      if (height < heightContent - 42) {
        document.getElementById(
          "fit-content-asset"
        ).style.height = `calc(100% - ${height + 42}px)`;
      } else {
        document.getElementById("fit-content-asset").style.height = "0";
      }
    },

    /**
     * phân trang
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    genPagination() {
      this.totalPageIndex = Math.ceil(this.recordNum / this.pageSize);
      var current = this.pageIndex,
        last = this.totalPageIndex,
        delta = 1,
        left = current - delta,
        right = current + delta + 1,
        range = [],
        rangeWithDots = [],
        l;

      // lấy danh sách page Index
      for (let i = 1; i <= last; i++) {
        if (i == 1 || i == last || (i >= left && i < right)) {
          range.push(i);
        }
      }
      // chèn dấu '...'
      for (let i of range) {
        if (l) {
          if (i - l === 2) {
            //rangeWithDots.push(l + 1);
            rangeWithDots.push("...");
          } else if (i - l !== 1) {
            rangeWithDots.push("...");
          }
        }
        rangeWithDots.push(i);
        l = i;
      }
      if (rangeWithDots) {
        this.listIndexs = [...rangeWithDots];
      }
    },

    /**
     * gán giá trị pageIndex
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    setPageIndex(index) {
      if (index > 0 && index <= this.totalPageIndex) {
        this.pageIndex = index;
        this.openLoading();
        setTimeout(() => {
          this.assetsViews = this.assets.filter(this.filterAssetsPaging);
          setTimeout(() => {
            this.closeLoading();
            this.calculateTotal();
            this.setFitContentAssetList();
          }, 1);
        }, 500);
      }
    },

    /**
     * gán giá trị pageSize
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    setPageSize(size) {
      if (size > 0) {
        this.openLoading();
        this.pageIndex = 1;
        this.pageSize = size;
        setTimeout(() => {
          this.assetsViews = this.assets.filter(this.filterAssetsPaging);
          setTimeout(() => {
            this.closeLoading();
            this.calculateTotal();
            this.setFitContentAssetList();
            this.genPagination();
          }, 1);
        }, 500);
      }
    },

    /**
     * bật loading
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    openLoading() {
      let lst = document.querySelectorAll("#receiptForm td:not(.general td)");
      for (let i = 0; i < lst.length; i++) {
        lst[i].classList.add("skeleton");
      }
    },

    /**
     * tắt loading
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    closeLoading() {
      let lst = document.querySelectorAll("#receiptForm td:not(.general td)");
      for (let i = 0; i < lst.length; i++) {
        lst[i].classList.remove("skeleton");
      }
      let tblSite = document.querySelectorAll("#receiptForm tbl-side");
      for (let i = 0; i < tblSite.length; i++) {
        tblSite[i].scrollTop = 0;
        tblSite[i].scrollLeft = 0;
      }
    },

    /**
     * lọc array phân trang
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    filterAssetsPaging(asset) {
      let start = (this.pageIndex - 1) * this.pageSize;
      let end = start + this.pageSize;
      if (end > this.recordNum) end = this.recordNum;
      for (let i = start; i < end; i++) {
        if (asset.FixedAssetId === this.assets[i].FixedAssetId) return true;
      }
      return false;
    },

    /**
     * di chuyển form
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    moveForm(event) {
      if (event.target.classList.contains("icon-close")) return;
      event.target.style.cursor = "grabbing";
      let mouseX = event.clientX;
      let mouseY = event.clientY;
      let moveX = 0;
      let moveY = 0;
      const form = document.getElementById("receiptForm");
      document.onmousemove = function (e) {
        moveX = mouseX - e.clientX;
        moveY = mouseY - e.clientY;
        mouseX = e.clientX;
        mouseY = e.clientY;
        form.style.left = `${form.offsetLeft - moveX}px`;
        form.style.top = `${form.offsetTop - moveY}px`;
      };
      document.onmouseup = function () {
        document.onmouseup = null;
        document.onmousemove = null;
        event.target.style.cursor = "grab";
      };
    },
  },
};
</script>

<style>
.form-site.receipt-form {
  width: 965px;
}

.f-content.f-receipt-content {
  padding-bottom: 16px;
  background-color: #f4f7ff;
  display: block;
}

.form-site.receipt-form .title {
  font-family: MISARoboto-Bold;
  font-size: 14px;
  padding: 8px 0;
}

.form-site.receipt-form .data-site {
  border-radius: 4px;
  padding-top: 16px;
  display: flex;
  flex-direction: column;
  background-color: #ffffff;
}

.form-site.receipt-form .tbl-site {
  margin-top: 14px;
  height: 200px;
  overflow-y: auto;
  overflow-x: hidden;
}

.f-content.f-receipt-content #asset-list .general {
  height: 40px;
  display: flex;
  align-items: center;
  position: sticky;
  bottom: 0px;
  /* border: 1px solid #ffffff; */
  border-top: 1px solid #e5e5e5;
  z-index: 30;
  background-color: #ffffff;
  width: fit-content;
  box-sizing: content-box;
  user-select: none;
}
</style>