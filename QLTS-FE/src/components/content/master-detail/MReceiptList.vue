<template>
  <div class="m-receipt">
    <div class="tool h-36">
      <div class="tool-left">Ghi tăng tài sản</div>

      <div class="tool-right">
        <button class="m-btn" @click="showForm(null)">Thêm</button>
        <div class="logo-site" @click="isShowListView = !isShowListView">
          <div
            class="icon-24 m-tooltip-site"
            :class="isHorizontal ? 'icon-horizontal' : 'icon-vertical'"
          >
            <Tooltip
              :content="isHorizontal ? 'Giao diện ngang' : 'Giao diện dọc'"
              :left="'-150%'"
            />
          </div>
          <div class="icon-24 icon-dropdown2 m-l-8"></div>
          <div class="list-view" v-show="isShowListView">
            <div
              class="option"
              @click="
                isHorizontal = true;
                changeLayout();
              "
            >
              <div class="icon-24 icon-horizontal m-r-8"></div>
              <div>Giao diện ngang</div>
            </div>
            <div
              class="option"
              @click="
                isHorizontal = false;
                changeLayout();
              "
            >
              <div class="icon-24 icon-vertical m-r-8"></div>
              <div>Giao diện dọc</div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="m-container" id="receipt">
      <div class="receipt-list" v-show="!isFull">
        <div class="tool p-x-16 p-t-14">
          <div class="tool-left" style="width: 280px">
            <div class="m-textbox-icon">
              <div class="icon-16 icon-search"></div>
              <input
                type="text"
                placeholder="Tìm kiếm theo số chứng từ, nội dung"
                v-model="keyword"
                @keyup.enter="search()"
              />
            </div>
          </div>
          <div class="tool-right">
            <div
              class="icon-24 action icon-trash pointer m-tooltip-site"
              v-show="isShowDelete"
              @click="showDialog()"
            >
              <Tooltip :content="'Xóa'" />
            </div>
            <div class="icon-24 action icon-print m-x-8 pointer m-tooltip-site">
              <Tooltip :content="'In'" />
            </div>
            <div class="icon-24 action icon-more pointer m-tooltip-site">
              <Tooltip :content="'Tùy chọn'" :left="'-110%'" />
            </div>
          </div>
        </div>
        <div class="tbl-side" id="receiptlst">
          <table class="m-table" id="tblreceipt">
            <thead>
              <tr>
                <th class="very-short-width">
                  <input
                    type="checkbox"
                    class="m-l-6"
                    id="checkAll"
                    @click="selectAllItem($event)"
                  />
                </th>
                <th class="very-short-width text-center">
                  <div class="text">STT</div>
                </th>
                <th class="medium-width">
                  <div class="text">Số chứng từ</div>
                </th>
                <th class="medium-width text-center">
                  <div class="text">Ngày chứng từ</div>
                </th>
                <th class="medium-width text-center">
                  <div class="text">Ngày ghi tăng</div>
                </th>
                <th class="medium-width text-right">
                  <div class="text">Tổng nguyên giá</div>
                </th>
                <th class="note"><div class="text">Nội dung</div></th>
              </tr>
            </thead>
            <tbody>
              <tr
                v-for="(receipt, index) in receipts"
                :key="receipt.ReceiptId"
                @click="
                  selectRow(`${receipt.ReceiptCode}`, receipt.ReceiptId, index)
                "
                @dblclick="showForm(receipt.ReceiptId)"
                @contextmenu="
                  showMenuContext(
                    $event,
                    receipt.ReceiptId,
                    `${receipt.ReceiptCode}`,
                    index
                  )
                "
              >
                <td class="very-short-width">
                  <input
                    type="checkbox"
                    class="m-l-6"
                    @click="
                      selectItem(
                        $event,
                        `${receipt.ReceiptCode}`,
                        receipt.ReceiptId,
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
                <td class="medium-width">
                  <div class="text info" @click="showForm(receipt.ReceiptId)">
                    {{ receipt.ReceiptCode }}
                  </div>
                </td>
                <td class="medium-width text-center">
                  <div class="text">{{ receipt.ReceiptDate | handleDate }}</div>
                </td>
                <td class="medium-width text-center">
                  <div class="text">
                    {{ receipt.IncreaseDate | handleDate }}
                  </div>
                </td>
                <td class="medium-width text-right">
                  <div class="text">
                    {{ receipt.TotalCost | handleNumer }}
                  </div>
                </td>
                <td class="last-col note">
                  <div class="text">{{ receipt.Note }}</div>
                  <div class="tool">
                    <div
                      class="icon-24 action m-tooltip-site icon-edit m-l-6"
                      @click="showForm(receipt.ReceiptId)"
                    >
                      <Tooltip :content="'Sửa'" :left="'-6px'" />
                    </div>
                    <div
                      class="icon-24 action m-tooltip-site icon-trash m-l-6"
                      @click="
                        deleteRow(
                          `${receipt.ReceiptCode}`,
                          receipt.ReceiptId,
                          index
                        )
                      "
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
            id="fit-content-receipt"
            :class="receipts.length < 1 ? 'no-data' : ''"
          ></div>
          <div class="general">
            <table style="border-collapse: collapse">
              <tbody>
                <td
                  class="dis-flex align-items-center"
                  style="min-width: 495px; max-width: 495px"
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
                  style="
                    min-width: 135px;
                    max-width: 135px;
                    font-family: MISARoboto-Bold;
                  "
                  class="text-right p-x-10"
                  :class="receipts.length > 0 ? '' : 'sum'"
                >
                  {{ totalCost | handleNumer }}
                </td>
                <td style="min-width: 150px; width: 806px"></td>
              </tbody>
            </table>
          </div>
        </div>
      </div>
      <div class="fixed-asset-list" v-show="isHorizontal">
        <div class="spacing" v-show="!isFull"></div>
        <div class="title-site">
          <div
            v-show="!isFull"
            class="icon-24 icon-resize-height"
            @mousedown="resizeHeight($event)"
          ></div>
          <div class="left-side">Thông tin chi tiết</div>
          <div class="right-side">
            <div
              class="icon-24 action m-tooltip-site pointer"
              :class="isFull ? 'icon-fullsize' : 'icon-collapse'"
              @click="changeFull()"
            >
              <Tooltip
                :content="isFull ? 'Thu nhỏ' : 'Phóng to'"
                :left="'-100%'"
              />
            </div>
          </div>
        </div>
        <div class="tbl-side" id="assetlst">
          <table class="m-table" id="tblasset">
            <thead>
              <tr>
                <th class="very-short-width text-center">
                  <div class="text">STT</div>
                </th>
                <th class="medium-width">
                  <div class="text">Mã tài sản</div>
                </th>
                <th style="width: 430px; max-width: 430px; min-width: 296px">
                  <div class="text">Tên tài sản</div>
                </th>
                <th class="very-long-width">
                  <div class="text">Bộ phận sử dụng</div>
                </th>
                <th class="long-width text-right">
                  <div class="text">Nguyên giá</div>
                </th>
                <th class="long-width text-right">
                  <div class="text">Hao mòn năm</div>
                </th>
                <th class="long-width text-right">
                  <div class="text">Giá trị còn lại</div>
                </th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(asset, index) in assets" :key="asset.FixedAssetId">
                <td class="very-short-width text-center">
                  <div class="text">{{ index + 1 }}</div>
                </td>
                <td class="medium-width">
                  <div class="text">{{ asset.FixedAssetCode }}</div>
                </td>
                <td style="width: 430px; max-width: 430px; min-width: 296px">
                  <div class="text">{{ asset.FixedAssetName }}</div>
                </td>
                <td class="very-long-width">
                  <div class="text">{{ asset.DepartmentName }}</div>
                </td>
                <td class="long-width text-right">
                  <div class="text">{{ asset.Cost | handleNumer }}</div>
                </td>
                <td class="long-width text-right">
                  <div class="text">
                    {{
                      setDepreciation(asset.Cost, asset.DepreciationRate)
                        | handleNumer
                    }}
                  </div>
                </td>
                <td class="long-width text-right">
                  <div class="text">
                    {{
                      (asset.Cost -
                        setDepreciation(asset.Cost, asset.DepreciationRate))
                        | handleNumer
                    }}
                  </div>
                </td>
              </tr>
            </tbody>
          </table>
          <div
            class="m-fit-content"
            :class="assets.length < 1 ? 'no-data' : ''"
            :style="assets.length < 1 ? 'height: calc(100% - 39px)' : ''"
          ></div>
        </div>
      </div>
    </div>
    <Dialog
      :dialogMode="dialogMode"
      :mapId="mapId"
      v-if="this.$store.getters.IsShowDialog"
      @funct="deleteReceipt()"
    />
    <Toast
      v-if="this.$store.getters.IsShowToast"
      :autoCloseToast="autoCloseToast()"
      >{{ this.$store.getters.ToastContent }}</Toast
    >
    <MenuContext
      v-if="this.$store.getters.IsShowMenuContext"
      :top="top"
      :left="left"
      :mapId="mapId"
      :replication="false"
      :add="false"
      @delete="showDialog()"
      @edit="showForm($event)"
    />
    <ReceiptForm
      v-if="this.$store.getters.IsShowReceiptForm"
      :id="id"
      @loadData="loadData()"
    />
    <Loading v-if="isLoading" />
  </div>
</template>

<script>
import axios from "axios";
import DxSelectBox from "devextreme-vue/select-box";
import Tooltip from "../../base/MTooltip.vue";
import moment from "moment";
import Dialog from "../../base/MDialog.vue";
import Toast from "../../base/MToast.vue";
import MenuContext from "../../base/MContext.vue";
import ReceiptForm from "./MReceiptDetail.vue";
import Loading from "../../base/MLoading.vue";
export default {
  components: {
    Tooltip,
    DxSelectBox,
    Dialog,
    Toast,
    MenuContext,
    ReceiptForm,
    Loading,
  },
  data() {
    return {
      hostApi: this.$store.getters.HostApi,
      paginationSource: [
        { key: 10 },
        { key: 20 },
        { key: 30 },
        { key: 50 },
        { key: 100 },
      ],
      top: -9999,
      left: -9999,
      recordNum: 0,
      totalCost: 0,
      totalPageIndex: 0,
      listIndexs: [],
      pageIndex: 1,
      pageSize: 20,
      keyword: "",
      timer: null,
      mapId: {},
      dialogMode: "",
      isLoading: false,
      startRow: -1,
      endRow: -1,
      isHorizontal: true,
      isShowListView: false,
      isFull: false,
      receipts: [],
      assets: [],
      isShowDelete: false,
      id: null,
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

    /**
     * xử lý format date
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    handleDate(date) {
      if (date == null || date == "") return;
      return moment(date).format("DD/MM/YYYY");
    },
  },
  created() {},
  mounted() {
    this.setFitContentReceipt();
    this.loadData();
  },
  updated() {},
  computed: {},
  methods: {
    /**
     * hiển thị form
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    showForm(id) {
      this.id = id;
      this.$store.commit("setIsShowReceiptForm", true);
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
      this.top = event.clientY + 30;
      this.left = event.clientX + 120;
    },

    /**
     * Xóa
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    deleteReceipt() {
      let length = Object.keys(this.mapId).length;
      this.isLoading = true;
      let apiUrl = this.hostApi + "Receipts/";
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
            this.$store.getters.Toast.DeleteReceipt
          );
          this.$store.commit("setIsShowToast", true);
        })
        .catch((error) => {
          console.log(error);
        });
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
     * showDialog
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    showDialog() {
      let length = Object.keys(this.mapId).length;
      if (length <= 0) {
        return;
      } else if (length === 1) {
        this.dialogMode = this.$store.getters.DialogMode.DeleteReceipt;
        this.$store.commit("setIsShowDialog", true);
      } else {
        this.dialogMode = this.$store.getters.DialogMode.MultiDeleteReceipt;
        this.$store.commit("setIsShowDialog", true);
      }
    },

    /**
     * thay đổi hiển thị tài sản
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    changeFull() {
      if (this.isFull) {
        this.isFull = false;
        document.getElementById("assetlst").style.height = "calc(100% - 43px)";
        document.querySelector(".fixed-asset-list").style.height = "40%";
      } else {
        this.isFull = true;
        document.getElementById("assetlst").style.height = "calc(100% - 35px)";
        document.querySelector(".fixed-asset-list").style.height = "100%";
      }
    },

    changeLayout() {
      if (this.isHorizontal) {
        document.querySelector(".receipt-list").style.height = "60%";
      } else {
        document.querySelector(".receipt-list").style.height = "100%";
      }
    },

    /**
     * tính khấu hao lũy kế
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    setDepreciation(cost, rate) {
      return parseInt((cost * rate) / 100);
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
     * load data lên table
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    async loadData() {
      this.beforeLoad();
      let param = "";
      if (this.keyword.trim() !== "") {
        param += `keyword=${this.keyword.trim()}`;
      }
      if (this.pageIndex) {
        param += `&pageIndex=${this.pageIndex}`;
      }
      if (this.pageSize) {
        param += `&pageSize=${this.pageSize}`;
      }
      let apiUrl = this.hostApi + "Receipts/filter?" + param;
      await axios
        .get(apiUrl)
        .then((res) => {
          this.receipts = res.data.Data;
          this.recordNum = res.data.Total;
          this.afterLoad();
        })
        .catch((error) => {
          console.log(error);
        });
      this.setFitContentReceipt();
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
      this.assets = [];
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
     * bật loading
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    openLoading() {
      let lst = document.querySelectorAll("#receipt td:not(.general td)");
      for (let i = 0; i < lst.length; i++) {
        lst[i].classList.add("skeleton");
      }
    },

    /**
     * tắt loading
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    closeLoading() {
      let lst = document.querySelectorAll("#receipt td:not(.general td)");
      for (let i = 0; i < lst.length; i++) {
        lst[i].classList.remove("skeleton");
      }
      let tblSite = document.querySelectorAll("#receipt tbl-side");
      for (let i = 0; i < tblSite.length; i++) {
        tblSite[i].scrollTop = 0;
        tblSite[i].scrollLeft = 0;
      }
    },

    /**
     * set chiều cao div fit content
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    setFitContentReceipt() {
      let heightContent = document.getElementById("receiptlst").offsetHeight;
      let height = document.getElementById("tblreceipt").offsetHeight;
      if (height < heightContent - 41) {
        document.getElementById(
          "fit-content-receipt"
        ).style.height = `calc(100% - ${height + 41}px)`;
      } else {
        document.getElementById("fit-content-receipt").style.height = "0";
      }
    },

    /**
     * tính tổng số các dữ liệu
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    calculateTotal() {
      let tempTotalCost = 0;
      if (this.receipts && this.receipts.length > 0) {
        this.receipts.forEach((ele) => {
          tempTotalCost += ele.TotalCost;
        });
      }
      this.totalCost = tempTotalCost;
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
     * lấy dữ liệu tài sản
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    getAsset(id) {
      let apiUrl = this.hostApi + "FixedAssets/Receipt/" + id;
      axios
        .get(apiUrl)
        .then((res) => {
          this.assets = res.data.Data;
        })
        .catch((error) => {
          console.log(error);
        });
    },

    /**
     * chọn đồng loạt
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    selectAllItem(event) {
      let ele = event.target;
      let checkboxs = document.querySelectorAll(
        '#receipt td input[type="checkbox"]'
      );
      if (ele.checked) {
        for (let i = 0; i < this.receipts.length; i++) {
          const element = this.receipts[i];
          const checkbox = checkboxs[i];
          if (!checkbox.checked) {
            checkbox.parentElement.parentElement.classList.add("selected");
            checkbox.checked = true;
            this.mapId[element.ReceiptId] = `${element.ReceiptCode}`;
          }
        }
      } else {
        for (let i = 0; i < this.receipts.length; i++) {
          const element = this.receipts[i];
          const checkbox = checkboxs[i];
          if (checkbox.checked) {
            checkbox.parentElement.parentElement.classList.remove("selected");
            checkbox.checked = false;
            delete this.mapId[element.ReceiptId];
          }
          this.startRow = -1;
        }
      }
      this.isShowDelete = Object.keys(this.mapId).length > 1;
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
      this.isShowDelete = Object.keys(this.mapId).length > 1;
    },

    /**
     * chọn một hàng bằng click
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    selectRow(val, id, index) {
      let checkbox = document.querySelectorAll(
        '#receipt td input[type="checkbox"]'
      );
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
            this.receipts[i].ReceiptId
          ] = `${this.receipts[i].ReceiptCode}`;
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
        this.getAsset(id);
        this.checkSelectAll();
      }
      this.isShowDelete = Object.keys(this.mapId).length > 1;
    },

    /**
     * bỏ chọn các row
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    unselectRow() {
      let checkboxs = document.querySelectorAll(
        '#receipt td input[type="checkbox"]'
      );
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
      let checkboxs = document.querySelectorAll(
        '#receipt td input[type="checkbox"]'
      );
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
    },

    /**
     * di chuyển form
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    resizeHeight(event) {
      let mouseY = event.clientY;
      let moveY = 0;
      let heightReceipt = 0;

      const receipt = document.querySelector(".receipt-list");
      document.onmousemove = function (e) {
        e.preventDefault();
        moveY = mouseY - e.clientY;
        mouseY = e.clientY;
        heightReceipt = `${receipt.offsetHeight - moveY}`;
        receipt.style.height = `${heightReceipt}px`;
        if (heightReceipt <= 0)
          document.querySelector(".spacing").style.display = "none";
        else document.querySelector(".spacing").style.display = "block";
        document.querySelector(
          ".fixed-asset-list"
        ).style.height = `calc(100% - ${heightReceipt}px)`;
      };
      document.onmouseup = function () {
        document.onmouseup = null;
        document.onmousemove = null;
      };
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
  },
};
</script>

<style>
.m-receipt {
  width: 100%;
  padding: 0 16px;
}

.m-receipt .tool {
  display: flex;
  align-items: center;
  justify-content: space-between;
  width: 100%;
}

.tool .tool-left {
  display: flex;
  align-items: center;
  font-family: MISARoboto-Bold;
  font-size: 20px;
}

.tool .tool-right {
  display: flex;
  align-items: center;
}

.tool-right .logo-site {
  position: relative;
  cursor: pointer;
  background-color: #ffffff;
  display: flex;
  align-items: center;
  height: 36px;
  border-radius: 4px;
  margin-left: 11px;
  padding: 0 6px;
  box-shadow: rgb(0 0 0 / 15%) 0 2px 2px;
}

.logo-site .list-view {
  z-index: 100;
  background-color: #ffffff;
  position: absolute;
  right: 0;
  width: 140px;
  bottom: -76px;
  border-radius: 4px;
  box-shadow: rgb(0 0 0 / 15%) 0 2px 5px;
}

.logo-site .list-view .option {
  padding: 0 6px;
  height: 36px;
  display: flex;
  align-items: center;
  user-select: none;
}

.logo-site .list-view .option:hover {
  background-color: #d1edf4;
}

.logo-site .list-view .option:active {
  background-color: #ffffff;
}

.m-receipt .m-container {
  height: calc(100vh - 124px);
  margin-top: 14px;
  border-radius: 4px;
  border: 1px solid #e5e5e5;
  background-color: #ffffff;
  position: relative;
  width: 100%;
}

.m-receipt .receipt-list {
  height: 60%;
  min-height: 0px;
  background-color: #ffffff;
  max-height: 100%;
}

.m-receipt .fixed-asset-list {
  height: 40%;
  min-height: 24px;
  background-color: #ffffff;
}

.m-receipt .tbl-side#receiptlst {
  height: calc(100% - 63px);
  overflow-x: auto;
  overflow-y: auto;
  margin-top: 14px;
  border-radius: 4px;
  background-color: #ffffff;
  position: relative;
  width: 100%;
}

.tbl-side#receiptlst .note {
  min-width: 150px;
  /* min-width: 404px; */
  width: 806px;
  max-width: 806px;
}

.tbl-side#receiptlst .text.info {
  width: fit-content;
  color: #42aeef;
}

.tbl-side#receiptlst .text.info:hover {
  font-family: MISARoboto-Bold;
}

#receiptlst .general-left {
  width: 696px;
}

.fixed-asset-list .spacing {
  position: relative;
  height: 8px;
  background-color: #f4f7ff;
}

.fixed-asset-list .icon-resize-height {
  position: absolute;
  left: 50%;
  top: -8px;
  cursor: pointer;
  z-index: 1;
}

.fixed-asset-list .title-site {
  position: relative;
  display: flex;
  align-items: center;
  justify-content: space-between;
  height: 35px;
  padding: 0 16px;
  background-color: #ffffff;
}

.fixed-asset-list .title-site .left-side {
  font-family: MISARoboto-Bold;
  font-size: 15px;
}

.fixed-asset-list .tbl-side#assetlst {
  height: calc(100% - 41px);
  overflow-x: auto;
  overflow-y: auto;
  border-radius: 4px;
  background-color: #ffffff;
  position: relative;
  width: 100%;
}
</style>