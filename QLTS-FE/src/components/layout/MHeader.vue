<template>
  <div class="m-header">
    <div class="m-header-left">Danh sách tài sản</div>
    <div class="m-header-right">
      <div class="m-x-8">Sở tài chính</div>
      <div class="year-side m-x-8 dis-flex align-items-center">
        <div class="m-x-8">Năm</div>
        <div class="m-l-8" style="font-family: MISARoboto-Bold">2021</div>
        <div class="icon-24 pointer icon-up-down"></div>
      </div>
      <div class="icon-24 pointer m-tooltip-site action icon-notify m-x-8">
        <Tooltip :content="'Thông báo'" :left="'-26px'" />
      </div>
      <div class="icon-24 pointer m-tooltip-site action icon-system m-x-8">
        <Tooltip :content="'Chức năng'" :left="'-26px'" />
      </div>
      <div class="icon-24 pointer m-tooltip-site action icon-support m-x-8">
        <Tooltip :content="'Hỗ trợ'" :left="'-16px'" />
      </div>
      <div class="icon-24 pointer m-tooltip-site action icon-user m-l-8">
        <Tooltip :content="'Cá nhân'" :left="'-16px'" />
      </div>
      <div class="icon-24 pointer action icon-down"></div>
    </div>
  </div>
</template>

<script>
import Tooltip from "../base/MTooltip.vue";
export default {
  components: {
    Tooltip,
  },
};
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
  font-family: MISARoboto-Bold;
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