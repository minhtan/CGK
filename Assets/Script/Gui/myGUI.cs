using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
public class myGUI : MonoBehaviour {

    //public GameObject signInPanel;
    public GameObject storePanel;
    private static myGUI mg;
    private int flag;
    private bool isHeldDown;
    // animation cac button;
    public Animator animSetting;
    public Animator animShop;
    // nut start bet
    private DateTime endTime;
    private bool isCountdownRunning = true;
    public Text timer;
    public GameObject btnStart;
    public GameObject parentBtnBet;
    public GameObject panelCoverPnlBet;
    //panel login
    public Account account;
    public Animator animCanvasLogin;
    public Text placeHolderUsername;
    public Text placeHolderPass;
    public GameObject inputPass;
    public GameObject inputUser;
    public GameObject panelDoorLeft;
    public GameObject panelDoorRight;
    public GameObject btnLogin;
    //panel error
    public GameObject panelError;
    public GameObject btnExitError;
    // canvas game
    public GameObject canvasGame;
    public GameObject canvasLogin;
    //panel animal
    public GameObject panelAnimal;
    private RectTransform rectAnimal;
    public Sprite spriteAnimal;
    public Sprite spriteIdleAnimal;
    private List <GameObject> listAnimal;
    private bool isCycleRunning = false;
    private bool serverFlag = false;
    private int result;
    private float timeAnimalTransite = 0.1f;
    private bool isStartTime = false;
    public GameObject imgAnimalWin;
    //Text coin
    public Text textCoin;
    private int coinServer;
    private int presentCoin;
    private bool isRepeatCycle = false;
    //timer
    private int totalTimeRuning = 15;
    private bool isCoinServer = false;
    public GameObject txtTimer;
    // SignUp
    public GameObject panelTerm;
    public GameObject panelSignUpMain;
    public Animator animSignUp;
    public GameObject pnlCover;
    private bool isSignUpSuccess = false;

    //shop
    public GameObject panelShop;
    //logout
    private bool isLogout = false;
    public GameObject btnLogout;
    // setting
    private bool isSetting = false;
    public GameObject panelOverSetting;
    private bool[] test;

    void Awake() { 
        rectAnimal = panelAnimal.GetComponent<RectTransform>();
        listAnimal = new List<GameObject>();
        swapListAnimal(addListAnimal());
        test = new bool[8];
        for (int i = 0; i < 8; i++)
        {
            test[i] = false;
        }      
    }

    void Start() {
        mg = this;
        RectTransform transform = animSetting.gameObject.transform as RectTransform;
        Vector2 position = transform.anchoredPosition;
        position.y -= transform.rect.height;
        transform.anchoredPosition = position;
        checkCurrentUser();
        StartCoroutine(betClick());
    }

    public static void checkCurrentUser(){
        if (Account.hasCurrentUser())
        {
            mg.animCanvasLogin.SetTrigger("login_skip_gd1");
            Bet.bet.getMyCoin();
        }
    }

    public static void signUpSuccess() {
        mg.isSignUpSuccess = true;
    }

    void Update()
    {
        if(isLoginFalse){
            animCanvasLogin.SetTrigger("closeLogin_gd1");
            isLoginFalse = false;
        }

        if(isSignUpSuccess){
            animSignUp.SetTrigger("openSignUp");
        }

        if (!isCountdownRunning && isStartTime)
        {
            StartCoroutine(countdown());
        }

        if (isStartTime)
        {
            showTimer();
        }

        if (isCoinServer)
        {
            isCoinServer = false;
            animCanvasLogin.SetTrigger("login_gd3");
            canvasGame.SetActive(true);
            textCoin.text = presentCoin + "";
        }

        if (serverFlag && !isCycleRunning)
        {
            isCycleRunning = true;
            StartCoroutine(cycleOnce(randomAnimalPosition(result)));
        }

        if (isRepeatCycle && !isLogout)
        {
            resetCycle();
        }
    }

    //lay coin tu server ve khi login
    public static void getCoinServer(int coin) {
        mg.presentCoin = coin;
        mg.isCoinServer = true;
    }

    private int random(int begin, int end) {
        return UnityEngine.Random.Range(begin,end);
    }

