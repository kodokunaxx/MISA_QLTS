<template>
  <div id="app">
    <Menu />
    <div class="content">
      <Header />
      <Content />
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
