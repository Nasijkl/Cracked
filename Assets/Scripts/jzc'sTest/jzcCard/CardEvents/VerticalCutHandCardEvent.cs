using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(
    menuName = "CardGame/Card Event/Vertical Cut Hand Card Event",
    fileName = "VerticalCutHandCardEvent",
    order = 4)]
public class VerticalCutHandCardEvent : CardEvent
{
    public VerticalCutHandCardEvent()
    {
        this.EventId = 4;
    }

    public override void Resolve(GameObject source_object, GameObject target_object, int value)
    {
        var target_component = target_object.GetComponent<CrackedCardObject>();
        var source_component = source_object.GetComponent<CardDisplayManager>();
        if(target_component == null || source_component == null)
        {
            return;
        }
        
        List<CrackedCardData> pieces = CardUtils.VerticalCut(target_component.data);
        source_component.DistroyCardInHand(target_object);
        source_component.CreateHandCards(pieces);

        return;
    }
}