    //randomAnimalPosition
    private int randomAnimalPosition(int result) {
        int random = UnityEngine.Random.Range(1,3);    
        return result +(random - 1) * 8 ;
    }

    // animal 
    private List<GameObject> addListAnimal()
    {
        foreach (Transform trans in rectAnimal)
        {
            if (trans.gameObject.name.Contains("imgAnimal"))
            {
                listAnimal.Add(trans.gameObject);
            }
        }
        return listAnimal;
    }

    public static void stopCycle(int result, int coin) {
        mg.serverFlag = true;
        mg.result = result;
        mg.coinServer = coin;
    }

    private int divisionNumber(int number) {
        int position = 0;
        if (number == 0)
        {
            position = 3;
        }
        else if (number == 1)
        {
            position = 2;
        }
        else
        {
            position = 1;
        }
        return position;
    }

    //anim so coin tang len hoac giam di 
    private IEnumerator animCoin(int lastCoin) {
        if (coinServer > lastCoin)
        {
            SoundControlCS.sound.playWinCoin();
            if (coinServer - lastCoin > 999)
            {
                for (int i = lastCoin; i <= coinServer; i += 2)
                {
                    yield return new WaitForSeconds(0.05f);
                    textCoin.text = i + "";
                }
            }
            else {
                for (int i = lastCoin; i <= coinServer; i++)
                {
                    yield return new WaitForSeconds(0.1f);
                    textCoin.text = i + "";
                }
            }         
        }
        else {
            for (int i = coinServer; i >= lastCoin; i--)
            {
                yield return new WaitForSeconds(0.05f);
                textCoin.text = i + "";
            }
        }
        SoundControlCS.sound.stopWinCoin();
        yield return new WaitForSeconds(3f);        
        isRepeatCycle = true;
        btnLogout.GetComponent<Button>().interactable = true;
    }

    private void resetCycle() {
        isRepeatCycle = false;
        isCycleRunning = false;
        serverFlag = false;
        isCountdownRunning = false;
        isStartTime = true;
        endTime = DateTime.Now.AddSeconds(totalTimeRuning);
        presentCoin = System.Convert.ToInt32(textCoin.text);
        for (int i = 0; i < 8; i ++)
        {
            GameObject.Find("TxtBet" + i).GetComponent<Text>().text = "00";
        }
        for (int i = 0; i < listAnimal.Count; i++)
        {
            listAnimal[i].GetComponent<AnimalControl>().hide();
        }
        imgAnimalWin.SetActive(false);
    }

