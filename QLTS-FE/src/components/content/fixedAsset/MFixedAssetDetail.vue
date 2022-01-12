<template>
  <div class="m-form">
    <div class="form-site" id="form">
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
      <div class="f-content">
        <div class="c-left w-1/3 m-r-16">
          <div class="row m-b-20">
            <div class="label m-b-8">
              Mã tài sản <span style="color: red">*</span>
            </div>
            <div class="m-textbox">
              <input
                type="text"
                name=""
                id=""
                v-model="faInfo.FixedAssetCode"
                ref="FixedAssetCode"
                placeholder="Nhập mã tài sản"
                tabindex="1"
                class="required"
                @focus="focusSelected('FixedAssetCode')"
                @blur="removeError('FixedAssetCode')"
                maxlength="10"
              />
            </div>
            <div class="tooltip">{{ tooltip }}</div>
          </div>
          <div class="row m-b-20">
            <div class="label m-b-8">
              Mã bộ phận sử dụng <span style="color: red">*</span>
            </div>
            <DxSelectBox
              v-model="faInfo.DepartmentId"
              height="35"
              :data-source="departments"
              value-expr="DepartmentId"
              display-expr="DepartmentCode"
              :search-enabled="true"
              :placeholder="'Chọn mã bộ phận sử dụng'"
              :noDataText="'Không có dữ liệu'"
              item-template="item"
              @value-changed="handleSelect($event, 'department')"
              :tabIndex="3"
              class="required"
              ref="DepartmentId"
              @focusIn="
                OpenSelectBox($event);
                focusSelected('DepartmentId');
              "
              @focusOut="removeError('DepartmentId')"
            >
              <template #item="{ data }">
                <div>
                  <div class="dx-template-item-data m-r-12" style="width: 60px">
                    {{ data.DepartmentCode }}
                  </div>
                  <div
                    class="dx-template-item-data"
                    style="width: calc(100% - 60px)"
                  >
                    {{ data.DepartmentName }}
                  </div>
                </div>
              </template>
            </DxSelectBox>
            <div class="tooltip">{{ tooltip }}</div>
          </div>
          <div class="row m-b-20">
            <div class="label m-b-8">
              Mã loại tài sản <span style="color: red">*</span>
            </div>
            <DxSelectBox
              v-model="faInfo.FixedAssetCategoryId"
              height="35"
              :data-source="fixedAssetCategorys"
              value-expr="FixedAssetCategoryId"
              display-expr="FixedAssetCategoryCode"
              :search-enabled="true"
              :placeholder="'Chọn mã loại tài sản'"
              :noDataText="'Không có dữ liệu'"
              item-template="item"
              @value-changed="handleSelect($event, 'fixedAssetCategory')"
              :tabIndex="4"
              class="required"
              ref="FixedAssetCategoryId"
              @focusIn="
                OpenSelectBox($event);
                focusSelected('FixedAssetCategoryId');
              "
              @focusOut="removeError('FixedAssetCategoryId')"
            >
              <template #item="{ data }">
                <div>
                  <div class="dx-template-item-data m-r-12" style="width: 60px">
                    {{ data.FixedAssetCategoryCode }}
                  </div>
                  <div
                    class="dx-template-item-data"
                    style="width: calc(100% - 60px)"
                  >
                    {{ data.FixedAssetCategoryName }}
                  </div>
                </div>
              </template>
            </DxSelectBox>
            <div class="tooltip">{{ tooltip }}</div>
          </div>
          <div class="row m-b-20">
            <div class="label m-b-8">
              Số lượng <span style="color: red">*</span>
            </div>
            <div class="m-textbox number">
              <input
                type="text"
                name=""
                id=""
                class="text-right required"
                v-model="faInfo.Quantity"
                @keypress="validate($event)"
                @keyup="faInfo.Quantity = formatCurrency(faInfo.Quantity)"
                tabindex="5"
                ref="Quantity"
                @focus="focusSelected('Quantity')"
                @blur="removeError('Quantity')"
              />
              <div class="number-side">
                <div
                  class="icon-14 icon-increase"
                  @click="increaseNum('Quantity')"
                ></div>
                <div
                  class="icon-14 icon-reduction"
                  @click="reductionNum('Quantity')"
                ></div>
              </div>
            </div>
            <div class="tooltip">{{ tooltip }}</div>
          </div>
          <div class="row m-b-20">
            <div class="label m-b-8">
              Ngày mua <span style="color: red">*</span>
            </div>
            <DxDateBox
              v-model="faInfo.PurchaseDate"
              class="required"
              ref="PurchaseDate"
              :show-clear-button="true"
              :use-mask-behavior="true"
              :value="new Date()"
              placeholder="dd/mm/yyyy"
              :display-format="typeDate"
              type="date"
              :tabIndex="8"
              @focusOut="removeError('PurchaseDate')"
            />
            <div class="tooltip">{{ tooltip }}</div>
          </div>
          <div class="row m-b-20">
            <div class="label m-b-8">
              Số năm sử dụng <span style="color: red">*</span>
            </div>
            <div class="m-textbox number">
              <input
                type="text"
                name=""
                id=""
                class="text-right required"
                v-model="faInfo.LifeTime"
                @keypress="validate($event)"
                @keyup="faInfo.LifeTime = formatCurrency(faInfo.LifeTime)"
                tabindex="10"
                ref="LifeTime"
                @focus="focusSelected('LifeTime')"
                @blur="removeError('LifeTime')"
              />
              <div class="number-side">
                <div
                  class="icon-14 icon-increase"
                  @click="increaseNum('LifeTime')"
                ></div>
                <div
                  class="icon-14 icon-reduction"
                  @click="reductionNum('LifeTime')"
                ></div>
              </div>
            </div>
            <div class="tooltip">{{ tooltip }}</div>
          </div>
        </div>
        <div class="c-right w-2/3">
          <div class="row m-b-20">
            <div class="label m-b-8">
              Tên tài sản <span style="color: red">*</span>
            </div>
            <div class="m-textbox">
              <input
                type="text"
                name=""
                id=""
                v-model="faInfo.FixedAssetName"
                placeholder="Nhập tên tài sản"
                tabindex="2"
                class="required"
                ref="FixedAssetName"
                @focus="focusSelected('FixedAssetName')"
                @blur="removeError('FixedAssetName')"
              />
            </div>
            <div class="tooltip">{{ tooltip }}</div>
          </div>
          <div class="row m-b-20">
            <div class="label m-b-8">Tên bộ phận sử dụng</div>
            <div class="m-textbox disable">
              <input
                type="text"
                name=""
                id=""
                disabled
                v-model="faInfo.DepartmentName"
              />
            </div>
          </div>
          <div class="row m-b-20">
            <div class="label m-b-8">Tên loại tài sản</div>
            <div class="m-textbox disable">
              <input
                type="text"
                name=""
                id=""
                disabled
                v-model="faInfo.FixedAssetCategoryName"
              />
            </div>
          </div>
          <div class="row m-b-20 dis-flex">
            <div class="w-1/2 m-r-16">
              <div class="label m-b-8">
                Nguyên giá <span style="color: red">*</span>
              </div>
              <div class="m-textbox" :class="isInputDisable ? 'disable' : ''">
                <input
                  type="text"
                  name=""
                  id=""
                  :disabled="isInputDisable ? true : false"
                  class="text-right required"
                  v-model="faInfo.Cost"
                  @keypress="validate($event)"
                  @keyup="
                    (faInfo.Cost = formatCurrency(faInfo.Cost)),
                      setDepreciationYear()
                  "
                  tabindex="6"
                  ref="Cost"
                  @focus="focusSelected('Cost')"
                  @blur="removeError('Cost')"
                />
              </div>
              <div class="tooltip">{{ tooltip }}</div>
            </div>
            <div class="w-1/2">
              <div class="label m-b-8">
                Tỷ lệ hao mòn (%) <span style="color: red">*</span>
              </div>
              <div class="m-textbox">
                <input
                  type="text"
                  name=""
                  id=""
                  class="text-right required"
                  v-model="faInfo.DepreciationRate"
                  @keypress="validate($event, 1)"
                  @keyup="
                    (faInfo.DepreciationRate = formatDecimal(
                      faInfo.DepreciationRate
                    )),
                      setDepreciationYear()
                  "
                  tabindex="7"
                  ref="DepreciationRate"
                  @focus="focusSelected('DepreciationRate')"
                  @blur="removeError('DepreciationRate')"
                  maxlength="5"
                />
              </div>
              <div class="tooltip">{{ tooltip }}</div>
            </div>
          </div>
          <div class="row m-b-20 dis-flex">
            <div class="w-1/2 m-r-16">
              <div class="label m-b-8">
                Ngày bắt đầu sử dụng <span style="color: red">*</span>
              </div>
              <DxDateBox
                v-model="faInfo.UseDate"
                class="required"
                ref="UseDate"
                :show-clear-button="true"
                :use-mask-behavior="true"
                :value="new Date()"
                placeholder="dd/mm/yyyy"
                :display-format="typeDate"
                type="date"
                :tabIndex="9"
                @focusOut="removeError('UseDate')"
              />
              <div class="tooltip">{{ tooltip }}</div>
            </div>
            <div class="w-1/2">
              <div class="label m-b-8">Năm theo dõi</div>
              <div class="m-textbox disable">
                <input
                  type="text"
                  name=""
                  id=""
                  disabled
                  class="text-right"
                  v-model="faInfo.TrackedYear"
                />
              </div>
            </div>
          </div>
          <div class="row m-b-20 dis-flex">
            <div class="w-1/2 m-r-16">
              <div class="label m-b-8">
                Giá trị hao mòn năm <span style="color: red">*</span>
              </div>
              <div class="m-textbox">
                <input
                  type="text"
                  name=""
                  id=""
                  class="text-right required"
                  v-model="faInfo.DepreciationYear"
                  @keypress="validate($event)"
                  @keyup="
                    (faInfo.DepreciationYear = formatCurrency(
                      faInfo.DepreciationYear
                    )),
                      setDepreciationRate()
                  "
                  tabindex="11"
                  ref="DepreciationYear"
                  @focus="focusSelected('DepreciationYear')"
                  @blur="removeError('DepreciationYear')"
                />
              </div>
              <div class="tooltip">{{ tooltip }}</div>
            </div>
            <div class="w-1/2"></div>
          </div>
        </div>
      </div>
      <div class="f-footer">
        <div></div>
        <div class="button-field dis-flex">
          <button
            class="m-btn m-btn-outline m-r-16"
            @click="closeForm(null)"
            tabindex="12"
          >
            <div class="label">Hủy</div>
          </button>
          <button
            class="m-btn m-tooltip-site"
            @click="saveData()"
            tabindex="12"
            v-shortkey="['ctrl', 's']"
            @shortkey="saveData()"
          >
            <div class="label">Lưu</div>
            <Tooltip :content="'Lưu (Ctrl + S)'" />
          </button>
        </div>
      </div>
    </div>
    <Dialog
      :dialogMode="dialogMode"
      :itemCode="faInfo.FixedAssetCode"
      :nameRequired="nameRequired"
      v-if="this.$store.getters.IsShowDialogForm"
      @funct="saveData()"
      @required="focusToError()"
    />
    <Loading v-if="isLoading" />
  </div>
