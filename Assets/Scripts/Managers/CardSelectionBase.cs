using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectionBase : BaseManager
{
    protected Camera mainCamera;
    public LayerMask cardLayer;

    public CardDisplayManager cardDisplayManager;
    public EffectResolutionManager effectResolutionManager;
    public RuntimeDeckManager deckManager;
    
    protected GameObject selectedCard;
    public IntVariable playerMana;

    public LayerMask enemyLayer;

    protected virtual void Start()
    {
        mainCamera = Camera.main;
    }

    protected virtual void PlaySelectedCard()
    {
        var cardObject = selectedCard.GetComponent<CrackedCardObject>();
        var cardData = cardObject.data;
        int cost = 0;
        CostPieceData piece = cardData.card_pieces[0] as CostPieceData;
        if(piece != null)
        {
            cost += piece.cost;
        }
        piece = cardData.card_pieces[1] as CostPieceData;
        if(piece != null)
        {
            cost += piece.cost;
        }
        playerMana.SetValue(playerMana.Value - cost);
        
        cardDisplayManager.ReOrganizeHandCards(selectedCard);
        
        // 当卡牌打出后，将手中选中的移除
        cardDisplayManager.MoveCardToDiscardPile(selectedCard);
        deckManager.discardCardsFromHand(cardObject.data);
    }
}

