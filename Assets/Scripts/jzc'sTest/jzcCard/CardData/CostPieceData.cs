using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(
    menuName = "CardGame/Card Piece/CostPiece",
    fileName = "CostPiece",
    order = 0)]
public class CostPieceData : CardPieceData
{
    public int cost;
}
