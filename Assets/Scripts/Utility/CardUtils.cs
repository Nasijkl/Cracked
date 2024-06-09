using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardUtils
{
   public static bool CardCanBePlayed(CrackedCardData card, IntVariable playerMana)
   {
      int cost = 0;
      CostPieceData top_left_piece = card.card_pieces[0] as CostPieceData;
      if(top_left_piece != null)
      {
         cost += top_left_piece.cost;
      }
      CostPieceData top_right_piece = card.card_pieces[1] as CostPieceData;
      if(top_right_piece != null)
      {
         cost += top_right_piece.cost;
      }
      return cost <= playerMana.Value;
   }
   
   public static bool CardHasTargetableEffect(CrackedCardData card)
   {
      //TODO
      /*
      // 判断卡牌是否要展示攻击箭头，判断标准是卡牌是否含有针对攻击敌人的特效，这是一个很必要的函数
      foreach (var effect in card.Effects)
      {
         if (effect is TargetableEffect targetableEffect)
         {
            if (targetableEffect.Target == EffectTargetType.TargetEnemy)
            {
               return true;
            }
         }
      }
      */

      return false;

   }
}
