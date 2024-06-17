using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEvent : ScriptableObject
{
    public EffectPieceObject parent;
    protected int EventId;
    public int Value;

    public EventTargetType primary_target;
    public EventTargetType secondary_target;
    
    public string text;

    public virtual string getText()
    {
        return text;
    }

    public virtual void Resolve(GameObject source_object, GameObject target_object, int value)
    {
        
    }

    
}
