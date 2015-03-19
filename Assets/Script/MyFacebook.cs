using UnityEngine;
using System.Collections;
using Facebook;

public class MyFacebook : MonoBehaviour {

    private void SetInit()
    {
        enabled = true;
        // "enabled" is a magic global; this lets us wait for FB before we start rendering
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // start the game back up - we're getting focus again
            Time.timeScale = 1;
        }
    }

	// Use this for initialization
	void Awake () {
        // Initialize FB SDK              
        enabled = false;
        FB.Init(SetInit, OnHideUnity);
	}
	
	// Update is called once per frame
	public void loginFB() {
        if (!FB.IsLoggedIn)
        {
            FB.Login("", LoginCallback);
        }
	}

    void LoginCallback(FBResult result)
    {
        if (FB.IsLoggedIn)
        {
            OnLoggedIn();
        }
    }

    void OnLoggedIn()
    {
        Debug.Log("Logged in");
    }  
}
