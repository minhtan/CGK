using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SignUpVsChangeUser : MonoBehaviour {
    //sign up
    public InputField username;
    public InputField password;
    public InputField re_Password;
    public InputField email;
    public InputField phone;
    // change user
    // change pass
    public InputField oldPass;
    public InputField newPass;
    public InputField re_newPass;
    //change email
    public InputField newEmail;
    public InputField passConfirmEmail;
    //change phone
    public InputField newPhone;
    public InputField passConfirmPhone;

    public void resetSignUp() {
        username.text = "";
        password.text = "";
        re_Password.text = "";
        email.text = "";
        phone.text = "";
    }

    public void resetChangePass() {
        oldPass.text = "";
        newPass.text = "";
        re_newPass.text = "";

    }

    public void resetChangeEmail() {
        newEmail.text = "";
        passConfirmEmail.text = "";
    }

    public void resetChangePhone() {
        newPhone.text = "";
        passConfirmPhone.text = "";
    }

    public void resetChangeUser() {
        oldPass.text = "";
        newPass.text = "";
        re_newPass.text = "";
        newEmail.text = "";
        passConfirmEmail.text = "";
        newPhone.text = "";
        passConfirmPhone.text = "";
    }
}
