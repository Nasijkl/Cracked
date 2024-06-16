using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CrackedCard", menuName = "CardGame/Templates/CrackedCard", order = 0)]
public class CrackedCardData : ScriptableObject
{
    public CardPieceData[] card_pieces = new CardPieceData[4];
}
