<template>
  <div>
    <div
      class="m-context p-y-2 p-x-1"
      tabindex="0"
      ref="context"
      :style="contextStyle"
      @blur="hideMenuContext()"
    >
      <div class="context-item pointer" @click="addItem()" v-if="hasAdd">
        <div class="icon-24 icon-add m-r-10"></div>
        <div class="duplicate">Thêm mới</div>
      </div>
      <div class="context-item pointer" @click="editItem()">
        <div class="icon-24 icon-edit m-r-10"></div>
        <div class="duplicate">Sửa</div>
      </div>
      <div class="context-item pointer" @click="deleteItem()">
        <div class="icon-24 icon-delete m-r-10"></div>
        <div class="delete">Xóa</div>
      </div>
      <div
        class="context-item pointer"
        @click="replicationItem()"
        v-if="hasReplication"
      >
        <div class="icon-24 icon-replication m-r-10"></div>
        <div class="duplicate">Nhân bản</div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ["top", "left", "mapId", "replication", "add"],
  computed: {
    contextStyle: function () {
      return {
        top: (this.top || -9999) + "px",
        left: (this.left || -9999) + "px",
      };
    },
  },
  data() {
    return {
      hasAdd: true,
      hasReplication: true,
    };
  },
  created() {
    if (this.add != undefined || this.add != null) {
      this.hasAdd = this.add;
    }
    if (this.replication != undefined || this.replication != null) {
      this.hasReplication = this.replication;
    }
  },
  mounted() {
    this.$refs.context.focus();
  },
  methods: {
    /**
     * ẩn menu context
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    hideMenuContext() {
      this.$store.commit("setIsShowMenuContext", false);
      let id = Object.keys(this.mapId)[0];
      if (Object.keys(this.mapId).length > 1) {
        delete this.mapId[id];
      }
      console.log(this.mapId);
    },

    /**
     * xóa tài sản
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    deleteItem() {
      this.hideMenuContext();
      this.$emit("delete");
    },

    /**
     * nhân bản tài sản
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    replicationItem() {
      this.hideMenuContext();
      let id = "";
      if (Object.keys(this.mapId).length > 1) {
        id = Object.keys(this.mapId)[1];
      } else {
        id = Object.keys(this.mapId)[0];
      }
      this.$emit("replication", id);
    },

    /**
     * sửa tài sản
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    editItem() {
      this.hideMenuContext();
      let id = "";
      if (Object.keys(this.mapId).length > 1) {
        id = Object.keys(this.mapId)[1];
      } else {
        id = Object.keys(this.mapId)[0];
      }
      this.$emit("edit", id);
    },

    /**
     * thêm mới
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    addItem() {
      this.hideMenuContext();
      this.$emit("add");
    },
  },
};
</script>

<style>
.m-context {
  position: absolute;
  z-index: 500;
  border: 1px solid #babec5;
  background-color: #fff;
  transform: translate(-100%, -50%);
  transition: 0.05s;
  outline: none;
  padding: 3px 0;
  border-radius: 4px;
  width: 120px;
}

.context-item {
  padding: 2px 8px;
  text-overflow: ellipsis;
  white-space: nowrap;
  overflow: hidden;
  user-select: none;
  display: flex;
  align-items: center;
}

.context-item:hover {
  background-color: #d3d5d8;
  transition: 0.2s;
}
.context-item:active {
  background-color: #d1edf4;
  transition: 0.2s;
}
</style>