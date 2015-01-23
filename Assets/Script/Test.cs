using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Parse;

public class Test : MonoBehaviour {

	void Start(){
		Debug.Log (ParseUser.CurrentUser);
	}

	public void test(){
		ParseCloud.CallFunctionAsync<IDictionary<string, object>> ("test", new Dictionary<string, object>()).ContinueWith (t => {
			if (t.IsFaulted){
				using (IEnumerator<System.Exception> enumerator = t.Exception.InnerExceptions.GetEnumerator()) {
					if (enumerator.MoveNext()) {
						ParseException error = (ParseException) enumerator.Current;
						Debug.Log("Error: " + error.Code + ", " + error.Message);
					}
				}
			}else{
				IDictionary<string, object> result = t.Result;
				Debug.Log ("Test result: " + result["test"]);
			}
		});
	}
}
