using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCardUI : MonoBehaviour
{
    public GameObject cardsCanvas; // 在Inspector中设置这个变量为你的CardsCanvas
    public UICardDisplay cardDisplay; // 在Inspector中设置这个变量为你的UICardDisplay组件

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleCardsDisplay();
        }
    }

    public void ToggleCardsDisplay()
    {
        bool isActive = !cardsCanvas.activeSelf;
        if (isActive)
        {
            // 在显示canvas之前更新卡牌显示
            cardDisplay.DisplayCards();
        }
        cardsCanvas.SetActive(isActive);
    }
}
