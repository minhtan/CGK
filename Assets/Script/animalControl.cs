using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AnimalControl : MonoBehaviour {

    public Sprite hideSprite;
    public Sprite showSprite;

    public void hide() { 
        GetComponent<Image>().sprite = hideSprite;
    }

    public void show() {
        GetComponent<Image>().sprite = showSprite;
    }
}
