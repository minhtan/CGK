using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
public class myGUI : MonoBehaviour {

    public GameObject storePanel;
    private static myGUI mg;
    private int flag;
    private GetMyInput getInput;
    // animation cac button;
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
    public GameObject panelDoorLeft;
    public GameObject panelDoorRight;
    public GameObject btnLogin;
	private bool isLoginFalse = false;
    public Text txtLoading;
    // canvas game
    public GameObject canvasGame;
    public GameObject canvasLogin;
    //panel animal
    public GameObject panelAnimal;
    private RectTransform rectAnimal;
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
    private int totalTimeRuning = 10;
    private bool isCoinServer = false;
    public GameObject txtTimer;
    // SignUp
    public GameObject panelTerm;
    public GameObject panelSignUpMain;
    public Animator animSignUp;
    public GameObject pnlCover;
    private bool isSignUpSuccess = false;
	public GameObject pnlSignUp;
    public Text txtTerm;
    //shop
    public GameObject panelShop;
    public GameObject btnShop;
    //logout
    private bool isLogout = false;
    public GameObject btnLogout;
    // array button bet
    private bool[] arrayBtnBet;
    private IEnumerator stopBetClick;
    //edit account
    public GameObject pnlEditProfile;
    public GameObject pnlEditPass;
    public GameObject pnlEditEmail;
    public GameObject pnlEditPhone;
    private bool isClickEdit = false;
    private int numberBtnEdit;
    private string currentPhone;
    public Text txtCurrentPhone;
    public Text txtCurrentEmail;
    private string currentEmail;
    public GameObject btnEditEmail;
    public GameObject btnEditPass;
    public GameObject btnEditPhone;
    public Text txtBtnEditPass;
    public Text txtBtnEditEmail;
    public Text txtBtnEditPhone;
    public Sprite spriteSoundOff;
    public Sprite spriteSoundOn;
    private bool isMusic = false;
    public GameObject objSound;
    public GameObject panelForgotMN;
    private bool isChangeEmail = false;
    private bool isChangePhone = false;
  
    void Awake() { 
        rectAnimal = panelAnimal.GetComponent<RectTransform>();
        listAnimal = new List<GameObject>();
        swapListAnimal(addListAnimal());
        arrayBtnBet = new bool[8];
        for (int i = 0; i < 8; i++)
        {
            arrayBtnBet[i] = false;
        }
        stopBetClick = betClick();
    }

    void Start() {
        mg = this;
        checkCurrentUser();
        getInput = GameObject.Find("GetMyInput").GetComponent<GetMyInput>();
        StartCoroutine((stopBetClick));

    }

