#pragma strict

private var gameControlFather : AnchorDelta;
var panelFather : GameObject;
var newAnchorX : float = 0 ;
var newAnchorY : float = 0;
var anchorYDelta : float = 0 ;

//private var gameControl : AnchorDelta;


function Awake(){
	gameControlFather = panelFather.GetComponent.<AnchorDelta>();
//	gameControl = GetComponent.<AnchorDelta>();
}

function Start(){
	retangleUI();
}

function retangleUI(){
	var myYSize : float = (Screen.height * gameControlFather.getAnchorYDelta()) * anchorYDelta;
//	var myYSize : float = (Screen.height * gameControlFather.getAnchorYDelta()) * gameControl.getAnchorYDelta();
	var fatherXSize : float = (Screen.width * gameControlFather.getAnchorXDelta());
	var deltaAnchorX : float = myYSize / fatherXSize;
	GetComponent.<RectTransform>().anchorMax.x = deltaAnchorX + newAnchorX;
	GetComponent.<RectTransform>().anchorMin.x = newAnchorX;
	GetComponent.<RectTransform>().anchorMin.y = newAnchorY;
	GetComponent.<RectTransform>().anchorMax.y = anchorYDelta + newAnchorY;
}