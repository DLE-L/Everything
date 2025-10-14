using System.Threading.Tasks;
using UnityEngine;

namespace Utils
{
  public static class SelectionHandler
  {
    private static object _currentTcs;
    public static bool IsSelecting => _currentTcs is not null;

    public static async Task<T> SelectAsync<T>()
    {
      if (IsSelecting)
      {
        Debug.Log($"[Selection Handler: Already Selecting]");
        return default;
      }

      var tcs = new TaskCompletionSource<T>();
      _currentTcs = tcs;

      void SelectionHandler(T selectedValue)
      {
        SelectableItem<T>.OnItemSelected -= SelectionHandler;
        tcs.TrySetResult(selectedValue);
      }

      SelectableItem<T>.OnItemSelected += SelectionHandler;

      try
      {
        return await tcs.Task;
      }
      finally
      {
        SelectableItem<T>.OnItemSelected -= SelectionHandler;
        _currentTcs = null;
      }
    }

    public static void CancelSelection()
    {
      if (!IsSelecting) return;

      var tcsType = _currentTcs.GetType();
      var trySetCancelMethod = tcsType.GetMethod("TrySetCanceled");
      trySetCancelMethod?.Invoke(_currentTcs, null);
      
      _currentTcs = null;
    }
  }
}