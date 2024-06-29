using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCardUI : MonoBehaviour
{
    public GameObject cardsCanvas; // ��Inspector�������������Ϊ���CardsCanvas
    public UICardDisplay cardDisplay; // ��Inspector�������������Ϊ���UICardDisplay���

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
            // ����ʾcanvas֮ǰ���¿�����ʾ
            cardDisplay.DisplayCards();
        }
        cardsCanvas.SetActive(isActive);
    }
}
