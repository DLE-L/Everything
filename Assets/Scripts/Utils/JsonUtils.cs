using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using Units;

namespace Utils
{
  public static class JsonData
  {
    public static StringBuilder _sb = new();
    private static string _path = "Assets/Scripts/JsonData/";

    public static Task SavePlayerDataAsync(PlayerAccountData data) => SaveJsonDataAsync(data, _path + "AccountData.json");
    public static Task<PlayerAccountData> LoadPlayerData() => LoadJsonData<PlayerAccountData>(_path + "AccountData.json");


    private async static Task SaveJsonDataAsync<T>(T data, string path)
    {
      string json = JsonConvert.SerializeObject(data, Formatting.Indented);
      await File.WriteAllTextAsync(path, json);
    }

    private async static Task<T> LoadJsonData<T>(string path)
    {
      if (File.Exists(path) == false)
      {
        File.CreateText(path);
        return default;
      }
      string json = await File.ReadAllTextAsync(path);
      T data = JsonConvert.DeserializeObject<T>(json);
      return data;
    }
  }
}