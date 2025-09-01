using UnityEngine;

namespace Player
{
  [CreateAssetMenu(fileName = "Player", menuName = "MyMenu/Player")]
  public class PlayerScriptableObject : ScriptableObject
  {
    [Header("Init Check")]
    public bool isUpdate;

    [Header("Base Player Stats")]
    public int BaseMaxHp;
    public int BaseCurrentHp;
    public int BaseMaxEnergy;
    public int BaseEnergy;

    [Space(20)]
    [Header("Update Player Stats")]
    public int UpdateMaxHp;
    public int UpdateCurrentHp;
    public int UpdateMaxEnergy;
    public int UpdateEnergy;
  }
}