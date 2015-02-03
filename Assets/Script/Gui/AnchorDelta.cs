using UnityEngine;
using System.Collections;

public class AnchorDelta : MonoBehaviour {

    public float newAnchorX = 0;
    public float anchorXDelta = 0;
    public float newAnchorY = 0;
    public float anchorYDelta = 0;

    void Start () {
	    setAnchor();
    }

    private void setAnchor(){ 
	    RectTransform rectTrans = GetComponent<RectTransform>();
 	    if(anchorXDelta == 0){
 		    newAnchorX = rectTrans.anchorMin.x;
 		    anchorXDelta = rectTrans.anchorMax.x - newAnchorX;
 	    }
 	    if(anchorYDelta == 0){
		    newAnchorY = rectTrans.anchorMin.y;
 		    anchorYDelta = rectTrans.anchorMax.y - newAnchorY;
 	    }
	    rectTrans.anchorMin = new Vector2(newAnchorX, newAnchorY);
	    rectTrans.anchorMax = new Vector2(newAnchorX + anchorXDelta,newAnchorY + anchorYDelta);
    }

    public float getAnchorYDelta(){
	    return anchorYDelta;
    }

    public float getAnchorXDelta(){
	    return anchorXDelta;
    }
	
}
