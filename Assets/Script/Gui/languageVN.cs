﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class languageVN : MonoBehaviour {

    //text canvas login
    public Text txtLogin;
    public Text txtSignUp;
    public Text txtUser;
    public Text txtPass;
    //text canvas game
    public Text txtStart;
    public Text txtTitleShop;
    public Text txtTitleItem1;
    public Text txtTitleItem2;
    public Text txtTitleItem3;
    public Text txtTitleItem4;
    public Text txtTitleItem5;
    public Text txtTitleItem6;
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
	}
}
