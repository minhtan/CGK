using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class myGUI : MonoBehaviour {

    public GameObject signInPanel;
    public GameObject storePanel;
    private static myGUI mg;
    private int flag;
    private bool isHeldDown;
    private bool isCoroutineRun = false;
    // animation cac button;
    public Animator settingAnim;
    public Animator imgSettingAnim;
    public Animator panelShopAnim;
    // nut start bet
    private DateTime endTime;
    private bool isCountdownRunning = true;
    public Text timer;
    public GameObject btnStart;
    public GameObject parentBtnBet;
    public GameObject panelCoverPnlBet;
    //panel login
    public Account account;
    private bool isLogin = false;
    public Animator animDoorLeft;
    public Animator animDoorRight;
    public Animator animInputPass;
    public GameObject panelError;
    // canvas game
    public GameObject canvasGame;
    public GameObject canvasLogin;

    void Start() {
        mg = this;
        RectTransform transform = settingAnim.gameObject.transform as RectTransform;
        Vector2 position = transform.anchoredPosition;
        position.y -= transform.rect.height;
        transform.anchoredPosition = position;
       
    }

    void Update()
    {
        if (isHeldDown && !isCoroutineRun)
        {
            isCoroutineRun = true;
            StartCoroutine(btnBetDown());
        }
        if (!isCountdownRunning)
        {
            StartCoroutine(countdown());
        }
        showTimer();
        if(account.getIsLoginClick()){
            StartCoroutine(animLogin());
            account.setIsLoginClick(false);
        }    
    }

    public void btnLoginClick() {
        canvasGame.SetActive(true);
    }
    private IEnumerator animLogin() {   
        yield return new WaitForSeconds(2f);
        if (account.getIsTrue())
        {
            
            animDoorLeft.enabled = true;
            animDoorRight.enabled = true;
            animInputPass.enabled = true;
            bool isDoorLeft = animDoorLeft.GetBool("isDoorLeftRun");
            bool isDoorRight = animDoorRight.GetBool("isDoorRightRun");
            bool isPassLeft = animInputPass.GetBool("isPassRun");
            runAnimationLogin(true, true, true);
        }
        else {
            panelError.SetActive(true);
        }
    }

    private void runAnimationLogin(bool isDoorLeft, bool isDoorRight, bool isPassLeft) {

            animInputPass.SetBool("isPassRun", !isPassLeft);
            animDoorLeft.SetBool("isDoorLeftRun", !isDoorLeft);
            animDoorRight.SetBool("isDoorRightRun", !isDoorRight);
        

    }

    public void btnLogoutClick() {
        runAnimationLogin(false, false, false);
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

    // start bet button click
    public void btnStartBetClick()
    {
        endTime = DateTime.Now.AddSeconds(15);
        isCountdownRunning = false;
    }

    private IEnumerator countdown()
    {
        isCountdownRunning = true;
        while (endTime > DateTime.Now)
        {
            yield return new WaitForSeconds(1f);
        }


    }

    public void showTimer()
    {
        TimeSpan timeSpan = endTime.Subtract(DateTime.Now);
        if (timeSpan.TotalSeconds < 0)
        {
            timer.enabled = false;
        }
        else {
            timer.enabled = true;
            timer.text = timeSpan.Seconds + "";
            btnStart.SetActive(false);
            panelCoverPnlBet.SetActive(true);
        }
    }
    


}
