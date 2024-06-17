using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(
    menuName = "CardGame/Card Event/Gain Armor Event",
    fileName = "GainArmorEvent",
    order = 2)]
public class GainArmorEvent : CardEvent
{
    public GainArmorEvent()
    {
        this.EventId = 2;
    }

    public override void Resolve(GameObject source_object, GameObject target_object, int value)
    {
        var target_character = target_object.GetComponent<CharacterObject>();
        if(target_character == null)
        {
            return;
        }
        RuntimeCharacter target = target_character.Character as RuntimeCharacter;
        
        var targetShield = target.Shield;
        targetShield.SetValue(targetShield.Value + value);
    }
}