<template>
  <div>
    <div
      class="m-context p-y-2 p-x-1"
      tabindex="0"
      ref="context"
      :style="contextStyle"
      @blur="hideMenuContext()"
    >
      <div class="context-item pointer duplicate" @click="addFA()">
        Thêm mới
      </div>
      <div class="context-item pointer duplicate" @click="editFA()">Sửa</div>
      <div class="context-item pointer delete" @click="deleteFA()">Xóa</div>
      <div class="context-item pointer duplicate" @click="replicationFA()">
        Nhân bản
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ["top", "left", "mapId"],
  computed: {
    contextStyle: function () {
      return {
        top: (this.top || -9999) + "px",
        left: (this.left || -9999) + "px",
      };
    },
  },
  data() {
    return {};
  },
  mounted() {
    this.$refs.context.focus();
  },
  methods: {
    /**
     * ẩn menu context
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
     */
    deleteFA() {
      this.hideMenuContext();
      this.$emit("deleteFA");
    },

    /**
     * nhân bản tài sản
     */
    replicationFA() {
      this.hideMenuContext();
      let id = "";
      if (Object.keys(this.mapId).length > 1) {
        id = Object.keys(this.mapId)[1];
      } else {
        id = Object.keys(this.mapId)[0];
      }
      this.$emit("replicationFA", id);
    },

    /**
     * sửa tài sản
     */
    editFA() {
      this.hideMenuContext();
      let id = "";
      if (Object.keys(this.mapId).length > 1) {
        id = Object.keys(this.mapId)[1];
      } else {
        id = Object.keys(this.mapId)[0];
      }
      this.$emit("editFA", id);
    },

    /**
     * thêm mới
     */
    addFA() {
      this.hideMenuContext();
      this.$emit("addFA");
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
  padding: 0;
}

.context-item {
  padding: 5px 10px;
  text-overflow: ellipsis;
  white-space: nowrap;
  overflow: hidden;
  user-select: none;
}

.context-item:hover {
  background-color: #f5f5f5;
  transition: 0.2s;
}
.context-item:active {
  background-color: #d1edf4;
  transition: 0.2s;
}
</style>