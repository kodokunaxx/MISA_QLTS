<template>
  <div class="m-form">
    <div class="form-site">
      <div class="f-header">
        <div class="f-title">THÔNG TIN NHÂN VIÊN</div>
        <div class="icon-24 icon-close" @click="closeForm('check')"></div>
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
                tabindex="1"
                class="required"
                @focus="removeError('FixedAssetCode')"
              />
            </div>
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
              :placeholder="'Bộ phận sử dụng'"
              :noDataText="'Không có dữ liệu'"
              item-template="item"
              @value-changed="handleSelect($event, 'department')"
              :tabIndex="3"
              class="required"
              ref="DepartmentId"
              @focusIn="removeError('DepartmentId')"
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
              :placeholder="'Loại tài sản'"
              :noDataText="'Không có dữ liệu'"
              item-template="item"
              @value-changed="handleSelect($event, 'fixedAssetCategory')"
              :tabIndex="4"
              class="required"
              ref="FixedAssetCategoryId"
              @focusIn="removeError('FixedAssetCategoryId')"
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
          </div>
          <div class="row m-b-20">
            <div class="label m-b-8">
              Số lượng <span style="color: red">*</span>
            </div>
            <div class="m-textbox">
              <input
                type="text"
                name=""
                id=""
                class="text-right required"
                v-model="faInfo.Quantity"
                @keyup="faInfo.Quantity = formatCurrency(faInfo.Quantity)"
                tabindex="5"
                ref="Quantity"
                @focus="removeError('Quantity')"
              />
            </div>
          </div>
          <div class="row m-b-20">
            <div class="label m-b-8">
              Ngày mua <span style="color: red">*</span>
            </div>
            <date-picker
              v-model="faInfo.PurchaseDate"
              type="date"
              placeholder="DD/MM/YYYY"
              :format="'DD/MM/YYYY'"
              :value-type="'YYYY-MM-DD'"
              :default-value="new Date()"
              class="required"
              ref="PurchaseDate"
              @focus="removeError('PurchaseDate')"
            ></date-picker>
          </div>
          <div class="row m-b-20">
            <div class="label m-b-8">
              Số năm sử dụng <span style="color: red">*</span>
            </div>
            <div class="m-textbox">
              <input
                type="text"
                name=""
                id=""
                class="text-right required"
                v-model="faInfo.LifeTime"
                @keyup="faInfo.LifeTime = formatCurrency(faInfo.LifeTime)"
                tabindex="10"
                ref="LifeTime"
                @focus="removeError('LifeTime')"
              />
            </div>
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
                tabindex="2"
                class="required"
                ref="FixedAssetName"
                @focus="removeError('FixedAssetName')"
              />
            </div>
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
              <div class="m-textbox">
                <input
                  type="text"
                  name=""
                  id=""
                  class="text-right required"
                  v-model="faInfo.Cost"
                  @keyup="
                    (faInfo.Cost = formatCurrency(faInfo.Cost)),
                      setDepreciationYear()
                  "
                  tabindex="6"
                  ref="Cost"
                  @focus="removeError('Cost')"
                />
              </div>
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
                  @keyup="
                    (faInfo.DepreciationRate = formatDecimal(
                      faInfo.DepreciationRate
                    )),
                      setDepreciationYear()
                  "
                  tabindex="7"
                  ref="DepreciationRate"
                  @focus="removeError('DepreciationRate')"
                />
              </div>
            </div>
          </div>
          <div class="row m-b-20 dis-flex">
            <div class="w-1/2 m-r-16">
              <div class="label m-b-8">
                Ngày bắt đầu sử dụng <span style="color: red">*</span>
              </div>
              <date-picker
                v-model="faInfo.UseDate"
                type="date"
                placeholder="DD/MM/YYYY"
                :format="'DD/MM/YYYY'"
                :value-type="'YYYY-MM-DD'"
                :default-value="new Date()"
                class="required"
                ref="UseDate"
                @focus="removeError('UseDate')"
              ></date-picker>
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
                  @keyup="
                    (faInfo.DepreciationYear = formatCurrency(
                      faInfo.DepreciationYear
                    )),
                      setDepreciationRate()
                  "
                  tabindex="11"
                  ref="DepreciationYear"
                  @focus="removeError('DepreciationYear')"
                />
              </div>
            </div>
            <div class="w-1/2"></div>
          </div>
        </div>
      </div>
      <div class="f-footer">
        <div></div>
        <div class="button-field dis-flex">
          <button class="m-btn m-btn-outline m-r-8" @click="closeForm(null)">
            <div class="label">Hủy</div>
          </button>
          <button class="m-btn" @click="saveData()">
            <div class="label">Lưu</div>
          </button>
        </div>
      </div>
    </div>
    <Dialog
      :dialogMode="dialogMode"
      :faCode="faInfo.FixedAssetCode"
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
import Loading from "../base/MLoading.vue";
import axios from "axios";
import moment from "moment";
import Dialog from "../base/MDialog.vue";
import DatePicker from "vue2-datepicker";
export default {
  props: ["id", "mode"],
  components: {
    DxSelectBox,
    Loading,
    Dialog,
    DatePicker,
  },
  data() {
    return {
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
        Active: true,
        CreatedDate: null,
        CreatedBy: null,
        ModifiedDate: null,
        ModifiedBy: null,
        DepreciationYear: 0,
        method: "",
        dialogMode: "",
        nameRequired: "",
        focusRequired: "",
      },
      temp: 0,
      faInfoClone: null,
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
    } else if (this.id) {
      this.bindData();
      this.method = this.$store.getters.Method.Edit;
      this.endPoint += this.id;

      this.$store.commit("setToastContent", this.$store.getters.Toast.Edit);
    } else {
      this.bindCode();

      this.$store.commit("setToastContent", this.$store.getters.Toast.Add);
    }
  },
  mounted() {
    this.setTabIndex();
  },
  beforeDestroy() {},
  methods: {
    /**
     * validate các trường bắt buộc
     */
    validateRequired() {
      let flag = true;
      let inputVal = "";
      let name = "";
      let count = 0;
      this.nameRequired = "";
      let requireds = document.querySelectorAll(".required");
      for (let i = 0; i < requireds.length; i++) {
        if (i == 1 || i == 2 || i == 4 || i == 9) {
          inputVal = requireds[i].querySelector("input").value;
          if (inputVal == null || inputVal == "") {
            requireds[i].classList.add("error");
            flag = false;
            name =
              requireds[i].parentElement.querySelector(".label").textContent;
            this.nameRequired += name.slice(0, name.length - 1) + ",";
            count++;
            if (count == 1) {
              this.focusRequired = requireds[i].querySelector("input");
            }
          }
        } else {
          inputVal = requireds[i].value;
          if (inputVal == null || inputVal == "") {
            requireds[i].classList.add("error");
            flag = false;
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
      return flag;
    },

    /**
     * xử lý nghiệp vụ
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
     * xóa error khi focus
     */
    removeError(refName) {
      let requireds = document.querySelectorAll(".required");
      if (refName == "DepartmentId") {
        requireds[1].classList.remove("error");
      } else if (refName == "FixedAssetCategoryId") {
        requireds[2].classList.remove("error");
      } else {
        this.$refs[refName].classList.remove("error");
      }
    },

    /**
     * focus vào lỗi
     */
    focusToError() {
      this.focusRequired.focus();
    },

    /**
     * set tabindex date picker
     */
    setTabIndex() {
      let dpicker = document.querySelectorAll(".mx-input");
      dpicker[0].tabIndex = "8";
      dpicker[1].tabIndex = "9";
    },

    /**
     * đóng form
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
     */
    saveData() {
      if (!this.validateRequired()) {
        this.dialogMode = this.$store.getters.DialogMode.Required;
        this.$store.commit("setIsShowDialogForm", true);
        return;
      }
      if (!this.validateMajor()) return;
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
              }
            });
          }
        });
    },

    /**
     * event trước khi lưu
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
     */
    formatNum(value) {
      if (value == null || value == 0 || value == "") return "0";
      let num = value.replace(/[^0-9-]+/g, "");
      return parseInt(num);
    },

    /**
     * format tiền
     */
    formatCurrency(value) {
      if (value == null || value == 0 || value == "") return "0";
      let num = this.formatNum(value + "") + "";
      return num.replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    },

    /**
     * format số thập phân
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
          console.log(error);
        });
    },

    /**
     * event sau khi binding
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
     */
    handleSelect(e, type) {
      if (!e) {
        return;
      }
      let payload = e.element.__vue__.$_innerChanges.selectedItem;
      if (type === "department") {
        this.faInfo.DepartmentName = payload.DepartmentName;
      } else {
        this.faInfo.FixedAssetCategoryName = payload.FixedAssetCategoryName;
        payload.DepreciationRate = payload.DepreciationRate
          ? payload.DepreciationRate
          : 0;
        this.faInfo.DepreciationRate = this.formatDecimal(
          ("" + payload.DepreciationRate.toFixed(2)).replaceAll(".", ",")
        );
        payload.LifeTime = payload.LifeTime ? payload.LifeTime : 0;
        this.faInfo.LifeTime = this.formatCurrency(payload.LifeTime + "");
      }
    },
    /**
     * gán dữ liệu cho dropdown Phòng ban
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
     */
    setFixedAssetCategory() {
      this.fixedAssetCategorys = [...this.$store.getters.FixedAssetCategorys];
      this.fixedAssetCategorys.unshift({
        FixedAssetCategoryCode: "Mã",
        FixedAssetCategoryName: "Tên",
        disabled: true,
      });
    },
  },
};
</script>

<style>
.m-form {
  display: flex;
  align-items: center;
  top: 0;
  left: 0;
  position: absolute;
  width: 100%;
  height: 100%;
  z-index: 50;
  background-color: rgba(0, 0, 0, 0.6);
}

.m-form .form-site {
  margin: auto;
  min-width: 750px;
  width: 65%;
  background-color: #ffffff;
  height: auto;
  border-radius: 4px;
}

.form-site .f-header {
  padding: 20px 16px;
  position: relative;
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.f-header .f-title {
  font-size: 15px;
  font-family: MISAGoogleSans-Bold;
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
</style>