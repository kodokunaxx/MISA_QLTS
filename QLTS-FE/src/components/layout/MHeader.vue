<template>
  <div class="m-header">
    <div class="m-header-left">Danh sách tài sản</div>
    <div class="m-header-right">
      <div class="m-x-8">Sở tài chính</div>
      <div class="year-side m-x-8 dis-flex align-items-center">
        <div class="m-x-8">Năm</div>
        <div class="m-l-8" style="font-family: MISAGoogleSans-Bold">2021</div>
        <div class="icon-24 pointer icon-up-down"></div>
      </div>
      <div class="icon-24 pointer action icon-notify m-x-8"></div>
      <div class="icon-24 pointer action icon-system m-x-8"></div>
      <div class="icon-24 pointer action icon-support m-x-8"></div>
      <div class="icon-24 pointer action icon-user m-l-8"></div>
      <div class="icon-24 pointer action icon-down"></div>
    </div>
  </div>
</template>

<script>
export default {};
</script>

<style>
.m-header {
  height: 44px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding-left: 16px;
  padding-right: 16px;
  width: 100%;
}
.m-header .m-header-left {
  font-family: MISAGoogleSans-Bold;
  font-size: 16px;
}

.m-header .m-header-right {
  display: flex;
  align-items: center;
}

.m-header-right .year-side {
  height: 30px;
  border-radius: 4px;
  background-color: #d1edf4;
}
</style>