</template>

<script>
import DxSelectBox from "devextreme-vue/select-box";
import Loading from "../../base/MLoading.vue";
import axios from "axios";
import moment from "moment";
import Dialog from "../../base/MDialog.vue";
import Tooltip from "../../base/MTooltip.vue";
import DxDateBox from "devextreme-vue/date-box";
export default {
  props: ["id", "mode"],
  components: {
    DxSelectBox,
    Loading,
    Dialog,
    Tooltip,
    DxDateBox,
  },
  data() {
    return {
      tooltip: "Không được bỏ trống!",
      typeDate: this.$store.getters.TypeDate,
      hostApi: this.$store.getters.HostApi,
      endPoint: "FixedAssets/",
      isLoading: false,
      departments: [],
      fixedAssetCategorys: [],
      faInfo: {
        FixedAssetCode: "",
        FixedAssetName: "",
        DepartmentId: "",
        DepartmentCode: null,
        DepartmentName: null,
        FixedAssetCategoryId: "",
        FixedAssetCategoryCode: null,
        FixedAssetCategoryName: null,
        PurchaseDate: moment(new Date()).format("YYYY-MM-DD"),
        Cost: 0,
        Quantity: 0,
        DepreciationRate: 0,
        TrackedYear: new Date().getFullYear(),
        LifeTime: 0,
        ProductionYear: 0,
        UseDate: moment(new Date()).format("YYYY-MM-DD"),
        Active: false,
        CreatedDate: null,
        CreatedBy: null,
        ModifiedDate: null,
        ModifiedBy: null,
        DepreciationYear: 0,
      },
      method: "",
      dialogMode: "",
      nameRequired: "",
      focusRequired: "",
      temp: 0,
      faInfoClone: null,
      titleForm: "",
      isInputDisable: false,
    };
  },
  async created() {
    this.setFixedAssetCategory();
    this.setDepartment();
    this.method = this.$store.getters.Method.Add;
    if (this.id && this.mode) {
      await this.bindData();
      await this.bindCode();
      this.$store.commit("setToastContent", this.$store.getters.Toast.Add);
      this.titleForm = this.$store.getters.TitleForm.Replication;
    } else if (this.id) {
      this.isInputDisable = true;
      this.bindData();
      this.method = this.$store.getters.Method.Edit;
      this.endPoint += this.id;
      this.titleForm = this.$store.getters.TitleForm.Edit;
      this.$store.commit("setToastContent", this.$store.getters.Toast.Edit);
    } else {
      this.bindCode();
      this.titleForm = this.$store.getters.TitleForm.Add;
      this.$store.commit("setToastContent", this.$store.getters.Toast.Add);
    }
  },
  mounted() {
    // this.setTabIndex();
  },
  beforeDestroy() {},
  methods: {
    /**
     * xổ dropdown
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    OpenSelectBox(e) {
      const needOpen =
        !e.component._popup || e.component._popup.option("visible") == false;
      if (needOpen) {
        setTimeout(() => e.component.open(), 200);
      }
    },

    /**
     * validate các trường bắt buộc
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    validateRequired() {
      let flag = true;
      let inputVal = "";
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
            flag = false;
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
            flag = false;
            count++;
            if (count == 1) {
              this.focusRequired = requireds[i];
            }
          }
        }
      }
      return flag;
    },

    /**
     * xử lý nghiệp vụ
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    validateMajor() {
      let tCost = parseInt((this.faInfo.Cost + "").replaceAll(".", ""));
      let tRate = parseFloat(
        ("" + this.faInfo.DepreciationRate).replaceAll(",", ".")
      ).toFixed(2);
      let tLife = parseInt((this.faInfo.LifeTime + "").replaceAll(".", ""));
      let tDYear = parseInt(
        (this.faInfo.DepreciationYear + "").replaceAll(".", "")
      );
      if (tLife !== 0 && tRate !== (100 / tLife).toFixed(2)) {
        this.$refs.DepreciationRate.classList.add("error");
        this.focusRequired = this.$refs.DepreciationRate;
        this.dialogMode = this.$store.getters.DialogMode.FixRate;
        this.$store.commit("setIsShowDialogForm", true);
        return false;
      }
      if (tDYear > tCost) {
        this.$refs.DepreciationYear.classList.add("error");
        this.focusRequired = this.$refs.DepreciationYear;
        this.dialogMode = this.$store.getters.DialogMode.FixDYear;
        this.$store.commit("setIsShowDialogForm", true);
        return false;
      }
      return true;
    },

    /**
     * validate Date
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    validateDate() {
      if (this.faInfo.PurchaseDate > this.faInfo.UseDate) {
        this.dialogMode = this.$store.getters.DialogMode.ErrorDate;
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
        case "DepartmentId":
          requireds[1].classList.remove("error");
          tooltip[1].style.display = "none";

          break;
        case "FixedAssetCategoryId":
          requireds[2].classList.remove("error");
          tooltip[2].style.display = "none";

          break;
        case "PurchaseDate":
          requireds[4].classList.remove("error");
          tooltip[4].style.display = "none";

          break;
        case "UseDate":
          requireds[9].classList.remove("error");
          tooltip[9].style.display = "none";

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
     * set tabindex date picker
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    setTabIndex() {
      let dpicker = document.querySelectorAll(".mx-input");
      dpicker[0].tabIndex = "8";
      dpicker[1].tabIndex = "9";
    },

    /**
     * đóng form
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    closeForm(check) {
      this.nameRequired = "";
      if (
        JSON.stringify(this.faInfoClone) !== JSON.stringify(this.faInfo) &&
        check !== null
      ) {
        if (this.method === this.$store.getters.Method.Add) {
          this.dialogMode = this.$store.getters.DialogMode.CancelAdd;
          this.$store.commit("setIsShowDialogForm", true);
        } else {
          this.dialogMode = this.$store.getters.DialogMode.Changed;
          this.$store.commit("setIsShowDialogForm", true);
        }
      } else {
        this.$store.commit("setIsShowForm", false);
      }
    },

    /**
     * lưu dữ liệu
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    saveData() {
      if (!this.validateRequired()) {
        this.focusToError();
        return;
      }
      if (!this.validateMajor()) return;
      if (!this.validateDate()) return;
      this.beforeSave();
      const config = {
        url: this.hostApi + this.endPoint,
        method: this.method,
        data: this.faInfo,
      };
      axios
        .request(config)
        .then(() => {
          this.isLoading = false;
          this.faInfoClone = { ...this.faInfo };
          this.closeForm(null);
          this.$emit("loadData");
          this.$store.commit("setIsShowToast", true);
        })
        .catch((error) => {
          this.setFaInfoClone();
          const validateInfo = error.response.data.ValidateInfo;
          this.isLoading = false;
          // check duplicate
          if (validateInfo && validateInfo.length) {
            validateInfo.forEach((ele) => {
              if (
                ele.column === "FixedAssetCode" &&
                ele.state === "duplicate"
              ) {
                this.dialogMode = this.$store.getters.DialogMode.Duplicate;
                this.$store.commit("setIsShowDialogForm", true);
                this.focusRequired = this.$refs.FixedAssetCode;
              }
            });
          }
        });
    },

    /**
     * event trước khi lưu
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    beforeSave() {
      this.isLoading = true;
      this.faInfo.Quantity = parseInt(
        (this.faInfo.Quantity + "").replaceAll(".", "")
      );
      this.faInfo.Cost = parseInt((this.faInfo.Cost + "").replaceAll(".", ""));
      this.faInfo.DepreciationRate = parseFloat(
        ("" + this.faInfo.DepreciationRate).replaceAll(",", ".")
      );
      this.faInfo.LifeTime = parseInt(
        (this.faInfo.LifeTime + "").replaceAll(".", "")
      );
      this.faInfo.DepreciationYear = parseInt(
        (this.faInfo.DepreciationYear + "").replaceAll(".", "")
      );
      this.faInfoClone = { ...this.faInfo };
    },

    /**
     * tính giá trị hao mòn năm
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    setDepreciationYear() {
      let tmpDR = this.faInfo.DepreciationRate + "";
      tmpDR = tmpDR.replaceAll(",", ".");
      this.faInfo.DepreciationYear = (
        0.01 *
        this.formatNum(this.faInfo.Cost + "") *
        parseFloat(tmpDR)
      ).toFixed(0);
      this.faInfo.DepreciationYear = this.formatCurrency(
        this.faInfo.DepreciationYear + ""
      );
    },

    /**
     * tính tỉ lệ hao mòn
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    setDepreciationRate() {
      if (
        this.faInfo.Cost == 0 ||
        this.faInfo.Cost == "0" ||
        this.faInfo.Cost == ""
      )
        return;
      this.faInfo.DepreciationRate = (
        (100 * this.formatNum(this.faInfo.DepreciationYear + "")) /
        this.formatNum(this.faInfo.Cost + "")
      )
        .toFixed(2)
        .replaceAll(".", ",");
      this.faInfo.DepreciationRate = this.formatDecimal(
        this.faInfo.DepreciationRate + ""
      );
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
     * format số thập phân
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    formatDecimal(value) {
      if (value == null || value == 0 || value == "") return "0";
      value = value.replace(/[^0-9-|,]+/g, "");
      if (value.charAt(0) === "0" && value.charAt(1) !== ",") {
        return this.formatDecimal(value.slice(1) + "");
      }
      let count = 0;
      for (let i = 0; i < value.length; i++) {
        if (value.charAt(i) === ",") {
          count++;
          if (count > 1) {
            value = value.slice(0, i) + value.slice(i + 1);
            return this.formatDecimal(value + "");
          }
        }
      }
      let num = value;
      return num;
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
          this.faInfo.FixedAssetCode = res.data.Data;
          this.$refs.FixedAssetCode.focus();
          this.faInfoClone = { ...this.faInfo };
        })
        .catch((error) => {
          this.isLoading = false;
          console.log(error);
        });
    },

    /**
     * event sau khi binding
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    setFaInfoClone() {
      this.faInfo.DepreciationYear = (
        this.faInfo.Cost *
        this.faInfo.DepreciationRate *
        0.01
      ).toFixed(0);
      this.$refs.FixedAssetCode.focus();
      this.faInfo.Quantity = this.formatCurrency(this.faInfo.Quantity + "");
      this.faInfo.Cost = this.formatCurrency(this.faInfo.Cost + "");
      this.faInfo.DepreciationRate = this.formatDecimal(
        ("" + this.faInfo.DepreciationRate).replaceAll(".", ",")
      );
      this.faInfo.LifeTime = this.formatCurrency(this.faInfo.LifeTime + "");
      this.faInfo.DepreciationYear = this.formatCurrency(
        this.faInfo.DepreciationYear + ""
      );
      this.faInfoClone = { ...this.faInfo };
    },
    /**
     * gán dữ liệu
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    async bindData() {
      this.isLoading = true;
      let urlApi = `${this.hostApi}${this.endPoint}${this.id}`;
      await axios
        .get(urlApi)
        .then((res) => {
          this.isLoading = false;
          this.faInfo = res.data.Data;
          this.setDepreciationYear();
          this.setFaInfoClone();
        })
        .catch(() => {
          this.isLoading = false;
          this.dialogMode = this.$store.getters.DialogMode.GetByIdFail;
          console.log(this.dialogMode);
          this.$store.commit("setIsShowDialogForm", true);
        });
      if (this.faInfoClone != null) {
        this.faInfo.DepreciationRate = this.faInfoClone.DepreciationRate;
        this.faInfo.LifeTime = this.faInfoClone.LifeTime;
      }
    },

    /**
     * gán index để set select dropdown
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    handleSelect(e, type) {
      if (!e) {
        return;
      }
      let payload = e.element.__vue__.$_innerChanges.selectedItem;
      if (type === "department") {
        this.faInfo.DepartmentName = payload?.DepartmentName;
      } else {
        this.faInfo.FixedAssetCategoryName = payload?.FixedAssetCategoryName;
        if (payload) {
          payload.DepreciationRate = payload.DepreciationRate
            ? payload.DepreciationRate
            : 0;

          this.faInfo.DepreciationRate = this.formatDecimal(
            ("" + payload.DepreciationRate.toFixed(2)).replaceAll(".", ",")
          );
          payload.LifeTime = payload.LifeTime ? payload.LifeTime : 0;
          this.faInfo.LifeTime = this.formatCurrency(payload.LifeTime + "");
        }
      }
    },
    /**
     * gán dữ liệu cho dropdown Phòng ban
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    setDepartment() {
      this.departments = [...this.$store.getters.Departments];
      this.departments.unshift({
        DepartmentCode: "Mã",
        DepartmentName: "Tên",
        disabled: true,
      });
    },

    /**
     * gán dữ liệu cho dropdown Loại tài sản
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    setFixedAssetCategory() {
      this.fixedAssetCategorys = [...this.$store.getters.FixedAssetCategorys];
      this.fixedAssetCategorys.unshift({
        FixedAssetCategoryCode: "Mã",
        FixedAssetCategoryName: "Tên",
        disabled: true,
      });
    },

    /**
     * handler key number
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    validate(evt, type) {
      var regex = /[0-9]/g;
      if (type) {
        regex = /[0-9|,]/g;
      }
      var theEvent = evt || window.event;

      // Handle paste
      if (theEvent.type === "paste") {
        key = event.clipboardData.getData("text/plain");
      } else {
        // Handle key press
        var key = theEvent.keyCode || theEvent.which;
        key = String.fromCharCode(key);
      }
      if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault) theEvent.preventDefault();
      }
    },

    /**
     * reset text
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    focusSelected(refName) {
      if (refName == "DepartmentId" || refName == "FixedAssetCategoryId") {
        this.$refs[refName].$el.querySelector(".dx-texteditor-input").select();
      } else {
        this.$refs[refName].select();
      }
    },

    /**
     * sự kiện tăng number
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    increaseNum(name) {
      let temp;
      switch (name) {
        case "LifeTime":
          temp = parseInt(this.formatNum(this.faInfo.LifeTime + "")) + 1;
          this.faInfo.LifeTime = this.formatCurrency(temp + "");
          break;
        case "Quantity":
          temp = parseInt(this.formatNum(this.faInfo.Quantity + "")) + 1;
          this.faInfo.Quantity = this.formatCurrency(temp + "");
          break;

        default:
          break;
      }
    },

    /**
     * sự kiện giảm number
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    reductionNum(name) {
      let temp;
      switch (name) {
        case "LifeTime":
          temp = parseInt(this.formatNum(this.faInfo.LifeTime + "")) - 1;
          if (temp < 0) return;
          this.faInfo.LifeTime = this.formatCurrency(temp + "");
          break;
        case "Quantity":
          temp = parseInt(this.formatNum(this.faInfo.Quantity + "")) - 1;
          if (temp < 0) return;
          this.faInfo.Quantity = this.formatCurrency(temp + "");
          break;

        default:
          break;
      }
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
      const form = document.getElementById("form");
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
.m-form {
  display: flex;
  align-items: center;
  justify-content: center;
  top: 0;
  left: 0;
  position: absolute;
  width: 100%;
  height: 100%;
  z-index: 50;
  background-color: rgba(0, 0, 0, 0.6);
}

.m-form .form-site {
  min-width: 750px;
  width: 65%;
  background-color: #ffffff;
  height: auto;
  border-radius: 4px;
  position: absolute;
  height: fit-content;
}

.form-site .f-header {
  padding: 16px;
  position: relative;
  display: flex;
  align-items: center;
  justify-content: space-between;
  cursor: grab;
  user-select: none;
}

.f-header .f-title {
  font-size: 17px;
  font-family: MISARoboto-Bold;
}

.f-header .icon-close {
  border-radius: 4px;
  cursor: pointer;
}

.f-header .icon-close:hover {
  background-color: #e5e5e5;
}
.f-header .icon-close:active {
  background-color: #f5f5f5;
}

.form-site .f-content {
  display: flex;
  padding: 0 16px;
}

.form-site .f-footer {
  height: 52px;
  display: flex;
  align-items: center;
  justify-content: right;
  background-color: #f5f5f5;
  border-radius: 0 0 4px 4px;
  padding: 0 16px;
}

.m-form .label {
  text-overflow: ellipsis;
  white-space: nowrap;
  overflow: hidden;
}

.m-form .error {
  border: 1px solid red !important;
}

.m-form .row {
  position: relative;
}
</style>