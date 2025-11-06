using System;
using System.Threading.Tasks;
using Core.Event;
using UnityEngine;
using UnityEngine.UI;

namespace UIs
{
  public class FadeManger : MonoBehaviour
  {
    public static FadeManger Instance { get; private set; }
    
    [SerializeField] private Image _imgFade;
    [SerializeField] private float _defaultFadeDuration = 0.1f;
    private bool _isFading = false;

    private void Awake()
    {
      if (Instance is null)
      {
        Instance = this;
      }
      else
      {
        Destroy(gameObject);
      }
      
      _imgFade ??= GetComponentInChildren<Image>();
    }

    private void Start()
    {
      SetAlpha(0f);
    }

    public async Task FadeIn()
    {
      await Fade(0f, _defaultFadeDuration);
    }

    public async Task FadeOut()
    {
      await Fade(1f, _defaultFadeDuration);
    }

    private async Task Fade(float targetAlpha, float duration)
    {
      if (_isFading) return;
      _isFading = true;

      _imgFade.enabled = true;

      float startAlpha = _imgFade.color.a;
      float time = 0f;

      while (time < duration)
      {
        float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
        SetAlpha(newAlpha);

        time += Time.deltaTime;
        await Task.Yield();
      }
      
      SetAlpha(targetAlpha);
      
      if (targetAlpha is 0f)
      {
        _imgFade.enabled = false;
      }

      _isFading = false;
    }

    private void SetAlpha(float alpha)
    {
      Color color = _imgFade.color;
      color.a = alpha;
      _imgFade.color = color;
    }
  }
}