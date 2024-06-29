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
<<<<<<< HEAD
=======
    public override CardPieceData deepCopy()
    {
        var copy = ScriptableObject.CreateInstance<CostPieceData>();
        copy.cost = this.cost;
        copy.sprite = this.sprite;
        return copy;
    }
>>>>>>> origin/lhr
}
