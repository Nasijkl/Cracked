using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDeckManager : MonoBehaviour
{
    public List<CrackedCardData> card_deck;

    public void addCard(CrackedCardData card){
        card_deck.Add(card);
    }

    public void removeCard(CrackedCardData card){
        card_deck.Remove(card);
    }

    public List<CrackedCardData> getDuplicatedDeck(){
        //TODO: implement deepcopy function of CrackedCardData and return duplicated card_deck
        return card_deck;
    }
}
