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


    void Awake(){
        rectCanvas = GetComponent<RectTransform>();
        rectSetting = btnSetting.GetComponent<RectTransform>();
        rectPanelBet = panelBet.GetComponent<RectTransform>();
        rectPanelAnimal = panelAnimal.GetComponent<RectTransform>();
    }

	void Start () {
        float widthCanvas = rectCanvas.sizeDelta.x;
        float ratioScreen = Screen.width / widthCanvas;
        rectSetting.sizeDelta = new Vector2(sizeBtnSetting / ratioScreen, sizeBtnSetting);
        panelMash.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeBtnSetting / ratioScreen, heightPanelMash);
        btnMusic.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeBtnSetting / ratioScreen, sizeBtnSetting);
        btnShop.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeBtnSetting / ratioScreen, sizeBtnSetting);
        txtCoin.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeBtnSetting / ratioScreen, sizeBtnSetting / 2);
        //size  panel bet
        float heightBet = sizeBtnSetting + sizeBtnSetting / 2;
        float widthBet = Screen.width / ratioScreen - sizeBtnSetting / ratioScreen;
        rectPanelBet.anchorMax = new Vector2(widthBet / widthCanvas, heightBet / heightScreenStandard);
        //size panel animal
        float heightAnimal = heightScreenStandard - heightBet;
        float widthAnimal = Screen.width / ratioScreen;
        rectPanelAnimal.anchorMin = new Vector2(0, heightBet / heightScreenStandard);
        rectPanelAnimal.anchorMax = new Vector2(widthAnimal / widthCanvas, heightAnimal / heightScreenStandard + heightBet / heightScreenStandard);
        positionAnimal(ratioScreen);
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
            Debug.Log("theo width");
        }
        gridLayout.cellSize = new Vector2((cellsize), (cellsize));
    }
}

 