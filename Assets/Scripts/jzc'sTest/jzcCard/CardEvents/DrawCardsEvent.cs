using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(
    menuName = "CardGame/Card Event/Draw Cards Event",
    fileName = "DrawCardsEvent",
    order = 7)]
public class DrawCardsEvent : CardEvent
{
    public DrawCardsEvent()
    {
        this.EventId = 7;
    }

    public override void Resolve(GameObject source_object, GameObject target_object, int value)
    {
        var source_component = source_object.GetComponent<RuntimeDeckManager>();
        if(source_component == null)
        {
            return;
        }

        source_component.drawCardsToHand(value);

        return;
    }
}