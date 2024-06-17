using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelPieceObject : CardPieceObject
{
    CardLabelType label;

    public override int pieceType(){
        return 2;
    }
    public override void initialize(CardPieceData data){
        LabelPieceData label_data = data as LabelPieceData;
        this.sprite = label_data.sprite;
        this.label = label_data.label;

        SpriteRenderer spriteRenderer = this.gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }
    public virtual void OnDrawnEventHandler(){}
}
