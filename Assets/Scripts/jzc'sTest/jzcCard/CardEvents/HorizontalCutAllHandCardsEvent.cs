using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(
    menuName = "CardGame/Card Event/Horizontal Cut All Hand Cards Event",
    fileName = "HorizontalCutAllHandCardsEvent",
    order = 5)]
public class HorizontalCutAllHandCardsEvent : CardEvent
{
    public HorizontalCutAllHandCardsEvent()
    {
        this.EventId = 5;
    }

    public override void Resolve(GameObject source_object, GameObject target_object, int value)
    {
        var source_component = source_object.GetComponent<RuntimeDeckManager>();
        if(source_component == null)
        {
            return;
        }

        foreach(var card_data in source_component.hand_cards)
        {
            List<CrackedCardData> pieces = CardUtils.HorizontalCut(card_data);
            if(pieces == null)
            {
                continue;
            }
            source_component.DestroyCardFromHand(card_data);
            source_component.AddCardToHand(pieces);
        }

        return;
    }
}