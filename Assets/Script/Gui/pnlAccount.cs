using UnityEngine;
using System.Collections;

public class pnlAccount : MonoBehaviour {

    public static pnlAccount pnlAcc;
    public GameObject panelChildren;
    private RectTransform rectChildren;
    public Animator animLogin;
    public GameObject canvas;

    void Awake() { 
        rectChildren = panelChildren.GetComponent<RectTransform>();
    }

	void Start () {
        pnlAcc = this;
	}

    public static void loginAnim() {
        pnlAcc.animLogin.enabled = true;
        pnlAcc.canvas.SetActive(true);
    }
}
