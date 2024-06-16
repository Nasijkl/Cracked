using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurnButton : MonoBehaviour
{
    private Button button;

    private CardDisplayManager cardDisplayManager;
    //private CardSelectionHasArrow cardSelectionHasArrow;
    //private CardSelectionNoArrow cardSelectionNoArrow;
    private CrackedCardPlayManager crackedCardPlayManager;
    
    private void Awake()
    {
        button = GetComponent<Button>();
    }
    
    private void Start()
    {
        cardDisplayManager = FindFirstObjectByType<CardDisplayManager>();
        //cardSelectionHasArrow = FindFirstObjectByType<CardSelectionHasArrow>();
        //cardSelectionNoArrow = FindFirstObjectByType<CardSelectionNoArrow>();
        crackedCardPlayManager = FindFirstObjectByType<CrackedCardPlayManager>();
    }

    public void OnButtonPressed()
    {
        if (cardDisplayManager.isMoving())
        {
            return;
        }

        if (crackedCardPlayManager.HasSelectedCard())
        {
            return;
        }

        button.interactable = false;

        var turnManager = FindFirstObjectByType<TurnManager>();
        turnManager.EndPlayerTurn();
    }

    public void OnPlayerTurnBegan()
    {
        button.interactable = true;
    }
}
