using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GameSystems.Act;
using Utils;

namespace GameSystems.Scene.Game
{
  public static class ActDatabase
  {
    public static Dictionary<string, ActSO> Acts = new();

    public async static Task<ActSO> GetNumberingActAsync(int actNumbering)
    {
      StringBuilder sb = new();
      sb.Append($"Act_{actNumbering}");
      return await AssetLoader.LoadAssetAsync<ActSO>(sb.ToString());
    }
  }
}