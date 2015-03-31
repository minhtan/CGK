using UnityEngine;
using System.Collections;
using Soomla.Store;
using Soomla.Profile;

public class SoomlaSocial : MonoBehaviour {

    Texture2D getImage() {
        Texture2D tex = new Texture2D(Screen.width, Screen.height);
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tex.Apply();
        return tex;
    }

    public void shareScore() {
        if (SoomlaProfile.IsLoggedIn(Provider.FACEBOOK))
        {
            SoomlaProfile.UploadImage(
                Provider.FACEBOOK,                  // Provider
                getImage(),                         // The image as a texture
                "Image title",                      // Name of image
                "Message",                          // Message to post with image
                ""                                  // Payload
            );
        }
        else
        {
            SoomlaProfile.Login(Provider.FACEBOOK);
        }
    }

}
