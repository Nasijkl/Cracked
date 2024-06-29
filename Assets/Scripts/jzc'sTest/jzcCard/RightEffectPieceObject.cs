using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class RightEffectPieceObject : EffectPieceObject
{
    private string name_text;
    private StringBuilder description_text;

    [SerializeField] private TextMeshPro nameText;
    [SerializeField] private TextMeshPro descriptionText;
    
    public override int pieceType(){
        return 4;
    }
    
    public override void initialize(CardPieceData data){
        EffectPieceData effect_data = data as EffectPieceData;
        this.sprite = effect_data.sprite;
        this.name_text = effect_data.name_text;
        nameText.text = effect_data.name_text;
        this.description_text = effect_data.effectText();
        descriptionText.SetText(effect_data.effectText());

        SpriteRenderer spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }
}
