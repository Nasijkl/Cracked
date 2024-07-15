using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CardGenerationSystem_Separate : MonoBehaviour
{
    public List<GameObject> slots = new List<GameObject>();
    public GameObject CardPrefab;

    public void Start()
    {
        GenerateCards();
    }
    public void GenerateCards()
    {
        GlobalDeckManager globalManager = FindObjectOfType<GlobalDeckManager>();
        List<CrackedCardData> GeneratedData = new List<CrackedCardData>();
        
        GeneratedData = globalManager.getRandomCardsForCracking(slots.Count);
        for(int i = 0; i < slots.Count; i++)
        {
            GameObject Card = Instantiate(CardPrefab, slots[i].transform);
            Card.transform.localPosition = Vector3.zero;
            CardStore CardObject = Card.GetComponent<CardStore>();
            CardObject.piece = GeneratedData[i];

            Transform[] cardChildren = new Transform[Card.transform.childCount];
            
            for (int j = 0; j < Card.transform.childCount; j++)
            {
                cardChildren[j] = Card.transform.GetChild(j);
            }

            CrackedCardData cardData = GeneratedData[i]; 
            for (int j = 0; j < 4; j++)
            {
                Image fragmentImage = cardChildren[j].GetComponent<Image>();
                fragmentImage.sprite = cardData.card_pieces[j].sprite;
                
                PieceStore pieceStore = cardChildren[j].GetComponent<PieceStore>();
                if (pieceStore != null)
                {
                    pieceStore.piece = cardData.card_pieces[j];
                }
            }
        }

    }
        
}