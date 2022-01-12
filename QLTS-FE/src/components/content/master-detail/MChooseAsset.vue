<template>
  <div class="m-form choose-asset-form">
    <div class="form-site choose-asset" id="chooseAsset">
      <div class="f-header" @mousedown="moveForm($event)">
        <div class="f-title">Chọn tài sản ghi tăng</div>
        <div class="icon-24 icon-close" @click="closeChooseForm()"></div>
      </div>
      <div class="f-content choose-asset-content">
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
        </div>
        <div class="tbl-site" id="chooseAssetlst">
          <table class="m-table" id="tblChooseAsset">
            <thead>
              <tr>
                <th class="very-short-width">
                  <input
                    type="checkbox"
                    class="m-l-6"
                    id="checkAllAsset"
                    @click="selectAllItem($event)"
                  />
                </th>
                <th class="very-short-width text-center">
                  <div class="text">STT</div>
                </th>
                <th class="medium-width">
                  <div class="text">Mã tài sản</div>
                </th>
                <th class="long-width">
                  <div class="text">Tên tài sản</div>
                </th>
                <th style="width: 133px; min-width: 115px; max-width: 133px">
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
                v-for="(asset, index) in assets"
                :key="asset.FixedAssetId"
                @click="
                  selectRow(
                    `${asset.FixedAssetCode}`,
                    asset.FixedAssetId,
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
                        `${asset.FixedAssetCode}`,
                        asset.FixedAssetId,
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
                  <div class="text">{{ asset.FixedAssetCode }}</div>
                </td>
                <td class="long-width">
                  <div class="text">{{ asset.FixedAssetName }}</div>
                  <Tooltip :content="asset.FixedAssetName" :bottom="'-16px'" />
                </td>
                <td style="width: 133px; min-width: 133px; max-width: 133px">
                  <div class="text">{{ asset.DepartmentName }}</div>
                  <Tooltip :content="asset.DepartmentName" :bottom="'-16px'" />
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
                <td class="medium-width text-right">
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
            id="fit-content-choose"
            :class="assets.length < 1 ? 'no-data' : ''"
            :style="assets.length < 1 ? 'height: calc(100% - 80px)' : ''"
          ></div>
          <div class="general">
            <table style="border-collapse: collapse">
              <tbody>
                <td
                  class="dis-flex align-items-center"
                  style="min-width: 965px; width: 965px; max-width: 965px"
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
              </tbody>
            </table>
          </div>
        </div>
      </div>
      <div class="f-footer">
        <div class="button-field dis-flex">
          <button class="m-btn m-btn-outline m-r-16" @click="closeChooseForm()">
            <div class="label">Hủy</div>
          </button>
          <button class="m-btn" @click="sendAsset()">
            <div class="label">Đồng ý</div>
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Tooltip from "../../base/MTooltip.vue";
import axios from "axios";
import DxSelectBox from "devextreme-vue/select-box";
export default {
  props: ["chosenAssets"],
  components: { Tooltip, DxSelectBox },
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
      recordNum: 0,
      totalPageIndex: 0,
      listIndexs: [],
      pageIndex: 1,
      pageSize: 20,
      keyword: "",
      timer: null,
      assets: [],
      tempAssets: [],
      startRow: -1,
      endRow: -1,
      assetsFilter: [],
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
  created() {},
  mounted() {
    this.setFitContentChooseAsset();
    this.loadData();
  },
  updated() {
    this.setTooltip();
  },
  methods: {
    /**
     * Gửi dữ liệu về form chi tiết
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    sendAsset() {
      if (Object.keys(this.mapId).length < 0) {
        this.closeChooseForm();
      } else {
        this.tempAssets = this.assets.filter(this.filterChosenAssets);
        console.log(Object.keys(this.mapId));
        this.$emit("chosenAssets", this.tempAssets);
        this.closeChooseForm();
      }
    },

    /**
     * đóng form chọn tài sản
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    closeChooseForm() {
      this.$store.commit("setIsReceiptFormDisplayNone", true);
      this.$store.commit("setIsShowChooseAsset", false);
    },

    /**
     * lọc lấy các tài sản được chọn
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    filterChosenAssets(asset) {
      for (let i = 0; i < Object.keys(this.mapId).length; i++) {
        if (asset.FixedAssetId == Object.keys(this.mapId)[i]) return true;
      }
      return false;
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
      let apiUrl = this.hostApi + "FixedAssets/Receipt/filter-null?" + param;
      await axios
        .get(apiUrl)
        .then((res) => {
          if (this.chosenAssets.length > 0) {
            this.assetsFilter = res.data.Data.filter(this.filterAssets);
          } else {
            this.assetsFilter = res.data.Data;
          }
          this.recordNum = this.assetsFilter.length;

          this.assets = this.assetsFilter.filter(this.filterAssetsPaging);
          this.afterLoad();
        })
        .catch((error) => {
          console.log(error);
        });
      this.setFitContentChooseAsset();
    },

    /**
     * lọc array loại bỏ các element đã có sẵn
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    filterAssets(asset) {
      for (let i = 0; i < this.chosenAssets.length; i++) {
        if (asset.FixedAssetId === this.chosenAssets[i].FixedAssetId)
          return false;
      }
      return true;
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
        if (asset.FixedAssetId === this.assetsFilter[i].FixedAssetId)
          return true;
      }
      return false;
    },

    /**
     * xử lý trước khi load data
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    beforeLoad() {
      document.getElementById("checkAllAsset").checked = false;
      let event = { target: { checked: false } };
      this.selectAllItem(event);
      this.mapId = {};
      this.openLoading();
    },

    /**
     * xử lý sau khi load data
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    afterLoad() {
      this.closeLoading();
      this.genPagination();
    },

    /**
     * bật loading
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    openLoading() {
      let lst = document.querySelectorAll("#chooseAsset td:not(.general td)");
      for (let i = 0; i < lst.length; i++) {
        lst[i].classList.add("skeleton");
      }
    },

    /**
     * tắt loading
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    closeLoading() {
      let lst = document.querySelectorAll("#chooseAsset td:not(.general td)");
      for (let i = 0; i < lst.length; i++) {
        lst[i].classList.remove("skeleton");
      }
      let tblSite = document.querySelectorAll("#chooseAsset tbl-side");
      for (let i = 0; i < tblSite.length; i++) {
        tblSite[i].scrollTop = 0;
        tblSite[i].scrollLeft = 0;
      }
    },

    /**
     * set chiều cao div fit content
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    setFitContentChooseAsset() {
      let heightContent =
        document.getElementById("chooseAssetlst").offsetHeight;
      let height = document.getElementById("tblChooseAsset").offsetHeight;
      if (height < heightContent - 42) {
        document.getElementById(
          "fit-content-choose"
        ).style.height = `calc(100% - ${height + 42}px)`;
      } else {
        document.getElementById("fit-content-choose").style.height = "0";
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
        this.beforeLoad();
        setTimeout(() => {
          this.assets = this.assetsFilter.filter(this.filterAssetsPaging);
          setTimeout(() => {
            this.closeLoading();
            this.setFitContentChooseAsset();
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
        this.beforeLoad();
        this.pageIndex = 1;
        this.pageSize = size;
        setTimeout(() => {
          this.assets = this.assetsFilter.filter(this.filterAssetsPaging);
          setTimeout(() => {
            this.closeLoading();
            this.setFitContentChooseAsset();
            this.genPagination();
          }, 1);
        }, 500);
      }
    },

    /**
     * chọn đồng loạt
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    selectAllItem(event) {
      let ele = event.target;
      let checkboxs = document.querySelectorAll(
        '#chooseAsset td input[type="checkbox"]'
      );
      if (ele.checked) {
        for (let i = 0; i < this.assets.length; i++) {
          const element = this.assets[i];
          const checkbox = checkboxs[i];
          if (!checkbox.checked) {
            checkbox.parentElement.parentElement.classList.add("selected");
            checkbox.checked = true;
            this.mapId[element.FixedAssetId] = `${element.FixedAssetCode}`;
          }
        }
      } else {
        for (let i = 0; i < this.assets.length; i++) {
          const element = this.assets[i];
          const checkbox = checkboxs[i];
          if (checkbox.checked) {
            checkbox.parentElement.parentElement.classList.remove("selected");
            checkbox.checked = false;
            delete this.mapId[element.FixedAssetId];
          }
          this.startRow = -1;
        }
      }
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
      let checkbox = document.querySelectorAll(
        '#chooseAsset td input[type="checkbox"]'
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
            this.assets[i].FixedAssetId
          ] = `${this.assets[i].FixedAssetCode}`;
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
    },

    /**
     * bỏ chọn các row
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    unselectRow() {
      let checkboxs = document.querySelectorAll(
        '#chooseAsset td input[type="checkbox"]'
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
        '#chooseAsset td input[type="checkbox"]'
      );
      let count = 0;
      for (let i = 0; i < checkboxs.length; i++) {
        const checkbox = checkboxs[i];
        if (checkbox.checked) {
          count++;
        }
      }
      if (count == checkboxs.length) {
        document.getElementById("checkAllAsset").checked = true;
      } else {
        document.getElementById("checkAllAsset").checked = false;
      }
      if (count == 0) this.startRow = -1;
    },

    /**
     * tính khấu hao lũy kế
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    setDepreciation(cost, rate) {
      return parseInt((cost * rate) / 100);
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
      const form = document.getElementById("chooseAsset");
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

    /**
     * set Tooltip nếu text dài quá
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */ setTooltip() {
      let cells = document.querySelectorAll("#chooseAsset tbody tr td");
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
.m-form.choose-asset-form {
  z-index: 100;
}

.form-site.choose-asset {
  width: 965px;
}

.f-content.choose-asset-content {
  padding: 16px 0 0;
  border-top: 1px solid #e5e5e5;
  display: flex;
  flex-direction: column;
}

.f-content.choose-asset-content .tbl-site {
  margin-top: 14px;
  height: 455px;
  overflow-y: auto;
  overflow-x: hidden;
}

.f-content.choose-asset-content .tbl-site .general {
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