using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Core.Event;
using GamePlay.Map;
using Data.Act.Encounter;

namespace UIs.Map
{
  public class Canvas_Encounter_Shop : CanvasEncounterBase
  {
    [SerializeField] private Transform _choiceRoot;
    [SerializeField] private TextMeshProUGUI _txtGoldAmount;
    [SerializeField] private List<btnShopCard> _restShopCard;
    
    private Canvas _canvas;

    private void Awake()
    {
      _choiceRoot ??= GetComponentInChildren<HorizontalLayoutGroup>().transform;
      SystemEvent.RaiseVisitShop();
    }

    private void Start()
    {
      _restShopCard.ForEach(choice => choice.gameObject.SetActive(false));
    }

    public override Task SettingUIAsync(Node node)
    {
      if (node.Encounter is not EncounterShop shop) { Debug.Log($"Encounter is not Shop"); return null; }
      
      for (var i = 0; i < shop.CardList.Count; i++)
      {
        _restShopCard[i].gameObject.SetActive(true);
        _restShopCard[i].Setup(shop.CardList[i]);
      }

      return null; // TODO Canvas_Encounter_Narrative 반환 Task? 계속 쓸건지
    }
  }
}