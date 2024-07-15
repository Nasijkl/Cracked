using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(
    menuName = "CardGame/CharacterStatus/EnemyStatus",
    fileName = "EnemyStatus",
    order = 1)]
public class EnemyStatus: CharacterStatus
{

    GameObject enemy_parent;

    public EnemyStatus(List<CardEventTuple> events, Trigger trigger, EffectResolutionManager manager, GameEventStatus ValueChangedEvent, GameObject parent, int value = 1)
    : base(events, trigger, manager, ValueChangedEvent, value)

    {
        this.events.AddRange(events);
        this.trigger = trigger;
        trigger.AddParent(this);
        //TO DO: Remove this from trigger before 
        this.manager = manager;
        this.sprite = trigger.sprite;
        this.enemy_parent = parent;
        this.value = value;
        this.ValueChangedEvent = ValueChangedEvent;
        ValueChangedEvent.Raise(this, this.value);
    }

    public override void Resolve()
    {
        foreach (var card_event in events)
        {
            manager.ResolveCardEvent(card_event.card_event, card_event.value * this.value, enemy_parent);
        }
    }
}
