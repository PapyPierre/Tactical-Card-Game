using System;
using Board;
using Cards;
using NaughtyAttributes;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class MobileInputHandler : MonoBehaviour
{
    private GameManager _gameManager;
    private BoardManager _boardManager;
    private UIManager _uiManager;
    
    private Camera _camera;
    
    // Serialized for debug
    [SerializeField, ReadOnly]  private float holdTime;

    // Serialized for debug
    [SerializeField, ReadOnly] private Tile fingerOverThisTile;
    
    [SerializeField] private LayerMask tileLayers;
    
    private void Start()
    {
        _gameManager = GameManager.instance;
        _boardManager = BoardManager.instance;
        _uiManager = UIManager.instance;

        _camera = Camera.main;
    }

    private void Update()
    {
        if (holdTime > 0) 
        {
            holdTime += Time.deltaTime;
            
            if (fingerOverThisTile != null)
            {
                ManageHoldingOverTile();
            }
        }
    }

    public void OnTap(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) // Player taped once the screen at ctx.ReadValue<Vector2>()
        {
            holdTime = 0.0001f;
            
            if (!_gameManager.gameHasStarted) return;
            
            ShootRayToTile(ctx.ReadValue<Vector2>());
        }
    }
    
    public void OnReleaseTouch(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)  // Player release touch
        {
            holdTime = 0;
            fingerOverThisTile = null;
        }
    }

    private void ShootRayToTile(Vector3 pos)
    {
        Ray ray = _camera.ScreenPointToRay(pos);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2);
            
        if (Physics.Raycast(ray, out var hit, 100, tileLayers))
        {
            if (hit.collider.CompareTag("Tile"))
            {
                fingerOverThisTile = hit.collider.GetComponent<Tile>();
                TryPlaceCard();
            }
        }
    }

    private void TryPlaceCard()
    {
        if (_gameManager.currentPlayer.selectedCardInHand == CardManager.Cards.None) return;
        if (_gameManager.currentPlayer.hasPlayedACardThisTurn) return;
        if (!fingerOverThisTile.IsLegalMove()) return;
        
        _boardManager.PlaceCard(fingerOverThisTile, _gameManager.currentPlayer.selectedCardInHand);
    }

    private void ManageHoldingOverTile()
    {
        if (fingerOverThisTile.cardOnThisTile == null) return;
            
        if (holdTime > _uiManager.timeToShowCardInfo)
        {
            _uiManager.ShowCardInfoDisplay(fingerOverThisTile.cardOnThisTile.data.thisCard);
            holdTime = 0;
        }
    }
}
