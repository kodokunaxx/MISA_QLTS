<template>
  <div class="m-form">
    <div class="form-site asset-resource-form" id="assetResourceForm">
      <div class="f-header" @mousedown="moveForm($event)">
        <div class="f-title">{{ titleForm }}</div>
        <div class="icon-24 icon-close" @click="closeChooseForm()"></div>
      </div>
      <div class="f-content asset-resource-content">
        <div class="w-4/5 m-b-20">
          <div class="label m-b-8" style="font-family: MISARoboto-Bold">
            Bộ phận sử dụng
          </div>
          <div class="field-disable">
            {{ departmentNameAsset }}
          </div>
        </div>
        <div class="m-b-8" style="font-family: MISARoboto-Bold">Nguyên giá</div>
        <div class="m-b-8 dis-flex align-items-center">
          <div class="w-4/5 dis-flex align-items-center m-r-16">
            <div class="w-3/5 m-r-16">Nguồn hình thành</div>
            <div class="w-2/5">Giá trị</div>
          </div>
        </div>
        <div class="field-row">
          <div
            class="m-b-20 dis-flex align-items-center"
            v-for="(assetResource, index) in assetResources"
            :key="index"
          >
            <div class="w-4/5 dis-flex align-items-center m-r-16">
              <div class="w-3/5 m-r-16" style="position: relative">
                <DxSelectBox
                  v-model="assetResource.ResourceId"
                  height="35"
                  :data-source="resources"
                  value-expr="ResourceId"
                  display-expr="ResourceName"
                  :search-enabled="false"
                  :placeholder="'Chọn nguồn hình thành'"
                  :noDataText="'Không có dữ liệu'"
                  item-template="item"
                  class="required"
                  :ref="'ResourceId' + index"
                  @focusIn="OpenSelectBox($event)"
                  @focusOut="removeError('ResourceId' + index)"
                >
                </DxSelectBox>
                <div class="tooltip">{{ tooltip }}</div>
                <div class="tooltip-duplicate">{{ tooltipDuplicate }}</div>
              </div>
              <div class="w-2/5" style="position: relative">
                <div class="m-textbox">
                  <input
                    type="text"
                    v-model="assetResource.Budget"
                    :ref="'Budget' + index"
                    placeholder="Nhập giá trị"
                    class="required text-right"
                    @focus="focusSelected('Budget' + index)"
                    @blur="removeError('Budget' + index)"
                    @keypress="validate($event)"
                    @keyup="
                      assetResource.Budget = formatCurrency(
                        assetResource.Budget
                      );
                      caculateBudget();
                    "
                  />
                </div>
                <div class="tooltip">{{ tooltip }}</div>
              </div>
            </div>
            <div
              class="icon-24 action pointer icon-add-row m-r-16 m-tooltip-site"
              @click="addRow()"
            >
              <Tooltip :content="'Thêm nguồn kinh phí'" :left="'-100%'" />
            </div>
            <div
              class="icon-24 pointer icon-remove-row"
              :class="
                assetResources.length <= 1
                  ? 'disable-icon'
                  : 'm-tooltip-site  action'
              "
              @click="removeRow(index)"
            >
              <Tooltip :content="'Xóa bỏ'" />
            </div>
          </div>
        </div>
        <div class="w-4/5 dis-flex align-items-center m-b-20">
          <div class="w-3/5 field-disable m-r-16">Tổng</div>
          <div class="w-2/5 field-disable text-right">
            {{ totalBudget | handleNumer(totalBudget) }}
          </div>
        </div>
      </div>
      <div class="f-footer">
        <div class="button-field dis-flex">
          <button class="m-btn m-btn-outline m-r-16" @click="closeChooseForm()">
            <div class="label">Hủy</div>
          </button>
          <button class="m-btn" @click="saveData()">
            <div class="label">Lưu</div>
          </button>
        </div>
      </div>
    </div>
    <Loading v-if="isLoading" />
  </div>
</template>

