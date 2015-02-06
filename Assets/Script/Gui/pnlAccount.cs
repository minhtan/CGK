using UnityEngine;
using System.Collections;

public class pnlAccount : MonoBehaviour {


    public GameObject panelChildren;
    private RectTransform rectChildren;
    private RectTransform rectParent;
    public Animator animLogin;
    public GameObject canvas;

    void Awake() { 
        rectChildren = panelChildren.GetComponent<RectTransform>();
        rectParent = GetComponent<RectTransform>();
    }

	void Start () {
       
        float heightParent = rectParent.sizeDelta.y;
        float widthParent = rectParent.sizeDelta.x;
        rectChildren.sizeDelta = new Vector2(widthParent, heightParent);
	}

    public void loginAnim() {
        animLogin.enabled = true;
        canvas.SetActive(true);
    }
}
