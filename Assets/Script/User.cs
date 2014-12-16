using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Parse;

public class User : MonoBehaviour {

	public void signUp(string username){
		IDictionary<string,object> dict = new Dictionary<string, object>()
		{
			{"username", username}
		};

		ParseCloud.CallFunctionAsync<IDictionary<string, object>> ("signUp", dict)
			.ContinueWith (t =>
				{
				if (t.IsFaulted){
					using (IEnumerator<System.Exception> enumerator = t.Exception.InnerExceptions.GetEnumerator()) {
						if (enumerator.MoveNext()) {
							ParseException error = (ParseException) enumerator.Current;
							Debug.Log("Message: " + error.Message + ", Code: " + error.Code);
						}
					}
				}else{
					IDictionary<string, object> result = t.Result;
					object code;
					if (result.TryGetValue("error", out code)) {
						Debug.Log ("Error: " + result["error"]);
					} else {
						Debug.Log ("Result: " + result["success"]);
					}
				}
			});
	}

}
