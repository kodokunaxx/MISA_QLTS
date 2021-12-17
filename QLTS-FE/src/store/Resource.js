export const Method = {
    Add: "POST",
    Edit: "PUT",
    View: "GET",
    Delete: "DELETE",
}

export const TitleForm = {
    Add: "Thêm mới tài sản",
    Edit: "Sửa tài sản",
    Replication: "Nhân bản tài sản"
}

export const DialogMode = {
    Duplicate: "Duplicate",
    CancelAdd: "CancelAdd",
    Changed: "Changed",
    Delete: "Delete",
    MultiDelete: "MultiDelete",
    ErrorDelete: "ErrorDelete",
    ErrorMultiDelete: "ErrorMultiDelete",
    GetByIdFail: "GetByIdFail",
    Required: "Required",
    FixRate: "FixRate",
    FixDYear: "FixDYear",
    ErrorDate: "ErrorDate"
}

export const Button = {
    Save: "Lưu",
    Cancel: "Hủy bỏ",
    NotSave: "Không lưu",
    No: "Không",
    Delete: "Xóa",
    Close: "Đóng",
    Agree: "Đồng ý"
}

export const Validate = {
    Email: 'Email chưa đúng định dạng!',
    Required: "Không được phép để trống!",
}
export const Warning = {
    Duplicate: (payload) => {
        return `Mã tài sản <span style="font-family: MISAGoogleSans-Bold;">${payload}</span> đã tồn tại trên hệ thống!`;
    },
    CancelAdd: `Bạn có muốn hủy khai báo tài sản này?`,
    Changed: `Thông tin thay đổi sẽ không được cập nhật nếu bạn không lưu. Bạn có muốn lưu các thay đổi này?`,
    Delete: (payload) => {
        return `Bạn có thực sự muốn xóa tài sản <span style="font-family: MISAGoogleSans-Bold;">${payload}</span>?`;
    },
    MultiDelete: (payload) => {
        return `<span style="font-family: MISAGoogleSans-Bold;">${payload}</span> tài sản này được chọn. Bạn có muốn xóa các tài sản này khỏi danh sách?`;
    },
    ErrorDelete: `Không thể xóa tài sản này vì đã có chứng từ phát sinh.`,
    ErrorMultiDelete: (payload) => {
        return `<span style="font-family: MISAGoogleSans-Bold;">${payload}</span> tài sản được chọn không thể xóa. Vui lòng kiểm tra lại tài sản trước khi thực hiện xóa.`;
    },
    GetByIdFail: `Không thể thực hiện vì đã có chứng từ phát sinh.`,
    Required: (payload) => {
        return `Cần phải nhập thông tin <span style="font-family: MISAGoogleSans-Bold;">${payload}</span>.`;
    },
    FixRate: "Tỷ lệ hao mòn phải bằng 1/Số năm sử dụng.",
    FixDYear: "Hao mòn năm phải nhỏ hơn hoặc bằng nguyên giá.",
    ErrorDate: "Ngày mua không được sau ngày sử dụng."
}

export const Toast = {
    Add: "Thêm tài sản thành công!",
    Edit: "Sửa thông tin tài sản thành công!",
    Delete: "Xóa tài sản thành công!",
}