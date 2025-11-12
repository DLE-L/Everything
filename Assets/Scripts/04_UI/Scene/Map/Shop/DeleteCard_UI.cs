using System;
using Core;
using TMPro;
using UnityEngine;

namespace UIs.Map
{
  public class DeleteCard_UI : MonoBehaviour
  {
    private const int DELETE_PRICE = 75;
    private const int DELETE_PRICE_INCREASE = 25;
    
    [SerializeField] private GameObject _deleteCardListPrefab;
    [SerializeField] private TextMeshProUGUI _txtPrice;
    [SerializeField] private btnDeleteSelect _btnDeleteSelect;
    public DeleteCard DeleteCard;

    private void Awake()
    {
      _txtPrice ??= transform.Find("txtPrice").GetComponent<TextMeshProUGUI>();
      _btnDeleteSelect ??= transform.Find("btnDeleteSelect").GetComponent<btnDeleteSelect>();
      DeleteCard ??= transform.Find("DeleteCard").GetComponent<DeleteCard>();
    }

    private void Start()
    {
      DeleteCard.gameObject.SetActive(false);
      _txtPrice.text = $"{DELETE_PRICE + DELETE_PRICE_INCREASE * GameSystem.Instance.Map.ShopVisitCount}";
    }
    
    public void ShowDeleteCardList()
    {
      _deleteCardListPrefab.SetActive(true);
    }
  }
}