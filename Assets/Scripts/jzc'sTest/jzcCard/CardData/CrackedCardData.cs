using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CrackedCard", menuName = "CardGame/Templates/CrackedCard", order = 0)]
public class CrackedCardData : ScriptableObject
{
    public CardPieceData[] card_pieces = new CardPieceData[4];

    public CrackedCardData(List<CardPieceData> card)
    {
        for(int i = 0; i < 4; i++)
        {
            card_pieces[i] = card[i];
        }
        return;
    }

    public CrackedCardData deepCopy()
    {

        CrackedCardData copiedCard = ScriptableObject.CreateInstance<CrackedCardData>();
        copiedCard.card_pieces = new CardPieceData[card_pieces.Length];

        for (int i = 0; i < card_pieces.Length; i++)
        {
            copiedCard.card_pieces[i] = card_pieces[i].deepCopy();
        }

        return copiedCard;
    }
}
