using UnityEngine;
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
    //edit email
    public InputField inputNewEmail;
    public InputField inputPassMail;
    //edit phone
    public InputField inputNewPhone;
    public InputField inputPassPhone;

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
        return inputOldPass.text;
    }

    public string getNewPass()
    {
        return inputNewPass.text;
    }

    public string getReNewPass()
    {
        return inputReNewPass.text;
    }
    //edit mail
    public string getNewEmail() {
        return inputNewEmail.text;
    }

    public string getPassConfirmEmail() {
        return inputPassMail.text;
    }

    //edit phone
    public string getNewPhone() {
        return inputNewPhone.text;
    }

    public string getPassConfirmPhone() {
        return inputPassPhone.text;
    }

}
