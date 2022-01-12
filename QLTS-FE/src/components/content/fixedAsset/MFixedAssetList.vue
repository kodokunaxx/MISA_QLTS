<template>
  <div class="m-fixed-asset">
    <div class="tool h-36">
      <div class="tool-left w-3/5">
        <div class="w-1/3 m-r-11">
          <div class="m-textbox-icon">
            <div class="icon-16 icon-search"></div>
            <input
              type="text"
              name=""
              id=""
              placeholder="Tìm kiếm tài sản"
              v-model="keyword"
              @keyup.enter="search()"
            />
          </div>
        </div>

        <div class="dx-filter w-1/3 m-r-11" v-if="fixedAssetCategorys[0]">
          <div class="icon-24 icon-filter"></div>
          <DxSelectBox
            height="35"
            :data-source="fixedAssetCategorys"
            value-expr="FixedAssetCategoryId"
            display-expr="FixedAssetCategoryName"
            :search-enabled="true"
            :placeholder="'Loại tài sản'"
            :noDataText="'Không có dữ liệu'"
            :value="fixedAssetCategorys[0].FixedAssetCategoryId"
            item-template="item"
            @value-changed="changeIndex($event, 'fixedAssetCategory')"
            @focusIn="OpenSelectBox($event)"
          >
            <template #item="{ data }">
              <div>
                <div
                  class="icon-16 m-r-12"
                  :class="
                    faCategoryId === data.FixedAssetCategoryId
                      ? 'icon-checked'
                      : ''
                  "
                ></div>
                <div class="dx-template-item-data">
                  {{ data.FixedAssetCategoryName }}
                </div>
              </div>
            </template>
          </DxSelectBox>
        </div>
        <div class="dx-filter w-1/3 m-r-11" v-if="departments[0]">
          <div class="icon-24 icon-filter"></div>
          <DxSelectBox
            height="35"
            :data-source="departments"
            value-expr="DepartmentId"
            display-expr="DepartmentName"
            :search-enabled="true"
            :placeholder="'Bộ phận sử dụng'"
            :noDataText="'Không có dữ liệu'"
            :value="departments[0].DepartmentId"
            item-template="item"
            @value-changed="changeIndex($event, 'department')"
            @focusIn="OpenSelectBox($event)"
          >
            <template #item="{ data }">
              <div>
                <div
                  class="icon-16 m-r-12"
                  :class="
                    departmentId === data.DepartmentId ? 'icon-checked' : ''
                  "
                ></div>
                <div class="dx-template-item-data">
                  {{ data.DepartmentName }}
                </div>
              </div>
            </template>
          </DxSelectBox>
        </div>
      </div>
      <div class="tool-right">
        <button class="m-btn" @click="showForm(null, false)">
          Thêm tài sản
        </button>
        <button
          class="btn-logo m-x-11 m-tooltip-site excel"
          @click="exportExcel()"
        >
          <div class="icon-24 icon-excel"></div>
          <Tooltip :content="'Xuất Excel'" />
        </button>
        <button class="btn-logo m-tooltip-site" @click="showDialog()">
          <div
            class="icon-24 icon-trash"
            :class="hasId ? '' : 'disable-icon'"
          ></div>
          <Tooltip :content="'Xóa'" v-show="hasId" />
        </button>
      </div>
    </div>
    <div class="m-container" id="container">
      <table class="m-table" id="tbListData">
        <thead>
          <tr>
            <th class="very-short-width">
              <input
                type="checkbox"
                class="m-l-6"
                name=""
                id="checkAll"
                @click="selectAllItem($event)"
              />
            </th>
            <th class="very-short-width text-center">
              <div class="text">STT</div>
            </th>
            <th class="short-width"><div class="text">Mã tài sản</div></th>
            <th class="long-width"><div class="text">Tên tài sản</div></th>
            <th class="long-width"><div class="text">Loại tài sản</div></th>
            <th class="long-width"><div class="text">Bộ phận sử dụng</div></th>
            <th class="short-width text-right">
              <div class="text">Số lượng</div>
            </th>
            <th class="medium-width text-right">
              <div class="text">Nguyên giá</div>
            </th>
            <th class="medium-width m-tooltip-site text-right">
              <div class="text">HM/KH lũy kế</div>
              <Tooltip :content="'Hao mòn/Khấu hao lỹ kế'" />
            </th>
            <th class="medium-width text-right">
              <div class="text">Giá trị còn lại</div>
            </th>
            <th class="medium-width">
              <div class="text">Trạng thái</div>
            </th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(fixedAsset, index) in fixedAssets"
            :key="fixedAsset.FixedAssetId"
            @dblclick="showForm(fixedAsset.FixedAssetId, false)"
            @click="
              selectRow(
                `${fixedAsset.FixedAssetCode} - ${fixedAsset.FixedAssetName}`,
                fixedAsset.FixedAssetId,
                index
              )
            "
            @contextmenu="
              showMenuContext(
                $event,
                fixedAsset.FixedAssetId,
                `${fixedAsset.FixedAssetCode} - ${fixedAsset.FixedAssetName}`,
                index
              )
            "
          >
            <td class="very-short-width">
              <input
                type="checkbox"
                class="m-l-6"
                name=""
                id=""
                @click="
                  selectItem(
                    $event,
                    `${fixedAsset.FixedAssetCode} - ${fixedAsset.FixedAssetName}`,
                    fixedAsset.FixedAssetId,
                    index
                  )
                "
              />
            </td>
            <td class="very-short-width text-center">
              <div class="text">
                {{ index + 1 + (pageIndex - 1) * pageSize }}
              </div>
            </td>
            <td class="short-width">
              <div class="text">{{ fixedAsset.FixedAssetCode }}</div>
            </td>
            <td class="long-width">
              <div class="text">{{ fixedAsset.FixedAssetName }}</div>
              <Tooltip :content="fixedAsset.FixedAssetName" :bottom="'-16px'" />
            </td>
            <td class="long-width">
              <div class="text">{{ fixedAsset.FixedAssetCategoryName }}</div>
              <Tooltip
                :content="fixedAsset.FixedAssetCategoryName"
                :bottom="'-16px'"
              />
            </td>
            <td class="long-width">
              <div class="text">{{ fixedAsset.DepartmentName }}</div>
              <Tooltip :content="fixedAsset.DepartmentName" :bottom="'-16px'" />
            </td>
            <td class="short-width text-right">
              <div class="text">{{ fixedAsset.Quantity | handleNumer }}</div>
            </td>
            <td class="medium-width text-right">
              <div class="text">{{ fixedAsset.Cost | handleNumer }}</div>
            </td>
            <td class="medium-width text-right">
              <div class="text">
                {{
                  setDepreciation(fixedAsset.Cost, fixedAsset.DepreciationRate)
                    | handleNumer
                }}
              </div>
            </td>
            <td class="medium-width text-right">
              <div class="text">
                {{
                  (fixedAsset.Cost -
                    setDepreciation(
                      fixedAsset.Cost,
                      fixedAsset.DepreciationRate
                    ))
                    | handleNumer
                }}
              </div>
            </td>
            <td class="last-col medium-width">
              <div class="text">
                {{
                  fixedAsset.ReceiptId != null
                    ? "Đang sử dụng"
                    : "Chưa ghi tăng"
                }}
              </div>
              <div class="tool">
                <div
                  class="icon-24 action m-tooltip-site icon-edit m-l-6"
                  @click="showForm(fixedAsset.FixedAssetId, false)"
                >
                  <Tooltip :content="'Sửa'" :left="'-6px'" />
                </div>
                <div
                  class="icon-24 action m-tooltip-site icon-trash m-l-6"
                  @click="
                    deleteRow(
                      `${fixedAsset.FixedAssetCode} - ${fixedAsset.FixedAssetName}`,
                      fixedAsset.FixedAssetId,
                      index
                    )
                  "
                >
                  <Tooltip :content="'Xóa'" :left="'-6px'" />
                </div>
                <div
                  class="icon-24 action m-tooltip-site icon-replication m-l-6"
                  @click="showForm(fixedAsset.FixedAssetId, true)"
                >
                  <Tooltip :content="'Nhân bản'" :left="'-32px'" />
                </div>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
      <div
        class="m-fit-content"
        id="fit-content"
        :class="fixedAssets.length < 1 ? 'no-data' : ''"
      ></div>
      <div class="general">
        <div class="general-left">
          <div style="width: 180px; padding-left: 16px">
            Tổng số:
            <span style="font-family: MISARoboto-Bold">{{ recordNum }}</span>
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
              @click="pageIndex === 1 ? '' : setPageIndex(pageIndex - 1)"
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
                pageIndex === totalPageIndex ? '' : setPageIndex(pageIndex + 1)
              "
            ></div>
          </div>
        </div>
        <div class="general-middle"></div>
        <div class="general-right" :class="fixedAssets.length > 0 ? '' : 'sum'">
          <div class="g-right-item" style="width: 100px">
            {{ totals.TotalQuantity | handleNumer }}
          </div>
          <div class="g-right-item" style="width: 135px">
            {{ totals.TotalCost | handleNumer }}
          </div>
          <div class="g-right-item" style="width: 135px">
            {{ totals.TotalDepreciation | handleNumer }}
          </div>
          <div class="g-right-item" style="width: 135px">
            {{ totals.TotalResidual | handleNumer }}
          </div>
          <div class="g-right-item" style="width: 135px"></div>
        </div>
      </div>
    </div>
    <Form
      :id="id"
      :mode="isReplication"
      v-if="this.$store.getters.IsShowForm"
      @loadData="loadData()"
    />
    <Dialog
      :dialogMode="dialogMode"
      :mapId="mapId"
      :code="receiptCode"
      v-if="this.$store.getters.IsShowDialog"
      @funct="deleteFA()"
    />
    <Loading v-if="isLoading" />
    <MenuContext
      v-if="this.$store.getters.IsShowMenuContext"
      :top="top"
      :left="left"
      :mapId="mapId"
      @delete="showDialog()"
      @replication="showForm($event, true)"
      @add="showForm(null, false)"
      @edit="showForm($event, false)"
    />
    <Toast
      v-if="this.$store.getters.IsShowToast"
      :autoCloseToast="autoCloseToast()"
      >{{ this.$store.getters.ToastContent }}</Toast
    >
  </div>
