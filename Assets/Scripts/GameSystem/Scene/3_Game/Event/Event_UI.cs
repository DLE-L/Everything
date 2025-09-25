using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GameSystems.Act;

namespace GameSystems.Scene.Game
{
  public class Event_UI : MonoBehaviour
  {
    private GameManager _gameManager;

    [SerializeField] private UI_Event_Choice _Event_UI_Choice;
    [SerializeField] private TextMeshProUGUI _txtEventName;
    [SerializeField] private TextMeshProUGUI _txtEventDescription;
    [SerializeField] private Transform _choiceRoot;

    private Canvas _canvas;
    private bool _isInitialized = false;

    public void Init()
    {
      if (_isInitialized) { Debug.Log("[Event_UI]는 이미 초기화 되었습니다."); return; }
      _gameManager = GameSystem.Instance.Game;
      _choiceRoot = GetComponentInChildren<HorizontalLayoutGroup>().transform;
      _txtEventName = GameObject.Find("txtEventName").GetComponent<TextMeshProUGUI>();
      _txtEventDescription = GameObject.Find("txtEventDescription").GetComponent<TextMeshProUGUI>();
      _canvas = GetComponentInParent<Canvas>();

      _gameManager.OnClickNode += OnClickNode;
      //_Event_UI_Choice = Instantiate(EventDatabase.EventUIs["Event_UI_Choice"].GetComponent<UI_Event_Choice>());
      _canvas.enabled = false;
      _isInitialized = true;
    }

    public void OnClickNode(Node node)
    {
      // if (node.NodeType != NodeType.Event) { return; }

      // NodeEvent nodeEvent = node.GetComponent<NodeEvent>();
      // _txtEventName.text = nodeEvent.Name;
      // _txtEventDescription.text = nodeEvent.Description;
      // // TODO: 선택지 생성 코드 구현
      // Debug.Log($"선택지 개수: {nodeEvent.ChoiceList.Count}");
      // Debug.Break();
      // for (int i = 0; i < nodeEvent.ChoiceList.Count; i++)
      // {
      //   var choice = Instantiate(_Event_UI_Choice.gameObject);
      //   choice.transform.SetParent(_choiceRoot);
      //   var @event = choice.GetComponent<UI_Event_Choice>();
      //   @event.SetChoice(nodeEvent.ChoiceList[i]);
      // }
      // _canvas.enabled = true;
    }

    void OnEnable()
    {
      if (_gameManager != null)
      {
        _gameManager.OnClickNode += OnClickNode;
      }
    }
    void OnDisable()
    {
      if(_gameManager != null)
      {
        _gameManager.OnClickNode -= OnClickNode;
      }      
    }
  }
}