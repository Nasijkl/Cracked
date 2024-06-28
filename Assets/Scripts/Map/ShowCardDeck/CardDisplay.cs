using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public CardBank cardBank; // 添加对CardBank的引用
    public Transform cardContainer; // 卡牌容器的引用
    public float pieceSpacing = 2f; // 卡片片段之间的间隔

    void Start()
    {
        DisplayCards();
    }

    void DisplayCards()
    {
        int temp = 0;
        int snum = 0;
        foreach (var item in cardBank.Items)
        {
            
            for (int i = 0; i < item.Amount; i++)
            {
                // 为每张卡牌创建一个容器
                string cardName = $"{item.Card.name}{i + 1}";

                // 为每张卡牌创建一个容器，并使用构造的名称
                GameObject cardObject = new GameObject(cardName);
                cardObject.transform.SetParent(cardContainer, false);
                cardObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                
                for (int j = 0; j < item.Card.card_pieces.Length; j++)
                {
                    // 为每个CardPieceData创建一个SpriteRenderer
                    GameObject pieceObject = new GameObject($"Piece_{j}");
                    pieceObject.transform.SetParent(cardObject.transform, false);
                    SpriteRenderer pieceSprite = pieceObject.AddComponent<SpriteRenderer>();
                    pieceSprite.sprite = item.Card.card_pieces[j].sprite;

                    // 可选：调整每个片段的位置
                    
                    // 注意：SpriteRenderer不使用RectTransform，因此我们调整transform的localPosition
                }
                cardObject.transform.localPosition = new Vector3((temp%5)*pieceSpacing, -snum* pieceSpacing, 0);
                temp += 1;
                snum = temp / 5;
            }
        }
    }
}