</template>

<script>
import DxSelectBox from "devextreme-vue/select-box";
import Dialog from "../../base/MDialog.vue";
import Form from "./MFixedAssetDetail.vue";
import axios from "axios";
import Loading from "../../base/MLoading.vue";
import MenuContext from "../../base/MContext.vue";
import Toast from "../../base/MToast.vue";
import Tooltip from "../../base/MTooltip.vue";
export default {
  components: {
    DxSelectBox,
    Dialog,
    Form,
    Loading,
    MenuContext,
    Toast,
    Tooltip,
  },
  data() {
    return {
      hostApi: this.$store.getters.HostApi,
      endPoint: "FixedAssets/filter?",
      paginationSource: [
        { key: 10 },
        { key: 20 },
        { key: 30 },
        { key: 50 },
        { key: 100 },
      ],
      top: -9999,
      left: -9999,
      departmentId: "",
      departments: [],
      faCategoryId: "",
      fixedAssetCategorys: [],
      fixedAssets: [],
      recordNum: 0,
      totals: {
        TotalQuantity: 0,
        TotalCost: 0,
        TotalDepreciation: 0,
        TotalResidual: 0,
      },
      totalPageIndex: 0,
      listIndexs: [],
      pageIndex: 1,
      pageSize: 20,
      keyword: "",
      timer: null,
      mapId: {},
      dialogMode: "",
      id: null,
      isReplication: false,
      isLoading: false,
      startRow: -1,
      endRow: -1,
      receiptCode: "",
      hasId: false,
    };
  },
  async created() {
    await this.$store.dispatch("getDepartments");
    await this.$store.dispatch("getFixedAssetCategorys");
    this.setDepartment();
    this.setFixedAssetCategory();
    this.loadData();
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
  updated() {
    this.setTooltip();
  },
  mounted() {
    this.setFitContent();
  },
  methods: {
    /**
     * check mapid có null hay không để set style cho button
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    checkMapIdNull() {
      if (Object.keys(this.mapId).length > 0) {
        this.hasId = true;
      } else {
        this.hasId = false;
      }
    },

    /**
     * check có chứng từ không
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    checkHasReceipt() {
      let length = Object.keys(this.mapId).length;
      let tempAssets = this.fixedAssets.filter(this.filterAssetByMapId);
      if (length == 1) {
        if (tempAssets[0].ReceiptId != null) {
          this.receiptCode = tempAssets[0].ReceiptCode;
          this.dialogMode = this.$store.getters.DialogMode.ErrorDelete;
          this.$store.commit("setIsShowDialog", true);
          return true;
        }
      } else {
        for (let i = 0; i < tempAssets.length; i++) {
          if (tempAssets[i].ReceiptId != null) {
            this.dialogMode = this.$store.getters.DialogMode.ErrorMultiDelete;
            this.receiptCode = tempAssets[i].FixedAssetCode;
            this.$store.commit("setIsShowDialog", true);
            return true;
          }
        }
      }
      return false;
    },

    /**
     * lọc lấy các tài sản có id trong map Id
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    filterAssetByMapId(asset) {
      for (let i = 0; i < Object.keys(this.mapId).length; i++) {
        if (asset.FixedAssetId == Object.keys(this.mapId)[i]) return true;
      }
      return false;
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
     * hiển thị form
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    showForm(id, formMode) {
      this.isReplication = formMode;
      this.id = id;
      this.$store.commit("setIsShowForm", true);
    },

    /**
     * showDialog
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    showDialog() {
      let length = Object.keys(this.mapId).length;
      if (length <= 0) {
        return;
      }
      if (this.checkHasReceipt()) {
        return;
      }
      if (length === 1) {
        this.dialogMode = this.$store.getters.DialogMode.Delete;
        this.$store.commit("setIsShowDialog", true);
      } else {
        this.dialogMode = this.$store.getters.DialogMode.MultiDelete;
        this.$store.commit("setIsShowDialog", true);
      }
    },

    /**
     * xóa hàng
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    deleteRow(val, id, index) {
      this.selectRow(val, id, index);
      this.showDialog();
    },

    /**
     * Xóa
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    deleteFA() {
      let length = Object.keys(this.mapId).length;
      this.isLoading = true;
      let apiUrl = this.hostApi + "FixedAssets/";
      const config = {
        url:
          length == 1
            ? apiUrl + Object.keys(this.mapId)[0]
            : apiUrl + "delete-multi",
        method: this.$store.getters.Method.Delete,
        data: length == 1 ? null : Object.keys(this.mapId),
      };
      axios
        .request(config)
        .then(() => {
          this.isLoading = false;
          this.$store.commit("setIsShowDialog", false);
          this.loadData();
          this.$store.commit(
            "setToastContent",
            this.$store.getters.Toast.Delete
          );
          this.$store.commit("setIsShowToast", true);
        })
        .catch((error) => {
          this.$store.commit("setIsShowDialog", false);
          this.isLoading = false;
          if (error.response.status === 400 || error.response.status === 500) {
            if (length == 1) {
              this.dialogMode = this.$store.getters.DialogMode.ErrorDelete;
              setTimeout(() => {
                this.$store.commit("setIsShowDialog", true);
              }, 100);
            } else {
              this.dialogMode = this.$store.getters.DialogMode.ErrorMultiDelete;
              setTimeout(() => {
                this.$store.commit("setIsShowDialog", true);
              }, 100);
            }
          }
        });
    },

    /**
     * chọn đồng loạt
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    selectAllItem(event) {
      let ele = event.target;
      let checkboxs = document.querySelectorAll('td input[type="checkbox"]');
      if (ele.checked) {
        for (let i = 0; i < this.fixedAssets.length; i++) {
          const element = this.fixedAssets[i];
          const checkbox = checkboxs[i];
          if (!checkbox.checked) {
            checkbox.parentElement.parentElement.classList.add("selected");
            checkbox.checked = true;
            this.mapId[
              element.FixedAssetId
            ] = `${element.FixedAssetCode} - ${element.FixedAssetName}`;
          }
        }
      } else {
        for (let i = 0; i < this.fixedAssets.length; i++) {
          const element = this.fixedAssets[i];
          const checkbox = checkboxs[i];
          if (checkbox.checked) {
            checkbox.parentElement.parentElement.classList.remove("selected");
            checkbox.checked = false;
            delete this.mapId[element.FixedAssetId];
          }
          this.startRow = -1;
        }
      }
      this.checkMapIdNull();
    },
    /**
     * select bằng checkbox
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    selectItem(event, val, id, index) {
      event.stopPropagation();
      let ele = event.target;
      if (ele.checked) {
        ele.parentElement.parentElement.classList.add("selected");
        if (id) {
          this.mapId[id] = val;
        }
        this.startRow = index;
      } else {
        ele.parentElement.parentElement.classList.remove("selected");
        delete this.mapId[id];
        this.startRow = -1;
      }
      this.checkSelectAll();
    },

    /**
     * chọn một hàng bằng click
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    selectRow(val, id, index) {
      let checkbox = document.querySelectorAll('td input[type="checkbox"]');
      // let count = 0;
      if (event.shiftKey && this.startRow >= 0) {
        this.unselectRow();
        this.endRow = index;
        if (this.startRow > index) {
          this.endRow = this.startRow;
          this.startRow = index;
        }
        this.mapId = {};
        for (let i = this.startRow; i <= this.endRow; i++) {
          checkbox[i].checked = true;
          checkbox[i].parentElement.parentElement.classList.add("selected");
          this.mapId[
            this.fixedAssets[i].FixedAssetId
          ] = `${this.fixedAssets[i].FixedAssetCode}-${this.fixedAssets[i].FixedAssetName}`;
          this.checkSelectAll();
        }
      } else if (event.ctrlKey) {
        if (checkbox[index].checked) {
          checkbox[index].checked = false;
          checkbox[index].parentElement.parentElement.classList.remove(
            "selected"
          );
          delete this.mapId[id];
        } else {
          checkbox[index].checked = true;
          checkbox[index].parentElement.parentElement.classList.add("selected");
          this.startRow = index;
          this.mapId[id] = val;
        }
        this.checkSelectAll();
      } else {
        this.unselectRow();
        checkbox[index].checked = true;
        checkbox[index].parentElement.parentElement.classList.add("selected");
        this.startRow = index;
        this.mapId = {};
        this.mapId[id] = val;
        this.checkSelectAll();
      }
      this.checkMapIdNull();
    },

    /**
     * bỏ chọn các row
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    unselectRow() {
      let checkboxs = document.querySelectorAll('td input[type="checkbox"]');
      for (let i = 0; i < checkboxs.length; i++) {
        const checkbox = checkboxs[i];
        checkbox.checked = false;
        checkbox.parentElement.parentElement.classList.remove("selected");
      }
    },

    /**
     * check select All
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    checkSelectAll() {
      let checkboxs = document.querySelectorAll('td input[type="checkbox"]');
      let count = 0;
      for (let i = 0; i < checkboxs.length; i++) {
        const checkbox = checkboxs[i];
        if (checkbox.checked) {
          count++;
        }
      }
      if (count == checkboxs.length) {
        document.getElementById("checkAll").checked = true;
      } else {
        document.getElementById("checkAll").checked = false;
      }
      if (count == 0) this.startRow = -1;
      this.checkMapIdNull();
    },

    /**
     * tìm kiếm theo keyword
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    search() {
      const me = this;
      this.keyword = this.keyword.trim();
      clearTimeout(this.timer);
      this.timer = setTimeout(() => {
        me.loadData();
      }, 0);
    },

    /**
     * tính tổng số các dữ liệu
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    calculateTotal() {
      let tempTotalQuantity = 0,
        tempTotalCost = 0,
        tempTotalDepreciation = 0,
        tempTotalResidual = 0;
      if (this.fixedAssets && this.fixedAssets.length > 0) {
        this.fixedAssets.forEach((ele) => {
          tempTotalQuantity += ele.Quantity;
          tempTotalCost += ele.Cost;
          tempTotalDepreciation += this.setDepreciation(
            ele.Cost,
            ele.DepreciationRate
          );
        });
        tempTotalResidual = tempTotalCost - tempTotalDepreciation;
      }
      this.totals.TotalQuantity = tempTotalQuantity;
      this.totals.TotalCost = tempTotalCost;
      this.totals.TotalDepreciation = tempTotalDepreciation;
      this.totals.TotalResidual = tempTotalResidual;
    },

    /**
     * tính khấu hao lũy kế
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    setDepreciation(cost, rate) {
      return parseInt((cost * rate) / 100);
    },

    /**
     * xử lý trước khi load data
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    beforeLoad() {
      document.getElementById("checkAll").checked = false;
      let event = { target: { checked: false } };
      this.selectAllItem(event);
      this.mapId = {};
      this.openLoading();
      this.checkMapIdNull();
    },

    /**
     * xử lý sau khi load data
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    afterLoad() {
      this.calculateTotal();
      this.closeLoading();
      this.genPagination();
    },

    /**
     * load data lên table
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    async loadData() {
      this.beforeLoad();
      let param = "";
      if (this.keyword.trim() !== "") {
        param += `keyword=${this.keyword.trim()}`;
      }
      if (this.departmentId !== "") {
        param += `&departmentId=${this.departmentId}`;
      }
      if (this.faCategoryId !== "") {
        param += `&fixedAssetCategoryId=${this.faCategoryId}`;
      }
      if (this.pageIndex) {
        param += `&pageIndex=${this.pageIndex}`;
      }
      if (this.pageSize) {
        param += `&pageSize=${this.pageSize}`;
      }
      let apiUrl = this.hostApi + this.endPoint + param;
      await axios
        .get(apiUrl)
        .then((res) => {
          this.fixedAssets = res.data.Data;
          this.recordNum = res.data.Total;
          this.afterLoad();
        })
        .catch((error) => {
          console.log(error);
        });
      this.setFitContent();
    },

    /**
     * gán index để set select dropdown
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    changeIndex(e, type) {
      if (!e) {
        return;
      }
      if (type === "department") {
        this.departmentId = e.value;
      } else {
        this.faCategoryId = e.value;
      }
      this.pageIndex = 1;
      this.loadData();
    },

    /**
     * gán dữ liệu cho dropdown Phòng ban
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    setDepartment() {
      this.departments = [...this.$store.getters.Departments];
      this.departments.unshift({
        DepartmentId: "",
        DepartmentName: "Bộ phận sử dụng",
      });
      if (this.departments && this.departments.length) {
        this.departmentId = this.departments[0]?.DepartmentId;
      }
    },
    /**
     * gán dữ liệu cho dropdown Loại tài sản
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    setFixedAssetCategory() {
      this.fixedAssetCategorys = [...this.$store.getters.FixedAssetCategorys];
      this.fixedAssetCategorys.unshift({
        FixedAssetCategoryId: "",
        FixedAssetCategoryName: "Loại tài sản",
      });
      if (this.fixedAssetCategorys && this.fixedAssetCategorys.length) {
        this.faCategoryId = this.fixedAssetCategorys[0]?.FixedAssetCategoryId;
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
        this.loadData();
      }
    },

    /**
     * gán giá trị pageSize
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    setPageSize(size) {
      if (size > 0) {
        this.pageIndex = 1;
        this.pageSize = size;
        this.loadData();
      }
    },

    /**
     * bật loading
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    openLoading() {
      let lst = document.querySelectorAll(".m-table td");
      for (let i = 0; i < lst.length; i++) {
        lst[i].classList.add("skeleton");
      }
    },

    /**
     * tắt loading
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    closeLoading() {
      let lst = document.querySelectorAll(".m-table td");
      for (let i = 0; i < lst.length; i++) {
        lst[i].classList.remove("skeleton");
      }
      document.getElementById("container").scrollTop = 0;
      document.getElementById("container").scrollLeft = 0;
    },

    /**
     * set chiều cao div fit content
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    setFitContent() {
      let heightContent = document.querySelector("body").clientHeight;
      let height = document.getElementById("tbListData").offsetHeight;
      if (height < heightContent - 126 - 48) {
        document.getElementById("fit-content").style.height = `calc(100vh - ${
          126 + height + 48
        }px)`;
      } else {
        document.getElementById("fit-content").style.height = "0";
      }
    },

    /**
     * xuất file excel
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    exportExcel() {
      let param = "";
      if (this.keyword.trim() !== "") {
        param += `keyword=${this.keyword.trim()}`;
      }
      if (this.departmentId !== "") {
        param += `&departmentId=${this.departmentId}`;
      }
      if (this.faCategoryId !== "") {
        param += `&fixedAssetCategoryId=${this.faCategoryId}`;
      }
      if (this.pageIndex) {
        param += `&pageIndex=${this.pageIndex}`;
      }
      if (this.pageSize) {
        param += `&pageSize=${this.pageSize}`;
      }
      let apiUrl = this.hostApi + "FixedAssets/export?" + param;
      window.location.href = apiUrl;
    },

    /**
     * show context menu
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    showMenuContext(event, id, name, index) {
      this.selectRow(name, id, index);
      this.$store.commit("setIsShowMenuContext", true);
      // }
      event.preventDefault();
      this.top = event.clientY + 60;
      this.left = event.clientX + 120;
    },

    /**
     * tự động đóng toast
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    autoCloseToast() {
      setTimeout(() => {
        this.$store.commit("setIsShowToast", false);
      }, 3000);
    },

    /**
     * set Tooltip nếu text dài quá
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    setTooltip() {
      let cells = document.querySelectorAll(".m-fixed-asset tbody tr td");
      for (let i = 0; i < cells.length; i++) {
        const cell = cells[i];
        let textWidth = cell.querySelector(".text")?.offsetWidth;
        let scrollWidth = cell.querySelector(".text")?.scrollWidth;
        if (scrollWidth > textWidth) {
          cell.classList.add("m-tooltip-site");
        }
      }
    },
  },
};
</script>

<style>
.m-fixed-asset {
  width: 100%;
  padding: 0 16px;
}

.m-fixed-asset .tool {
  display: flex;
  align-items: center;
  justify-content: space-between;
  width: 100%;
}

.tool .tool-left {
  display: flex;
  align-items: center;
}

.tool .tool-right {
  display: flex;
  align-items: center;
}

.m-fixed-asset .m-container {
  height: calc(100vh - 124px);
  overflow-x: auto;
  overflow-y: auto;
  margin-top: 14px;
  border-radius: 4px;
  border: 1px solid #e5e5e5;
  background-color: #ffffff;
  position: relative;
  width: 100%;
}

.m-container .general {
  height: 40px;
  display: flex;
  align-items: center;
  position: sticky;
  bottom: -1px;
  /* border: 1px solid #ffffff; */
  border-top: 1px solid #e5e5e5;
  z-index: 30;
  background-color: #ffffff;
  width: fit-content;
  box-sizing: content-box;
  user-select: none;
}

.general .general-left {
  height: 39px;
  width: 500px;
  display: flex;
  align-items: center;
  background-color: #ffffff;
  position: sticky;
  left: 0;
}

.general .general-middle {
  height: 39px;
  width: 296px;
  display: flex;
  align-items: center;
  background-color: #ffffff;
}

.general .general-right {
  z-index: 35;
  height: 39px;
  display: flex;
  align-items: center;
  font-family: MISARoboto-Bold;
  background-color: #ffffff;
}

.general-right .g-right-item {
  text-align: right;
  line-height: 39px;
  vertical-align: middle;
  padding: 0 10px;
  height: 39px;
  background-color: #ffffff;
}

.general-right .feature-sticky {
  z-index: 50;
  position: sticky;
  right: 0;
}

.general .paging {
  margin-left: 24px;
  display: flex;
  align-items: center;
}

.paging .paging-item {
  width: 24px;
  height: 24px;
  vertical-align: middle;
  text-align: center;
  line-height: 24px;
  user-select: none;
  border-radius: 4px;
}

.paging .paging-item.current {
  background-color: #ededed;
}

.paging .paging-item.pointer:hover {
  background-color: #dbdbdb;
}

.paging .paging-item.pointer:active {
  background-color: #ededed;
}

.dx-filter .icon-filter {
  position: absolute;
  z-index: 10;
  top: 5px;
  left: 4px;
}

.m-fit-content.no-data {
  background-image: url("../../../assets/icon/no_data.svg");
  background-position: center;
  background-repeat: no-repeat;
}
</style>