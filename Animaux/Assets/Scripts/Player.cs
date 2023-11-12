using System.Collections.Generic;
using Cards;
using NaughtyAttributes;
using UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Expandable] private DeckList deckList;
    private List<CardManager.Cards> deck = new ();
    
    // Serialised for debug
    public List<CardManager.Cards> cardsInHand = new ();
    
    [HideInInspector] public CardManager.Cards selectedCardInHand;

    public Color playerColor;

    [HideInInspector] public bool isReadyToPlay;

    private void Start()
    {
        selectedCardInHand = CardManager.Cards.Uninitialised;
        Init();
    }
    
    private void Init()
    {
        foreach (var card in deckList.list)
        {
            deck.Add(card);
        }
        
        deck.Shuffle();
    }

    private void Draw()
    {
        if (deck.Count <= 0)
        {
            Debug.Log("no card left in the deck");
            return;
        }
        
        CardManager.Cards cardToDraw = deck[^1];
        cardsInHand.Add(cardToDraw);
        deck.Remove(cardToDraw);
        UIManager.instance.UpdateCardInHandSprites();
    }

    public void SelectCardInHand(int i)
    {
        selectedCardInHand = cardsInHand[i];
    }
    
    public void StartTurn(bool isFirstPlayerTurn)
    {
        GameManager.instance.turnNumber++;
        
        if (isFirstPlayerTurn)
        {
            for (int i = 0; i < 3; i++)
            {
                Draw();
            }
        }
        else
        {
            Draw();
        }
        
        isReadyToPlay = true;
    }

    public void FinishTurn()
    {
        GameManager.instance.EndOfTurn();
    }
}
