using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEvent : ScriptableObject
{
    public EffectPieceObject parent;
    protected int EventId;
    public int Value;
    
    protected string text;

    public virtual string getText(){
        return text;
    }

    
}
