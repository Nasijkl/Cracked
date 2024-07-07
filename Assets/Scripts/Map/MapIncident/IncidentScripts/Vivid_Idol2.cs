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
        // 在运行时查找GlobalDeckManager的实例
        GlobalDeckManager deckManager = Object.FindObjectOfType<GlobalDeckManager>();
        if (deckManager != null)
        {
            // 从all_kind_card中查找名为"SoulSteal"的卡牌
            CrackedCardData soulStealCard = deckManager.all_kind_card.Find(card => card.name == "SoulSteal");
            if (soulStealCard != null)
            {
                // 如果找到了名为"SoulSteal"的卡牌，则将其添加到卡组中
                //deckManager.card_deck.Add(soulStealCard);
                deckManager.addCard(soulStealCard);
            }
        }
    }

}



