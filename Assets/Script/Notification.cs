using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class Notification : MonoBehaviour {

    public GameObject panelError;
    public Text message;
    public Text title;
    public static Notification notify;
    System.Net.WebClient client;
    System.IO.Stream stream;
    private bool isError = false;
    private string textError;


    void Awake() {
        notify = this; 
        conectInternet();
    }

    private void conectInternet() { 
        if(!isConectInternet()){
            messageError("Không có kết nối mạng");
        }
    }

    void Update() {
        if (isError)
        {
            panelError.SetActive(true);
            notify.message.text = textError;
        }
        else {
            panelError.SetActive(false);
        }
    }

    public void btnExitError() {
        isError = false;
    }

    public static void messageError(string message)
    {
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
            return false;
        }
    }
}
