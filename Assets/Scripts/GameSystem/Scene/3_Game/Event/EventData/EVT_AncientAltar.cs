using UnityEngine;
using System.Collections.Generic;

namespace GameSystems.Scene.Game
{
  // 'EVT_AncientAltar' Event 프리팹의 이름입니다.
  public class EVT_AncientAltar : MonoBehaviour, IEvent
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<ChoiceButton> ChoiceList { get; set; } = new();

    private string _choiceText;
    private List<IEventAction> _choiceActions;
    private ChoiceButton _choiceButton;

    // TODO: 이 이벤트에 필요한 초기화 구현
    public void Init()
    {

    }
  }
}