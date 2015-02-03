using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PostionAnimal : MonoBehaviour {

	void Start () {
        GridLayoutGroup gridLayout;
        gridLayout = GetComponent<GridLayoutGroup>();
        float spacing = gridLayout.spacing.x;
        int paddingSide = gridLayout.padding.left;
        int paddingTall = gridLayout.padding.top;
        float cellsize = (float)((Screen.width - (9 * spacing) - (paddingSide * 2)) / 10);
        float panelheight = (float)(Screen.height * 0.7);
        if ((cellsize * 4 + paddingTall * 2 + spacing * 3) > panelheight)
        {
            cellsize = (panelheight - paddingTall * 2 - spacing * 3) / 4;
            paddingSide = System.Convert.ToInt32((Screen.width - cellsize * 10 - spacing * 9) / 2);
            gridLayout.padding.left = paddingSide;
            gridLayout.padding.right = paddingSide;
        }else {
            paddingTall = System.Convert.ToInt32((panelheight - 3 * spacing - cellsize * 4) / 2);
            gridLayout.padding.top = paddingTall;
            gridLayout.padding.bottom = paddingTall;
        }
        gridLayout.cellSize = new Vector2(cellsize, cellsize);
        //float ratioWidthScreen = 1024 / Screen.width;
        //float cellSize = (float)((Screen.width - (9 * spacing) - (paddingSide * 2)) / 10);
        ////float ratioHeightScreen = 768 / Screen.height;
        //float panelHeight = (float)(768 * 0.74);
        ////float cellSize = (panelHeight - paddingTall * 2 - spacing * 3) / 4;
        //Debug.Log(Screen.width);
        //Debug.Log(Screen.height);
        //if ((cellSize * 4 + paddingTall * 2 + spacing * 3) > panelHeight)
        //{
        //    cellSize = (panelHeight - paddingTall * 2 - spacing * 3) / 4;
        //    paddingSide = System.Convert.ToInt32((Screen.width - cellSize * 10 - spacing * 9) / 2);
        //    gridLayout.padding.left = paddingSide;
        //    gridLayout.padding.right = paddingSide;
        //    Debug.Log("no----");
        //}
        //else {
        //    paddingTall = System.Convert.ToInt32((panelHeight - 3 * spacing - cellSize * 4) / 2);
        //    gridLayout.padding.top = paddingTall;
        //    gridLayout.padding.bottom = paddingTall;
        //}
        //gridLayout.cellSize = new Vector2(cellSize, cellSize);

	}

}
