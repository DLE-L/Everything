using Data.Units;
using UnityEngine;

namespace Data.Map
{
  public abstract class RestOptionSO : ScriptableObject
  {
    public string OptionName;
    public string Description;
    public Sprite Icon;
    public abstract void Execute(PlayerRunData runData);
  }
}