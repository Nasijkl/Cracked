using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(
    menuName = "CardGame/Card Event/Trigger/Trigger",
    fileName = "Trigger",
    order = 0)]
public class  Trigger: CardEvent
{
    public int trigger_id;
    public GameEvent Event;
    public List<CharacterStatus> parent_characters;
    public Sprite sprite;


    
    private void OnEnable()
    {
        Event.RegisterTrigger(this);
    }

    private void OnDisable()
    {
        Event.UnregisterTrigger(this);
    }

    public void AddParent(CharacterStatus parent)
    {
        parent_characters.Add(parent);
        return;
    }

    public void RemoveParent(CharacterStatus parent)
    {
        parent_characters.Remove(parent);
        return;
    }

    public void OnEventRaised()
    {
        foreach(CharacterStatus c in parent_characters)
        if(c != null)
        {
            c.Resolve();
        }
    }
}
