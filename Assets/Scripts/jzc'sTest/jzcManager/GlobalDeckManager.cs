using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
}
