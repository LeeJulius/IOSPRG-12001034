using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Singleton<MenuManager>
{
    private List<BaseCanvas> menuCanvasList = new List<BaseCanvas>();

    public void RegisterMenu(BaseCanvas currentCanvas)
    {
        menuCanvasList.Add(currentCanvas);
    }

    public void HideAll()
    {
        foreach (BaseCanvas currentCanvas in menuCanvasList)
            currentCanvas.HideCanvas();
    }

    public void ShowCanvas(MenuTypes menuType)
    {
        HideAll();
        foreach (BaseCanvas currentCanvas in menuCanvasList)
        {
            if (currentCanvas.MenuType == menuType)
            {
                currentCanvas.ShowCanvas();
                break;
            }
        }  
    }
}
