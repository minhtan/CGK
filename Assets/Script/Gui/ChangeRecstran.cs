using UnityEngine;
using System.Collections;

public class ChangeRecstran : MonoBehaviour {

    public GameObject panelGetWidth;
    private AnchorDelta anchorParent;
    private RectTransform rectChildren;
    public GameObject panelChildren;
    public float ratio;
    void Awake() {
        anchorParent = panelGetWidth.GetComponent<AnchorDelta>();
        rectChildren = panelChildren.GetComponent<RectTransform>();
    }
	// Use this for initialization
	void Start () {
        float widthChildren = anchorParent.getAnchorXDelta() * Screen.width;
        float heightchildren = Screen.height * ratio;
        GetComponent<RectTransform>().sizeDelta = new Vector2(widthChildren, heightchildren);
        rectChildren.offsetMax = new Vector2(0, heightchildren);
	}
}
