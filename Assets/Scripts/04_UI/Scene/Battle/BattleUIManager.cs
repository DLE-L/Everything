using System;
using System.Collections.Generic;
using Core;
using Core.Event;
using Data.Collectible.Card;
using GamePlay.Battle;
using UnityEngine;
using Utils;

namespace UIs.Battle
{
  public class BattleUIManager : MonoBehaviour
  {
    [Header("Battle UI Manager")]
    [SerializeField] private Battle_Canvas _battleCanvas;
    [SerializeField] private btnPlayerTurnEnd _btnPlayerTurnEnd;
    public AddressableObjectPooler AddressableObjectPooler { get; private set; }

    private readonly Dictionary<Guid, GameObject> _cardObjects = new();

    private void Awake()
    {
      _battleCanvas ??= FindAnyObjectByType<Battle_Canvas>();
      _btnPlayerTurnEnd ??= FindAnyObjectByType<btnPlayerTurnEnd>();
    }

    private async void DrawHandCard(RuntimeCard runtimeCard)
    {
      try
      {
        var obj = await AddressableObjectPooler.Get(_battleCanvas.HandTr);
        var battleCard = obj.GetComponent<Button_BattleCard>();
        battleCard?.Setup(runtimeCard.Data);
        _cardObjects.Add(runtimeCard.InstanceID, obj);
      }
      catch (Exception e)
      {
        Debug.Log($"DrawHandCard Error: {e.Message}");
      }
    }

    private void DiscardCard(RuntimeCard runtimeCard)
    {
      _cardObjects.TryGetValue(runtimeCard.InstanceID, out var obj);
      if (obj is null) return;
      AddressableObjectPooler.Release(obj);
      _cardObjects.Remove(runtimeCard.InstanceID);
    }

    public void Init()
    {
      var battleCardRef = GameSystem.Instance.Battle.AssetLoader.BattleCardRef;
      AddressableObjectPooler = new AddressableObjectPooler(battleCardRef, CardManager.MAX_COUNT_HAND, true, _battleCanvas.HandTr);
    }

    private void EnableTurnEndButton()
    {
      _btnPlayerTurnEnd.enabled = true;
    }

    private void DisableTurnEndButton()
    {
      _btnPlayerTurnEnd.enabled = false;
    }

    private void OnEnable()
    {
      BattleEvent.OnPlayerTurnStart += EnableTurnEndButton;
      BattleEvent.OnPlayerTurnEnd += DisableTurnEndButton;
      BattleEvent.OnCardDraw += DrawHandCard;
      BattleEvent.OnCardDiscard += DiscardCard;
    }

    private void OnDisable()
    {
      BattleEvent.OnPlayerTurnStart -= EnableTurnEndButton;
      BattleEvent.OnPlayerTurnEnd -= DisableTurnEndButton;
      BattleEvent.OnCardDraw -= DrawHandCard;
      BattleEvent.OnCardDiscard -= DiscardCard;
    }
  }
}