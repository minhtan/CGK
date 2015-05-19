using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class languageVN : MonoBehaviour {

    //text canvas login
    public Text txtLogin;
    public Text txtSignUp;
    public Text txtUser;
    public Text txtPass;
    public Text txtBtnExit;
    public Text txtForgotUser;
    public Text txtTitleForgot;
    public Text txtContentForgot;
    public Text txtInputForgot;
    public Text txtBtnOkForgot;
    public Text txtBtnExitForgot;
    //text canvas game
    public Text txtStart;
    public Text txtTitleShop;
    public Text txtTitleItem1;
    public Text txtTitleItem2;
    public Text txtTitleItem3;
    public Text txtTitleItem4;
    public Text txtTitleItem5;
    public Text txtTitleItem6;
    //text signUp
    public Text txtUserUp;
    public Text txtPassUp;
    public Text txtPassReUp;
    public Text txtEmail;
    public Text txtPhone;
    public Text btnSignUp;
    //text EditAccount
    public Text txtEditPass;
    public Text txtEditEmail;
    public Text txtEditPhone;
    public Text txtBtnEditAccount;
    public Text txtTitleEditAccount;
    public Text txtBtnChangeEmail;
    public Text txtBtnChangePhone;
    //text editPass
    public Text txtOldPass;
    public Text txtNewPass;
    public Text txtReNewPass;
    //text edit email
    public Text txtNewEmail;
    public Text txtConfirmMail;
    //text edit phone
    public Text txtNewPhone;
    public Text txtConfirmPhone;

	void Start () {
        txtLogin.text = "Đăng nhập";
        txtSignUp.text = "Đăng ký";
        txtUser.text = "Tên đăng nhập";
        txtPass.text = "Mật khẩu";
        txtStart.text = "Bắt đầu";
        txtTitleShop.text = "Cửa hàng";
        txtTitleItem1.text = "Gói 10 xèng";
        txtTitleItem2.text = "Gói 60 xèng";
        txtTitleItem3.text = "Gói 120 xèng";
        txtTitleItem4.text = "Gói 250 xèng";
        txtTitleItem5.text = "Gói 600 xèng";
        txtTitleItem6.text = "Gói 1250 xèng";
        txtUserUp.text = "Tên đăng nhập";
        txtPassUp.text = "Mật khẩu";
        txtEmail.text = "Hòm thư";
        txtPhone.text = "Số điện thoại";
        txtPassReUp.text = "Xác nhận mật khẩu";
        btnSignUp.text = "Đăng ký";
        // edit account
        txtEditPass.text = "Đổi mật khẩu";
        txtEditEmail.text = "Đổi email";
        txtEditPhone.text = "Đổi số điện thoại";
        txtBtnEditAccount.text = "Đồng ý";
        //edit pass
        txtOldPass.text = "Mật khẩu cũ";
        txtNewPass.text = "Mật khẩu mới";
        txtReNewPass.text = "Nhập lại mật khẩu mới";
        //edit email
        txtNewEmail.text = "Email mới";
        txtConfirmMail.text = "Nhập password để xác nhận";
        //edit phone
        txtNewPhone.text = "Số điện thoại mới";
        txtConfirmPhone.text = "Nhập password để xác nhận";
        txtBtnExit.text = "Xác nhận";
        txtForgotUser.text = "Bạn muốn lấy lại mật khẩu?";
        txtTitleForgot.text = "Lấy lại mật khẩu";
        txtContentForgot.text = "Mật khẩu của bạn sẽ được gửi vào hòm mail đã được đăng ký.";
        txtInputForgot.text = "Email của bạn";
        txtBtnOkForgot.text = "Đồng ý";
        txtBtnExitForgot.text = "Bỏ qua";
        txtBtnChangeEmail.text = "Đồng ý";
        txtBtnChangePhone.text = "Đồng ý";
        txtTitleEditAccount.text = "Thay đổi thông tin";
    }
}
