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
}
