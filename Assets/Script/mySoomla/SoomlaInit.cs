using UnityEngine;
using System.Collections;
using Soomla.Highway;
using Soomla.Store;
using Soomla.Profile;

public class SoomlaInit : MonoBehaviour {
    // Initialize
    void Start() {
        SoomlaHighway.Initialize();
        SoomlaStore.Initialize(new SoomlaItems());
        SoomlaProfile.Initialize();
        SoomlaStore.StartIabServiceInBg();
    }
}
