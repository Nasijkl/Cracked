using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
using UnityEngine;

public class GlobalDeckManager : MonoBehaviour
{
    public List<CrackedCardData> card_deck = new List<CrackedCardData>();

    

    public void addCard(CrackedCardData card){
        card_deck.Add(card);
    }

    public void removeCard(CrackedCardData card){
        card_deck.Remove(card);
    }

    public List<CrackedCardData> getDuplicatedDeck(){
        //TODO: implement deepcopy function of CrackedCardData and return duplicated card_deck
        return card_deck;
=======
using System.Linq;
using UnityEngine;
using UnityEngine.tvOS;

public class GlobalDeckManager : MonoBehaviour
{
    public CardBank cardBank;
    public List<CrackedCardData> card_deck = new List<CrackedCardData>();
    public List<CrackedCardData> all_kind_card = new List<CrackedCardData>();

    void Start()
    {
        LoadCardsFromCardBank();
    }

    // 从CardBank加载卡牌到card_deck
    void LoadCardsFromCardBank()
    {
        if (cardBank != null)
        {
            card_deck.Clear();

            // 创建一个列表来存储需要删除的项
            List<CardBankItem> itemsToRemove = new List<CardBankItem>();

            foreach (CardBankItem item in cardBank.Items)
            {
                // 检查卡牌是否为None
                if (item.Card != null && item.Card.name != "Missing")
                {
                    // 将卡牌数量设置为5
                    item.Amount = 5;
                    for (int i = 0; i < item.Amount; i++)
                    {
                        card_deck.Add(item.Card);
                    }
                }
                else
                {
                    // 将需要删除的项添加到列表中
                    itemsToRemove.Add(item);
                }
            }

            // 遍历完成后，删除所有需要删除的项
            foreach (var itemToRemove in itemsToRemove)
            {
                cardBank.Items.Remove(itemToRemove);
            }
        }
    }

    public void addCard(CrackedCardData card)
    {
        card_deck.Add(card);
        if (cardBank != null)
        {
            cardBank.AddCardToBank(card);
        }
    }
    public void removeCard(CrackedCardData card)
    {
        bool removed = card_deck.Remove(card);

        // 如果成功移除，并且cardBank不为空，则更新cardBank
        if (removed && cardBank != null)
        {
            cardBank.RemoveCardFromBank(card);
        }
    }
    public List<CrackedCardData> getRandomCards(int num)
    {
        if (card_deck.Count == 0 || num <= 0) return new List<CrackedCardData>(); // 如果卡组为空或请求卡牌数量小于等于0，则返回空列表

        if (num >= card_deck.Count) return getDuplicatedDeck(); // 如果请求卡牌数量大于等于卡组中的数量，则返回卡组的副本

        List<CrackedCardData> result = new List<CrackedCardData>();

        while (result.Count < num)
        {
            int randomIndex = UnityEngine.Random.Range(0, card_deck.Count); // 随机获取卡组中的一个索引
            CrackedCardData randomCard = card_deck[randomIndex]; // 获取卡组中对应索引的卡牌
            if (!result.Contains(randomCard)) result.Add(randomCard); // 如果结果列表中不包含该卡牌，则添加到结果列表中
        }

        return result;
    }
    public List<CrackedCardData> getRandomCardsForOne(int num)
    {
        List<CrackedCardData> validCards = new List<CrackedCardData>();

        // Check if the card deck is not empty and num is greater than 0
        if (card_deck.Count > 0 && num > 0)
        {
            // Add all valid cards to the validCards list
            foreach (CrackedCardData card in card_deck)
            {
                bool isValid = true;

                // Check if the card has at least one non-null piece
                bool hasNonNullPiece = false;
                for (int i = 1; i < card.card_pieces.Length; i++)
                {
                    if (card.card_pieces[i] != null)
                    {
                        hasNonNullPiece = true;
                        break;
                    }
                }

                if (card.card_pieces[0] == null || !hasNonNullPiece)
                {
                    isValid = false;
                }

                if (isValid)
                {
                    validCards.Add(card);
                }
            }

            int n = validCards.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                CrackedCardData temp = validCards[k];
                validCards[k] = validCards[n];
                validCards[n] = temp;
            }

            // Return num number of cards or all valid cards if less than num
            if (validCards.Count <= num)
            {
                return validCards;
            }
            else
            {
                return validCards.Take(num).ToList();
            }
        }
        return new List<CrackedCardData>();
    }
    public List<CrackedCardData> getRandomCardsForTwo(int num)
    {
        List<CrackedCardData> validCards = new List<CrackedCardData>();

        // Check if the card deck is not empty and num is greater than 0
        if (card_deck.Count > 0 && num > 0)
        {
            // Add all valid cards to the validCards list
            foreach (CrackedCardData card in card_deck)
            {
                bool isValid = true;

                // Check if the card has at least two non-null pieces
                int nonNullPieces = 0;
                for (int i = 0; i < card.card_pieces.Length; i++)
                {
                    if (card.card_pieces[i] != null)
                    {
                        nonNullPieces++;
                    }
                }

                if (card.card_pieces[1] == null || nonNullPieces < 2)
                {
                    isValid = false;
                }

                if (isValid)
                {
                    validCards.Add(card);
                }
            }

            // Shuffle the validCards list
            int n = validCards.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                CrackedCardData temp = validCards[k];
                validCards[k] = validCards[n];
                validCards[n] = temp;
            }

            // Return num number of cards or all valid cards if less than num
            if (validCards.Count <= num)
            {
                return validCards;
            }
            else
            {
                return validCards.Take(num).ToList();
            }
        }

        return new List<CrackedCardData>();
    }
    public List<CrackedCardData> getRandomCardsForThree(int num)
    {
        List<CrackedCardData> validCards = new List<CrackedCardData>();

        // Check if the card deck is not empty and num is greater than 0
        if (card_deck.Count > 0 && num > 0)
        {
            // Add all valid cards to the validCards list
            foreach (CrackedCardData card in card_deck)
            {
                bool isValid = true;

                // Check if the card has at least two non-null pieces
                int nonNullPieces = 0;
                for (int i = 0; i < card.card_pieces.Length; i++)
                {
                    if (card.card_pieces[i] != null)
                    {
                        nonNullPieces++;
                    }
                }

                if (card.card_pieces[3] == null || nonNullPieces < 2)
                {
                    isValid = false;
                }

                if (isValid)
                {
                    validCards.Add(card);
                }
            }

            // Shuffle the validCards list
            int n = validCards.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                CrackedCardData temp = validCards[k];
                validCards[k] = validCards[n];
                validCards[n] = temp;
            }

            // Return num number of cards or all valid cards if less than num
            if (validCards.Count <= num)
            {
                return validCards;
            }
            else
            {
                return validCards.Take(num).ToList();
            }
        }

