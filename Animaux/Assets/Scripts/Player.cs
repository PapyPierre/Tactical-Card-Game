using System.Collections.Generic;
using Cards;
using NaughtyAttributes;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    private UIManager _uiManager;
    private GameManager _gameManager;
        
    [SerializeField, Expandable] private DeckList deckList;
    
    [SerializeField]
    private List<CardManager.Cards> deck = new ();
    
    // Serialised for debug
    public List<CardManager.Cards> cardsInHand = new ();
    
    [HideInInspector] public CardManager.Cards selectedCardInHand;
    [HideInInspector] public bool hasPlayedACardThisTurn;
    
    public Color playerColor;

    [HideInInspector] public uint currentPoints;
    
    [HideInInspector] public bool isReadyToPlay;

    private void Start()
    {
        _uiManager = UIManager.instance;
        _gameManager = GameManager.instance;
        selectedCardInHand = CardManager.Cards.None;
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
            _gameManager.gameIsFinish = true;
            return;
        }
        
        cardsInHand.Add(deck[0]);
        deck.RemoveAt(0);
        _uiManager.UpdateCardInHandSprites();
    }

    public void SelectCardInHand(int i)
    {
        selectedCardInHand = cardsInHand[i];

        foreach (var cardInHand in _uiManager.cardInHandDisplays)
        {
            cardInHand.UnSelect();
        }
        
        _uiManager.cardInHandDisplays[i].Select();
    }
    
    public void StartTurn(bool isFirstPlayerTurn)
    {
        _gameManager.TurnNumber++;
        
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
