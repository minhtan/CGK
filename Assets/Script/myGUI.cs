﻿using UnityEngine;
using System.Collections;

public class myGUI : MonoBehaviour {

    public GameObject signInPanel;
    public GameObject gamePanel;
    private static myGUI mg;

    void Start() {
        mg = this;
    }

    public static void signIn(bool success) {
        if (success) {
            mg.signInPanel.SetActive(false);
            mg.gamePanel.SetActive(true);
        } else {
            mg.signInPanel.SetActive(true);
            mg.gamePanel.SetActive(false);
        }
    }
}
