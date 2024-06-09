using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPieceObject : CardPieceObject
{
    public List<CardEvent> event_list;

    public override int pieceType(){
        return 5;
    }
    public override void initialize(CardPieceData data){
        EffectPieceData effect_data = data as EffectPieceData;
        this.sprite = effect_data.sprite;
        //TODO: add event class

        SpriteRenderer spriteRenderer = this.gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }
}
