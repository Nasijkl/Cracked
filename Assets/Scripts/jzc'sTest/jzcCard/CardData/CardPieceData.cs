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
        // Ĭ��ʵ�֣�����еĻ����������׳�һ���쳣��ʾ�������ʵ���������
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
