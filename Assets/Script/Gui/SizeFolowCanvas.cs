using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SizeFolowCanvas : MonoBehaviour {

    private RectTransform rectCanvas;
    public int heightScreenStandard = 768;
    // chinh size cho nut setting 
    // chinh size cho panel Bet
    public GameObject panelBet;
    private RectTransform rectPanelBet;
    //chinh size cho panel Animal
    public GameObject panelAnimal;
	public GameObject panelMainAnimal;
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
	//thu win hien len
	public GameObject imgAnimalWin;
	//chinh size cho cac o bet
	public GameObject imgBet;
	private float numBet = 46.0f;
	private float Size_IMG_BET = 115.2f;
	private RectTransform rectTxtBet;
	public GameObject pnlTxtBet;

    void Awake(){
        rectCanvas = GetComponent<RectTransform>();
        rectPanelBet = panelBet.GetComponent<RectTransform>();
        rectPanelAnimal = panelAnimal.GetComponent<RectTransform>();
        //rectTimeBet = panelTimeBet.GetComponent<RectTransform>();
        rectCoverPnlBet = panelCoverPanelBet.GetComponent<RectTransform>();
        //rectAcount = panelAcount.GetComponent<RectTransform>();
		rectTxtBet = pnlTxtBet.GetComponent<RectTransform>();
    }

	void Start () {
        float widthCanvas = rectCanvas.sizeDelta.x;
        float ratioScreen = Screen.width / widthCanvas;
        float heightBet = 192.0f;
        float ratioBetVsScreen = heightBet / heightScreenStandard;
        panelBetSize(ratioScreen, widthCanvas, ratioBetVsScreen, rectPanelBet);
        panelBetSize(ratioScreen, widthCanvas, ratioBetVsScreen, rectCoverPnlBet);
        panelAnimalVsTimeBetSize(heightBet, ratioScreen, widthCanvas, ratioBetVsScreen, rectPanelAnimal);
        positionAnimal(ratioScreen);
        changeSize(panelShop, widthCanvas, heightScreenStandard);
        //shop
        float spacingItem = pnlListItem.GetComponent<HorizontalLayoutGroup>().spacing;
        float heightShop = spacingItem / heightScreenStandard;
        pnlMainShop.GetComponent<RectTransform>().anchorMin = new Vector2(0, heightShop);
        pnlMainShop.GetComponent<RectTransform>().anchorMax = new Vector2(1, ratioHeightShop- heightShop); ;
		getSizeImgBet(widthCanvas);

	}

    private void changeSize(GameObject obj, float width, float height) {
        obj.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }

    private void panelBetSize(float ratio, float widthCanvas, float ratioBetVsScreen, RectTransform rect)
    {
        float height = Screen.width / ratio;
        rect.anchorMax = new Vector2(height/widthCanvas, ratioBetVsScreen);
    }

    private void panelAnimalVsTimeBetSize(float heightBet, float ratioScreen, float widthCanvas, float ratioBetVsScreen, RectTransform rect)
    { 
        float height = heightScreenStandard - heightBet - numBet;
        float width = Screen.width / ratioScreen;
        
        rect.anchorMin = new Vector2(0, ratioBetVsScreen + 0.085f);
        rect.anchorMax = new Vector2(width / widthCanvas, height / heightScreenStandard + ratioBetVsScreen + 0.055f);
    }

    private float getDeltaAnchorPanelAnimal(float anchorMiny, float anchorMaxY) {
        return (anchorMaxY - anchorMiny);
    }

    private void positionAnimal(float ratioScreen)
    {
        GridLayoutGroup gridLayout;
        gridLayout = panelMainAnimal.GetComponent<GridLayoutGroup>();
        float spacing = gridLayout.spacing.x;
        int paddingSide = gridLayout.padding.left;
        int paddingTall = gridLayout.padding.top;
        float cellsize = (float)(((Screen.width / ratioScreen) - (9 * spacing) - (paddingSide * 2)) / 10);
        float panelheight = (float)((Screen.height / ratioScreen) * 0.665);
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
		positionAnimalWin(panelheight, paddingTall, cellsize);
    }

	private void positionAnimalWin(float heightPnlMain, float padding, float cellSize){
		RectTransform rect;
		rect = imgAnimalWin.GetComponent<RectTransform>();
		float height =  heightPnlMain - cellSize * 2 - padding * 2;
		rect.sizeDelta = new Vector2(height, height);
	}

	private void getSizeImgBet(float widthCanvas){
		float widthCellBet = 0.125f * widthCanvas;
		float marginImgBet = (widthCellBet -  Size_IMG_BET) /2;
		float ratioMarginVsCanvas = marginImgBet / widthCanvas;
		float ratioImgBet = Size_IMG_BET / widthCanvas;
		float number = 0f;
		foreach (RectTransform rectBetNumber in rectTxtBet) {
			rectBetNumber.anchorMin = new Vector2(ratioMarginVsCanvas + number, 0.1f);
			rectBetNumber.anchorMax = new Vector2(ratioMarginVsCanvas + ratioImgBet + number, 0.9f);
			number += 0.125f; 
		}
	}
}

 