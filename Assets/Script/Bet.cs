﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Parse;

public class Bet : MonoBehaviour {
	private int coin;

	public void getMyCoin(){
		if (Account.hasCurrentUser ()) {
			IDictionary<string,object> dict = new Dictionary<string, object>();
			ParseCloud.CallFunctionAsync<IDictionary<string, object>> ("getMyCoin", dict).ContinueWith (t => {
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
						coin = Convert.ToInt32(result["coin"]);
						Debug.Log(coin);
					}
				}
			});
		} else {
			Debug.Log ("User need to sign in");
            Notification.invalidUser();
		}
	}

	public void betRequest(){
        if (Account.hasCurrentUser ()) {
		    IDictionary<string,object> dict = new Dictionary<string, object>();

		    for (int i = 0; i < 8; i++) {
			    dict.Add("bet"+i, int.Parse(GameObject.Find("TxtBet"+i).GetComponent<Text> ().text));
		    }

		    ParseCloud.CallFunctionAsync<IDictionary<string, object>> ("takeMyBet", dict).ContinueWith (t => {
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
					    Debug.Log(Convert.ToInt32(result["result"]));
                        Debug.Log(Convert.ToInt32(result["coin"]));
				    }
			    }
		    });
        }
        else
        {
            Debug.Log("User need to sign in");
            Notification.invalidUser();
        }
	}

}