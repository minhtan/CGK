using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SizeFolowCanvas : MonoBehaviour {

    private RectTransform rectCanvas;
    public int heightScreenStandard = 768;
    // chinh size cho nut setting 
    public GameObject btnSetting;
    public float sizeBtnSetting = 128.0f;
    public GameObject panelMash;
    public float heightPanelMash = 384;
    public GameObject btnMusic;
    public GameObject btnShop;
    public Text txtCoin;
    public GameObject btnLogout;
    private RectTransform rectSetting;
    // chinh size cho panel Bet
    public GameObject panelBet;
    private RectTransform rectPanelBet;
    //chinh size cho panel Animal
    public GameObject panelAnimal;
    private RectTransform rectPanelAnimal;
    //chinh size cho panel time bet
   // public GameObject panelTimeBet;
    //private RectTransform rectTimeBet;
    //chinh size panelShop
    public GameObject panelShop;
    //chinh size panel cover panel bet
    public GameObject panelCoverPanelBet;
    private RectTransform rectCoverPnlBet;
    //shopping
    public GameObject pnlMainShop;
    public GameObject pnlListItem;
    public float ratioHeightShop = 0.8f;

    void Awake(){
        rectCanvas = GetComponent<RectTransform>();
        rectSetting = btnSetting.GetComponent<RectTransform>();
        rectPanelBet = panelBet.GetComponent<RectTransform>();
        rectPanelAnimal = panelAnimal.GetComponent<RectTransform>();
        //rectTimeBet = panelTimeBet.GetComponent<RectTransform>();
        rectCoverPnlBet = panelCoverPanelBet.GetComponent<RectTransform>();
        //rectAcount = panelAcount.GetComponent<RectTransform>();
    }

	void Start () {
        float widthCanvas = rectCanvas.sizeDelta.x;
        float ratioScreen = Screen.width / widthCanvas;
        float heightBet = sizeBtnSetting + sizeBtnSetting / 2;
        float ratioBetVsScreen = heightBet / heightScreenStandard;
        btnSettingSize(ratioScreen);
        panelBetSize(ratioScreen, widthCanvas, ratioBetVsScreen, rectPanelBet);
        panelBetSize(ratioScreen, widthCanvas, ratioBetVsScreen, rectCoverPnlBet);
        panelAnimalVsTimeBetSize(heightBet, ratioScreen, widthCanvas, ratioBetVsScreen, rectPanelAnimal);
       // panelAnimalVsTimeBetSize(heightBet, ratioScreen, widthCanvas, ratioBetVsScreen, rectTimeBet);
        positionAnimal(ratioScreen);
        changeSize(panelShop, widthCanvas, heightScreenStandard);
        //shop
        float spacingItem = pnlListItem.GetComponent<HorizontalLayoutGroup>().spacing;
        float heightShop = spacingItem / 768;
        pnlMainShop.GetComponent<RectTransform>().anchorMin = new Vector2(spacingItem/widthCanvas, heightShop);
        pnlMainShop.GetComponent<RectTransform>().anchorMax = new Vector2(1 - spacingItem / widthCanvas, ratioHeightShop- heightShop); ;

	}

    private void btnSettingSize(float ratioScreen) {
        rectSetting.sizeDelta = new Vector2(sizeBtnSetting, sizeBtnSetting);
        changeSize(panelMash, sizeBtnSetting, heightPanelMash);
        changeSize(btnMusic, sizeBtnSetting, sizeBtnSetting);
        changeSize(btnShop, sizeBtnSetting, sizeBtnSetting);
        changeSize(btnLogout, sizeBtnSetting, sizeBtnSetting);
        txtCoin.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeBtnSetting, sizeBtnSetting / 2);
    }

    private void changeSize(GameObject obj, float width, float height) {
        obj.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }

    private void panelBetSize(float ratioScreen, float widthCanvas, float ratioBetVsScreen, RectTransform rect)
    {
        float widthBet = Screen.width / ratioScreen - sizeBtnSetting;
        rect.anchorMax = new Vector2(widthBet / widthCanvas, ratioBetVsScreen);
    }

    private void panelAnimalVsTimeBetSize(float heightBet, float ratioScreen, float widthCanvas, float ratioBetVsScreen, RectTransform rect)
    { 
        float height = heightScreenStandard - heightBet;
        float width = Screen.width / ratioScreen;
        
        rect.anchorMin = new Vector2(0, ratioBetVsScreen);
        rect.anchorMax = new Vector2(width / widthCanvas, height / heightScreenStandard + ratioBetVsScreen);
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

 