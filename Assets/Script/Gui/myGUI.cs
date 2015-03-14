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
    public Animator animDoorLeft;
    public Animator animDoorRight;
    public Animator animInputPass;
    public Text txtUserName;
    public Text txtPass;
    public Text placeHolderUsername;
    public Text placeHolderPass;
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
    //Text coin
    public Text textCoin;
    private int coinServer;
    private int presentCoin;
    private bool isRepeatCycle = false;
    //timer
    private int totalTimeRuning = 5;
    private bool isCoinServer = false;
    //Error
    public Text message;
    public Text titleError;

    void Awake() { 
        rectAnimal = panelAnimal.GetComponent<RectTransform>();
        listAnimal = new List<GameObject>();
        swapListAnimal(addListAnimal());
    }

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
            animLogin();
        }
        if (serverFlag && !isCycleRunning)
        {
            isCycleRunning = true;
            Debug.Log(result + "---------------------");
            StartCoroutine(cycleOnce(randomAnimalPosition(result)));
        }
        if (isRepeatCycle)
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

    //tach chuoi text coin
    private int splitTextCoin() {
        string[] arrTextCoin = textCoin.text.Split(' ');
        int lastCoin = System.Convert.ToInt32(arrTextCoin[1]);
        return lastCoin;
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
            for (int i = lastCoin; i <= coinServer; i++)
            {
                yield return new WaitForSeconds(0.2f);
                textCoin.text = "Coin: " + i;
            }
        }
        yield return new WaitForSeconds(3f);
        isRepeatCycle = true;
    }

    private void resetCycle() {
        isRepeatCycle = false;
        isCycleRunning = false;
        serverFlag = false;
        isCountdownRunning = false;
        isStartTime = true;
        endTime = DateTime.Now.AddSeconds(totalTimeRuning);
        presentCoin = splitTextCoin();
        for (int i = 0; i < 8; i ++)
        {
            GameObject.Find("TxtBet" + i).GetComponent<Text>().text = "00";
        }
        for (int i = 0; i < listAnimal.Count; i++)
        {
            listAnimal[i].GetComponent<animalControl>().hide();
        }
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
                            listAnimal[i].GetComponent<animalControl>().show();
                            yield return new WaitForSeconds(timeAnimalTransite);
                        }
                        else if(i < listAnimal.Count - divisionNumber(number))
                        {
                            listAnimal[i - 1].GetComponent<animalControl>().hide();
                            listAnimal[i].GetComponent<animalControl>().show();
                            yield return new WaitForSeconds(timeAnimalTransite);
                        }
                        if (number == 0 && i == listAnimal.Count - 3)
                        {
                            listAnimal[i - 1].GetComponent<animalControl>().hide();
                            listAnimal[i].GetComponent<animalControl>().show();
                            yield return new WaitForSeconds(timeAnimalTransite * 4);
                        }
                        else if (number == 0 && i == listAnimal.Count - 2)
                        {
                            listAnimal[i - 1].GetComponent<animalControl>().hide();
                            listAnimal[i].GetComponent<animalControl>().show();
                            yield return new WaitForSeconds(timeAnimalTransite * 6);
                        }else if(number == 0 && i == listAnimal.Count - 1){
                            listAnimal[i - 1].GetComponent<animalControl>().hide();
                            listAnimal[i].GetComponent<animalControl>().show();
                            yield return new WaitForSeconds(timeAnimalTransite * 10);
                            listAnimal[i].GetComponent<animalControl>().hide();
                        }
                        else if (number == 1 && i == listAnimal.Count - 2)
                        {
                            listAnimal[i - 1].GetComponent<animalControl>().hide();
                            listAnimal[i].GetComponent<animalControl>().show();
                            yield return new WaitForSeconds(timeAnimalTransite * 4);
                            listAnimal[i].GetComponent<animalControl>().hide();
                        }
                        else if (number == 1 && i == listAnimal.Count - 1)
                        {
                            listAnimal[i - 1].GetComponent<animalControl>().hide();
                            listAnimal[i].GetComponent<animalControl>().show();
                            yield return new WaitForSeconds(timeAnimalTransite * 6);
                            listAnimal[i].GetComponent<animalControl>().hide();
                        }
                        else if (number == 2 && i == listAnimal.Count - 1)
                        {
                            listAnimal[i - 1].GetComponent<animalControl>().hide();
                            listAnimal[i].GetComponent<animalControl>().show();
                            yield return new WaitForSeconds(timeAnimalTransite * 4);
                            listAnimal[i].GetComponent<animalControl>().hide();    
                        }
                    }
                }
                else {
                    if (number == 0)
                    {
                        listAnimal[number].GetComponent<animalControl>().show();
                    }
                    else if (number == 1)
                    {
                        listAnimal[number - 1].GetComponent<animalControl>().show();
                        yield return new WaitForSeconds(timeAnimalTransite * 10);
                        listAnimal[number - 1].GetComponent<animalControl>().hide();
                        listAnimal[number].GetComponent<animalControl>().show();
                    }
                    else
                    {
                        listAnimal[number - 2].GetComponent<animalControl>().show();
                        yield return new WaitForSeconds(timeAnimalTransite *6);
                        listAnimal[number - 2].GetComponent<animalControl>().hide();
                        listAnimal[number - 1].GetComponent<animalControl>().show();
                        yield return new WaitForSeconds(timeAnimalTransite * 10);
                        listAnimal[number - 1].GetComponent<animalControl>().hide();
                        listAnimal[number].GetComponent<animalControl>().show();
                    }
                }
            }
        }
        else {
            for (int i = 0; i <= number - 3; i++)
            {
                if (i == 0)
                {
                    listAnimal[i].GetComponent<animalControl>().show();
                    yield return new WaitForSeconds(timeAnimalTransite);
                }
                else
                {
                    listAnimal[i - 1].GetComponent<animalControl>().hide();
                    listAnimal[i].GetComponent<animalControl>().show();;
                    yield return new WaitForSeconds(timeAnimalTransite);
                }   
            }
            listAnimal[number - 3].GetComponent<animalControl>().hide();
            listAnimal[number - 2].GetComponent<animalControl>().show();
            yield return new WaitForSeconds(timeAnimalTransite * 6);
            listAnimal[number - 2].GetComponent<animalControl>().hide();
            listAnimal[number - 1].GetComponent<animalControl>().show();
            yield return new WaitForSeconds(timeAnimalTransite * 10);
            listAnimal[number - 1].GetComponent<animalControl>().hide();
            listAnimal[number].GetComponent<animalControl>().show();
        }
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
                        listAnimal[i].GetComponent<animalControl>().show();
                    }
                    else
                    {
                        listAnimal[i - 1].GetComponent<animalControl>().hide();
                        listAnimal[i].GetComponent<animalControl>().show();
                    }
                    yield return new WaitForSeconds(timeAnimalTransite);
                }
                else
                {
                    listAnimal[i - 1].GetComponent<animalControl>().hide();
                    listAnimal[i].GetComponent<animalControl>().show();
                    yield return new WaitForSeconds(timeAnimalTransite);
                    listAnimal[i].GetComponent<animalControl>().hide();

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
    private void animLogin() {   
        textCoin.text = "Coin: " + presentCoin;
        canvasGame.SetActive(true);
        animDoorLeft.enabled = true;
        animDoorRight.enabled = true;
        animInputPass.enabled = true;
        runAnimationLogin(true, true, true); 
    }

    private void runAnimationLogin(bool isDoorLeft, bool isDoorRight, bool isPassLeft) {

            animInputPass.SetBool("isPassRun", !isPassLeft);
            animDoorLeft.SetBool("isDoorLeftRun", !isDoorLeft);
            animDoorRight.SetBool("isDoorRightRun", !isDoorRight);
    }

    // logout
    public void btnLogoutClick() {
        runAnimationLogin(false, false, false);
        StartCoroutine(logoutWait());
    }

    private IEnumerator logoutWait() {
        yield return new WaitForSeconds(1.5f);
        canvasGame.SetActive(false);
    }

    //setting
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
        endTime = DateTime.Now.AddSeconds(totalTimeRuning);
        isCountdownRunning = false;
        isStartTime = true;
    }

    // start bet button click
    public void btnStartBetClick()
    {
        startTimeBet();
    }

    public void btnBetClick(int number)
    {

        Text text = GameObject.Find("TxtBet" + number).GetComponent<Text>();
        int value = System.Convert.ToInt32(text.text);
        if (value < 99 && presentCoin > 0)
        {
            value++;
            presentCoin--;

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

    IEnumerator btnBetDown()
    {
        btnBetClick(flag);
        yield return new WaitForSeconds(0.1f);
        textCoin.text = "Coin: " + presentCoin;
        isCoroutineRun = false;
    }


    public void btnDown(int number)
    {
        isHeldDown = true;
        flag = number;
    }

    public void btnUp()
    {
        isHeldDown = false;
    }

    private IEnumerator countdown()
    {
        isCountdownRunning = true;
        while (endTime > DateTime.Now)
        {
            yield return new WaitForSeconds(1f);
        }
        isCycleRunning = true;
        if (checkBet())
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

    public void btnExitErrorClick()
    {
        panelError.SetActive(false);
    }

    public static void messageError(string message, string titleError)
    {
        mg.panelError.SetActive(true);
        mg.titleError.text = titleError;
        mg.message.text = message;
    }
}
