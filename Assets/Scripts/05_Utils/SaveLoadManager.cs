using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using System;
using Data.Units;

namespace Utils
{
  public static class SaveLoadManager
  {
    public static StringBuilder _sb = new();
    private static bool _isSaving;
    private static readonly string _savePath = Path.Combine(Application.persistentDataPath, "AccountData.json");

    public static Task SavePlayerDataAsync(PlayerAccountData data) => SaveJsonDataAsync(data, _savePath);
    public static Task<PlayerAccountData> LoadPlayerData() => LoadJsonData<PlayerAccountData>(_savePath);

    private static async Task SaveJsonDataAsync<T>(T data, string path)
    {
      if (_isSaving) return;

      try
      {
        _isSaving = true;
        var jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);
        await File.WriteAllTextAsync(path, jsonString);
        Debug.Log($"<color=green>Save Succeed: {path}</color>");
      }
      catch (Exception e)
      {
        Debug.LogError($"Save Error: {e.GetType().Name} - {e.Message}");
      }
      finally
      {
        _isSaving = false;
      }
    }

    private static async Task<T> LoadJsonData<T>(string path) where T : class
    {
      if (!File.Exists(path))
      {
        Debug.LogWarning($"{path} does not exist");
        return null;
      }
      var jsonString = await File.ReadAllTextAsync(path);
      if (string.IsNullOrWhiteSpace(jsonString))
      {
        return null;
      }
      var data = JsonConvert.DeserializeObject<T>(jsonString);
      //Debug.Log($"[Json Data Load: {path}]");
      return data;
    }
  }
}