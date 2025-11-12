using System;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIs.Map
{
  public class DeleteCard_UI : MonoBehaviour
  {
    private const int DELETE_PRICE = 75;
    private const int DELETE_PRICE_INCREASE = 25;
    
    [SerializeField] private GameObject _deleteCard;
    [SerializeField] private Image _imgDeleteSelect;
    [SerializeField] private TextMeshProUGUI _txtPrice;
    [SerializeField] private GameObject _deleteCardListPrefab;

    private void Start()
    {
      _deleteCard.SetActive(false);
      _txtPrice.text = $"{DELETE_PRICE + DELETE_PRICE_INCREASE * GameSystem.Instance.Map.ShopVisitCount}";
    }
    
    public void ShowDeleteCardList()
    {
      _deleteCardListPrefab.SetActive(true);
    }
  }
}