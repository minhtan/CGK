#pragma strict

var newAnchorX : float = 0;
var anchorXDelta : float = 0;
var newAnchorY : float = 0;
var anchorYDelta : float = 0;

function Start () {
	setAnchor();
}

function setAnchor(){ 
	var rectTrans : RectTransform = GetComponent.<RectTransform>();
 	if(anchorXDelta == 0){
 		newAnchorX = rectTrans.anchorMin.x;
 		anchorXDelta = rectTrans.anchorMax.x - newAnchorX;
 	}
 	if(anchorYDelta == 0){
		newAnchorY = rectTrans.anchorMin.y;
 		anchorYDelta = rectTrans.anchorMax.y - newAnchorY;
 	}
	rectTrans.anchorMin = Vector2(newAnchorX, newAnchorY);
	rectTrans.anchorMax = Vector2(newAnchorX + anchorXDelta,newAnchorY + anchorYDelta);
}

function getAnchorYDelta(){
	return anchorYDelta;
}

function getAnchorXDelta(){
	return anchorXDelta;
}