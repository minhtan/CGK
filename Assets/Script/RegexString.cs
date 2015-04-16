using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;
public class RegexString : MonoBehaviour {

    public static Regex usernameReg = new Regex("([a-zA-Z]+)");
    public static Regex passReg = new Regex("([a-zA-Z!@#$%^&*0-9]+)");
    public static Regex phoneReg = new Regex("[0-9]{9,11}");
    public static Regex emailReg = new Regex("(\\w+)(@)(\\w+)(\\.)(\\w+)");

    public static RegexString regex; 

    public static bool isValid(string text, Regex reg) {
        if(reg.IsMatch(text)){
            return true;
        }
        return false;
    }

    public static bool checkRePass(string text1, string text2) {
        if (text1.Equals(text2))
        {
            return true;
        }
        else {
            return false;
        }
    }

    public static bool checkString(string username, string password)
    {
        if (username.Length > 2 && password.Length > 2)
        {
            return true;
        }
        return false;
    }
}
