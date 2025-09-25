using UnityEngine;

namespace Item
{
  [CreateAssetMenu(fileName = "NewStatusType", menuName = "MyMenu/StatusEffect/StatusType")]
  public class StatusTypeSO : ScriptableObject
  {
    public string Name;
    public Sprite Icon;
    public string Description;
  }
}