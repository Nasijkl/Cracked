using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(
    menuName = "CardGame/Card Piece/EffectPiece",
    fileName = "EffectPiece",
    order = 3)]
public class EffectPieceData : CardPieceData
{
    public List<CardEventTuple> events = new List<CardEventTuple>();
}
