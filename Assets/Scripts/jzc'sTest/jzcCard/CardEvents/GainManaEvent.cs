using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(
    menuName = "CardGame/Card Event/Gain Mana Event",
    fileName = "GainManaEvent",
    order = 6)]
public class GainManaEvent : CardEvent
{
    public GainManaEvent()
    {
        this.EventId = 6;
    }

    public override void Resolve(GameObject source_object, GameObject target_object, int value)
    {
        var target_character = target_object.GetComponent<CharacterObject>();
        if(target_character == null)
        {
            return;
        }
        var mana = target_character.Character.Mana;
        mana.SetValue(mana.GetValue());
        return;
    }
}