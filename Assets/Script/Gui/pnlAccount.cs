using UnityEngine;
using System.Collections;

public class pnlAccount : MonoBehaviour {

    public static pnlAccount pnlAcc;
    public GameObject panelChildren;
    public Animator animLogin;
    public GameObject canvas;

	void Start () {
        pnlAcc = this;
	}

    public static void loginAnim() {
        pnlAcc.animLogin.enabled = true;
        pnlAcc.canvas.SetActive(true);
    }
}
