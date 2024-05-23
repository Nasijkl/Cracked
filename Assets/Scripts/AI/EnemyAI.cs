using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI
{
   public CharacterObject Enemy;

   public int PatternIndex;
   public int EffectIndex;
   public int RepeatFlag;
   public int RepeatIndex;
   

   public List<Effect> Effects;

   public EffectTuple.EffectTuple selected_effect;

   public EnemyAI(CharacterObject enemy)
   {
      Enemy = enemy;
   }
}
