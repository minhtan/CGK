using UnityEngine;
using System.Collections;

public class SizeFlolowCanvasLogin : MonoBehaviour {

    public GameObject canvasInput;
    private RectTransform rectCanvas;
    public GameObject inputPassword;
    private RectTransform rectPass;

    void Awake() { 
        rectPass = inputPassword.GetComponent<RectTransform>();
        rectCanvas = canvasInput.GetComponent<RectTransform>();
    }

	void Start () {
        float scaleCanvas = rectCanvas.localScale.y;
	}
}
