using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using System.Threading.Tasks;
using GamePlay.Map;
using Data.Act.Encounter;
using Data.Map;

namespace UI.Map
{
  public class Narrative_UI : MonoBehaviour
  {
    [SerializeField] private UI_Narrative_Choice _Event_UI_Choice;
    [SerializeField] private TextMeshProUGUI _txtEncounterName;
    [SerializeField] private TextMeshProUGUI _txtEncounterDescription;
    [SerializeField] private Transform _choiceRoot;

    private Canvas _canvas;
    public async Task Init()
    {
      _choiceRoot = GetComponentInChildren<HorizontalLayoutGroup>().transform;
      GameObject go = await AssetLoader.LoadAssetAsync<GameObject>("UI_Event_Choice");
      _Event_UI_Choice = Instantiate(go).GetComponent<UI_Narrative_Choice>();
    }

    public void SettingUI(Node node)
    {
      if (node.Encounter is not NarrativeEncounter) return;

      EncounterTypeSO type = node.EncounterType;

      _txtEncounterName.text = type.Name;
      _txtEncounterDescription.text = type.Description;
      // TODO: 선택지 생성 코드 구현
      Debug.Log($"선택지 개수: {node.Encounter.Reward.Count}");
      Debug.Break();
      for (int i = 0; i < node.Encounter.Reward.Count; i++)
      {
        var choice = Instantiate(_Event_UI_Choice.gameObject);
        choice.transform.SetParent(_choiceRoot);
        var @event = choice.GetComponent<UI_Narrative_Choice>();
        @event.SetChoice(node.Encounter.Reward[i]);
      }

      _canvas.enabled = true;
    }
  }
}