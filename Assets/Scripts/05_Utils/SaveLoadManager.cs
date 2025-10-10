using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using System;
using Data.Units;

namespace Utils
{
  public static class JsonData
  {
    public static StringBuilder _sb = new();
    private static bool _isSaving = false;
    private static string _savePath = Path.Combine(Application.persistentDataPath, "AccountData.json");

    public static Task SavePlayerDataAsync(PlayerAccountData data) => SaveJsonDataAsync(data, _savePath);
    public static Task<PlayerAccountData> LoadPlayerData() => LoadJsonData<PlayerAccountData>(_savePath);

    private async static Task SaveJsonDataAsync<T>(T data, string path)
    {
      Debug.Log("..저장중..");
      if (_isSaving) return;

      try
      {
        _isSaving = true;
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        await File.WriteAllTextAsync(path, json);
        Debug.Log($"<color=green>저장 성공: {path}</color>");
      }
      catch (Exception e)
      {
        Debug.LogError($"저장 중 에러 발생: {e.GetType().Name} - {e.Message}");
      }
      finally
      {
        _isSaving = false;
      }
    }

    private async static Task<T> LoadJsonData<T>(string path) where T : class
    {
      if (File.Exists(path) == false)
      {
        return null;
      }
      string json = await File.ReadAllTextAsync(path);
      if (string.IsNullOrWhiteSpace(json))
      {
        return null;
      }
      T data = JsonConvert.DeserializeObject<T>(json);
      //Debug.Log($"[Json Data Load: {path}]");
      return data;
    }
  }
}