        return new List<CrackedCardData>();
    }
    public List<CrackedCardData> getRandomCardsForFour(int num)
    {
        List<CrackedCardData> validCards = new List<CrackedCardData>();

        // Check if the card deck is not empty and num is greater than 0
        if (card_deck.Count > 0 && num > 0)
        {
            // Add all valid cards to the validCards list
            foreach (CrackedCardData card in card_deck)
            {
                bool isValid = true;

                // Check if the card has at least two non-null pieces
                int nonNullPieces = 0;
                for (int i = 0; i < card.card_pieces.Length; i++)
                {
                    if (card.card_pieces[i] != null)
                    {
                        nonNullPieces++;
                    }
                }

                if (card.card_pieces[3] == null || nonNullPieces < 2)
                {
                    isValid = false;
                }

                if (isValid)
                {
                    validCards.Add(card);
                }
            }

            // Shuffle the validCards list
            int n = validCards.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                CrackedCardData temp = validCards[k];
                validCards[k] = validCards[n];
                validCards[n] = temp;
            }

            // Return num number of cards or all valid cards if less than num
            if (validCards.Count <= num)
            {
                return validCards;
            }
            else
            {
                return validCards.Take(num).ToList();
            }
        }

        return new List<CrackedCardData>();
    }
    public List<CrackedCardData> getRandomCardsForCracking(int num)
    {
        List<CrackedCardData> validCards = new List<CrackedCardData>();

        // Check if the card deck is not empty and num is greater than 0
        if (card_deck.Count > 0 && num > 0)
        {
            // Add all valid cards to the validCards list
            foreach (CrackedCardData card in card_deck)
            {
                bool isValid = true;

                // Check if the card has at least three non-null pieces
                int nonNullPieces = 0;
                for (int i = 0; i < card.card_pieces.Length; i++)
                {
                    if (card.card_pieces[i] != null)
                    {
                        nonNullPieces++;
                    }
                }

                if (nonNullPieces < 2)
                {
                    isValid = false;
                }
                /*else if ((card.card_pieces[0] == null || card.card_pieces[1] == null) && (card.card_pieces[2] == null || card.card_pieces[3] == null))
                {
                    isValid = false;
                }*/

                if (isValid)
                {
                    validCards.Add(card);
                }
            }

            // Shuffle the validCards list
            int n = validCards.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                CrackedCardData temp = validCards[k];
                validCards[k] = validCards[n];
                validCards[n] = temp;
            }

            // Return num number of cards or all valid cards if less than num
            if (validCards.Count <= num)
            {
                return validCards;
            }
            else
            {
                return validCards.Take(num).ToList();
            }
        }

        return new List<CrackedCardData>();
    }
    public List<CrackedCardData> getRandomCardsForCombining(int num)
    {
        List<CrackedCardData> validCards = new List<CrackedCardData>();

        // Check if the card deck is not empty and num is greater than 0
        if (card_deck.Count > 0 && num > 0)
        {
            // Add all valid cards to the validCards list
            foreach (CrackedCardData card in card_deck)
            {
                bool isValid = true;

                // Check if the card has at least three non-null pieces
                int nonNullPieces = 0;
                for (int i = 0; i < card.card_pieces.Length; i++)
                {
                    if (card.card_pieces[i] != null)
                    {
                        nonNullPieces++;
                    }
                }

                if (nonNullPieces == card.card_pieces.Length)
                {
                    isValid = false;
                }
                if (isValid)
                {
                    validCards.Add(card);
                }
            }
            int n = validCards.Count;
            if (validCards.Count <= 1)
            {
                return new List<CrackedCardData>();
            }
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                CrackedCardData temp = validCards[k];
                validCards[k] = validCards[n];
                validCards[n] = temp;
            }

            // Select num random cards or all validCards if less than num
            List<CrackedCardData> selectedCards = new List<CrackedCardData>();
            if (validCards.Count <= num)
            {
                selectedCards = validCards;
            }
            else
            {
                for (int i = 0; i < num; i++)
                {
                    selectedCards.Add(validCards[i]);
                }
            }
            bool canCombine = false;
            for (int i = 0; i < selectedCards.Count && !canCombine; i++)
            {
                for (int j = i + 1; j < selectedCards.Count && !canCombine; j++)
                {
                    bool disjoint = true;
                    for (int k = 0; k < selectedCards[i].card_pieces.Length && disjoint; k++)
                    {
                        if (selectedCards[i].card_pieces[k] != null && selectedCards[j].card_pieces[k] != null)
                        {
                            disjoint = false;
                        }
                    }
                    if (disjoint)
                    {
                        canCombine = true;
                    }
                }
            }
            // Return the selected combinable cards or an empty list
            if (canCombine)
            {
                return selectedCards;
            }
            else
            {
                return new List<CrackedCardData>();
            }
        }

        return new List<CrackedCardData>();
    }

    public List<CrackedCardData> getRandomCardsForHorizontalCut(int num)
    {
        List<CrackedCardData> validCards = new List<CrackedCardData>();

        // Check if the card deck is not empty and num is greater than 0
        if (card_deck.Count > 0 && num > 0)
        {
            // Add all valid cards to the validCards list
            foreach (CrackedCardData card in card_deck)
            {
                bool isValid = false;

                // Check if the card has at least one non-null piece in 1 and 2, and at least one non-null piece in 3 and 4
                if ((card.card_pieces[0] != null || card.card_pieces[1] != null) && (card.card_pieces[2] != null || card.card_pieces[3] != null))
                {
                    isValid = true;
                }

                if (isValid)
                {
                    validCards.Add(card);
                }
            }

            // Shuffle the validCards list
            int n = validCards.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                CrackedCardData temp = validCards[k];
                validCards[k] = validCards[n];
                validCards[n] = temp;
            }

            // Return num number of cards or all valid cards if less than num
            if (validCards.Count <= num)
            {
                return validCards;
            }
            else
            {
                return validCards.Take(num).ToList();
            }
        }

        return new List<CrackedCardData>();
    }
    public List<CrackedCardData> getRandomCardsForVerticalCut(int num)
    {
        List<CrackedCardData> validCards = new List<CrackedCardData>();

        // Check if the card deck is not empty and num is greater than 0
        if (card_deck.Count > 0 && num > 0)
        {
            // Add all valid cards to the validCards list
            foreach (CrackedCardData card in card_deck)
            {
                bool isValid = false;

                // Check if the card has at least one non-null piece in 1 and 3, and at least one non-null piece in 2 and 4
                if ((card.card_pieces[0] != null || card.card_pieces[2] != null) && (card.card_pieces[1] != null || card.card_pieces[3] != null))
                {
                    isValid = true;
                }

                if (isValid)
                {
                    validCards.Add(card);
                }
            }

            // Shuffle the validCards list
            int n = validCards.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                CrackedCardData temp = validCards[k];
                validCards[k] = validCards[n];
                validCards[n] = temp;
            }

            // Return num number of cards or all valid cards if less than num
            if (validCards.Count <= num)
            {
                return validCards;
            }
            else
            {
                return validCards.Take(num).ToList();
            }
        }

        return new List<CrackedCardData>();
    }
    public void addCards(List<CrackedCardData> cardsToAdd)
    {
        foreach (CrackedCardData card in cardsToAdd)
        {
            card_deck.Add(card);
            if (cardBank != null)
            {
                cardBank.AddCardToBank(card);
            }
        }
    }
    public int deleteCards(List<CrackedCardData> cardsToDelete)
    {
        int failedDeletions = 0;

        // Check if the card deck is not empty
        if (card_deck.Count > 0)
        {
            // Loop through the cardsToDelete list and remove each card from the card_deck
            foreach (CrackedCardData card in cardsToDelete)
            {
                bool removed = card_deck.Remove(card); // 尝试移除卡牌，并记录结果
                if (!removed)
                {
                    failedDeletions++; // 如果移除失败，增加失败计数器
                }
                else if (cardBank != null) // 如果移除成功，并且cardBank不为空
                {
                    cardBank.RemoveCardFromBank(card); // 从cardBank中移除卡牌
                }
            }
        }

        return failedDeletions;
    }
    public List<CrackedCardData> getDuplicatedDeck()
    {
        List<CrackedCardData> duplicatedDeck = new List<CrackedCardData>();

        foreach (CrackedCardData card in card_deck)
        {
            duplicatedDeck.Add(card.deepCopy());
        }

        return duplicatedDeck;
>>>>>>> origin/lhr
    }
}
