using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class Notification : MonoBehaviour {

    public GameObject panelError;
    public Text message;
    public static Notification notify;
    System.Net.WebClient client;
    System.IO.Stream stream;
    private bool isError = false;
    private string textError;
    public GameObject btnSignIn;
    public GameObject txtForgotMeNot;
    private bool isForgotError = false;
    public static int NETWORK_ERROR = 0;
    public static int WARRNING_ERROR = 1;
    public static int FORGOT_ERROR = 2;
    private bool isNetworkError = false;

    void Awake() {
        notify = this;
        isConectInternet();
    }

    void Update() {
        if (isError)
        {
            panelError.SetActive(true);
            message.text = textError;
        }
        else {
            panelError.SetActive(false);
        }
        if (isForgotError)
        {
            txtForgotMeNot.SetActive(true);
        }
        else {
            txtForgotMeNot.SetActive(false);
        }
    }

    public void btnExitError() {
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
        btnSignIn.GetComponent<Button>().interactable = true;     
    }

    public static void messageError(string message, int errorCode)
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
        }
        notify.isError = true;
        notify.textError = message;
        
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
            Debug.Log(ex);
            messageError("Không có kết nối mạng", Notification.NETWORK_ERROR);
            return false;
        }
    }
}
