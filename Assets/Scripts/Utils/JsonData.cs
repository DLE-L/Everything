using Newtonsoft.Json;
using UnityEngine;

namespace Utils
{
  public static class JsonData
  {
    public static T ConvertJsonData<T>(string text)
    {
      T data = JsonConvert.DeserializeObject<T>(text);
      return data;
    }

    public static void SaveJsonData<T>(T data)
    {
      string json = JsonConvert.SerializeObject(data, Formatting.Indented);
    }
  }
}