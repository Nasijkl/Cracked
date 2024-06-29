using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(
    menuName = "CardGame/Card Piece/LabelPiece",
    fileName = "LabelPiece",
    order = 1)]
public class LabelPieceData : CardPieceData
{
    public CardLabelType label;
<<<<<<< HEAD
=======
    public override CardPieceData deepCopy()
    {
        var copy = ScriptableObject.CreateInstance<LabelPieceData>();
        copy.label = this.label;
        copy.sprite = this.sprite;
        return copy;
    }
>>>>>>> origin/lhr
}
