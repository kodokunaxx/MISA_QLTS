<template>
  <div class="m-loading dis-flex align-items-center justify-content-center">
    <div class="loading-content">
      <img src="../../assets/icon/loading.svg" alt="" />
    </div>
  </div>
</template>

<script>
export default {};
</script>

<style>
.m-loading {
  user-select: none;
  position: absolute;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  background-color: rgba(0, 0, 0, 0.3);
  z-index: 300;
}
.m-loading .loading-content img {
  animation: clockwise 0.8s infinite linear;
}

@keyframes clockwise {
  from {
    transform: rotate(0deg);
  }
  to {
    transform: rotate(360deg);
  }
}
@keyframes anticlockwise {
  from {
    transform: rotate(0deg);
  }
  to {
    transform: rotate(-180deg);
  }
}
</style>