using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEngine.SceneManagement;

public class CardClickHandler : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // find all pieces
        Transform[] cardChildren = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            cardChildren[i] = transform.GetChild(i);
        }

        // piece ScriptableObject
        CostPieceData piece1 = cardChildren[0].GetComponent<PieceStore>().piece as CostPieceData;
        LabelPieceData piece2 = cardChildren[1].GetComponent<PieceStore>().piece as LabelPieceData;
        EffectPieceData piece3 = cardChildren[2].GetComponent<PieceStore>().piece as EffectPieceData;
        EffectPieceData piece4 = cardChildren[3].GetComponent<PieceStore>().piece as EffectPieceData;

        // CrackedCardData
        CrackedCardData firstCard = ScriptableObject.CreateInstance<CrackedCardData>();
        firstCard.card_pieces = new CardPieceData[4];
        firstCard.card_pieces[0] = piece1;
        firstCard.card_pieces[1] = piece2;
        firstCard.name = "CrackedCard_" + piece1.name + "_" + piece2.name;

        CrackedCardData secondCard = ScriptableObject.CreateInstance<CrackedCardData>();
        secondCard.card_pieces = new CardPieceData[4];
        secondCard.card_pieces[2] = piece3;
        secondCard.card_pieces[3] = piece4;
        secondCard.name = "CrackedCard_" + piece3.name + "_" + piece4.name;

        GlobalDeckManager globalManager = FindObjectOfType<GlobalDeckManager>();
        if (globalManager != null)
        {
            globalManager.addCard(firstCard);
            globalManager.addCard(secondCard);
            CardStore CardObject = this.GetComponent<CardStore>();
            globalManager.removeCard(CardObject.piece);
        }
        
        SaveCrackedCardData(firstCard);
        SaveCrackedCardData(secondCard);

        Destroy(gameObject);
    }

    private void SaveCrackedCardData(CrackedCardData cardData)
    {
        string path = "Assets/CrackedCards/" + cardData.name + ".asset";
        #if UNITY_EDITOR
        AssetDatabase.CreateAsset(cardData, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        #endif
        SceneManager.LoadScene("Map");
    }
}
