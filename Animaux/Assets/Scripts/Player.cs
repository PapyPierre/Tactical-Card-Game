using System.Collections.Generic;
using Cards;
using NaughtyAttributes;
using UI;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private UIManager _uiManager;
    
    [SerializeField, Expandable] private DeckList deckList;
    
    [SerializeField]
    private List<CardManager.Cards> deck = new ();
    
    // Serialised for debug
    public List<CardManager.Cards> cardsInHand = new ();
    
    [HideInInspector] public CardManager.Cards selectedCardInHand;
    [HideInInspector] public bool hasPlayedACardThisTurn;

    public Color playerColor;

    [HideInInspector] public uint numberOfPoints; 

    [HideInInspector] public bool isReadyToPlay;

    private void Start()
    {
        _uiManager = UIManager.instance;
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
        
        cardsInHand.Add(deck[0]);
        deck.RemoveAt(0);
        _uiManager.UpdateCardInHandSprites();
    }

    public void SelectCardInHand(int i)
    {
        selectedCardInHand = cardsInHand[i];
        
        _uiManager.cardInfoDisplayer.ShowCardInfoDisplay(cardsInHand[i]);

        foreach (var cardInHand in _uiManager.cardInHandDisplays)
        {
            cardInHand.isLocked = false;
        }

        _uiManager.cardInHandDisplays[i].isLocked = true;
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
}