    private void checkCurrentUser(){
        if (Account.hasCurrentUser())
        {
            animCanvasLogin.SetTrigger("login_skip_gd1");
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
            txtLoading.active = false;
            textCoin.text = presentCoin + "";
            txtCurrentPhone.text = currentPhone;
            txtCurrentEmail.text = currentEmail;
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
        if (isClickEdit)
        {
            isClickEdit = false;
            switch (numberBtnEdit)
            {
                case 0:
                    setActivePnlEdit(true, false, false);
                    changeColorText(txtBtnEditPass, txtBtnEditEmail, txtBtnEditPhone);
                    break;
                case 1:
                    setActivePnlEdit(false, true, false);
                    changeColorText(txtBtnEditEmail, txtBtnEditPass, txtBtnEditPhone);
                    break;
                case 2:
                    setActivePnlEdit(false, false, true);
                    changeColorText(txtBtnEditPhone, txtBtnEditEmail, txtBtnEditPass);
                    break;
                default:
                    setActivePnlEdit(true, false, false);
                    changeColorText(txtBtnEditPass, txtBtnEditEmail, txtBtnEditPhone);
                    break;
            }
        }
        if(isChangeEmail){
            isChangeEmail = false;
            txtCurrentEmail.text = currentEmail;
        }
        if(isChangePhone){
            isChangePhone = false;
            txtCurrentPhone.text = currentPhone;
        }
    }

    //lay coin tu server ve khi login
    public static void getUserInfo(int coin, string phone, string email) {
        mg.presentCoin = coin;
        mg.currentPhone = phone;
        mg.currentEmail = email;
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
		int deltaCoin = coinServer - lastCoin;
		float deltaTime = 0;
        if (deltaCoin > 0)
        {
            SoundControlCS.sound.playWinCoin();
            if (deltaCoin < 50)
            {
				deltaTime = 0.15f;
			}else if(deltaCoin >= 50 && deltaCoin < 100){
				deltaTime = 0.125f;
			}else if(deltaCoin >= 100 && deltaCoin < 200){
				deltaTime = 0.083333f;
			}else if(deltaCoin >= 200 && deltaCoin < 400){
				deltaTime = 0.05f;
			}else if(deltaCoin >= 400 && deltaCoin < 800){
				deltaTime = 0.03333f;
			}else if(deltaCoin >= 800 && deltaCoin < 1600){
				deltaTime = 0.025f;
			}else if(deltaCoin >= 1600 && deltaCoin < 3001){
				deltaTime = 0.02f;
			}    
			for (int i = lastCoin; i <= coinServer; i ++)
			{
				yield return new WaitForSeconds(deltaTime);
				textCoin.text = i + "";
			}
        }
        SoundControlCS.sound.stopWinCoin();
        yield return new WaitForSeconds(2f);        
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
            GameObject.Find("ImgNumber" + i + "/TxtBet" + i).GetComponent<Text>().text = "00";
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
            panelCoverPnlBet.SetActive(true);
        }
        string username = getUsernameInput();
        string password = getPasswordInput();
        isLogout = false;
        if (!RegexString.checkString(username, password))
        {
            Notification.messageError("Tên người dùng hoặc mật khẩu phải có nhiều hơn 2 kí tự", "Lỗi đăng nhập", Notification.WARRNING_ERROR);
        }
        else if (RegexString.isValid(username, RegexString.usernameReg) && RegexString.isValid(password, RegexString.passReg))
        {
            animOpenLogin();
            txtLoading.active = true;
            account.signIn(username, password);
        }
        else
        {
            Notification.messageError("Tên người dùng hoặc mật khẩu không hợp lệ", "Lỗi đăng nhập", Notification.WARRNING_ERROR);
        }
        getInput.inputPass.text = "";
        getInput.inputUser.text = "";
    }

