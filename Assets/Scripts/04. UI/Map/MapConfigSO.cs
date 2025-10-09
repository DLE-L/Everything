using UnityEngine;

namespace UI.Map
{
  [CreateAssetMenu(fileName = "GenerateMap_Data", menuName = "GenerateMap_Data")]
  public class MapConfigSO : ScriptableObject
  {
    // 맵 생성 관련
    public int Act_FloorCount;
    public int Node_BossIndex;
    public int Act_FinalZoneIndex;
    public int Act_StartZoneEndIndex;
    public int Node_MinDistance;

    // 노드 배치 관련
    public int Node_RandomRange;
    public int Node_Distance;
    public int Floor_MaxNode;
    public int Floor_MinNode;
  }
}
/*
  // 맵 생성 관련
  private const int ACT_FLOOR_COUNT = 15;
  private const int NODE_BOSS_INDEX = 14;
  private const int ACT_FINAL_ZONE_INDEX = 13;
  private const int ACT_START_ZONE_END_INDEX = 3;
  private const int NODE_MIN_DISTANCE = 2;

  // 노드 배치 관련
  private const int NODE_RANDOM_RANGE = 1;
  private const int NODE_DISTANCE = 3;
  private const int FLOOR_MAX_NODE = 3;
  private const int FLOOR_MIN_NODE = 2;
*/