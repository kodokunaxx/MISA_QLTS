<template>
  <div class="m-dialog">
    <div class="m-dialog-error-main">
      <div class="m-dialog-content">
        <div class="icon-40 icon-warning"></div>
        <div class="error-text m-l-10"></div>
      </div>
      <div class="m-dialog-footer">
        <div class="button-field dis-flex">
          <button
            class="m-btn m-btn-outline"
            v-if="isShowBtnOutline"
            @click="handleOutlineButton()"
          >
            <div class="label">{{ btnOutline }}</div>
          </button>
          <button
            class="m-btn m-btn-sub m-l-8"
            v-if="isShowBtnSub"
            @click="handleSublineButton()"
          >
            <div class="label">{{ btnSub }}</div>
          </button>
          <button class="m-btn m-l-8" @click="handleMainButton()">
            <div class="label">{{ btnMain }}</div>
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ["dialogMode", "mapId", "faCode", "nameRequired"],
  components: {},
  data() {
    return {
      mode: this.dialogMode,
      btnMain: "",
      btnSub: "",
      btnOutline: "",
      isShowBtnOutline: true,
      isShowBtnSub: true,
      urlApi: this.$store.getters.HostApi + "FixedAssets/",
      method: "",
      btnText: this.$store.getters.ButtonText,
      modeList: this.$store.getters.DialogMode,
      countItem: this.mapId ? Object.values(this.mapId).length : 0,
    };
  },
  mounted() {
    this.setDialogByMode();
  },
  methods: {
    /**
     * xử lý sự kiện cho sub button
     */
    handleSublineButton() {
      switch (this.mode) {
        case this.modeList.Changed:
          this.closeDialogAndForm();
          break;

        default:
          break;
      }
    },

    /**
     * xử lý sự kiện cho outline button
     */
    handleOutlineButton() {
      switch (this.mode) {
        case this.modeList.Delete:
          this.closeDialog();
          break;
        case this.modeList.MultiDelete:
          this.closeDialog();
          break;
        case this.modeList.Changed:
          this.closeDialog();
          break;
        case this.modeList.CancelAdd:
          this.closeDialog();
          break;

        default:
          break;
      }
    },

    /**
     * xử lý sự kiện cho main button
     */
    handleMainButton() {
      switch (this.mode) {
        case this.modeList.Delete:
          this.$emit("funct");
          break;
        case this.modeList.MultiDelete:
          this.$emit("funct");
          break;
        case this.modeList.ErrorDelete:
          this.closeDialog();
          break;
        case this.modeList.ErrorMultiDelete:
          this.closeDialog();
          break;
        case this.modeList.Duplicate:
          this.closeDialog();
          break;
        case this.modeList.GetByIdFail:
          this.closeDialog();
          this.$store.commit("setIsShowForm", false);
          break;
        case this.modeList.Changed:
          this.closeDialog();
          this.$emit("funct");
          break;
        case this.modeList.CancelAdd:
          this.closeDialogAndForm();
          break;
        case this.modeList.Required:
          this.closeDialogFocus();
          break;
        case this.modeList.FixRate:
          this.closeDialogFocus();
          break;
        case this.modeList.FixDYear:
          this.closeDialogFocus();
          break;
        default:
          break;
      }
    },

    /**
     * set text, event theo dialogMode
     */
    setDialogByMode() {
      let dialogContent = document.querySelector(
        ".m-dialog-content .error-text"
      );
      switch (this.mode) {
        // Dialog Xoá
        case this.modeList.Delete:
          this.btnOutline = this.btnText.No;
          this.btnMain = this.btnText.Delete;
          this.isShowBtnSub = false;
          this.$store.commit("setDelete", Object.values(this.mapId)[0]);
          dialogContent.innerHTML = this.$store.getters.Delete;
          break;

        // Dialog Xoá nhiều
        case this.modeList.MultiDelete:
          this.btnOutline = this.btnText.No;
          this.btnMain = this.btnText.Delete;
          this.isShowBtnSub = false;
          this.$store.commit(
            "setMultiDelete",
            this.handleCount(this.countItem)
          );
          dialogContent.innerHTML = this.$store.getters.MultiDelete;
          break;
        // Dialog Trùng mã
        case this.modeList.Duplicate:
          this.btnMain = this.btnText.Close;
          this.isShowBtnOutline = false;
          this.isShowBtnSub = false;
          this.$store.commit("setDuplicate", this.faCode);
          dialogContent.innerHTML = this.$store.getters.Duplicate;
          break;
        // Dialog Xóa lỗi
        case this.modeList.ErrorDelete:
          this.btnMain = this.btnText.Close;
          this.isShowBtnOutline = false;
          this.isShowBtnSub = false;
          dialogContent.innerHTML = this.$store.getters.ErrorDelete;
          break;
        // Dialog Xoá nhiều lỗi
        case this.modeList.ErrorMultiDelete:
          this.btnMain = this.btnText.Close;
          this.isShowBtnOutline = false;
          this.isShowBtnSub = false;
          this.$store.commit(
            "setErrorMultiDelete",
            this.handleCount(this.countItem)
          );
          dialogContent.innerHTML = this.$store.getters.ErrorMultiDelete;
          break;
        // Dialog Lấy thông tin theo Id lỗi
        case this.modeList.GetByIdFail:
          this.btnMain = this.btnText.Close;
          this.isShowBtnOutline = false;
          this.isShowBtnSub = false;
          dialogContent.innerHTML = this.$store.getters.GetByIdFail;
          break;
        case this.modeList.Changed:
          this.btnMain = this.btnText.Save;
          this.btnSub = this.btnText.NotSave;
          this.btnOutline = this.btnText.Cancel;
          dialogContent.innerHTML = this.$store.getters.Changed;
          break;
        case this.modeList.CancelAdd:
          this.btnMain = this.btnText.Cancel;
          this.btnOutline = this.btnText.No;
          this.isShowBtnSub = false;
          dialogContent.innerHTML = this.$store.getters.CancelAdd;
          break;
        case this.modeList.Required:
          this.btnMain = this.btnText.Close;
          this.isShowBtnOutline = false;
          this.isShowBtnSub = false;
          this.$store.commit(
            "setRequired",
            this.nameRequired.slice(0, this.nameRequired.length - 1)
          );
          dialogContent.innerHTML = this.$store.getters.Required;
          break;
        case this.modeList.FixRate:
          this.btnMain = this.btnText.Close;
          this.isShowBtnOutline = false;
          this.isShowBtnSub = false;
          dialogContent.innerHTML = this.$store.getters.FixRate;
          break;
        case this.modeList.FixDYear:
          this.btnMain = this.btnText.Close;
          this.isShowBtnOutline = false;
          this.isShowBtnSub = false;
          dialogContent.innerHTML = this.$store.getters.FixDYear;
          break;
        default:
          break;
      }
    },

    /**
     * xử lý số lượng tài sản xóa
     */
    handleCount(count) {
      if (count < 10) return `0${count}`;
      return count;
    },

    /**
     * đóng dialog
     */
    closeDialog() {
      this.$store.commit("setIsShowDialog", false);
      this.$store.commit("setIsShowDialogForm", false);
    },

    /**
     * đóng dialog + form
     */
    closeDialogAndForm() {
      this.$store.commit("setIsShowDialog", false);
      this.$store.commit("setIsShowDialogForm", false);
      this.$store.commit("setIsShowForm", false);
    },

    /**
     * close Dialog and focus
     */
    closeDialogFocus() {
      this.$store.commit("setIsShowDialogForm", false);
      this.$emit("required");
    },
  },
};
</script>

<style>
.m-dialog {
  position: absolute;
  top: 0;
  left: 0;
  bottom: 0;
  right: 0;
  background-color: rgba(0, 0, 0, 0.6);
  z-index: 150;
  display: flex;
  align-items: center;
  justify-content: center;
  white-space: normal;
}

.m-dialog .m-dialog-error-main {
  width: 444px;
  height: auto;
  background-color: #fff;
  border-radius: 4px;
  padding: 24px 0;
}

.m-dialog .m-dialog-content {
  display: flex;
  padding: 0 16px;
  align-items: center;
  margin-top: 24px;
}

.m-dialog .m-dialog-footer {
  margin-top: 24px;
  display: flex;
  align-items: center;
  justify-content: right;
  border-radius: 0 0 4px 4px;
  padding: 0 24px;
}
</style>