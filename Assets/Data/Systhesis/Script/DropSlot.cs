using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class DropSlot : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        Transform FirstChild_Drag = droppedObject.transform.GetChild(0);
        Transform FirstChild_Here = transform.GetChild(0).GetChild(0);

        if(FirstChild_Drag.gameObject.activeSelf == false && FirstChild_Here.gameObject.activeSelf == true)
        {
            CostPieceData piece1 = transform.GetChild(0).GetChild(0).GetComponent<PieceStore>().piece as CostPieceData;
            LabelPieceData piece2 = transform.GetChild(0).GetChild(1).GetComponent<PieceStore>().piece as LabelPieceData;
            EffectPieceData piece3 = droppedObject.transform.GetChild(2).GetComponent<PieceStore>().piece as EffectPieceData;
            EffectPieceData piece4 = droppedObject.transform.GetChild(3).GetComponent<PieceStore>().piece as EffectPieceData;

            
            CrackedCardData Card = ScriptableObject.CreateInstance<CrackedCardData>();
            Card.card_pieces = new CardPieceData[4];
            Card.card_pieces[0] = piece1;
            Card.card_pieces[1] = piece2;
            Card.card_pieces[2] = piece3;
            Card.card_pieces[3] = piece4;
            Card.name = "CrackedCard_" + piece1.name + "_" + piece2.name + "_" + piece3.name + "_" + piece4.name;

            GlobalDeckManager globalManager = FindObjectOfType<GlobalDeckManager>();
            if (globalManager != null)
            {
                globalManager.addCard(Card);
                CardStore FirstCard = droppedObject.transform.GetComponent<CardStore>();
                CardStore SecondCard = transform.GetChild(0).GetComponent<CardStore>();
                globalManager.removeCard(FirstCard.piece);
                globalManager.removeCard(SecondCard.piece);
            }

            SaveCrackedCardData(Card);

            Destroy(droppedObject);
            Destroy(transform.GetChild(0).gameObject);
        }
        else if(FirstChild_Drag.gameObject.activeSelf == true && FirstChild_Here.gameObject.activeSelf == false)
        {
            CostPieceData piece1 = droppedObject.transform.GetChild(0).GetComponent<PieceStore>().piece as CostPieceData;
            LabelPieceData piece2 = droppedObject.transform.GetChild(1).GetComponent<PieceStore>().piece as LabelPieceData;
            EffectPieceData piece3 = transform.GetChild(0).GetChild(2).GetComponent<PieceStore>().piece as EffectPieceData;
            EffectPieceData piece4 = transform.GetChild(0).GetChild(3).GetComponent<PieceStore>().piece as EffectPieceData;

            CrackedCardData Card = ScriptableObject.CreateInstance<CrackedCardData>();
            Card.card_pieces = new CardPieceData[4];
            Card.card_pieces[0] = piece1;
            Card.card_pieces[1] = piece2;
            Card.card_pieces[2] = piece3;
            Card.card_pieces[3] = piece4;
            Card.name = "CrackedCard_" + piece1.name + "_" + piece2.name + "_" + piece3.name + "_" + piece4.name;

            GlobalDeckManager globalManager = FindObjectOfType<GlobalDeckManager>();
            if (globalManager != null)
            {
                globalManager.addCard(Card);
                CardStore FirstCard = droppedObject.transform.GetComponent<CardStore>();
                CardStore SecondCard = transform.GetChild(0).GetComponent<CardStore>();
                globalManager.removeCard(FirstCard.piece);
                globalManager.removeCard(SecondCard.piece);
            }

            SaveCrackedCardData(Card);

            Destroy(droppedObject);
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    private void SaveCrackedCardData(CrackedCardData cardData)
    {
        string path = "Assets/CrackedCards/" + cardData.name + ".asset";
        AssetDatabase.CreateAsset(cardData, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        SceneManager.LoadScene("Map");
    }
}