using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardPieceObject : MonoBehaviour
{
    public Sprite sprite;

    public CrackedCardObject parent;

    public abstract int pieceType(); // 1:part 1 - same as follows

    public abstract void initialize(CardPieceData data);

    //public abstract void setInfo();
}
