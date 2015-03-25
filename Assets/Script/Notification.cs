using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Notification : MonoBehaviour {

    public GameObject panelError;
    public Text message;
    public Text title;
    public static Notification notify;

    void Start() {
        notify = this;
    }

    public static void messageError(string message, string title) {
        notify.panelError.SetActive(true);
        notify.message.text = message;
        notify.title.text = title;
    }
}