    private IEnumerator cycleOnce(int number) {
        if (number < 3)
        {
            for (int k = 0; k < 2; k++)
            {
                if (k == 0)
                {
                    for (int i = 0; i < listAnimal.Count; i++)
                    {
                        if (i == 0)
                        {
                            listAnimal[i].GetComponent<AnimalControl>().show();
                            yield return new WaitForSeconds(timeAnimalTransite);
                        }
                        else if(i < listAnimal.Count - divisionNumber(number))
                        {
                            listAnimal[i - 1].GetComponent<AnimalControl>().hide();
                            listAnimal[i].GetComponent<AnimalControl>().show();
                            yield return new WaitForSeconds(timeAnimalTransite);
                        }
                        if (number == 0 && i == listAnimal.Count - 3)
                        {
                            listAnimal[i - 1].GetComponent<AnimalControl>().hide();
                            listAnimal[i].GetComponent<AnimalControl>().show();
                            yield return new WaitForSeconds(timeAnimalTransite * 4);
                        }
                        else if (number == 0 && i == listAnimal.Count - 2)
                        {
                            listAnimal[i - 1].GetComponent<AnimalControl>().hide();
                            listAnimal[i].GetComponent<AnimalControl>().show();
                            yield return new WaitForSeconds(timeAnimalTransite * 6);
                        }else if(number == 0 && i == listAnimal.Count - 1){
                            listAnimal[i - 1].GetComponent<AnimalControl>().hide();
                            listAnimal[i].GetComponent<AnimalControl>().show();
                            yield return new WaitForSeconds(timeAnimalTransite * 10);
                            listAnimal[i].GetComponent<AnimalControl>().hide();
                        }
                        else if (number == 1 && i == listAnimal.Count - 2)
                        {
                            listAnimal[i - 1].GetComponent<AnimalControl>().hide();
                            listAnimal[i].GetComponent<AnimalControl>().show();
                            yield return new WaitForSeconds(timeAnimalTransite * 4);
                            listAnimal[i].GetComponent<AnimalControl>().hide();
                        }
                        else if (number == 1 && i == listAnimal.Count - 1)
                        {
                            listAnimal[i - 1].GetComponent<AnimalControl>().hide();
                            listAnimal[i].GetComponent<AnimalControl>().show();
                            yield return new WaitForSeconds(timeAnimalTransite * 6);
                            listAnimal[i].GetComponent<AnimalControl>().hide();
                        }
                        else if (number == 2 && i == listAnimal.Count - 1)
                        {
                            listAnimal[i - 1].GetComponent<AnimalControl>().hide();
                            listAnimal[i].GetComponent<AnimalControl>().show();
                            yield return new WaitForSeconds(timeAnimalTransite * 4);
                            listAnimal[i].GetComponent<AnimalControl>().hide();    
                        }
                    }
                }
                else {
                    if (number == 0)
                    {
                        listAnimal[number].GetComponent<AnimalControl>().show();                      
                        imgAnimalWin.GetComponent<Image>().sprite = listAnimal[number].GetComponent<AnimalControl>().showSprite;
                    }
                    else if (number == 1)
                    {
                        listAnimal[number - 1].GetComponent<AnimalControl>().show();
                        yield return new WaitForSeconds(timeAnimalTransite * 10);
                        listAnimal[number - 1].GetComponent<AnimalControl>().hide();
                        listAnimal[number].GetComponent<AnimalControl>().show();
                        imgAnimalWin.GetComponent<Image>().sprite = listAnimal[number].GetComponent<AnimalControl>().showSprite;
                    }
                    else
                    {
                        listAnimal[number - 2].GetComponent<AnimalControl>().show();
                        yield return new WaitForSeconds(timeAnimalTransite *6);
                        listAnimal[number - 2].GetComponent<AnimalControl>().hide();
                        listAnimal[number - 1].GetComponent<AnimalControl>().show();
                        yield return new WaitForSeconds(timeAnimalTransite * 10);
                        listAnimal[number - 1].GetComponent<AnimalControl>().hide();
                        listAnimal[number].GetComponent<AnimalControl>().show();
                        imgAnimalWin.GetComponent<Image>().sprite = listAnimal[number].GetComponent<AnimalControl>().showSprite;
                    }
                }
            }
        }
        else {
            for (int i = 0; i <= number - 3; i++)
            {
                if (i == 0)
                {
                    listAnimal[i].GetComponent<AnimalControl>().show();
                    yield return new WaitForSeconds(timeAnimalTransite);
                }
                else
                {
                    listAnimal[i - 1].GetComponent<AnimalControl>().hide();
                    listAnimal[i].GetComponent<AnimalControl>().show();;
                    yield return new WaitForSeconds(timeAnimalTransite);
                }   
            }
            listAnimal[number - 3].GetComponent<AnimalControl>().hide();
            listAnimal[number - 2].GetComponent<AnimalControl>().show();
            yield return new WaitForSeconds(timeAnimalTransite * 6);
            listAnimal[number - 2].GetComponent<AnimalControl>().hide();
            listAnimal[number - 1].GetComponent<AnimalControl>().show();
            yield return new WaitForSeconds(timeAnimalTransite * 10);
            listAnimal[number - 1].GetComponent<AnimalControl>().hide();
            listAnimal[number].GetComponent<AnimalControl>().show();
            imgAnimalWin.GetComponent<Image>().sprite = listAnimal[number].GetComponent<AnimalControl>().showSprite;
        }
        imgAnimalWin.SetActive(true);
        StartCoroutine(animCoin(presentCoin));
    }

