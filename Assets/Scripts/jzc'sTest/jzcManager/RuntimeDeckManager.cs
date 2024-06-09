using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeDeckManager : MonoBehaviour
{
    private List<CrackedCardData> draw_pile;
    private List<CrackedCardData> discard_pile;
    private List<CrackedCardData> hand_cards;
    private const int preset_draw_pile_size = 50;
    private const int preset_hand_size = 10;
    private const int preset_discard_pile_size = 50;
    public CardDisplayManager cardDisplayManager;
    private DeckWidget _draw_pile_widget;
    private DiscardPileWidget _discard_pile_widget;

    private void Awake()
    {
        draw_pile = new List<CrackedCardData>(preset_draw_pile_size);
        hand_cards = new List<CrackedCardData>(preset_hand_size);
        discard_pile = new List<CrackedCardData>(preset_discard_pile_size);
    }

    public void Initialize(DeckWidget deck, DiscardPileWidget discardPile)
    {
        _draw_pile_widget = deck;
        _discard_pile_widget = discardPile;
    }

    public int LoadDeck(List<CrackedCardData> playerDeck)
    {
        var deckSize = 0;

        foreach (var card in playerDeck)
        {
            if(card == null)
                continue;

            draw_pile.Add(card);

            ++deckSize;

        }
        
        _draw_pile_widget.SetAmount(draw_pile.Count);
        _discard_pile_widget.SetAmount(0);

        return deckSize;
    }

    public void drawCardsToHand(int draws_num){
        List<CrackedCardData> drawn_cards = new List<CrackedCardData>(draws_num);

        while(draws_num > 0 && draw_pile.Count > 0)
        {
            if(draws_num > draw_pile.Count)
            {
                draws_num -= draw_pile.Count;
                drawn_cards.AddRange(draw_pile);
                draw_pile.Clear();
                shufflePile();
            }
            else
            {
                for (int i = 0; i < draws_num; i++)
                {
                    drawn_cards.Add(draw_pile[0]);
                    draw_pile.RemoveAt(0);
                }
                draws_num = 0;
            }
        }
        hand_cards.AddRange(drawn_cards);
        //Debug.Log(drawn_cards[0]);
        //Debug.Log(drawn_cards[0].card_pieces[0]);
        //Debug.Log(drawn_cards[0].card_pieces[1]);
        //Debug.Log(drawn_cards[0].card_pieces[2]);
        //Debug.Log(drawn_cards[0].card_pieces[3]);
        cardDisplayManager.CreateHandCards(drawn_cards, draw_pile.Count);

        return;
    }

    public void discardCardsFromHand(List<CrackedCardData> discarded_cards){
        for(int i = 0;i<discarded_cards.Count;i++){
            hand_cards.Remove(discarded_cards[i]);
            discard_pile.Add(discarded_cards[i]);
        }
        
        return;
    }

    public void discardCardsFromHand(CrackedCardData discarded_card){
        
        hand_cards.Remove(discarded_card);
        discard_pile.Add(discarded_card);
        return;
    }

    public void discardCardsFromHand(){
        discard_pile.AddRange(hand_cards);
        hand_cards.Clear();
        return;
    }

    public void shuffleDrawPile()
    {
        draw_pile.Shuffle();
    }

    public void shufflePile(){
        draw_pile.AddRange(discard_pile);
        shuffleDrawPile();
        discard_pile.Clear();
    }
}
