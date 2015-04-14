﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Parse;

public class Account : MonoBehaviour {

    //signUp
    public Text inpEmail;
    public Text inpPhone;
    public Text inpUsername_Up;
    public Text inRePassword;
    public Text inpPassword_Up;

    private string getEmailInput() {
        return inpEmail.text;
    }

    private string getPhoneInput() {
        return inpPhone.text;
    }

    private string getUsernameUp() {
        return inpUsername_Up.text;
    }

    private string getPasswordUp() {
        return inpPassword_Up.text;
    }
    private bool checkString(string username, string password) {
        if (username.Length > 2 && password.Length > 2)
        { 
            return true;
        }        
        return false;
    }

    private string getRePassword() {
        return inRePassword.text;
    }

	public void signUp(){
		string username = getUsernameUp ();
		string password = getPasswordUp ();
        string rePassword = getRePassword();
        string email = getEmailInput();
        string phone = getPhoneInput();
        if (!RegexString.checkString(username, password))
        {
            Notification.messageError("Tên người dùng hoặc mật khẩu phải có nhiều hơn 2 kí tự");
        }
        else if (RegexString.isValid(username, RegexString.usernameReg) && RegexString.isValid(password, RegexString.passReg)
        && RegexString.checkRePass(password, rePassword) && RegexString.isValid(email, RegexString.emailReg) && RegexString.isValid(phone, RegexString.phoneReg))
        {
            IDictionary<string, object> dict = new Dictionary<string, object>()
		    {
			    {"username", username},
			    {"password", password},
                {"email",email},
                {"phone",phone}
		    };
            ParseCloud.CallFunctionAsync<IDictionary<string, object>>("signUp", dict).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    using (IEnumerator<System.Exception> enumerator = t.Exception.InnerExceptions.GetEnumerator())
                    {
                        if (enumerator.MoveNext())
                        {
                            ParseException error = (ParseException)enumerator.Current;
                            Debug.Log("Error: " + error.Code + ", " + error.Message);
                        }
                    }
                }
                else
                {
                    IDictionary<string, object> result = t.Result;
                    object errorCode;
                    if (result.TryGetValue("errorCode", out errorCode))
                    {
                        Debug.Log("Error: " + result["errorCode"] + ", " + result["message"]);
                    }
                    else
                    {
                        Debug.Log("Success: " + result["successCode"]);
                    }
                }
            });
        }
        else {
            Notification.messageError("Đọc hướng dẫn trước khi đăng ký");
        }	
	}

	public void signIn(string username, string password){
        ParseUser.LogInAsync(username, password).ContinueWith(t =>
        {
            if (t.IsFaulted || t.IsCanceled)
            {
                using (IEnumerator<System.Exception> enumerator = t.Exception.InnerExceptions.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        ParseException error = (ParseException)enumerator.Current;
                        Debug.Log("Error: " + error.Code + ", " + error.Message);
                    }
                }
                Notification.messageError("Lỗi mạng");
                myGUI.loginFaild();
            }
            else
            {
                Debug.Log("Sign in successfully");
                Bet.bet.getMyCoin();
            }
        });
	}

	public static bool hasCurrentUser(){
		if (ParseUser.CurrentUser != null){
			return true;
		}else{
			return false;
		}
	}

	public void signOut(){
		ParseUser.LogOut();
	}

    public void get1000Coin() {
        IDictionary<string, object> dict = new Dictionary<string, object>();

        ParseCloud.CallFunctionAsync<IDictionary<string, object>>("test", dict).ContinueWith(t =>
        {
            if (t.IsFaulted)
            {
                using (IEnumerator<System.Exception> enumerator = t.Exception.InnerExceptions.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        ParseException error = (ParseException)enumerator.Current;
                        Debug.Log("Error: " + error.Code + ", " + error.Message);
                        
                    }
                }
            }
            else
            {
                IDictionary<string, object> result = t.Result;
                object errorCode;
                if (result.TryGetValue("errorCode", out errorCode))
                {
                    Debug.Log("Error: " + result["errorCode"] + ", " + result["message"]);
                }
                else
                {
                    Debug.Log("Success: " + result["successCode"]);
                }
            }
        });
    }
}
