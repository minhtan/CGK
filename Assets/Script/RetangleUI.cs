using UnityEngine;
using System.Collections;

public class RetangleUI : MonoBehaviour {

    private AnchorDelta gameControlFather;
    public GameObject panelFather;
    public float newAnchorX = 0 ;
    public float newAnchorY = 0;
    public float anchorYDelta = 0 ;

    //private var gameControl : AnchorDelta;


    void Awake(){
	    gameControlFather = panelFather.GetComponent<AnchorDelta>();
    //	gameControl = GetComponent.<AnchorDelta>();
    }

    void Start(){
	    retangleUI();
    }

    private void retangleUI(){
	    float myYSize = (Screen.height * gameControlFather.getAnchorYDelta()) * anchorYDelta;
    //	var myYSize : float = (Screen.height * gameControlFather.getAnchorYDelta()) * gameControl.getAnchorYDelta();
	    float fatherXSize = (Screen.width * gameControlFather.getAnchorXDelta());
	    float deltaAnchorX = myYSize / fatherXSize;
        GetComponent<RectTransform>().anchorMax = new Vector2(deltaAnchorX + newAnchorX, anchorYDelta + newAnchorY);
        GetComponent<RectTransform>().anchorMin = new Vector2(newAnchorX, newAnchorY);
    }
}
