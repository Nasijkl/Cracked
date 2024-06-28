using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
[CreateAssetMenu(fileName = "Vivid_Idol2", menuName = "Incident/IncidentPageData/Vivid_Idol/Vivid_Idol2")]
public class Vivid_Idol2 : IncidentPageData
{
    public override void Resolve()
    {
        // ������ʱ����GlobalDeckManager��ʵ��
        GlobalDeckManager deckManager = Object.FindObjectOfType<GlobalDeckManager>();
        if (deckManager != null)
        {
            // ��all_kind_card�в�����Ϊ"SoulSteal"�Ŀ���
            CrackedCardData soulStealCard = deckManager.all_kind_card.Find(card => card.name == "SoulSteal");
            if (soulStealCard != null)
            {
                // ����ҵ�����Ϊ"SoulSteal"�Ŀ��ƣ�������ӵ�������
                //deckManager.card_deck.Add(soulStealCard);
                deckManager.addCard(soulStealCard);
            }
        }
    }

}



