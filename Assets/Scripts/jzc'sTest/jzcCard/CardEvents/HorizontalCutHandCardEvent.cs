using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(
    menuName = "CardGame/Card Event/Horizontal Cut Hand Card Event",
    fileName = "HorizontalCutHandCardEvent",
    order = 3)]
public class HorizontalCutHandCardEvent : CardEvent
{
    public HorizontalCutHandCardEvent()
    {
        this.EventId = 3;
    }

    public override void Resolve(GameObject source_object, GameObject target_object, int value)
    {
        var target_component = target_object.GetComponent<CrackedCardObject>();
        var source_component = source_object.GetComponent<CardDisplayManager>();
        if(target_component == null || source_component == null)
        {
            return;
        }
        
        List<CrackedCardData> pieces = CardUtils.HorizontalCut(target_component.data);
        source_component.DistroyCardInHand(target_object);
        source_component.CreateHandCards(pieces);

        return;
    }
}