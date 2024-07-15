using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
[CreateAssetMenu(
    menuName = "CardGame/Card Piece/EffectPiece",
    fileName = "EffectPiece",
    order = 3)]
public class EffectPieceData : CardPieceData
{
    public string name_text;

    public List<CardEventTuple> events = new List<CardEventTuple>();

    public StringBuilder effectText()
    {
        StringBuilder text = new StringBuilder();
        for(int i= 0; i < events.Count; i++)
        {
            text.Append(events[i].card_event.getText().Replace("$value", events[i].value.ToString()));
            text.Append(" ");
        }
        return text;
    }
}
