using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardPieceData : ScriptableObject
{
    public Sprite sprite;

    public virtual CardPieceData deepCopy()
    {
<<<<<<< HEAD
        return this;
    }
    
=======
        // 默认实现（如果有的话），或者抛出一个异常提示子类必须实现这个方法
        throw new NotImplementedException("Subclass must implement deepCopy method.");
    }



    /*public virtual CardPieceData deepCopy()
    {
        CardPieceData copiedPiece = ScriptableObject.CreateInstance<CardPieceData>();
        copiedPiece.sprite = sprite;

        return copiedPiece;

    }*/

>>>>>>> origin/lhr
}
