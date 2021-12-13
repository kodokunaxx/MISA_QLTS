<template>
  <div class="m-menu" id="menu">
    <div class="logo-box">
      <div class="icon-36 icon-home"></div>
      <div class="app-title" v-if="isFullMenu">MISA QLTS</div>
    </div>
    <div class="menu-list">
      <div
        class="menu-item"
        v-for="(item, i) in items"
        :key="i"
        :class="i === index ? 'selected' : ''"
        @click="index = i"
      >
        <div class="icon-box">
          <div
            class="icon-24"
            :class="i !== index ? item.icon : item.icon + '-selected'"
          ></div>
        </div>
        <div class="side" v-if="isFullMenu">
          <div class="item-title">{{ item.title }}</div>
          <div class="dropdown" v-if="item.isDropdown">
            <div class="icon-14 icon-dropdown"></div>
          </div>
        </div>
      </div>
    </div>
    <div class="menu-footer">
      <div class="hide-side">
        <div
          class="icon-24 border-solid"
          :class="isFullMenu ? 'icon-hide' : 'icon-show'"
          @click="zoomInOutMenu()"
        ></div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      index: 1,
      items: [
        { icon: "icon-overview", title: "Tổng quan", isDropdown: false },
        { icon: "icon-asset", title: "Tài sản", isDropdown: true },
        { icon: "icon-async-asset", title: "Tài sản HT-ĐB", isDropdown: true },
        { icon: "icon-tool", title: "Công cụ dụng cụ", isDropdown: true },
        { icon: "icon-category", title: "Danh mục", isDropdown: false },
        { icon: "icon-lookup", title: "Tra cứu", isDropdown: true },
        { icon: "icon-report", title: "Báo cáo", isDropdown: true },
      ],
      isFullMenu: true,
    };
  },
  mounted() {
    this.zoomInOutMenu();
  },
  methods: {
    zoomInOutMenu() {
      if (this.isFullMenu) {
        this.isFullMenu = false;
        document.getElementById("menu").style.minWidth = "66px";
      } else {
        this.isFullMenu = true;
        document.getElementById("menu").style.minWidth = "200px";
      }
    },
  },
};
</script>

<style>
.m-menu {
  background-color: #1c3048;
  box-shadow: inset 0 3px 6px rgba(0, 0, 0, 0.16);
  min-width: 200px;
  height: 100vh;
}

.m-menu .logo-box {
  height: 50px;
  display: flex;
  margin-top: 7px;
  align-items: center;
  cursor: pointer;
  user-select: none;
}

.logo-box .icon-36 {
  margin: 4px 15px 4px 15px;
}

.logo-box .app-title {
  margin-right: 15px;
  color: #ffffff;
  font-family: MISAGoogleSans-Bold;
  font-size: 20px;
}

.m-menu .menu-list {
  margin-top: 15px;
  height: calc(100vh - 113px);
}

.menu-list .menu-item {
  display: flex;
  align-items: center;
  cursor: pointer;
  user-select: none;
  color: #838e9b;
  border-radius: 4px;
  margin: 0 11px 0 11px;
  height: 40px;
}

.menu-list .menu-item:active {
  color: #ffffff;
  background-color: #28b7dc;
}

.menu-item .icon-box {
  width: 44px;
  height: 40px;
  min-width: 44px;
  min-height: 40px;
  border-radius: 6px;
  display: flex;
  align-items: center;
  justify-content: center;
}

.menu-item.selected {
  background-color: #1aa4c8;
  color: #ffffff;
}

.menu-item .side {
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.menu-item .dropdown {
  margin: 0 8px 0 8px;
}

.m-menu .menu-footer {
  border-top: 1px solid #32445a;
  height: 40px;
  display: flex;
  justify-content: right;
  align-items: center;
}

.border-solid {
  border-radius: 4px;
  border: 2px solid #32445a;
  margin: 0 11px;
  cursor: pointer;
}

.border-solid:hover {
  background-color: #31526a;
}
</style>