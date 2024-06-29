using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(
    menuName = "CardGame/Card Event/Deal Damage Event",
    fileName = "DealDamageEvent",
    order = 1)]
public class DealDamageEvent : CardEvent
{
    public DealDamageEvent()
    {
        this.EventId = 1;
    }

    public override void Resolve(GameObject source_object, GameObject target_object, int value)
    {
        var target_character = target_object.GetComponent<CharacterObject>();
        var source_character = source_object.GetComponent<CharacterObject>();
        if(target_character == null || source_character == null)
        {
            return;
        }
        RuntimeCharacter target = target_character.Character as RuntimeCharacter;
        RuntimeCharacter source = source_character.Character as RuntimeCharacter;
        
        var targetHp = target.Hp;
        var hp = targetHp.GetValue();

        var targetShield = target.Shield;
        var shield = targetShield.GetValue();
        
        var damage = value;

        if (source.Status != null)
        {
            //TODO: raise deal damage event
        }

        if (damage >= shield)
        {
            var newHp = hp - (damage - shield);
            if (newHp < 0)
            {
                newHp = 0;
            }
            targetHp.SetValue(newHp);
            targetShield.SetValue(0);
        }
        else
        {
            targetShield.SetValue(shield - damage);
        }
    }
}