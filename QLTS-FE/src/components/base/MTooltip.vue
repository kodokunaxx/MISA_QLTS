<template>
  <div
    class="m-tooltip"
    :style="`left: ${left}; right: ${right}; bottom: ${bottom};`"
  >
    {{ content }}
  </div>
</template>

<script>
export default {
  props: ["content", "left", "right", "bottom"],
};
</script>

<style>
.m-tooltip {
  position: absolute;
  bottom: -26px;
  padding: 4px 6px;
  color: #000;
  border-radius: 3px;
  background-color: #ffffff;
  display: none;
  box-shadow: 0 3px 10px rgb(0 0 0 / 25%), 0 10px 10px rgb(0 0 0 / 22%);
  z-index: 1000;
  text-overflow: ellipsis;
  white-space: nowrap;
  font-weight: normal !important;
  user-select: none;
}

.m-tooltip-site:hover .m-tooltip:hover {
  display: none;
}

.m-tooltip-site {
  position: relative !important;
  overflow: visible !important;
}

.m-tooltip-site:hover .m-tooltip {
  display: block;
}
</style>