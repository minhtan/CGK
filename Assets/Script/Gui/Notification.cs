using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Notification : MonoBehaviour {

    public GameObject panelError;
    public Text message;
    public static Notification notify;

    public static void messageError(string message) {
        notify.panelError.SetActive(true);
        notify.message.text = message;
    }
}
