using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Data.Act
{
  public static class ActDatabase
  {
    public static Dictionary<string, ActSO> Acts = new();

    public static async Task<ActSO> GetNumberingActAsync(int actNumbering)
    {
      StringBuilder sb = new();
      sb.Append($"Act_{actNumbering}");
      return null; //await AssetLoader.LoadAssetAsync<ActSO>(sb.ToString());
    }
  }
}