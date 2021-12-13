<template>
  <div class="m-toast-mess">
    <div class="m-toast-left">
      <div class="icon-24 icon-toast"></div>
      <div class="m-toast-text m-l-10"><slot></slot></div>
    </div>
    <div class="m-toast-right">
      <div class="btn-close" @click="closeToast()"></div>
    </div>
  </div>
</template>

<script>
export default {
  created() {
    this.$emit("autoCloseToast");
  },
  methods: {
    closeToast() {
      this.$store.commit("setIsShowToast", false);
    },
  },
};
</script>

<style>
.m-toast-mess {
  z-index: 150;
  top: 100px;
  right: -450px;
  width: 444px;
  height: 48px;
  background-color: #fff;
  border-radius: 4px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  position: absolute;
  box-shadow: 1px 2px 1px 2px #bbbbbb;
  animation: toast 5s linear;
}

.m-toast-mess .btn-close {
  cursor: pointer;
  border-radius: 4px;
  width: 24px;
  height: 24px;
  margin-right: 12px;
  background-image: url("../../assets/icon/x.svg");
}

.m-toast-mess .btn-close:hover {
  background-color: #f5f5f5;
}

.m-toast-mess .btn-close:active {
  background-color: #d1edf4;
}
.m-toast-mess .m-toast-left {
  display: flex;
  align-items: center;
  margin-left: 16px;
}

@keyframes toast {
  0% {
    right: -450px;
  }
  5% {
    right: 10px;
  }
  100% {
    right: 10px;
  }
}
</style>