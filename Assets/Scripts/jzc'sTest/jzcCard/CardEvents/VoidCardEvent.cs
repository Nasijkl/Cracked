using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(
    menuName = "CardGame/Card Effect/Void Effect",
    fileName = "VoidEffect",
    order = 0)]
public class VoidCardEvent : CardEvent
{
    public VoidCardEvent()
    {
        this.EventId = 0;
        this.Value = 0;
        this.text = "";
    }
}
