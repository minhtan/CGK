using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Parse;

public class Account : MonoBehaviour {

    private GetMyInput getInput;

    void Start() {
        getInput = GameObject.Find("GetMyInput").GetComponent<GetMyInput>();
    }

	public void signUp(){
		string username = getInput.getUsernameUp ();
        string password = getInput.getPasswordUp();
        string rePassword = getInput.getRePassword();
        string email = getInput.getEmailInput();
        string phone = getInput.getPhoneInput();
        if (!RegexString.checkString(username, password))
        {
            Notification.messageError("Tên người dùng hoặc ", Notification.WARRNING_ERROR);
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
                            Notification.messageError("Không có kết nối mạng", Notification.WARRNING_ERROR);
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
                        int numberError = System.Convert.ToInt32(result["errorCode"]);
                        if (numberError== 203)
                        {
                            Notification.messageError("Email đăng ký đã tồn tại", Notification.WARRNING_ERROR);
                        }
                        else if (numberError == 202)
                        {
                            Notification.messageError("Tên đăng ký đã tồn tại", Notification.WARRNING_ERROR);
                        }else if(numberError == 2){
                            Notification.messageError("Đăng ký không hợp lệ", Notification.WARRNING_ERROR);
                        }
                    }
                    else
                    {
                        Notification.messageError("Đăng ký thành công", Notification.WARRNING_ERROR);
                        Debug.Log("Success: " + result["successCode"]);
                    }
                }
            });
        }
        else {
            Notification.messageError("Đọc hướng dẫn trước khi đăng ký", Notification.WARRNING_ERROR);
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
                        string[] errorNumber = System.Convert.ToString(error.Message).Split(' ');
                        if (errorNumber[0].Equals("404"))
                        {
                            Notification.messageError("Lỗi đăng nhập", Notification.FORGOT_ERROR);
                        }
                        else {
                            Notification.messageError("Không có kết nối mạng", Notification.NETWORK_ERROR);
                        }
                    }
                }
                myGUI.loginFaild();
            }
            else
            {
                Debug.Log("Sign in successfully");
                Bet.bet.getMyCoin();
                Debug.Log(ParseUser.CurrentUser.ToString());
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

    public void changePass() {
        string oldPass = getInput.getOldPass();
        string newPass = getInput.getNewPass();
		string reNewPass = getInput.getReNewPass();
		if(!RegexString.checkString(oldPass, newPass)){
            Notification.messageError("Mật khẩu phải có nhiều hơn 2 kí tự", Notification.WARRNING_ERROR);
        }else if(!RegexString.checkRePass(newPass, reNewPass)){
            Notification.messageError("Nhập lại mật khẩu không đúng *", Notification.WARRNING_ERROR);
        }else if(!RegexString.isValid(newPass, RegexString.passReg)){
            Notification.messageError("Mật khẩu không được chứa ký tự đặc biệt", Notification.WARRNING_ERROR);
        }else{
            IDictionary<string, object> dict = new Dictionary<string, object>()
			{
				{"oldPass", oldPass},
				{"newPass", newPass}
			};
            ParseCloud.CallFunctionAsync<IDictionary<string, object>>("changePass", dict).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    using (IEnumerator<System.Exception> enumerator = t.Exception.InnerExceptions.GetEnumerator())
                    {
                        if (enumerator.MoveNext())
                        {
                            ParseException error = (ParseException)enumerator.Current;
                            Debug.Log("Error: " + error.Code + ", " + error.Message);
                            Notification.messageError("Không có kết nối mạng", Notification.WARRNING_ERROR);
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
                        int numberError = System.Convert.ToInt32(result["errorCode"]);
                        if (numberError == 3)
                        {
                            Notification.messageError("Người dùng không hợp lệ", Notification.WARRNING_ERROR);
                        }
                        else if (numberError == 7)
                        {
                            Notification.messageError("Mật khẩu cũ không đúng", Notification.WARRNING_ERROR);
                        }
                    }
                    else
                    {
                        Notification.messageError("Đổi mật khẩu thành công", Notification.WARRNING_ERROR);
                        Debug.Log("Success: " + result["successCode"]);
                    }
                }
            });
        }
    }

    public void changeEmail() {
        string newEmail = getInput.getNewEmail();
        string confirmEmail = getInput.getPassConfirmEmail();
        if(!RegexString.isValid(newEmail, RegexString.emailReg)){
            Notification.messageError("Email không hợp lệ", Notification.WARRNING_ERROR);
        }
        else if (!RegexString.isValid(confirmEmail, RegexString.passReg))
        {
            Notification.messageError("Mật khẩu không hợp lệ", Notification.WARRNING_ERROR);
        }
        else {
            IDictionary<string, object> dict = new Dictionary<string, object>()
			{
				{"newEmail", newEmail},
				{"oldPass", confirmEmail}
			};
            ParseCloud.CallFunctionAsync<IDictionary<string, object>>("changeEmail", dict).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    using (IEnumerator<System.Exception> enumerator = t.Exception.InnerExceptions.GetEnumerator())
                    {
                        if (enumerator.MoveNext())
                        {
                            ParseException error = (ParseException)enumerator.Current;
                            Debug.Log("Error: " + error.Code + ", " + error.Message);
                            Notification.messageError("Không có kết nối mạng", Notification.WARRNING_ERROR);
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
                        int numberError = System.Convert.ToInt32(result["errorCode"]);
                        if (numberError == 3)
                        {
                            Notification.messageError("Người dùng không hợp lệ", Notification.WARRNING_ERROR);
                        }
                        else if (numberError == 7)
                        {
                            Notification.messageError("Mật khẩu không đúng", Notification.WARRNING_ERROR);
                        }
                    }
                    else
                    {
                        myGUI.successChangeEmail(System.Convert.ToString(result["newMail"]));
                        Notification.messageError("Đổi email thành công", Notification.WARRNING_ERROR);
                        Debug.Log("Success: " + result["successCode"]);
                    }
                }
            });
        }
    }

    public void changePhone() {
        string newPhone = getInput.getNewPhone();
        string passConfirmPhone = getInput.getPassConfirmPhone();
        if(!RegexString.isValid(newPhone, RegexString.phoneReg)){
            Notification.messageError("Số điện thoại không hợp lệ", Notification.WARRNING_ERROR);
        }
        else if (!RegexString.isValid(passConfirmPhone, RegexString.passReg))
        {
            Notification.messageError("Mật khẩu không hợp lệ", Notification.WARRNING_ERROR);
        }
        else {
            IDictionary<string, object> dict = new Dictionary<string, object>()
			{
				{"newPhone", newPhone},
				{"oldPass", passConfirmPhone}
			};
            ParseCloud.CallFunctionAsync<IDictionary<string, object>>("changePhone", dict).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    using (IEnumerator<System.Exception> enumerator = t.Exception.InnerExceptions.GetEnumerator())
                    {
                        if (enumerator.MoveNext())
                        {
                            ParseException error = (ParseException)enumerator.Current;
                            Debug.Log("Error: " + error.Code + ", " + error.Message);
                            Notification.messageError("Không có kết nối mạng", Notification.WARRNING_ERROR);
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
                        int numberError = System.Convert.ToInt32(result["errorCode"]);
                        if (numberError == 3)
                        {
                            Notification.messageError("Người dùng không hợp lệ", Notification.WARRNING_ERROR);
                        }
                        else if (numberError == 7)
                        {
                            Notification.messageError("Mật khẩu không đúng", Notification.WARRNING_ERROR);
                        }
                    }
                    else
                    {
                        myGUI.successChangePhone(System.Convert.ToString(result["newPhone"]));
                        Notification.messageError("Đổi số điện thoại thành công", Notification.WARRNING_ERROR);
                        Debug.Log("Success: " + result["successCode"]);
                    }
                }
            });
        }
    }
}
