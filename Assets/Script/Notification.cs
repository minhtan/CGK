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

    void Awake() {
        notify = this; 
        conectInternet();
    }

    private void conectInternet() { 
        if(!isConectInternet()){
            messageError("Loi mang", "loi internte");

        }
    }

    public static void messageError(string message, string title) {
        notify.panelError.SetActive(true);
        notify.message.text = message;
        notify.title.text = title;
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
