using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardPieceData : ScriptableObject
{
    public Sprite sprite;

    public virtual CardPieceData deepCopy()
    {
        return this;
    }
    
}
