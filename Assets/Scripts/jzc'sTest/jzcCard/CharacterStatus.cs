using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(
    menuName = "CardGame/CharacterStatus/Status",
    fileName = "Status",
    order = 0)]
public class CharacterStatus: ScriptableObject
{
    public Trigger trigger;
    public List<CardEventTuple> events = new List<CardEventTuple>();
    protected int value;

    public Sprite sprite;

    public EffectResolutionManager manager;

    public GameEventStatus ValueChangedEvent;

    public CharacterStatus(List<CardEventTuple> events, Trigger trigger, EffectResolutionManager manager, GameEventStatus ValueChangedEvent, int value = 1)
    {
        this.events.AddRange(events);
        this.trigger = trigger;
        trigger.AddParent(this);
        //TO DO: Remove this from trigger before 
        this.manager = manager;
        this.sprite = trigger.sprite;
        this.value = value;
        this.ValueChangedEvent = ValueChangedEvent;
        ValueChangedEvent.Raise(this, this.value);
    }

    public virtual void Resolve()
    {
        foreach (var card_event in events)
        {
            manager.ResolveCardEvent(card_event.card_event, card_event.value * this.value);
        }
    }

    public int getValue()
    {
        return value;
    }

    public void SetValue(int value)
    {
        this.value = value;
        ValueChangedEvent.Raise(this, this.value);
    }

}
