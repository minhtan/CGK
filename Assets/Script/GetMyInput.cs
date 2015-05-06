﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetMyInput : MonoBehaviour {

    //sign in
    public InputField inputPass;
    public InputField inputUser;

    //signUp
    public Text inpEmail;
    public Text inpPhone;
    public Text inpUsername_Up;
    public InputField inRePassword;
    public InputField inpPassword_Up;

    //change account
    //Edit pass
    public InputField inputOldPass;
    public InputField inputNewPass;
    public InputField inputReNewPass;

    public string getEmailInput()
    {
        return inpEmail.text;
    }

    public string getPhoneInput()
    {
        return inpPhone.text;
    }

    public string getUsernameUp()
    {
        return inpUsername_Up.text;
    }

    public string getPasswordUp()
    {
        return inpPassword_Up.text;
    }

    public string getRePassword()
    {
        return inRePassword.text;
    }

    //edit pass
    public string getOldPass()
    {
        return inputOldPass.GetComponent<InputField>().text;
    }

    public string getNewPass()
    {
        return inputNewPass.GetComponent<InputField>().text;
    }

    public string getReNewPass()
    {
        return inputReNewPass.GetComponent<InputField>().text;
    }
    
}