<script>
import axios from "axios";
import Tooltip from "../../base/MTooltip.vue";
import Loading from "../../base/MLoading.vue";
import DxSelectBox from "devextreme-vue/select-box";
export default {
  props: ["idAsset", "nameAsset", "departmentNameAsset"],
  components: { DxSelectBox, Loading, Tooltip },
  data() {
    return {
      titleForm: `Sửa tài sản ${this.nameAsset}`,
      tooltip: "Không được bỏ trống!",
      tooltipDuplicate: "Nguồn chi phí đã tồn tại",
      resources: [],
      assetResources: [],
      hostApi: this.$store.getters.HostApi,
      endPoint: "AssetResources/",
      isLoading: false,
      tempAssetResource: {
        AssetResourceId: null,
        FixedAssetId: this.idAsset,
        ResourceId: null,
        Budget: 0,
        Resource: null,
        CreatedDate: null,
        CreatedBy: "Đỗ Mạnh Hà",
        ModifiedDate: null,
        ModifiedBy: null,
      },
      aRClone: [],
      nameRequired: "",
      focusRequired: "",
      flag: true,
      totalBudget: 0,
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
  async created() {
    await this.setResource();
    this.bindData();
  },
  methods: {
    /**
     * tính tổng tiền
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    caculateBudget() {
      this.totalBudget = 0;
      this.assetResources.forEach((ele) => {
        this.totalBudget += parseInt((ele.Budget + "").replaceAll(".", ""));
      });
    },

    /**
     * reset text
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    focusSelected(refName) {
      this.$refs[refName][0].select();
    },

    /**
     * xóa error khi focus
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    removeError(refName) {
      let requireds = document.querySelectorAll("#assetResourceForm .required");
      let tooltip = document.querySelectorAll("#assetResourceForm .tooltip");
      let tooltipDuplicate = document.querySelectorAll(
        "#assetResourceForm .tooltip-duplicate"
      );
      for (let i = 0; i < this.assetResources.length; i++) {
        if (refName == `ResourceId${i}`) {
          requireds[2 * i].classList.remove("error");
          tooltip[2 * i].style.display = "none";
          tooltipDuplicate[i].style.display = "none";
          return;
        }
      }

      this.$refs[refName][0].classList.remove("error");
      this.$refs[refName][0].parentElement.parentElement.querySelector(
        "#assetResourceForm .tooltip"
      ).style.display = "none";
    },
    /**
     * event trước khi lưu
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    beforeSave() {
      this.isLoading = true;
      this.assetResources.forEach((ele) => {
        ele.Budget = parseInt((ele.Budget + "").replaceAll(".", ""));
      });
    },

    /**
     * focus vào lỗi
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    focusToError() {
      this.focusRequired.focus();
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
      if (this.validateResourceDuplicate()) {
        return;
      }
      this.beforeSave();

      let url = this.hostApi + this.endPoint + "FixedAsset/" + this.idAsset;

      axios
        .delete(url)
        .then(() => {
          const config = {
            url: this.hostApi + this.endPoint + "FixedAsset/insert-multi",
            method: "POST",
            data: this.assetResources,
          };
          axios
            .request(config)
            .then(() => {
              this.isLoading = false;
              this.aRClone = [...this.assetResources];
              this.$emit("funct", this.totalBudget);
              this.closeChooseForm();
            })
            .catch((error) => {
              //   this.setARClone();
              this.isLoading = false;
              console.log(error);
            });
        })
        .catch((error) => {
          //   this.setARClone();
          this.isLoading = false;
          console.log(error);
        });
    },

    /**
     * validate trùng nguồn ngân sách
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    validateResourceDuplicate() {
      let arr = [];
      let flag = false;
      let tooltipDuplicate = document.querySelectorAll(
        "#assetResourceForm .tooltip-duplicate"
      );
      let requireds = document.querySelectorAll("#assetResourceForm .required");
      for (let i = 0; i < this.assetResources.length; i++) {
        if (arr.includes(this.assetResources[i].ResourceId)) {
          requireds[2 * i].classList.add("error");
          tooltipDuplicate[i].style.display = "block";
          flag = true;
        } else {
          arr.push(this.assetResources[i].ResourceId);
        }
      }
      return flag;
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
      let requireds = document.querySelectorAll("#assetResourceForm .required");
      let tooltip = document.querySelectorAll("#assetResourceForm .tooltip");
      for (let i = 0; i < requireds.length; i++) {
        if (i % 2 == 0) {
          inputVal = requireds[i].querySelector("input").value;
          if (inputVal == null || inputVal == "") {
            requireds[i].classList.add("error");
            tooltip[i].style.display = "block";
            flag = false;
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
     * xóa Row
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    removeRow(index) {
      if (this.assetResources.length <= 1) return;
      this.assetResources.splice(index, 1);
      this.caculateBudget();
    },

    /**
     * thêm row
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    addRow() {
      let temp = { ...this.tempAssetResource };
      this.assetResources.push(temp);
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
     * gán dữ liệu
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    bindData() {
      this.isLoading = true;
      let urlApi = `${this.hostApi}${this.endPoint}FixedAsset/${this.idAsset}`;
      axios
        .get(urlApi)
        .then((res) => {
          this.isLoading = false;
          this.assetResources = res.data.Data;
          this.assetResources.forEach((ele) => {
            this.totalBudget += ele.Budget;
            ele.Budget = this.formatCurrency(ele.Budget);
          });
        })
        .catch((error) => {
          console.log(error);
        });
    },
    /**
     * gán dữ liệu cho dropdown Phòng ban
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    setResource() {
      this.resources = [...this.$store.getters.Resources];
    },
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
     * đóng form
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    closeChooseForm() {
      this.$store.commit("setIsShowAssetResourceForm", false);
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
      const form = document.getElementById("assetResourceForm");
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
.form-site.asset-resource-form {
  width: 600px;
}

.form-site.asset-resource-form .f-content {
  flex-direction: column;
}

.form-site.asset-resource-form .field-row {
  height: 250px;
  overflow-y: auto;
}
</style>