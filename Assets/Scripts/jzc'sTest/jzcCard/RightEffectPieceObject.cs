using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RightEffectPieceObject : EffectPieceObject
{
    [SerializeField] private TextMeshPro nameText;
    [SerializeField] private TextMeshPro descriptionText;
    
    public override int pieceType(){
        return 4;
    }
    
}
