using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardUtils
{
   public static bool CardCanBePlayed(CrackedCardData card, IntVariable playerMana)
   {
      if(card.card_pieces[0] == null ||
      card.card_pieces[1] == null ||
      card.card_pieces[2] == null ||
      card.card_pieces[3] == null)
      {
         return false;
      }

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
      return cost <= playerMana.GetValue();
   }

   public static List<CrackedCardData> HorizontalCut(CrackedCardData card)
   {
      if((card.card_pieces[0]==null && card.card_pieces[1]==null) ||
      (card.card_pieces[2]==null && card.card_pieces[3]==null))
      {
         return null;
      }
      List<CrackedCardData> pieces = new List<CrackedCardData>();
      var copy_card = card.deepCopy();
      List<CardPieceData> splited_card = new List<CardPieceData>();
      splited_card.Add(copy_card.card_pieces[0]);
      splited_card.Add(copy_card.card_pieces[1]);
      splited_card.Add(null);
      splited_card.Add(null);
      pieces.Add(new CrackedCardData(splited_card));
      splited_card.Clear();
      splited_card.Add(null);
      splited_card.Add(null);
      splited_card.Add(copy_card.card_pieces[2]);
      splited_card.Add(copy_card.card_pieces[3]);
      pieces.Add(new CrackedCardData(splited_card));
      return pieces;
   }

   public static List<CrackedCardData> VerticalCut(CrackedCardData card)
   {
      if((card.card_pieces[0]==null && card.card_pieces[2]==null) ||
      (card.card_pieces[1]==null && card.card_pieces[3]==null))
      {
         return null;
      }
      List<CrackedCardData> pieces = new List<CrackedCardData>();
      var copy_card = card.deepCopy();
      List<CardPieceData> splited_card = new List<CardPieceData>();
      splited_card.Add(copy_card.card_pieces[0]);
      splited_card.Add(null);
      splited_card.Add(copy_card.card_pieces[2]);
      splited_card.Add(null);
      pieces.Add(new CrackedCardData(splited_card));
      splited_card.Clear();
      splited_card.Add(null);
      splited_card.Add(copy_card.card_pieces[1]);
      splited_card.Add(null);
      splited_card.Add(copy_card.card_pieces[3]);
      pieces.Add(new CrackedCardData(splited_card));
      return pieces;
   }

   public static CrackedCardData Combine(List<CrackedCardData> cards)
   {
      var card_1 = cards[0];
      var card_2 = cards[1];
      if((card_1.card_pieces[0]!=null && card_2.card_pieces[0]!=null) ||
      (card_1.card_pieces[1]!=null && card_2.card_pieces[1]!=null) ||
      (card_1.card_pieces[2]!=null && card_2.card_pieces[2]!=null) ||
      (card_1.card_pieces[3]!=null && card_2.card_pieces[3]!=null) ||
      (card_1.card_pieces[0]!=null && card_2.card_pieces[1]==null && card_2.card_pieces[2]==null) ||
      (card_1.card_pieces[1]!=null && card_2.card_pieces[0]==null && card_2.card_pieces[3]==null) ||
      (card_1.card_pieces[2]!=null && card_2.card_pieces[0]==null && card_2.card_pieces[3]==null) ||
      (card_1.card_pieces[3]!=null && card_2.card_pieces[1]==null && card_2.card_pieces[2]==null) ||
      (card_2.card_pieces[0]!=null && card_1.card_pieces[1]==null && card_1.card_pieces[2]==null) ||
      (card_2.card_pieces[1]!=null && card_1.card_pieces[0]==null && card_1.card_pieces[3]==null) ||
      (card_2.card_pieces[2]!=null && card_1.card_pieces[0]==null && card_1.card_pieces[3]==null) ||
      (card_2.card_pieces[3]!=null && card_1.card_pieces[1]==null && card_1.card_pieces[2]==null))
      {
         return null;
      }
      var copy_card_1 = card_1.deepCopy();
      var copy_card_2 = card_2.deepCopy();
      //CrackedCardData completeCard = new


      List<CardPieceData> complete_card = new List<CardPieceData>();
      for(int i = 0; i < 4; i++)
      {
         if(copy_card_1.card_pieces[i] != null)
         {
            complete_card.Add(copy_card_1.card_pieces[i]);
         }
         else
         {
            complete_card.Add(copy_card_2.card_pieces[i]);
         }
      }
      
      return new CrackedCardData(complete_card);
   }
   
   public static bool CardHasTargetableEffect(CrackedCardData card)
   {
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
