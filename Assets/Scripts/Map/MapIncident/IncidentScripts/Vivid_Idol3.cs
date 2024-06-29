using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vivid_Idol3", menuName = "Incident/IncidentPageData/Vivid_Idol/Vivid_Idol3")]
public class Vivid_Idol3 : IncidentPageData
{
    public override void Resolve()
    {
        AddBloodSuckTagToRandomCards(3);
    }
    private void AddBloodSuckTagToRandomCards(int count)
    {
        GlobalDeckManager deckManager = FindObjectOfType<GlobalDeckManager>();
        if (deckManager != null && deckManager.card_deck.Count >= count)
        {
            List<CrackedCardData> selectedCards = new List<CrackedCardData>();
            List<CrackedCardData> modifiedCards = new List<CrackedCardData>();

            // 随机选择卡牌
            while (selectedCards.Count < count)
            {
                int randomIndex = Random.Range(0, deckManager.card_deck.Count);
                CrackedCardData card = deckManager.card_deck[randomIndex];
                if (!selectedCards.Contains(card))
                {
                    selectedCards.Add(card);
                    // 创建一个修改后的卡牌副本
                    CrackedCardData modifiedCard = card.deepCopy();
                    // 为卡牌添加嗜血标签
                    if (modifiedCard.card_pieces[1] is LabelPieceData labelPiece)
                    {
                        labelPiece.label = CardLabelType.BloodSuck;
                        modifiedCard.name += card.name+"(BloodSuck)";
                    }
                    modifiedCards.Add(modifiedCard);
                }
            }
            // 从卡组中删除原始的卡牌
            foreach (var card in selectedCards)
            {
                deckManager.removeCard(card);
            }

            // 将修改后的卡牌加回卡组
            foreach (var card in modifiedCards)
            {
                deckManager.addCard(card);
            }
        }
    }
}


