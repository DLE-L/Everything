using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using GamePlay.Map;
using Data.Act.Encounter;
using Data.Map;
using UnityEngine.AddressableAssets;

namespace UI.Map
{
  public class Narrative_UI : MonoBehaviour
  {
    [SerializeField] private AssetReference _uiNarrativeChoiceRef;
    [SerializeField] private TextMeshProUGUI _txtEncounterName;
    [SerializeField] private TextMeshProUGUI _txtEncounterDescription;
    [SerializeField] private Transform _choiceRoot;

    private Canvas _canvas;
    public void Init()
    {
      _choiceRoot = GetComponentInChildren<HorizontalLayoutGroup>().transform;
    }

    public async Task SettingUI(Node node)
    {
      if (node.Encounter.Type is not EncounterType.Narrative) return;
      
      // var spawnTasks = new List<Task<GameObject>>();
      // EncounterTypeSO type = node.EncounterType;
      //
      // // TODO: 선택지 생성 코드 구현
      // Debug.Log($"선택지 개수: {node.Encounter.RewardInfos.Count}");
      // Debug.Break();
      // for (int i = 0; i < node.Encounter.RewardInfos.Count; i++)
      // {
      //   var spawnTask = _uiNarrativeChoiceRef.InstantiateAsync(_choiceRoot).Task;
      //   spawnTasks.Add(spawnTask);
      // }
      //
      // GameObject[] choiceInstance = await Task.WhenAll(spawnTasks);
      //
      // foreach (var choice in choiceInstance)
      // {
      //   choice.
      // }

      _canvas.enabled = true;
    }
  }
}