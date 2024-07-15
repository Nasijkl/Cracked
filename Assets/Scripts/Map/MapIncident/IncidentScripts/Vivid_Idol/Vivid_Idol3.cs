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

            // ���ѡ����
            while (selectedCards.Count < count)
            {
                int randomIndex = Random.Range(0, deckManager.card_deck.Count);
                CrackedCardData card = deckManager.card_deck[randomIndex];
                if (!selectedCards.Contains(card))
                {
                    selectedCards.Add(card);
                    // ����һ���޸ĺ�Ŀ��Ƹ���
                    CrackedCardData modifiedCard = card.deepCopy();
                    // Ϊ���������Ѫ��ǩ
                    if (modifiedCard.card_pieces[1] is LabelPieceData labelPiece)
                    {
                        labelPiece.label = CardLabelType.BloodSuck;
                        modifiedCard.name += card.name+"(BloodSuck)";
                    }
                    modifiedCards.Add(modifiedCard);
                }
            }
            // �ӿ�����ɾ��ԭʼ�Ŀ���
            foreach (var card in selectedCards)
            {
                deckManager.removeCard(card);
            }

            // ���޸ĺ�Ŀ��Ƽӻؿ���
            foreach (var card in modifiedCards)
            {
                deckManager.addCard(card);
            }
        }
    }
}


