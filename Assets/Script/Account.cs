using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Parse;

public class Account : MonoBehaviour {
	public GameObject usernameInput;
	public GameObject passwordInput;

	public string a;

	private void setUsernameInput(string input){
		Text text = usernameInput.GetComponent<Text> ();
		text.text = input;
	}

	private string getUsernameInput(){
		return usernameInput.GetComponent<Text> ().text;
	}

	private string getPasswordInput(){
		return passwordInput.GetComponent<Text> ().text;
	}

	public void signUp(){
		string username = getUsernameInput ();
		string password = getPasswordInput ();
		IDictionary<string,object> dict = new Dictionary<string, object>()
		{
			{"username", username},
			{"password", password}
		};

		ParseCloud.CallFunctionAsync<IDictionary<string, object>> ("signUp", dict)
			.ContinueWith (t =>
				{
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

}
