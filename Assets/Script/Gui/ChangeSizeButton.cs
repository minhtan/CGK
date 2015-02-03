using UnityEngine;
using System.Collections;

public class ChangeSizeButton : MonoBehaviour {

    public GameObject panelParent;
    private AnchorDelta rectParent;
    private RectTransform rectChildren;
    public float anchorYDelta;
    private float ratio;

    void Start() { 
        rectParent = panelParent.GetComponent<AnchorDelta>();
        float widthParent = rectParent.getAnchorXDelta() * Screen.width;
        float heightParent = rectParent.getAnchorYDelta() * Screen.height;
        rectChildren = GetComponent<RectTransform>();
        
        if (anchorYDelta == 0.1f)
        {
            ratio = 0;
        }
        else if (anchorYDelta == 0.2f)
        {
            ratio = anchorYDelta - 0.1f;
        }
        else {
            ratio = anchorYDelta;
            anchorYDelta = anchorYDelta - 0.1f;
            
        }
        rectChildren.sizeDelta = new Vector2(widthParent, anchorYDelta * Screen.height);
        rectChildren.anchoredPosition = new Vector2(0, - (ratio * Screen.height));
    }
}
