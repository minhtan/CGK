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

    public static languageVN language;
    public string TEXT_OK = "Đồng ý";
    public string TEXT_PASS = "Mật khẩu";
    public string TEXT_CONFIRM_PASS = "Nhập mật khẩu xác nhận";
    public string TEXT_OLD_PASS = "Mật khẩu cũ";
    public string TEXT_NEW_PASS = "Mật khẩu mới";
    public string TEXT_RENEW_PASS = "Nhập lại mật khẩu mới";
    public string TEXT_NEW_EMAIL = "Email mới";
    public string TEXT_NEW_PHONE = "Số điện thoại mới";
    public string TEXT_EMAIL_FORGOTPASS = "Email của bạn";
    public string TEXT_USERNAME = "Tên đăng nhập";
    public string TEXT_SIGNUP_EMAIL = "Email";
    public string TEXT_SIGNUP_PHONE = "Số điện thoại";
    public string TEXT_SGINUP_REPASS = "Xác nhận mật khẩu";
	void Start () {
        language = this;
        txtLogin.text = "Đăng nhập";
        txtSignUp.text = "Đăng ký";
        txtUser.text = TEXT_USERNAME;
        txtPass.text = TEXT_PASS;
        txtStart.text = "Bắt đầu";
        txtTitleShop.text = "Cửa hàng";
        txtTitleItem1.text = "Gói 10 xèng";
        txtTitleItem2.text = "Gói 60 xèng";
        txtTitleItem3.text = "Gói 120 xèng";
        txtTitleItem4.text = "Gói 250 xèng";
        txtTitleItem5.text = "Gói 600 xèng";
        txtTitleItem6.text = "Gói 1250 xèng";
        txtUserUp.text = TEXT_USERNAME;
        txtPassUp.text = TEXT_PASS;
        txtEmail.text = TEXT_SIGNUP_EMAIL;
        txtPhone.text = TEXT_SIGNUP_PHONE;
        txtPassReUp.text = TEXT_SGINUP_REPASS;
        btnSignUp.text = "Đăng ký";
        // edit account
        txtEditPass.text = "Đổi mật khẩu";
        txtEditEmail.text = "Đổi email";
        txtEditPhone.text = "Đổi điện thoại";
        txtBtnEditAccount.text = TEXT_OK;
        //edit pass
        txtOldPass.text = TEXT_OLD_PASS;
        txtNewPass.text = TEXT_NEW_PASS;
        txtReNewPass.text = TEXT_RENEW_PASS;
        //edit email
        txtNewEmail.text = TEXT_NEW_EMAIL;
        txtConfirmMail.text = TEXT_CONFIRM_PASS;
        //edit phone
        txtNewPhone.text = TEXT_NEW_PHONE;
        txtConfirmPhone.text = TEXT_CONFIRM_PASS;
        txtBtnExit.text = "Xác nhận";
        txtForgotUser.text = "Bạn muốn lấy lại mật khẩu?";
        txtTitleForgot.text = "Lấy lại mật khẩu";
        txtContentForgot.text = "Mật khẩu của bạn sẽ được gửi vào hòm mail đã được đăng ký.";
        txtInputForgot.text = TEXT_EMAIL_FORGOTPASS;
        txtBtnOkForgot.text = TEXT_OK;
        txtBtnExitForgot.text = "Bỏ qua";
        txtBtnChangeEmail.text = TEXT_OK;
        txtBtnChangePhone.text = TEXT_OK;
        txtTitleEditAccount.text = "Thay đổi thông tin";
    }
}
