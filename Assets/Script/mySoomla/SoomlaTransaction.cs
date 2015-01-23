using UnityEngine;
using System.Collections;
using Soomla.Store;
using Soomla.Profile;

public class SoomlaTransaction : MonoBehaviour {

    public void buyWithGoogle(string item)
    {
        if (SoomlaProfile.IsLoggedIn(Provider.GOOGLE))
        {
            StoreInventory.BuyItem(item);
        }
        else
        {
            SoomlaProfile.Login(Provider.GOOGLE);
            StoreInventory.BuyItem(item);
        }
    }

}
