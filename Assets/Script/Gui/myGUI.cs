using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class myGUI : MonoBehaviour {

    public GameObject signInPanel;
    public GameObject storePanel;
    private static myGUI mg;
    private int flag;
    private bool isHeldDown;
    private bool isCoroutineRun = false;
   // private bool isUp = false;
    public Animator settingAnim;
    public Animator imgSettingAnim;
    public Animator panelShopAnim;
    void Start() {
        mg = this;
        RectTransform transform = settingAnim.gameObject.transform as RectTransform;
        Vector2 position = transform.anchoredPosition;
        position.y -= transform.rect.height;
        transform.anchoredPosition = position;
    }

    public static void signIn(bool success) {
        if (success) {
            mg.signInPanel.SetActive(false);
            mg.storePanel.SetActive(true);
        } else {
            mg.signInPanel.SetActive(true);
        }
    }

    public void btnBetClick(int number) {

        Text text = GameObject.Find("TxtBet" + number).GetComponent<Text>();
        int value = System.Convert.ToInt32(text.text);
        if (value < 99)
        {
            value++;
        }
        if (value < 10)
        {
            text.text = "0" + System.Convert.ToString(value);
        }
        else {
            text.text = System.Convert.ToString(value);
        }
    }

    IEnumerator btnBetDown(){
        btnBetClick(flag);
        yield return new WaitForSeconds(0.1f);
        isCoroutineRun = false;
    }

    void Update()
    {
        if (isHeldDown && !isCoroutineRun)
        {
            isCoroutineRun = true;
            StartCoroutine(btnBetDown());
        }
    }

    public void btnDown(int number) {
        isHeldDown = true;
        flag = number;
    }

    public void btnUp() {
        isHeldDown = false;
    }

    public void btnSettingClick()
    {
        imgSettingAnim.enabled = true;
        bool isHiddenImg = imgSettingAnim.GetBool("isHidden");
        imgSettingAnim.SetBool("isHidden", !isHiddenImg);
        settingAnim.enabled = true;
        bool isHidden = settingAnim.GetBool("isHidden");
        settingAnim.SetBool("isHidden", !isHidden);
    }

    public void btnShopClick() {
        panelShopAnim.enabled = true;
        bool isShopDown = panelShopAnim.GetBool("isShopDown");
        panelShopAnim.SetBool("isShopDown", !isShopDown);
        if(isShopDown){
            bool isHidden = settingAnim.GetBool("isHidden");
            settingAnim.SetBool("isHidden", !isHidden);
        }

    }
    

}
