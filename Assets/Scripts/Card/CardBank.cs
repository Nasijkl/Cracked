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
        // ����Ƿ��Ѿ������ſ�
        var existingItem = Items.FirstOrDefault(item => item.Card == card);
        if (existingItem != null)
        {
            // ��������Ѵ��ڣ���������
            existingItem.Amount += amount;
        }
        else
        {
            // ������Ʋ����ڣ������µ�CardBankItem����ӵ�Items��
            Items.Add(new CardBankItem { Card = card, Amount = amount });
        }
    }
    public void RemoveCardFromBank(CrackedCardData card, int amount = 1)
    {
        var existingItem = Items.FirstOrDefault(item => item.Card == card);
        if (existingItem != null)
        {
            existingItem.Amount -= amount;
            // ����������ٵ�0�����£�����ѡ����б����Ƴ�����
            if (existingItem.Amount <= 0)
            {
                Items.Remove(existingItem);
            }
        }
        // ע�⣺������Ʋ������������У�������Ը�����Ҫ����߼�����
    }
}