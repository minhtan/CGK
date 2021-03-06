﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class Notification : MonoBehaviour {

    public GameObject panelError;
    public Text message;
    public Text titleError;
    public static Notification notify;
    System.Net.WebClient client;
    System.IO.Stream stream;
    private bool isError = false;
    private string textError;
    private string textTitle;
    public GameObject btnSignIn;
    public GameObject btnForgotMeNot;
    private bool isForgotError = false;
    public static int NETWORK_ERROR = 0;
    public static int WARRNING_ERROR = 1;
    public static int FORGOT_ERROR = 2;
    public static int END_COIN = 3;
    public static int RELOAD_SENCE = 4;
    private bool isNetworkError = false;
    public Animator animShop;
    public GameObject panelMainError;
    private bool isReloadSence = false;
    public GameObject btnExit;
    public GameObject txtLoading;

    void Awake() {
        notify = this;
        isConectInternet();
    }

    void Update() {
        if (isError)
        {
            panelError.SetActive(true);
            message.text = textError;
            titleError.text = textTitle;
            txtLoading.SetActive(false);
        }
        else {
            panelError.SetActive(false);
        }
        if (isForgotError)
        {
            btnForgotMeNot.SetActive(true);
            btnExit.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0f);
            btnExit.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 0.2f);
        }
        else {
            btnExit.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            btnExit.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0.2f);
            btnForgotMeNot.SetActive(false);
        }
    }

    public void btnExitErrorClick() {
        isError = false;
        if (isNetworkError)
        {
            if (isConectInternet())
            {
                isNetworkError = false;
            }    
        }
        if (isForgotError)
        {
            isForgotError = false;
        }
        if(isReloadSence){
            Application.LoadLevel("game");
            isReloadSence = false;
        }
        btnSignIn.GetComponent<Button>().interactable = true;     
    }

    public static void messageError(string message, string title, int errorCode)
    {
        switch(errorCode){
            case 0:
                notify.isNetworkError = true;
                break;
            case 1: 
                break;
            case 2:
                notify.isForgotError = true;
                break; 
            case 4:
                notify.isReloadSence = true;
                break;
        }
        notify.isError = true;
        notify.textError = message;
        notify.textTitle = title;
    }

    public static bool isConectInternet()
    {
        try
        {
            notify.client = new System.Net.WebClient();
            notify.stream = notify.client.OpenRead("http://www.google.com");
            notify.stream.Close();
            Debug.Log("conect internet");
            return true;
        }
        catch (Exception ex)
        {
            messageError("Không có kết nối mạng, kiểm tra đường truyền internet", "Lỗi mạng", Notification.NETWORK_ERROR);
            return false;
        }
    }
}
