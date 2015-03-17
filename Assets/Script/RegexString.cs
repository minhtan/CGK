using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;
public class RegexString : MonoBehaviour {

    public static Regex usernameReg = new Regex("(^[a-zA-Z])(\\w+)");
    public static Regex passReg = new Regex("(\\w+)");
    public static Regex phoneReg = new Regex("[0-9]{9,11}");
    public static Regex emailReg = new Regex("(\\w+)(@)(\\w+)(\\.)(\\w+)");

    public static RegexString regex; 

    public static bool check(string text, Regex reg) {
        if(reg.IsMatch(text)){
            return true;
        }
        return false;
    }
}
