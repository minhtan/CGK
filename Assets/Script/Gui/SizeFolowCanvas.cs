using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SizeFolowCanvas : MonoBehaviour {

    private RectTransform rectCanvas;
    public int heightScreenStandard = 768;
    // chinh size cho nut setting 
    public GameObject btnSetting;
    public float sizeBtnSetting;
    public GameObject panelMash;
    public float heightPanelMash = 384;
    public GameObject btnMusic;
    public GameObject btnShop;
    public Text txtCoin;
    private RectTransform rectSetting;
    // chinh size cho panel Bet
    public GameObject panelBet;
    private RectTransform rectPanelBet;
    //chinh size cho panel Animal
    public GameObject panelAnimal;
    private RectTransform rectPanelAnimal;

    //chinh size panelShop
    public GameObject panelShop;

    void Awake(){
        rectCanvas = GetComponent<RectTransform>();
        rectSetting = btnSetting.GetComponent<RectTransform>();
        rectPanelBet = panelBet.GetComponent<RectTransform>();
        rectPanelAnimal = panelAnimal.GetComponent<RectTransform>();
    }

	void Start () {
        float widthCanvas = rectCanvas.sizeDelta.x;
        float ratioScreen = Screen.width / widthCanvas;
        float heightBet = sizeBtnSetting + sizeBtnSetting / 2;
        float ratioBetVsScreen = heightBet / heightScreenStandard;
        btnSettingSize(ratioScreen);
        panelBetSize(ratioScreen, widthCanvas, ratioBetVsScreen);
        panelAnimalSize(heightBet, ratioScreen, widthCanvas, ratioBetVsScreen);
        positionAnimal(ratioScreen);
        panelShopSize(widthCanvas);
	}

    private void panelShopSize(float widthCanvas) {
        panelShop.GetComponent<RectTransform>().sizeDelta = new Vector2(widthCanvas, heightScreenStandard);
    }

    private void btnSettingSize(float ratioScreen) {
        rectSetting.sizeDelta = new Vector2(sizeBtnSetting / ratioScreen, sizeBtnSetting);
        panelMash.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeBtnSetting / ratioScreen, heightPanelMash);
        btnMusic.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeBtnSetting / ratioScreen, sizeBtnSetting);
        btnShop.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeBtnSetting / ratioScreen, sizeBtnSetting);
        txtCoin.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeBtnSetting / ratioScreen, sizeBtnSetting / 2);
    }

    private void panelBetSize(float ratioScreen, float widthCanvas, float ratioBetVsScreen)
    {
        float widthBet = Screen.width / ratioScreen - sizeBtnSetting / ratioScreen;
        rectPanelBet.anchorMax = new Vector2(widthBet / widthCanvas, ratioBetVsScreen);
    }

    private void panelAnimalSize(float heightBet, float ratioScreen, float widthCanvas, float ratioBetVsScreen)
    { 
        float heightAnimal = heightScreenStandard - heightBet;
        float widthAnimal = Screen.width / ratioScreen;
        
        rectPanelAnimal.anchorMin = new Vector2(0, ratioBetVsScreen);
        rectPanelAnimal.anchorMax = new Vector2(widthAnimal / widthCanvas, heightAnimal / heightScreenStandard + ratioBetVsScreen);
    }

    private float getDeltaAnchorPanelAnimal(float anchorMiny, float anchorMaxY) {
        return (anchorMaxY - anchorMiny);
    }

    private void positionAnimal(float ratioScreen)
    {
        GridLayoutGroup gridLayout;
        gridLayout = panelAnimal.GetComponent<GridLayoutGroup>();
        float spacing = gridLayout.spacing.x;
        int paddingSide = gridLayout.padding.left;
        int paddingTall = gridLayout.padding.top;
        float cellsize = (float)(((Screen.width / ratioScreen) - (9 * spacing) - (paddingSide * 2)) / 10);
        float panelheight = (float)((Screen.height / ratioScreen) * 0.7);
        if ((cellsize * 4 + paddingTall * 2 + spacing * 3) > panelheight)
        {
            cellsize = (panelheight - paddingTall * 2 - spacing * 3) / 4;
            paddingSide = System.Convert.ToInt32(((Screen.width / ratioScreen - cellsize * 10 - spacing * 9) / 2));
            gridLayout.padding.left = paddingSide;
            gridLayout.padding.right = paddingSide;
        }
        else
        {
            paddingTall = System.Convert.ToInt32((panelheight - 3 * spacing - cellsize * 4) / 2);
            gridLayout.padding.top = paddingTall;
            gridLayout.padding.bottom = paddingTall;
        }
        gridLayout.cellSize = new Vector2((cellsize), (cellsize));
    }
}

 