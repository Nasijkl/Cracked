using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "CardGame/Patterns/Adaptive Pattern",
    fileName = "AdaptivePattern",
    order = 2)]
public class AdaptivePattern : ScriptableObject
{
    [SerializeField]
    public List<EffectTuple.EffectTuple> Effects = new List<EffectTuple.EffectTuple>();
    public List<PatternStatus> Patterns = new List<PatternStatus>();

    public string GetName(){
        return "Adaptive Pattern";
    }
}