    private IEnumerator guiReset() {
        for (int i = 0; i < 8; i++)
        {
            GameObject.Find("ImgNumber" + i + "/TxtBet" + i).GetComponent<Text>().text = "00";
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
        isStartTime = false;
        btnLogin.GetComponent<Button>().interactable = true;
    }

    public void btnShopClick() {
        showShop();
    }

    private void showShop() {
        panelShop.SetActive(true);
        btnShop.GetComponent<Button>().interactable = false;
        animShop.SetTrigger("openShop");
    }

    public void btnExitShopClick() {
        exitShop();
    }

    private void exitShop() {
        animShop.SetTrigger("closeShop");
        btnShop.GetComponent<Button>().interactable = true;
    }

    //bet
    private void showTimer()
    {
        TimeSpan timeSpan = endTime.Subtract(DateTime.Now);
        if (timeSpan.TotalSeconds <= 0)
        {
            timer.enabled = false;
            panelCoverPnlBet.SetActive(true);
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
                if (arrayBtnBet[i] == true)
                {
                    btnBetClick(i);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void btnBetClick(int number)
    {
        Text text = GameObject.Find("ImgNumber" + number + "/TxtBet" + number).GetComponent<Text>();
        int value = System.Convert.ToInt32(text.text);
        if (presentCoin < 1)
        {
            Notification.messageError("Xèng của bạn không đủ để chơi tiếp. Vào shop để mua xèng", "", Notification.END_COIN);
        }
        else if (value < 99 && presentCoin > 0)
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

    public void btnDown(int number)
    {
        arrayBtnBet[number] = true;
    }

    public void btnUp(int number)
    {
        arrayBtnBet[number] = false;
    }

    private IEnumerator countdown()
    {
        StartCoroutine(stopBetClick);
        isCountdownRunning = true;
        while (endTime > DateTime.Now)
        {
            yield return new WaitForSeconds(1f);
            if (isLogout) {
                break;            
            }
        }
        if (!isLogout)
        {
            isCycleRunning = true;
            StopCoroutine(stopBetClick);
            btnLogout.GetComponent<Button>().interactable = false;
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
    }

    //check xem co dat cuoc khong
    private bool checkBet()
    {
        for (int i = 0; i < 8; i++)
        {
            int coin = System.Convert.ToInt32(GameObject.Find("ImgNumber" + i + "/TxtBet" + i).GetComponent<Text>().text);
            if (coin > 0)
            {
                return true;
            }
        }
        return false;
    }
    
    //btn apply term


    public void btnTermClick() {
        pnlCover.SetActive(false);
        txtTerm.text = "Ông Thảo chỉ đạo đối với những cây đã hạ chuyển thì phải thay thế cây xanh mới đảm bảo mật độ theo quy hoạch. Khu vực này cũng phải hoàn thiện hè đường, đảm bảo giao thông đô thị. Chủ tịch thành phố nhắc nhở đơn vị chức năng tổ chức chăm sóc, quản lý theo phân cấp, quy định." +
       "Theo ông Thảo, việc bảo tồn, cải tạo, bổ sung, thay thế cây xanh trên địa bàn Thủ đô là việc làm cần thiết liên quan đến không chỉ đến quản lý phát triển đô thị mà còn là tâm tư tình cảm của nhân dân Thủ đô.";
    }

    public void closeSignUp() {
        animSignUp.SetTrigger("openSignUp");
        StartCoroutine(waitAnimCloseSignUp());
        pnlCover.SetActive(true);
        txtTerm.text = "Edit the Expression & Text to see matches. Roll over matches or the expression for details. Undo mistakes with ctrl-z. Save & Share expressions with friends or the Community. A full Reference & Help is available in the Library, or watch the video Tutorial";
        
    }

    private IEnumerator waitAnimCloseSignUp() {
        yield return new WaitForSeconds(1.0f);
        pnlSignUp.SetActive(false);
    }

    public void openSignUp(){
        pnlSignUp.SetActive(true);
        animSignUp.SetTrigger("closeSignUp");
    }   

    private string getUsernameInput()
    {
        return getInput.inputUser.GetComponent<InputField>().text;
    }

    private string getPasswordInput()
    {
        return getInput.inputPass.GetComponent<InputField>().text;
    }
	
    public static void loginFaild()
    {
        mg.isLoginFalse = true;
    }

	public void btnMusicClick(){
		if(isMusic){
			SoundControlCS.sound.adjustVol(true);
            objSound.GetComponent<Image>().sprite = spriteSoundOn;
			isMusic = false;

		}else{
			SoundControlCS.sound.adjustVol(false);
            objSound.GetComponent<Image>().sprite = spriteSoundOff;
			isMusic = true;
		}
	}

    // edit profile
    public void editAccountClick() {
        pnlEditProfile.SetActive(true);
    }

    public void btnExitEditAcc() {
        pnlEditProfile.SetActive(false);
    }


    public void btnEditAccount(int number) {
        isClickEdit = true;
        numberBtnEdit = number;
    }

    private void setActivePnlEdit(bool isPass, bool isEmail, bool isPhone) {
        pnlEditPass.SetActive(isPass);
        pnlEditEmail.SetActive(isEmail);
        pnlEditPhone.SetActive(isPhone);
    }

    private void changeColorText(Text txt1, Text txt2, Text txt3) {
        txt1.color = Color.white;
        txt2.color = Color.black;
        txt3.color = Color.black;
    }

    public static void successChangeEmail(string newEmail) {
        mg.isChangeEmail = true;
        mg.currentEmail = newEmail;
    }

    public static void successChangePhone(string newPhone) {
        mg.isChangePhone = true;
        mg.currentPhone = newPhone; 
    }

    public void btnForgotMNClick() {
        panelForgotMN.SetActive(true);
    }

    public void btnExitForgotMN() { 
        panelForgotMN.SetActive(false);
    }
}
