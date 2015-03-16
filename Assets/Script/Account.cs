using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Parse;

public class Account : MonoBehaviour {
	public Text inpUsername;
	public Text inpPassword;
	private string getUsernameInput(){
		return inpUsername.text;
	}

	private string getPasswordInput(){
		return inpPassword.text;
	}

    private bool checkString(string username, string password) {
        if (username.Length > 2 && password.Length > 2)
        { 
            return true;
        }        
        return false;
    }


	public void signUp(){
		string username = getUsernameInput ();
		string password = getPasswordInput ();
        if (checkString(username, password))
        {
            IDictionary<string, object> dict = new Dictionary<string, object>()
		    {
			    {"username", username},
			    {"password", password}
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
            myGUI.messageError("Độ dài phải lớn hơn 2", "Lỗi đăng ký");
        }	
	}

	public void signIn(){
		string username = getUsernameInput ();
		string password = getPasswordInput ();
        if (checkString(username, password))
        {
            ParseUser.LogInAsync(username, password).ContinueWith(t =>
            {
                if (t.IsFaulted || t.IsCanceled)
                {
                    Debug.Log("Sign in failed");
                    myGUI.messageError("Đăng nhập sai tài khoản","Lỗi đăng nhập");
                }
                else
                {
                    Debug.Log("Sign in successfully");
                    Bet.bet.getMyCoin();
                }
            });
        }
        else {
            myGUI.messageError("Độ dài phải lớn hơn 2","Lỗi đăng nhập");
        }
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
