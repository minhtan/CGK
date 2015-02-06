using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Parse;

public class Account : MonoBehaviour {
	public GameObject inpUsername;
	public GameObject inpPassword;
    private bool isTrue = false;

	private string getUsernameInput(){
		return inpUsername.GetComponent<Text> ().text;
	}

	private string getPasswordInput(){
		return inpPassword.GetComponent<Text> ().text;
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
        return isTrue;
    }

	public void signIn(){
		string username = getUsernameInput ();
		string password = getPasswordInput ();
		ParseUser.LogInAsync (username, password).ContinueWith (t => {
			if (t.IsFaulted || t.IsCanceled) {
				Debug.Log ("Sign in failed");
                isTrue = false;
			} else {
				Debug.Log ("Sign in successfully");
                isTrue = true;
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
