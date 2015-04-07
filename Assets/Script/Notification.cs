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

    void Awake() {
        notify = this;
        isConectInternet();
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
        if (isConectInternet())
        {
            isError = false;
        }
        btnSignIn.GetComponent<Button>().interactable = true;     
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
            messageError("Không có kết nối mạng");
            return false;
        }
    }
}
