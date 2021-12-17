<template>
  <div id="app">
    <Menu />
    <div class="content">
      <Header />
      <Content />
    </div>
    <div class="fix-screen" v-if="isFalseSize">
      <img
        src="./assets/icon/not-support.png"
        height="120px"
        width="120px"
        alt=""
      />
      <div class="title m-t-12">
        Hiện tại chưa hỗ trợ màn hình này, yêu cầu tối thiểu 1024x768
      </div>
    </div>
  </div>
</template>

<script>
import Menu from "./components/layout/MMenu.vue";
import Header from "./components/layout/MHeader.vue";
import Content from "./components/layout/MContent.vue";
export default {
  name: "App",
  components: {
    Menu,
    Header,
    Content,
  },
  data() {
    return {
      isFalseSize: false,
    };
  },
  mounted() {
    window.addEventListener("resize", this.reportWindowSize);
  },
  methods: {
    reportWindowSize() {
      const heightOutput = window.outerHeight;
      const widthOutput = window.outerWidth;
      const cHeight = document.documentElement.clientHeight;
      const cWidth = document.documentElement.clientWidth;
      if (
        heightOutput < 768 ||
        widthOutput < 1024 ||
        cWidth < 850 ||
        cHeight < 350
      ) {
        this.isFalseSize = true;
      } else {
        this.isFalseSize = false;
      }
    },
  },
};
</script>

<style>
@import "./assets/css/main.css";
#app {
  display: flex;
  margin: 0;
}

#app .content {
  width: 100%;
  min-width: calc(100vw - 200px);
}

.fix-screen {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  position: absolute;
  width: 100%;
  height: 100%;
  z-index: 500;
  background-color: #ffffff;
}

.fix-screen .title {
  font-family: MISAGoogleSans-Medium;
  font-size: 15px;
  text-align: center;
  color: #333;
}

::-webkit-scrollbar {
  width: 5px;
  height: 6px;
}

::-webkit-scrollbar-track {
  background: #f4f4f4;
}

::-webkit-scrollbar-thumb {
  background: #e5e5e5;
  border-radius: 10px;
}

::-webkit-scrollbar-thumb:hover {
  background: #bbbbbb;
}

input[type="checkbox"] {
  cursor: pointer;
  width: 16px;
  height: 16px;
}

.icon-24.action:hover {
  background-color: #dbdbdb;
  border-radius: 4px;
}

.icon-24.action:active {
  background-color: #ededed;
  border-radius: 4px;
}
</style>
