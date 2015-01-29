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
        float cellSize = (float)((Screen.width - (9 * spacing) - (paddingSide * 2)) / 10);
        float panelHeight = (float)(Screen.height * 0.7); 
        if ((cellSize * 4 + paddingTall * 2 + spacing * 3) > panelHeight)
        {
            cellSize = (panelHeight - paddingTall * 2 - spacing * 3) / 4;
            paddingSide = System.Convert.ToInt32((Screen.width - cellSize * 10 - spacing * 9) / 2);
            gridLayout.padding.left = paddingSide;
            gridLayout.padding.right = paddingSide;

        }else {
            paddingTall = System.Convert.ToInt32((panelHeight - 3 * spacing - cellSize * 4) / 2);
            gridLayout.padding.top = paddingTall;
            gridLayout.padding.bottom = paddingTall;
        }
        gridLayout.cellSize = new Vector2(cellSize, cellSize);
	}

}
