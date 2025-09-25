
using UnityEngine;

namespace Item
{
  [CreateAssetMenu(fileName = "NewStatus", menuName = "MyMenu/StatusEffect/Status")]
  public class StatusEffectSO : ScriptableObject
  {
    public string Name;
    public Sprite Icon;
    public string Description;
    public StatusType Type;
    public bool IsStackable;
  }

  public enum StatusType
  {
    Buff,   // 플레이어/적에게 이로운 효과 (예: 힘, 재생)
    Debuff  // 플레이어/적에게 해로운 효과 (예: 약화, 취약)
  }
}