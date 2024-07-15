using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public CardBank cardBank; // ���Ӷ�CardBank������
    public Transform cardContainer; // ��������������
    public float pieceSpacing = 2f; // ��ƬƬ��֮��ļ��

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
                // Ϊÿ�ſ��ƴ���һ������
                string cardName = $"{item.Card.name}{i + 1}";

                // Ϊÿ�ſ��ƴ���һ����������ʹ�ù��������
                GameObject cardObject = new GameObject(cardName);
                cardObject.transform.SetParent(cardContainer, false);
                cardObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                
                for (int j = 0; j < item.Card.card_pieces.Length; j++)
                {
                    // Ϊÿ��CardPieceData����һ��SpriteRenderer
                    GameObject pieceObject = new GameObject($"Piece_{j}");
                    pieceObject.transform.SetParent(cardObject.transform, false);
                    SpriteRenderer pieceSprite = pieceObject.AddComponent<SpriteRenderer>();
                    pieceSprite.sprite = item.Card.card_pieces[j].sprite;

                    // ��ѡ������ÿ��Ƭ�ε�λ��
                    
                    // ע�⣺SpriteRenderer��ʹ��RectTransform��������ǵ���transform��localPosition
                }
                cardObject.transform.localPosition = new Vector3((temp%5)*pieceSpacing, -snum* pieceSpacing, 0);
                temp += 1;
                snum = temp / 5;
            }
        }
    }
}
