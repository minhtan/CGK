using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Parse;
using UnityEngine.UI;
public class Account : MonoBehaviour {
	public Text inpUsername;
	public Text inpPassword;
    private bool isLoginTrue = false;
    private bool isLoginClick = false;
	private string getUsernameInput(){
		return inpUsername.text;
	}

	private string getPasswordInput(){
		return inpPassword.text;
	}

	public void signUp(){
		string username = getUsernameInput ();
		string password = getPasswordInput ();
		IDictionary<string,object> dict = new Dictionary<string, object>()
		{
			{"username", username},
			{"password", password}
		};

		ParseCloud.CallFunctionAsync<IDictionary<string, object>> ("signUp", dict).ContinueWith (t => {
			if (t.IsFaulted){
				using (IEnumerator<System.Exception> enumerator = t.Exception.InnerExceptions.GetEnumerator()) {
					if (enumerator.MoveNext()) {
						ParseException error = (ParseException) enumerator.Current;
						Debug.Log("Error: " + error.Code + ", " + error.Message);
					}
				}
			}else{
				IDictionary<string, object> result = t.Result;
				object errorCode;
				if (result.TryGetValue("errorCode", out errorCode)) {
					Debug.Log ("Error: " + result["errorCode"] + ", " + result["message"]);
				} else {
					Debug.Log ("Success: " + result["successCode"]);
				}
			}
		});
	}

    public bool getIsTrue(){
        return isLoginTrue;
    }

    public bool getIsLoginClick() {
        return isLoginClick;
    }

    public void setIsLoginClick(bool isClick) {
        isLoginClick = isClick;
    }

	public void signIn(){
        isLoginClick = true;
		string username = getUsernameInput ();
		string password = getPasswordInput ();
		ParseUser.LogInAsync (username, password).ContinueWith (t => {
			if (t.IsFaulted || t.IsCanceled) {
				Debug.Log ("Sign in failed");
                isLoginTrue = false; 
			} else {
				Debug.Log ("Sign in successfully");
                isLoginTrue = true;
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
