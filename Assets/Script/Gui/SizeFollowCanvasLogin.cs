using UnityEngine;
using System.Collections;

public class SizeFollowCanvasLogin : MonoBehaviour {

    public GameObject panelParent;
    private AnchorDelta rectParent;
    void Awake() { 
        rectParent = panelParent.GetComponent<AnchorDelta>();
    }

	void Start () {
        
	}
}
