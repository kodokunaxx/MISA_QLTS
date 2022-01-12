<template>
  <div class="m-menu" id="menu">
    <div class="logo-box">
      <div class="icon-36 icon-home" @click="zoomInOutMenu()"></div>
      <div class="app-title" v-if="isFullMenu">MISA QLTS</div>
    </div>
    <div class="menu-list">
      <div v-for="(item, i) in items" :key="i">
        <div
          class="menu-item"
          :class="i === index ? 'selected' : ''"
          @click="(index = i), redirect(item.title)"
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
              <div
                class="icon-14 icon-dropdown"
                @click="item.title === 'Tài sản' ? setShowAction() : ''"
              ></div>
            </div>
          </div>
        </div>
        <div
          class="menu-action"
          v-if="item.actions.length > 0"
          v-show="isShowAction"
        >
          <div
            class="action-item"
            v-for="(action, i_a) in item.actions"
            :key="i_a"
            @click="(index_a = i_a), redirect(action)"
            :class="i_a === index_a ? 'selected' : ''"
          >
            <div class="icon-box">
              <div
                class="icon-24"
                :class="i_a === index_a ? 'icon-right-arrow' : ''"
              ></div>
            </div>
            <div class="a-title" v-if="isFullMenu">{{ action }}</div>
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
      index_a: -1,
      items: [
        {
          icon: "icon-overview",
          title: "Tổng quan",
          isDropdown: false,
          actions: [],
        },
        {
          icon: "icon-asset",
          title: "Tài sản",
          isDropdown: true,
          actions: [
            "Ghi tăng",
            "Thay đổi thông tin",
            "Đánh giá lại",
            "Tính hao mòn",
            "Điều chuyển tài sản",
            "Ghi giảm",
            "Kiểm kê",
            "Khác",
          ],
        },
        {
          icon: "icon-async-asset",
          title: "Tài sản HT-ĐB",
          isDropdown: true,
          actions: [],
        },
        {
          icon: "icon-tool",
          title: "Công cụ dụng cụ",
          isDropdown: true,
          actions: [],
        },
        {
          icon: "icon-category",
          title: "Danh mục",
          isDropdown: false,
          actions: [],
        },
        {
          icon: "icon-lookup",
          title: "Tra cứu",
          isDropdown: true,
          actions: [],
        },
        {
          icon: "icon-report",
          title: "Báo cáo",
          isDropdown: true,
          actions: [],
        },
      ],
      isFullMenu: false,
      isShowAction: false,
    };
  },
  mounted() {
    // this.zoomInOutMenu();
  },
  methods: {
    /**
     * sự kiện thu phóng menu
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    zoomInOutMenu() {
      if (this.isFullMenu) {
        this.isShowAction = false;
        this.isFullMenu = false;
        this.index_a = -1;
        document.getElementById("menu").style.animation =
          "togglehide 0.25s linear";
        setTimeout(() => {
          document.getElementById("menu").style.minWidth = "66px";
        }, 240);
      } else {
        document.getElementById("menu").style.animation =
          "toggleshow 0.25s linear";
        setTimeout(() => {
          this.isFullMenu = true;
          document.getElementById("menu").style.minWidth = "200px";
        }, 240);
      }
    },

    /**
     * sự kiện ẩn hiện menu tài sản
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    setShowAction() {
      this.index = 1;
      event.stopPropagation();
      this.isShowAction = !this.isShowAction;
    },

    /**
     * sự kiện chuyển hướng
     * CreatedBy: Đỗ Mạnh Hà - 01/01/2022
     */
    redirect(val) {
      switch (val) {
        case "Tài sản":
          this.$router.push("/").catch((error) => {
            if (error.name != "NavigationDuplicated") {
              throw error;
            }
          });
          this.index_a = -1;
          document.getElementById("menu").style.animation =
            "toggleshow 0.25s linear";
          setTimeout(() => {
            this.isFullMenu = true;
            document.getElementById("menu").style.minWidth = "200px";
            this.isShowAction = true;
          }, 240);
          break;
        case "Ghi tăng":
          this.$router.push("Receipt").catch((error) => {
            if (error.name != "NavigationDuplicated") {
              throw error;
            }
          });
          break;

        default:
          break;
      }
    },
  },
};
</script>

<style>
.m-menu {
  background-color: #1c3048;
  box-shadow: inset 0 3px 6px rgba(0, 0, 0, 0.16);
  min-width: 66px;
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
  font-family: MISARoboto-Bold;
  font-size: 20px;
}

.m-menu .menu-list {
  margin-top: 15px;
  height: calc(100vh - 113px);
}

.menu-list .menu-item {
  display: flex;
  color: #ffffff;
  align-items: center;
  cursor: pointer;
  user-select: none;
  opacity: 0.4;
  border-radius: 4px;
  margin: 0 11px 0 11px;
  height: 40px;
}

.menu-list .menu-action {
  display: block;
}

.menu-list .action-item {
  background-color: #33455b;
  display: flex;
  color: rgba(255, 255, 255, 0.4);
  align-items: center;
  cursor: pointer;
  user-select: none;
  margin: 0 11px 0 11px;
  height: 40px;
}

.menu-list .action-item.selected,
.action-item:hover {
  background-color: rgba(255, 255, 255, 0.2);
  color: rgba(255, 255, 255, 1);
  border-radius: 4px;
}

.menu-list .menu-item:hover {
  color: #ffffff;
  background-color: #0582a2;
  opacity: 1;
}

.menu-list .menu-item:active {
  color: #ffffff;
  background-color: #28b7dc;
  opacity: 1;
}

.menu-list .icon-box {
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
  opacity: 1;
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
  opacity: 0.4;
  border-radius: 4px;
  border: 2px solid #32445a;
  margin: 0 11px;
  cursor: pointer;
}

.border-solid:hover {
  background-color: #31526a;
}

@keyframes toggleshow {
  0% {
    min-width: 66px;
  }
  100% {
    min-width: 200px;
  }
}

@keyframes togglehide {
  0% {
    min-width: 200px;
  }
  100% {
    min-width: 66px;
  }
}
</style>