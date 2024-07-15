using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "CardGame/Templates/Card Bank", fileName = "CardBank", order = 3)]
public class CardBank : ScriptableObject
{
    public string Name;
    public List<CardBankItem> Items = new List<CardBankItem>();

    public void AddCardToBank(CrackedCardData card, int amount = 1)
    {
        // 检查是否已经有这张卡
        var existingItem = Items.FirstOrDefault(item => item.Card == card);
        if (existingItem != null)
        {
            // 如果卡牌已存在，增加数量
            existingItem.Amount += amount;
        }
        else
        {
            // 如果卡牌不存在，创建新的CardBankItem并添加到Items中
            Items.Add(new CardBankItem { Card = card, Amount = amount });
        }
    }
    public void RemoveCardFromBank(CrackedCardData card, int amount = 1)
    {
        var existingItem = Items.FirstOrDefault(item => item.Card == card);
        if (existingItem != null)
        {
            existingItem.Amount -= amount;
            // 如果数量减少到0或以下，可以选择从列表中移除该项
            if (existingItem.Amount <= 0)
            {
                Items.Remove(existingItem);
            }
        }
        // 注意：如果卡牌不存在于银行中，这里可以根据需要添加逻辑处理
    }
}