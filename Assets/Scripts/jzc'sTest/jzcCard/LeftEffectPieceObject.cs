using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeftEffectPieceObject : EffectPieceObject
{
    [SerializeField] private TextMeshPro nameText;
    [SerializeField] private TextMeshPro descriptionText;

    
    public override int pieceType(){
        return 3;
    }
    
}
