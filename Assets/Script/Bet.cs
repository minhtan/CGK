﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Parse;

public class Bet : MonoBehaviour {
	private int coin;
    public string key;
    public static Bet bet;

    void Start() {
        bet = this;
    }

	public void getMyCoin(){
		if (Account.hasCurrentUser ()) {
			IDictionary<string,object> dict = new Dictionary<string, object>();
			ParseCloud.CallFunctionAsync<IDictionary<string, object>> ("getMyCoin", dict).ContinueWith (t => {
				if (t.IsFaulted){
					using (IEnumerator<System.Exception> enumerator = t.Exception.InnerExceptions.GetEnumerator()) {
						if (enumerator.MoveNext()) {
							ParseException error = (ParseException) enumerator.Current;
							Debug.Log("Error: " + error.Code + ", " + error.Message);
                            Notification.messageError("Không có kết nối mạng, kiểm tra kết nối mạng", "Lỗi mạng", Notification.WARRNING_ERROR);
                            myGUI.loginFaild();
						}
					}
				}else{
					IDictionary<string, object> result = t.Result;
					object errorCode;
					if (result.TryGetValue("errorCode", out errorCode)) {
						Debug.Log ("Error: " + result["errorCode"] + ", " + result["message"] + "222222222222222");
                        Notification.messageError("Error: " + result["errorCode"] + ", " + result["message"], "Lỗi", Notification.WARRNING_ERROR);
                        myGUI.loginFaild();
					} else {
						coin = Convert.ToInt32(result["coin"]);
						Debug.Log(coin);
                        myGUI.getUserInfo(coin, System.Convert.ToString(result["phone"]), ParseUser.CurrentUser.Email);
					}
				}
			});
		} else {
			Debug.Log ("User need to sign in");
            Notification.messageError("Bạn cần phải đăng nhập", "Lỗi đăng nhập", Notification.WARRNING_ERROR);
		}
	}

	public void betRequest(){
        if (Account.hasCurrentUser ()) {
		    IDictionary<string,object> dict = new Dictionary<string, object>();

		    for (int i = 0; i < 8; i++) {
			    dict.Add("bet" + i, int.Parse(GameObject.Find("ImgNumber" + i + "/TxtBet" + i).GetComponent<Text> ().text));
		    }

		    ParseCloud.CallFunctionAsync<IDictionary<string, object>> ("takeMyBet", dict).ContinueWith (t => {
			    if (t.IsFaulted){
				    using (IEnumerator<System.Exception> enumerator = t.Exception.InnerExceptions.GetEnumerator()) {
					    if (enumerator.MoveNext()) {
						    ParseException error = (ParseException) enumerator.Current;
						    Debug.Log("Error: " + error.Code + ", " + error.Message);
                            Notification.messageError("Không có kết nối mạng, kiểm tra kết nối mạng", "Lỗi mạng", Notification.RELOAD_SENCE);
					    }
				    }
			    }else{
				    IDictionary<string, object> result = t.Result;
				    object errorCode;
				    if (result.TryGetValue("errorCode", out errorCode)) {
					    Debug.Log ("Error 2: " + result["errorCode"] + ", " + result["message"]);
                        Notification.messageError("Error: " + result["errorCode"] + ", " + result["message"], "Lỗi", Notification.WARRNING_ERROR);
				    } else {
                        int[] re = new int[2];
                        re[0] = Convert.ToInt32(result["result"]);
                        re[1] = Convert.ToInt32(result["coin"]);
					    Debug.Log(re[0]);
                        Debug.Log(re[1]);
                        myGUI.stopCycle(re[0],re[1]);
				    }
			    }
		    });
        }
        else
        {
            Debug.Log("User need to sign in");
            Notification.messageError("Bạn cần phải đăng nhập", "Lỗi đăng nhập", Notification.WARRNING_ERROR);
        }
	}

    public void takeMyCoin(string bundle) {
        if (Account.hasCurrentUser())
        {
            IDictionary<string, object> dict = new Dictionary<string, object>() { 
                {"key", this.key},
			    {"bundle", bundle}
            };
            ParseCloud.CallFunctionAsync<IDictionary<string, object>>("takeMyCoin", dict).ContinueWith(t =>
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
                        Notification.messageError("Error: " + result["errorCode"] + ", " + result["message"], "Lỗi", Notification.WARRNING_ERROR);
                    }
                    else
                    {
                        coin = Convert.ToInt32(result["coin"]);
                        Debug.Log(coin);
                    }
                }
            });
        }
        else
        {
            Debug.Log("User need to sign in");
            Notification.messageError("Bạn cần phải đăng nhập", "Lỗi đăng nhập", Notification.WARRNING_ERROR);
        }
    }

}
