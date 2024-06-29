using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(
    menuName = "CardGame/Card Piece/EffectPiece",
    fileName = "EffectPiece",
    order = 3)]
public class EffectPieceData : CardPieceData
{
    public List<CardEventTuple> events = new List<CardEventTuple>();
<<<<<<< HEAD
=======
    public override CardPieceData deepCopy()
    {
        var copy = ScriptableObject.CreateInstance<EffectPieceData>();
        foreach (var cardEventTuple in this.events)
        {
            // ����CardEventTuple��һ���ṹ�壬����ĸ�ֵ�ᴴ��һ���µĸ���
            copy.events.Add(new CardEventTuple { card_event = cardEventTuple.card_event, value = cardEventTuple.value });
        }
        copy.sprite = this.sprite;
        return copy;
    }
>>>>>>> origin/lhr
}
