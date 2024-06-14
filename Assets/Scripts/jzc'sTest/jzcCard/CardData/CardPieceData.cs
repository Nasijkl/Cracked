using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardPieceData : ScriptableObject
{
    public Sprite sprite;

    public CardPieceData DeepCopy()
    {
        CardPieceData copiedPiece = ScriptableObject.CreateInstance<CardPieceData>();
        copiedPiece.sprite = sprite;

        return copiedPiece;
    }
}