    private IEnumerator cycleAnimal(int number, int cycleTurn)
    {
        while (!serverFlag || cycleTurn > 0)
        {
            for (int i = 0; i < number; i++)
            {
                if (i < number - 1)
                {
                    if (i == 0)
                    {
                        listAnimal[i].GetComponent<AnimalControl>().show();
                    }
                    else
                    {
                        listAnimal[i - 1].GetComponent<AnimalControl>().hide();
                        listAnimal[i].GetComponent<AnimalControl>().show();
                    }
                    yield return new WaitForSeconds(timeAnimalTransite);
                }
                else
                {
                    listAnimal[i - 1].GetComponent<AnimalControl>().hide();
                    listAnimal[i].GetComponent<AnimalControl>().show();
                    yield return new WaitForSeconds(timeAnimalTransite);
                    listAnimal[i].GetComponent<AnimalControl>().hide();

                }
            }
            cycleTurn--;
        }
        isCycleRunning = false;
    }

    private void swapListAnimal(List<GameObject> list)
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            for (int j = i + 1; j < list.Count; j++)
            {
                string[] nameA = list[i].name.Split('_');
                string[] nameB = list[j].name.Split('_');
                int number1 = System.Convert.ToInt32(nameA[2]);
                int number2 = System.Convert.ToInt32(nameB[2]);
                if (number1 > number2)
                {
                    GameObject temp = list[i];
                    list[i] = list[j];
                    list[j] = temp;
                }
            }
        }
    }

    //login
    private void animOpenLogin() {   
        animCanvasLogin.SetTrigger("login_gd1");    
    }

    public void btnLoginClick()
    {
        if(isLogout){
            imgAnimalWin.SetActive(false);
            btnLogout.GetComponent<Button>().interactable = true;
            btnLogin.GetComponent<Button>().interactable = false;
            btnStart.SetActive(true);
        }
        string username = getUsernameInput();
        string password = getPasswordInput();
        isLogout = false;
        if (!RegexString.checkString(username, password))
        {
            Notification.messageError("Tên người dùng hoặc mật khẩu phải có nhiều hơn 2 kí tự", Notification.WARRNING_ERROR);
        }
        else if (RegexString.isValid(username, RegexString.usernameReg) && RegexString.isValid(password, RegexString.passReg))
        {
            animOpenLogin();
            account.signIn(username, password);
        }
        else
        {
            Notification.messageError("Tên người dùng hoặc mật khẩu không hợp lệ", Notification.WARRNING_ERROR);
        }
        inputPass.GetComponent<InputField>().text = "";
        inputUser.GetComponent<InputField>().text = "";
    }

    private IEnumerator guiReset() {
        for (int i = 0; i < 8; i++)
        {
            GameObject.Find("TxtBet" + i).GetComponent<Text>().text = "00";
        }
        for (int i = 0; i < listAnimal.Count; i++)
        {
            listAnimal[i].GetComponent<AnimalControl>().hide();
        }
        yield return new WaitForSeconds(5f);
        canvasGame.SetActive(false); 
    }

    // logout
    public void btnLogoutClick() {
        isLogout = true;
        animCanvasLogin.SetTrigger("closeSignIn");
        StartCoroutine(guiReset());
        isSetting = false;
        isStartTime = false;
        btnLogin.GetComponent<Button>().interactable = true;
    }

    //setting
    public void btnSettingClick()
    {
        if (isSetting)
        {
            panelOverSetting.SetActive(true);
            animSetting.SetTrigger("closeSetting");
            isSetting = false;
        }
        else {
            panelOverSetting.SetActive(false);
            animSetting.SetTrigger("openSetting");
            isSetting = true;
        }
    }

    public GameObject btnShop;

    public void btnShopClick() {
        panelShop.SetActive(true);
        btnShop.GetComponent<Button>().interactable = false;
        panelOverSetting.SetActive(true);
        isSetting = false;
        animShop.SetTrigger("openShop");
    }

    public void btnExitShopClick() {
        animSetting.SetTrigger("closeSetting");
        animShop.SetTrigger("closeShop");
        btnShop.GetComponent<Button>().interactable = true;
        panelOverSetting.SetActive(true);
    }

    //bet
    private void showTimer()
    {
        TimeSpan timeSpan = endTime.Subtract(DateTime.Now);
        if (timeSpan.TotalSeconds <= 0)
        {
            timer.enabled = false;
            panelCoverPnlBet.SetActive(true);
            isHeldDown = false;
        }
        else
        {
            panelCoverPnlBet.SetActive(false);
            timer.enabled = true;
            timer.text = timeSpan.Seconds + "";
            btnStart.SetActive(false);
        }
    }

    private void startTimeBet()
    {
        txtTimer.SetActive(true);
        endTime = DateTime.Now.AddSeconds(totalTimeRuning);
        isCountdownRunning = false;
        isStartTime = true;
    }

    // start bet button click
    public void btnStartBetClick()
    {
        startTimeBet();
    }

    private IEnumerator betClick()
    {
        while (true)
        {
            for (int i = 0; i < 8; i++)
            {
                if (test[i] == true)
                {
                    btnBetClick(i);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void btnBetClick(int number)
    {
        Text text = GameObject.Find("TxtBet" + number).GetComponent<Text>();
        int value = System.Convert.ToInt32(text.text);
        if (value < 99 && presentCoin > 0)
        {
            value++;
            presentCoin--;
            textCoin.text = presentCoin + "";

        }
        if (value < 10)
        {
            text.text = "0" + System.Convert.ToString(value);
        }
        else
        {
            text.text = System.Convert.ToString(value);
        }

    }

    private IEnumerator btnBetDown()
    {
            btnBetClick(flag);
            yield return new WaitForSeconds(0.05f);
            textCoin.text = presentCoin + "";
    }

    public void btnDown(int number)
    {
        test[number] = true;
        isHeldDown = true;
        flag = number;
    }

    public void btnUp(int number)
    {
        test[number] = false;
        isHeldDown = false;
    }

    private IEnumerator countdown()
    {
        isCountdownRunning = true;
        while (endTime > DateTime.Now)
        {
            yield return new WaitForSeconds(1f);
            if (isLogout) {
                break;            
            }
        }
        Debug.Log(isLogout);
        if (!isLogout)
        {
            isCycleRunning = true;
            btnLogout.GetComponent<Button>().interactable = false;
            if (checkBet() && Notification.isConectInternet())
            {
                StartCoroutine(cycleAnimal(listAnimal.Count, random(1, 2)));
                Bet.bet.betRequest();
            }
            else
            {
                StartCoroutine(cycleAnimal(listAnimal.Count, random(2, 4)));
                serverFlag = true;
                result = random(0, 7);
            }
        }   
    }

    //check xem co dat cuoc khong
    private bool checkBet()
    {
        for (int i = 0; i < 8; i++)
        {
            int coin = System.Convert.ToInt32(GameObject.Find("TxtBet" + i).GetComponent<Text>().text);
            if (coin > 0)
            {
                return true;
            }
        }
        return false;
    }

    public void btnExitClick()
    {
        panelError.SetActive(false);
    }
    
    //btn apply term
    public Text txtTerm;
    public void btnTermClick() {
        pnlCover.SetActive(false);
        txtTerm.text = "Ông Thảo chỉ đạo đối với những cây đã hạ chuyển thì phải thay thế cây xanh mới đảm bảo mật độ theo quy hoạch. Khu vực này cũng phải hoàn thiện hè đường, đảm bảo giao thông đô thị. Chủ tịch thành phố nhắc nhở đơn vị chức năng tổ chức chăm sóc, quản lý theo phân cấp, quy định." +
       "Theo ông Thảo, việc bảo tồn, cải tạo, bổ sung, thay thế cây xanh trên địa bàn Thủ đô là việc làm cần thiết liên quan đến không chỉ đến quản lý phát triển đô thị mà còn là tâm tư tình cảm của nhân dân Thủ đô.";
    }

    public void closeSignUp() {
        animSignUp.SetTrigger("openSignUp");
        StartCoroutine(waitAnimCloseSignUp());
    }

    private IEnumerator waitAnimCloseSignUp() {
        yield return new WaitForSeconds(1.0f);
        pnlSignUp.SetActive(false);
    }

    public GameObject pnlSignUp;

    public void openSignUp(){
        pnlSignUp.SetActive(true);
        animSignUp.SetTrigger("closeSignUp");
    }

    

    private string getUsernameInput()
    {
        return inputUser.GetComponent<InputField>().text;
    }

    private string getPasswordInput()
    {
        return inputPass.GetComponent<InputField>().text;
    }

    private bool isLoginFalse = false;

    public static void loginFaild()
    {
        mg.isLoginFalse = true;
    }
}
